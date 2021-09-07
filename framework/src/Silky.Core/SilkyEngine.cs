﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Silky.Core.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Silky.Core.Configuration;
using Silky.Core.DependencyInjection;
using Silky.Core.Exceptions;
using Silky.Core.Modularity;
using Silky.Core.Rpc;

namespace Silky.Core
{
    internal sealed class SilkyEngine : IEngine, IModuleContainer
    {
        private ITypeFinder _typeFinder;

        public IConfiguration Configuration { get; private set; }

        public IHostEnvironment HostEnvironment { get; private set; }

        public string AppName => GetOptionsSnapshot<AppSettingsOptions>().AppName;

        public string HostName { get; private set; }

        public void ConfigureServices(IServiceCollection services, IConfiguration configuration,
            IHostEnvironment hostEnvironment)
        {
            _typeFinder = new SilkyAppTypeFinder();
            ServiceProvider = services.BuildServiceProvider();
            Configuration = configuration;
            HostEnvironment = hostEnvironment;
            HostName = Assembly.GetEntryAssembly()?.GetName().Name;

            var startupConfigurations = _typeFinder.FindClassesOfType<IConfigureService>();

            //create and sort instances of startup configurations
            var instances = startupConfigurations
                .Select(startup => (IConfigureService)Activator.CreateInstance(startup))
                .OrderBy(startup => startup.Order);

            //configure services
            foreach (var instance in instances)
                instance.ConfigureServices(services, configuration);
            // configure modules 
            foreach (var module in Modules)
                module.Instance.ConfigureServices(services, configuration);

            AppDomain.CurrentDomain.AssemblyResolve += CurrentDomain_AssemblyResolve;
        }

        /// <summary>
        /// 获取选项
        /// </summary>
        /// <typeparam name="TOptions">强类型选项类</typeparam>
        /// <returns>TOptions</returns>
        public TOptions GetOptions<TOptions>()
            where TOptions : class, new()
        {
            return Resolve<IOptions<TOptions>>()?.Value;
        }

        public TOptions GetOptionsMonitor<TOptions>()
            where TOptions : class, new()
        {
            return Resolve<IOptionsMonitor<TOptions>>().CurrentValue;
        }

        /// <summary>
        /// 获取选项
        /// </summary>
        /// <typeparam name="TOptions">强类型选项类</typeparam>
        /// <returns>TOptions</returns>
        public TOptions GetOptionsMonitor<TOptions>(Action<TOptions, string> listener)
            where TOptions : class, new()
        {
            var optionsMonitor = Resolve<IOptionsMonitor<TOptions>>();
            if (optionsMonitor != null)
            {
                optionsMonitor.OnChange(listener);
            }

            return optionsMonitor?.CurrentValue;
        }

        /// <summary>
        /// 获取选项
        /// </summary>
        /// <typeparam name="TOptions">强类型选项类</typeparam>
        /// <returns>TOptions</returns>
        public TOptions GetOptionsSnapshot<TOptions>()
            where TOptions : class, new()
        {
            // 这里不能从根服务解析，因为是 Scoped 作用域
            return Resolve<IOptionsSnapshot<TOptions>>()?.Value;
        }

        public ITypeFinder TypeFinder => _typeFinder;


        public void ConfigureRequestPipeline(IApplicationBuilder application)
        {
            ServiceProvider = application.ApplicationServices;
            var typeFinder = Resolve<ITypeFinder>();
            var startupConfigurations = typeFinder.FindClassesOfType<ISilkyStartup>();

            var webSilkyModules = Modules.Where(p => p.Instance is WebSilkyModule);
            foreach (var webSilkyModule in webSilkyModules)
            {
                ((WebSilkyModule)webSilkyModule.Instance).Configure(application);
            }
            
            //create and sort instances of startup configurations
            var instances = startupConfigurations
                .Select(startup => (ISilkyStartup)Activator.CreateInstance(startup))
                .OrderBy(startup => startup.Order);

            //configure request pipeline
            foreach (var instance in instances)
                instance.Configure(application);
            
          
        }

        public T Resolve<T>() where T : class
        {
            return (T)Resolve(typeof(T));
        }

        public object Resolve(Type type)
        {
            var sp = GetServiceProvider();
            if (sp == null)
                return null;
            return sp.GetService(type);
        }

        public object ResolveNamed(string name, Type type)
        {
            var sp = GetServiceProvider();
            if (sp == null)
                return null;
            return sp.GetAutofacRoot().ResolveNamed(name, type);
        }

        public object ResolveServiceEntryInstance(string serviceKey, Type serviceType)
        {
            object instance = null;
            if (!serviceKey.IsNullOrEmpty())
            {
                if (!EngineContext.Current.IsRegisteredWithName(serviceKey, serviceType))
                {
                    throw new UnServiceKeyImplementationException(
                        $"系统中没有存在serviceKey为{serviceKey}的{serviceType.FullName}接口的实现类");
                }

                instance = EngineContext.Current.ResolveNamed(serviceKey, serviceType);
            }
            else
            {
                instance = EngineContext.Current.Resolve(serviceType);
            }

            return instance;
        }

        public T ResolveNamed<T>(string name)
        {
            return (T)ResolveNamed(name, typeof(T));
        }

        public IEnumerable<T> ResolveAll<T>()
        {
            return (IEnumerable<T>)GetServiceProvider().GetServices(typeof(T));
        }

        public bool IsRegistered(Type type)
        {
            return GetServiceProvider().GetAutofacRoot().IsRegistered(type);
        }

        public bool IsRegisteredWithName(string name, Type type)
        {
            return GetServiceProvider().GetAutofacRoot().IsRegisteredWithName(name, type);
        }

        public object ResolveUnregistered(Type type)
        {
            Exception innerException = null;
            foreach (var constructor in type.GetConstructors())
            {
                try
                {
                    //try to resolve constructor parameters
                    var parameters = constructor.GetParameters().Select(parameter =>
                    {
                        var service = Resolve(parameter.ParameterType);
                        if (service == null)
                            throw new SilkyException("Unknown dependency");
                        return service;
                    });

                    //all is ok, so create instance
                    return Activator.CreateInstance(type, parameters.ToArray());
                }
                catch (Exception ex)
                {
                    innerException = ex;
                }
            }

            throw new SilkyException("No constructor was found that had all the dependencies satisfied.",
                innerException);
        }

        private IServiceProvider GetServiceProvider()
        {
            if (ServiceProvider == null)
                return null;
            var serviceProvider = ServiceProvider;
            var httpContextAccessor = serviceProvider?.GetService<IHttpContextAccessor>();
            if (httpContextAccessor is { HttpContext: { } })
            {
                serviceProvider = httpContextAccessor.HttpContext.RequestServices;
            }

            var rpcContextAccessor = serviceProvider.GetService<IRpcContextAccessor>();
            if (rpcContextAccessor is { RpcContext: { RpcServices: { } } })
            {
                serviceProvider = rpcContextAccessor.RpcContext.RpcServices;
            }

            return serviceProvider;
        }

        public void RegisterDependencies(ContainerBuilder containerBuilder)
        {
            containerBuilder.RegisterInstance(this).As<IEngine>().SingleInstance();
            containerBuilder.RegisterInstance(_typeFinder).As<ITypeFinder>().SingleInstance();

            var dependencyRegistrars = _typeFinder.FindClassesOfType<IDependencyRegistrar>();
            var instances = dependencyRegistrars
                .Select(dependencyRegistrar => (IDependencyRegistrar)Activator.CreateInstance(dependencyRegistrar))
                .OrderBy(dependencyRegistrar => dependencyRegistrar.Order);
            foreach (var dependencyRegistrar in instances)
                dependencyRegistrar.Register(containerBuilder, _typeFinder);
        }

        private Assembly CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs args)
        {
            //check for assembly already loaded
            var assembly = AppDomain.CurrentDomain.GetAssemblies().FirstOrDefault(a => a.FullName == args.Name);
            if (assembly != null)
                return assembly;

            //get assembly from TypeFinder
            var tf = Resolve<ITypeFinder>();
            if (tf == null)
                return null;
            assembly = tf.GetAssemblies().FirstOrDefault(a => a.FullName == args.Name);
            return assembly;
        }

        public void RegisterModules(IServiceCollection services, ContainerBuilder containerBuilder)
        {
            containerBuilder.RegisterInstance(this).As<IModuleContainer>().SingleInstance();
            var assemblyNames = ((AppDomainTypeFinder)_typeFinder).AssemblyNames;
            foreach (var module in Modules)
            {
                if (!assemblyNames.Contains(module.Assembly.FullName))
                {
                    ((AppDomainTypeFinder)_typeFinder).AssemblyNames.Add(module.Assembly.FullName);
                }

                containerBuilder.RegisterModule((SilkyModule)module.Instance);
            }
        }

        public void LoadModules<T>(IServiceCollection services, IModuleLoader moduleLoader) where T : StartUpModule
        {
            Modules = moduleLoader.LoadModules(services, typeof(T));
        }

        public bool IsEnvironment(string environmentName)
        {
            if (HostEnvironment != null)
            {
                return HostEnvironment.IsEnvironment(environmentName);
            }

            var dotnetEnvironment = Environment.GetEnvironmentVariable("DOTNET_ENVIRONMENT");
            if (!dotnetEnvironment.IsNullOrEmpty())
            {
                return dotnetEnvironment.Equals(environmentName);
            }

            var aspNetCoreEnvironment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            if (!aspNetCoreEnvironment.IsNullOrEmpty())
            {
                return aspNetCoreEnvironment.Equals(environmentName);
            }

            return false;
        }

        public IServiceProvider ServiceProvider { get; set; }

        public IReadOnlyList<ISilkyModuleDescriptor> Modules { get; private set; }
    }
}