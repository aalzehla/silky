---
title: host build
lang: zh-cn
---

## host concept

first，我们来了解host concept。exist[Asp.net Corehost documentation](https://learn.microsoft.com/zh-cn/aspnet/core/fundamentals/host/generic-host?view=aspnetcore-6.0),Define the host as: **A host is an object that encapsulates application resources，将应用of所有相互依赖资源包括exist一个对象middle可控制应用启动和正常关闭**。in other words,It is used to host and manage application resources and application life cycle。

exist.net middle,There are two types of hosts,one is[generic host](https://learn.microsoft.com/zh-cn/aspnet/core/fundamentals/host/generic-host?view=aspnetcore-6.0)，one is[Webhost](https://learn.microsoft.com/zh-cn/aspnet/core/fundamentals/host/web-host?view=aspnetcore-6.0)。区别主要exist于**Webhost**providedwebContainers and ConfigurationhttpThe ability to request processing pipelines(sowebhostexist启动后就有一个`httpport`and can pass`StartUp`class configurationhttpmiddle间件),而通用host并没有这个能力。

forsilkyfor,Depend onsilky框架开发of微Serve集群存exist两种通信方式:

1.  accept externalhttpask，and throughwebapi路Depend on到相应ofServe条目,然后pass本地或远程执行器执行forofServe条目方法。

2. Between the service and the service throughdotnettyrealizedrpccarry out network communication。

so,According to different usage scenarios,我们可以使用不同ofhostto host the application。

for *Scenes1* ,We can only choose to pass**Webhost**to host the application,because he has to provideHttpability to serve,It is provided internally and externally by the clusterwebapiAccess to the portal application,Can generally be used to build gateways,certainly,Can also be used for hosting business applications，In this case,The business application can also directly provide externalwebapiServe。

but,一般Scenes下,普通of业务微Serve我们并不需要它直接对外部提供webapiServe。so,更多通信Scenes是*Scenes2*。 so,for一般of业务微Serve而言,we can use him **通用host** 来构建微Serve应用。

## registerSilky微Serve应用

我们exist[silkyDocumentation Home](https://docs.silky-fk.com/)See,build the easiestsilky微Serve应用,You can do it with the following simple line of code。

```csharp
 private static IHostBuilder CreateHostBuilder(string[] args)
{
    return Host.CreateDefaultBuilder(args)
            .ConfigureSilkyGeneralHostDefaults();
            
}
```

we pass the package **Silky.Agent.Host** which provided `HostBuilderExtensions`which provided扩展方法来构建Silky微Serve应用。**Silky.Agent.Host** The package depends on the buildSilky微Serve应用其他of必要of包。

`HostBuilderExtensions`by implementing`IHostBuilder`extension method of来registerSilky微Serve应用。exist[HostBuilderExtensions](https://github.com/liuhll/silky/blob/main/framework/src/Silky.Agent.Host/HostBuilderExtensions.cs)类middle,provided诸如: `ConfigureSilkyWebHostDefaults`、`ConfigureSilkyGateway`、`ConfigureSilkyGeneralHostDefaults`Wait`IHostBuilder`extension method of。exist这些方法middle,whichever method,我们See,The core code is `hostBuilder.RegisterSilkyServices<T>()`,under,We know in depth `hostBuilder.RegisterSilkyServices<T>()`what exactly is done。

exist**Silky.Core**包middle,we pass[HostBuilderExtensions](https://github.com/liuhll/silky/blob/main/framework/src/Silky.Core/HostBuilderExtensions.cs)which provided扩展方法`RegisterSilkyServices`实现了Serve引擎(`IEngine`)build、模块of依赖与register、Serveof依赖与register、Loading of configuration files、模块of顺序执行Wait工作。

```csharp
public static IHostBuilder RegisterSilkyServices<T>(this IHostBuilder builder)
  where T : StartUpModule
{
    IEngine engine = null;
    IServiceCollection services = null;
    builder.UseServiceProviderFactory(new AutofacServiceProviderFactory())
        .UseContentRoot(Directory.GetCurrentDirectory())
        .ConfigureServices((hostBuilder, sc) => // (2) Serveregister和Serve引擎构建
        {
            engine = sc.AddSilkyServices<T>(hostBuilder.Configuration,
                hostBuilder.HostingEnvironment);
            services = sc;
        })
        .ConfigureContainer<ContainerBuilder>(builder => // (3)passContainerBuilder实现Serve依赖register
        {
            engine.RegisterModules(services, builder);
            engine.RegisterDependencies(builder);
        })
        .ConfigureAppConfiguration((hosting, config) => // (1)load configuration file、add environment variable
        {
            config.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{hosting.HostingEnvironment.EnvironmentName}.json", optional: true,
                    true);
            // Adds YAML settings later
            config.AddYamlFile("appsettings.yml", optional: true, true)
                .AddYamlFile($"appsettings.{hosting.HostingEnvironment.EnvironmentName}.yml", optional: true,
                    true)
                .AddYamlFile("appsettings.yaml", optional: true, true)
                .AddYamlFile($"appsettings.{hosting.HostingEnvironment.EnvironmentName}.yaml", optional: true,
                    true);
            // add RateLimit configfile
            config.AddJsonFile("ratelimit.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"ratelimit.{hosting.HostingEnvironment.EnvironmentName}.json", optional: true,
                    true);
            config.AddYamlFile("ratelimit.yml", optional: true, reloadOnChange: true)
                .AddYamlFile($"ratelimit.{hosting.HostingEnvironment.EnvironmentName}.yml", optional: true,
                    true)
                .AddYamlFile("ratelimit.yaml", optional: true, reloadOnChange: true)
                .AddYamlFile($"ratelimit.{hosting.HostingEnvironment.EnvironmentName}.yaml", optional: true,
                    true);
            config.AddEnvironmentVariables();
        })
        ;
    return builder;
}
```

1. The method constrains the generic parameter`T`must be a startup module`StartUpModule`type。

2. 将Serve提供者工厂替换为`AutofacServiceProviderFactory`，so,我们就可以pass[Autofac](https://autofac.readthedocs.io/en/latest/integration/aspnetcore.html#asp-net-core-3-0-and-generic-hosting)来实现Serveof依赖注入。

3. pass`UseContentRoot`The root directory of the project is specified。

4. `IHostBuilder`There are three core configuration methods: (1) `ConfigureAppConfiguration` (2) `ConfigureServices` (3) `ConfigureContainer`; exist应用启动时,will press`ConfigureAppConfiguration` --> `ConfigureServices` --> `ConfigureContainer` execute in sequence。

4.1 exist执行`ConfigureAppConfiguration`method,Mainly complete loading local configuration files and environment variables;

4.2 exist执行`ConfigureServices`method，pass`IServiceCollection`extension method of`AddSilkyServices<T>()`实现必要ofServeregister和 [**Serve引擎(IEngine)**](/source/startup/engine.html) build;

4.3 exist执行`ContainerBuilder`method，主要passAutofacof`ContainerBuilder`实现Serveof依赖register;

5. exist完成上述指定of方法后,host接下来将会执行后台任务`InitSilkyHostedService`,并根据模块of依赖顺序,execute in sequence各个模块of启动方法。完成Serve以及Serve条目of发现、向Serveregistermiddle心registerServe信息以及启动rpc消息监听器Wait工作。


