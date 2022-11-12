---
title: service engine
lang: zh-cn
---

## 构建service engine

exist[registerSilkyMicroservice application](/source/startup/host.html#registersilkyMicroservice application)in a section,我们了解到exist`ConfigureServices`stage,pass`IServiceCollection`extension method of`AddSilkyServices<T>()`除了register必要of服务之外,更主要of是构建了service engine(`IEngine`)。

under,我们学习exist`IServiceCollection`extension method of`AddSilkyServices<T>()`what kind of work was done in。like下所示of代码为exist包 **Silky.Core** of [ServiceCollectionExtensions.cs](https://github.com/liuhll/silky/blob/main/framework/src/Silky.Core/ServiceCollectionExtensions.cs)middle提供extension method of`AddSilkyServices<T>()`。

```csharp
public static IEngine AddSilkyServices<T>(this IServiceCollection services, IConfiguration configuration,
            IHostEnvironment hostEnvironment) where T : StartUpModule
{
    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12; // 指定通信管道of加密传输协议
    CommonSilkyHelpers.DefaultFileProvider = new SilkyFileProvider(hostEnvironment); // Build a file service provider
    services.TryAddSingleton(CommonSilkyHelpers.DefaultFileProvider);  // Towardsservicesregister单例offile service provider
    var engine = EngineContext.Create(); // 创建单例ofservice engine
    services.AddOptions<AppSettingsOptions>()
        .Bind(configuration.GetSection(AppSettingsOptions.AppSettings)); // newAppSettingsOptionsconfigure
    var moduleLoader = new ModuleLoader(); // Create a module loader
    engine.LoadModules<T>(services, moduleLoader); // load all modules
    services.TryAddSingleton<IModuleLoader>(moduleLoader); // register单例ofmoduleload器
    services.AddHostedService<InitSilkyHostedService>();  // register InitSilkyHostedService Background task service,该服务用于初始化各个moduleof任务或是exist应用停止Time释放module资源
    services.AddSingleton<ICancellationTokenProvider>(NullCancellationTokenProvider.Instance); //register默认ofCancellationTokenProvider
    engine.ConfigureServices(services, configuration, hostEnvironment); // passservice engine扫描所有IConfigureService接口of类,其Implementation class可以passIServiceCollection对服务进行register;以及pass各个moduleofConfigureServicesmethod对服务进行register
    return engine; // 返回service engineobject
}
```

创建service engineofobjectmethodlike下所示，we can see,service engineexist整个应用of生命周期是全局单例of。

```csharp
internal static IEngine Create()
{
    return Singleton<IEngine>.Instance ?? (Singleton<IEngine>.Instance = new SilkyEngine()); // service engineexist应用of整个生命周期是单例of
}
```


pass我们对上述代码注释可以看出，exist`AddSilkyServices<T>()`method,exist该method做了like下关键性of工作:

1. 构建了一个关键性ofobject **file service provider(`SilkyFileProvider`)** ,该object主要用于扫描或是获取指定of文件(such as application sets etc.)as well as providing help methods such as folders;

2. use`EngineContext`创建了service engineobject`SilkyEngine`object;

3. use`IServiceCollection`register了必要of核心ofobject,like:`SilkyFileProvider`、`ModuleLoader`、`NullCancellationTokenProvider`Wait;

4. Create a module loader`ModuleLoader`object,并passservice engine解析、loadsilkymodule,需要指出of是,exist这里我们需要指定启动module,系统会根据启动module指定of依赖关系进行排序;

5. registerBackground task service`InitSilkyHostedService`,该服务用于初始化各个moduleof任务或是exist应用停止Time释放module资源;exist各个moduleof初始化工作middle完成了很多核心of工作，例like:对应用服务以及服务条目of解析、服务元数据ofregister、服务实例ofregister与更新、Rpc消息监听者of启动WaitWait;

6. exist调用service engineof`ConfigureServices()`method,passservice engine扫描所有`IConfigureService`接口of类,pass反射创建Implementation classofobject,pass`IServiceCollection`对服务进行register;以及passtraverse所有ofSilkymodule实例,passmoduleof提供of`ConfigureServices()`ofmethodpass`IServiceCollection`对服务进行register。

::: tip hint

like果熟悉 [nopCommerce](https://github.com/nopSolutions/nopCommerce) 框架of小伙伴们应该注意到,`SilkyEngine`service engineof作用与构建与该框架of设计基本是一致of。

:::

## service engineof作用

service engineof`SilkyEngine`of作用主要由like下几点:

1. passmoduleload器`ModuleLoader`解析和loadmodule，关于modulelike何解析和load,please check[下一节module](#)content;

2. 实现服务of依赖注入,本质上来说要么pass`IServiceCollection`服务实现服务register,要么passAutufac提供of`ContainerBuilder`实现服务register;

service engine实现服务of依赖注入主要由like下几种方式实现:

2.1 pass扫描所有`IConfigureService`接口of类,并pass反射of方式构建Implementation classofobject,然后可以pass`IServiceCollection`对服务进行register;以及passtraverse所有ofSilkymodule实例,passmoduleof提供of`ConfigureServices()`ofmethodpass`IServiceCollection`对服务进行register。

like下代码为service engine提供of`ConfigureServices()`method source code:

```csharp
// SilkyEngine 实现ofConfigureServicesregister服务ofmethod
public void ConfigureServices(IServiceCollection services, IConfiguration configuration,
            IHostEnvironment hostEnvironment)
{
    _typeFinder = new SilkyAppTypeFinder(); // Create a type finder
    ServiceProvider = services.BuildServiceProvider();
    Configuration = configuration;
    HostEnvironment = hostEnvironment;
    HostName = Assembly.GetEntryAssembly()?.GetName().Name;  // Resolve app service hostname

    var configureServices = _typeFinder.FindClassesOfType<IConfigureService>(); // pass查找器查找所有of`IConfigureService`Implementation class

    var instances = configureServices
        .Select(configureService => (IConfigureService)Activator.CreateInstance(configureService));  // pass反射of方式创建`IConfigureService`Implementation classof实例

    foreach (var instance in instances) // traverse`IConfigureService`ofImplementation classof实例，并pass其实例实现passIServiceCollection对服务ofregister
        instance.ConfigureServices(services, configuration);
    // configure modules 
    foreach (var module in Modules) // traverse各个module,pass各个module提供`ConfigureServices`实现服务ofregister
        module.Instance.ConfigureServices(services, configuration);

    AppDomain.CurrentDomain.AssemblyResolve += CurrentDomain_AssemblyResolve;

}
```

exist上述代码middle,我们可以看到exist该method体内主要完成like下工作:

A) Create a type finder、构建服务提供者以及为configure器、host environment variables、主机名Wait赋值;

B) use类型查找器查找到所有`IConfigureService`Implementation class,并pass反射of方式创建其实例,traverse其实例,其实例pass`IServiceCollection`实现对服务ofregister;

C) traverse所有ofmodule,passmoduleof实例提供of`ConfigureServices()`method,pass`IServiceCollection`实现对服务ofregister;

2.2 exist上一章[registersilkyMicroservice application](/source/startup/host#registersilkyMicroservice application)pointed out, implement`ContainerBuilder`method，主要pass`Autofac`of`ContainerBuilder`实现服务of依赖register。

```csharp
public static IHostBuilder RegisterSilkyServices<T>(this IHostBuilder builder)
  where T : StartUpModule
{   
    // other codes...
    builder
      .UseServiceProviderFactory(new AutofacServiceProviderFactory()) // Replace service provider job class
      .ConfigureContainer<ContainerBuilder>(builder => // passContainerBuilder实现服务依赖register
        {
            engine.RegisterModules(services, builder);
            engine.RegisterDependencies(builder);
        })
}
```

We saw,like何pass`ContainerBuilder`实现服务register，也是passservice engine巧妙of实现：一种方式是passmodule，另外一种方式是pass约定of依赖方式。

2.2.1 passmoduleregister服务

exist`SilkyModule`of定义middle,We sawmoduleof基类是`Autofac.Module`,我们existtraverse所有ofmodule实例of过程middle，pass`ContainerBuilder`提供of`RegisterModule()`method实现module指定of服务ofregister。in other words,就是existexistimplement`RegisterModule()`ofmethod过程middle,Autofac会调用moduleof提供of`RegisterServices(ContainerBuilder builder)`实现具体of服务register。

```csharp
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
```

soexistSilkymoduleof定义[SilkyModule](https://github.com/liuhll/silky/blob/main/framework/src/Silky.Core/Modularity/SilkyModule.cs)middle,提供了like下虚method(`RegisterServices`),in realityAutofacof基类`Autofac.Module`of一个基础method,exist调用`containerBuilder.RegisterModule((SilkyModule)module.Instance)`Time，底层会pass调用moduleof`Load()`实现moduleof具体服务ofregister。exist`Load()`method,每个module会调用`RegisterServices(builder)`实现pass`ContainerBuilder`对服务进行register。


```csharp
protected override void Load([NotNull] ContainerBuilder builder)
{
    base.Load(builder);
    RegisterServices(builder);
}
```

so,Silky具体ofmodule可以pass重写`RegisterServices([NotNull] ContainerBuilder builder)`实现该moduleuse`ContainerBuilder`实现服务of依赖register。

```csharp
protected virtual void RegisterServices([NotNull] ContainerBuilder builder)
{
}
```

::: tip hint

use`ContainerBuilder`实现服务ofregister和pass`IServiceCollection`实现服务ofregisterof效果是一致of;use`ContainerBuilder`实现服务ofregisterof优势exist于支持命名服务ofregister。也就是exist服务registerof过程middle,can give the service a name,exist服务解析of过程middle，pass名称去resolve to指定名称of接口of实现ofobject。

:::

2.2.2 pass约定register服务

service engine`SilkyEngine`pass调用`RegisterDependencies()`method,use`ContainerBuilder`实现对约定of规范of服务进行register。

```csharp
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
```

exist上面of代码middle,We sawpass构建约定register器(`IDependencyRegistrar`)of实例,pass约定register器实现指定服务ofregister。系统存exist两个默认of约定register器: 

(1) [DefaultDependencyRegistrar](https://github.com/liuhll/silky/blob/main/framework/src/Silky.Core/DependencyInjection/DefaultDependencyRegistrar.cs),该服务register器可以实现对标识接口of服务register;
  
  A) to inheritance`ISingletonDependency`of类register为单例;
  B) to inheritance`ITransientDependency`of类register为瞬态;
  C) to inheritance`IScopedDependency`of类register为范围;

(2) [NamedServiceDependencyRegistrar](https://github.com/liuhll/silky/blob/main/framework/src/Silky.Core/DependencyInjection/NamedServiceDependencyRegistrar.cs) 实现了对命名服务ofregister;exist某个类继承上述标识接口Time,like果pass`InjectNamedAttribute`Attributes to name services,那么该服务of将会被命名为该名称of服务,exist解析该服务ofTime候,可以pass名称进行解析。
例like:


```csharp
// 该服务将会被register为范围of,and named as:DemoService,exist服务解析过程middle可以pass服务名 DemoService resolve to
[InjectNamed("DemoService")]
public class DemoService : IScopedDependency
{

}

```

3. service engine提供了多种判断服务是否register以及服务解析method;

4. service engine提供了获取指定ofconfigure项ofmethod;

5. 可以passservice engine获取类型查找器(`TypeFinder`)、服务configure器(`Configuration`)、host environment variables提供者(`IHostEnvironment`)、and hostname(`HostName`)Wait信息。


## 获取和useservice engine

exist开发过程middle，可以pass`EngineContext.Current`获取service engine,并useservice engine提供of各个method,例like:判断服务是否register、Parse service、获取configure类、获取当前原因of主机名称、或是use类型查找器(`TypeFinder`)、服务configure器(`Configuration`)、host environment variables提供者(`IHostEnvironment`)Wait。

::: tip hint

exist开发过程middle,useservice engineof大部分场景是，exist不方便实现对某个服务进行构造注入of场景下,passservice engine实现对某个服务解析,从而得到该服务of实例。

:::