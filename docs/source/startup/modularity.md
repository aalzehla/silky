---
title: module
lang: zh-cn
---

## moduleof定义

Silkyis a multiplenuget包构成ofmodule化offrame,每个moduleWill程序划分为One个小of结构,In this structure; it has its own logic code and its own scope，Does not affect other structures。

### module类

normally，Onemoduleof定义是passexist该程序集内创建One派生自 `SilkyModule`the type,As follows:

```csharp

public class RpcModule : SilkyModule
{

}

```

[SilkyModule](https://github.com/liuhll/silky/blob/main/framework/src/Silky.Core/Modularity/SilkyModule.cs)是One抽象the type,它定义了moduleof基础method，体现了moduleexistin the frameof作用;

`SilkyModule`module定义of核心代码As follows:

```csharp
public abstract class SilkyModule : Autofac.Module, ISilkyModule, IDisposable
{

   protected SilkyModule()
   {
       Name = GetType().Name.RemovePostFix(StringComparison.OrdinalIgnoreCase, "Module");
   }

   protected override void Load([NotNull] ContainerBuilder builder)
   {
       base.Load(builder);
       RegisterServices(builder);
   }

    public virtual void ConfigureServices(IServiceCollection services, IConfiguration configuration)
    {
    }
 
    protected virtual void RegisterServices([NotNull] ContainerBuilder builder)
    {
    }

    public virtual Task Initialize([NotNull] ApplicationContext applicationContext)
    {
       return Task.CompletedTask;
    }

    public virtual Task Shutdown([NotNull] ApplicationContext applicationContext)
    {
        return Task.CompletedTask;
    }

    public virtual string Name { get; }

   // other codes...
}
```

through the pair`SilkyModule`module代码定义of分析我们可以得知,OneSilkymodule有like下几个作用:

1. exist`ConfigureServices()`method,pass`IServiceCollection`Implement service registration;

2. exist`RegisterServices()`method,pass`ContainerBuilder`Implement service registration;

3. exist应用程序start up时,pass`Initialize()`methodaccomplishmoduleof初始化method; 

4. exist应用程序停止时,implement`Shutdown()`method,可以accomplishmodule资源of释放;


Regarding the above1、2 point of action, 我们已经exist[Chapter Service Engine](/source/startup/engine.html#The role of the service engine)detailed analysis in;About the3、4point of action,应用程序是like何existstart up时调用`Initialize()`method或是exist停止时implement`Shutdown()`method呢?

exist[Build a service engine](/source/startup/engine.html#Build a service engine)in a chapter,we mentioned,existBuild a service engine时,We have a very important job is to register[InitSilkyHostedService](https://github.com/liuhll/silky/blob/main/framework/src/Silky.Core/InitSilkyHostedService.cs)[background tasks](https://learn.microsoft.com/zh-cn/aspnet/core/fundamentals/host/hosted-services?view=aspnetcore-6.0&tabs=visual-studio)。

background tasks`InitSilkyHostedService`of源码As follows:

```csharp
    public class InitSilkyHostedService : IHostedService
    {
        private readonly IModuleManager _moduleManager;
        private readonly IHostApplicationLifetime _hostApplicationLifetime;

        public InitSilkyHostedService(IServiceProvider serviceProvider,
            IModuleManager moduleManager,
            IHostApplicationLifetime hostApplicationLifetime)
        {
            if (EngineContext.Current is SilkyEngine)
            {
                EngineContext.Current.ServiceProvider = serviceProvider;
            }

            _moduleManager = moduleManager;
            _hostApplicationLifetime = hostApplicationLifetime;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            Console.WriteLine(@"                                              
   _____  _  _  _           
  / ____|(_)| || |          
 | (___   _ | || | __ _   _ 
  \___ \ | || || |/ /| | | |
  ____) || || ||   < | |_| |
 |_____/ |_||_||_|\_\ \__, |
                       __/ |
                      |___/
            ");
            var version = Assembly.GetExecutingAssembly().GetName().Version;
            var ver = $"{version.Major}.{version.Minor}.{version.Build}";
            Console.WriteLine($" :: Silky ::        {ver}");
            _hostApplicationLifetime.ApplicationStarted.Register(async () =>
            {
                await _moduleManager.InitializeModules();
            });
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            _hostApplicationLifetime.ApplicationStopped.Register(async () =>
            {
                await _moduleManager.ShutdownModules();
            });
        }
    }
```

1. existbackground tasks`StartAsync()`,exist打印Silkyofbannerback,exist应用start up时注册One回调method,passmodule管理器`IModuleManager`implement初始化modulemethod;

2. existbackground tasks`StopAsync()`,exist应用停止back注册One回调method,passmodule管理器`IModuleManager`implement关闭modulemethod,一般用于各个moduleof资源释放;

under,我们查看module管理器[ModuleManager](https://github.com/liuhll/silky/blob/main/framework/src/Silky.Core/Modularity/ModuleManager.cs)是like何初始化moduleof:

```csharp
        public async Task InitializeModules()
        {
            foreach (var module in _moduleContainer.Modules)
            {
                try
                {
                    Logger.LogInformation("Initialize the module {0}", module.Name);
                    await module.Instance.Initialize(new ApplicationContext(_serviceProvider, _moduleContainer));
                }
                catch (Exception e)
                {
                    Logger.LogError($"Initializing the {module.Name} module is an error, reason: {e.Message}");
                    throw;
                }
            }
        }
```

module容器`_moduleContainer`of属性`_moduleContainer.Modules`是passmodule加载器`ModuleLoader`加载并pass依赖关系进行排序得到of所有moduleof实例,We sawpass`foreach`对所有ofmodule实例进行遍历,并依次implement各个moduleof`Initialize()`method。

同样of，exist应用程序停止时,will call`InitSilkyHostedService`任务of`StopAsync()`,该methodpass调用module管理器of`ShutdownModules()`method,accomplish对各个module资源of释放;

```csharp
    public async Task ShutdownModules()
    {
        foreach (var module in _moduleContainer.Modules)
        {
            await module.Instance.Shutdown(new ApplicationContext(_serviceProvider, _moduleContainer));
        }
    }
```

## modulethe type型

existSilkyin the frame,我Willmodulethe type型划分为like下几种type:

1. moduleof定义`SilkyModule`: [SilkyModule](https://github.com/liuhll/silky/blob/main/framework/src/Silky.Core/Modularity/SilkyModule.cs)是One抽象ofmodule,用于定义moduleof概念;其他业务module必须要派生自该类;

2. 业务module: directly derived from`SilkyModule`类of非抽象类,Silkyin the frame,几乎所有of包existpass定义业务moduleback从而accomplishmodule化编程of,很多核心of包都是业务module,like:`SilkyModule`、`ConsulModule`、`DotNettyModule`等等module都属于业务module;

3. Httptypeof业务module:该typeof业务module派生自[HttpSilkyModule](https://github.com/liuhll/silky/blob/main/framework/src/Silky.Core/Modularity/HttpSilkyModule.cs),相比一般of业务module,该typeofmodule增加了`Configure(IApplicationBuilder application)`method,该typeofmodule一般用于passweb主机构建of微Serve应用或是网关中,可以exist`Configure()`methodpass`IApplicationBuilder`quotehttpmiddleware,existsilkyin the frame,诸like: `CorsModule`、`IdentityModule`、`MiniProfilerModule`等均是该typeofmodule; need特别注意of是，needhttp业务module配置ofmiddleware起效果of话，不要忘记needexist`Startup`类中of`Configure`进行like下配置：

```csharp

public void Configure(IApplicationBuilder app, IWebHostEnvironment env) 
{
   app.ConfigureSilkyRequestPipeline();
}
```

4. start upmodule:该typeofmodule派生自[StartUpModule](https://github.com/liuhll/silky/blob/main/framework/src/Silky.Core/Modularity/StartUpModule.cs)of非抽象类;existmodule加载过程中,pass指定start upmodule,从而得知moduleof依赖关系,module加载器会passmoduleof依赖对module进行排序,从而影响应用existstart up时各个moduleofimplementof先back顺序;Silkymodule预定义了多个start upmodule,can be used for differentsilky主机of构成:
  A) `DefaultGeneralHostModule` 用于构建普通of业务主机,Generally available for hosting onlyRPCServeof微Serve应用;
  B) `WebSocketHostModule` for build provisionWebSocketServe能力of业务主机;
  C) `DefaultWebHostModule` used to build the ability to provideHttpServeof业务主机,available externallyhttpServe,Can also be used internallyrpccommunication;
  D) `DefaultGatewayHostModule` 用于构建网关微Serve,一般为微Serve集群暴露对外部ofhttpaccess port,pass路由机制,Willhttp请求转发到具体某个Serve条目,对内passRPC进行communication;

besides,开发者也可以自己of需求,为自己定义needofstart upmodule,exist构建微Serve主机时,指定相应ofstart upmodule。


## moduleof加载

Silky所有ofmodule是exist什么时候以及like何进行加载和排序of呢?

exist之前of[Build a service engine](/source/startup/engine.html#Build a service engine)ofin a chapter,我们知道exist`AddSilkyServices<T>()`method,我们pass泛型`T`来指定应用程序of启用module`StartUpModule`type。并构建了module加载器对象`ModuleLoader`,并且Willmodule加载器对象作为Serve引擎of`LoadModules()`method参数:

```csharp
public static IEngine AddSilkyServices<T>(this IServiceCollection services, IConfiguration configuration,
            IHostEnvironment hostEnvironment) where T : StartUpModule
{
    var moduleLoader = new ModuleLoader();
    engine.LoadModules<T>(services, moduleLoader);
}
```

existServe引擎`SilkyEngine`in the implementation class,Apart from realizing`IEngine`outside the interface,还needaccomplish了`IModuleContainer`interface,`IModuleContainer`只定义了One只读属性`Modules`，要求pass该属性获取所有ofmodule;existServe引擎中,我们passmodule加载器对象`moduleLoader.LoadModules()`methodaccomplish对moduleof加载与解析,and for properties`Modules`make assignment;

```csharp
internal sealed class SilkyEngine : IEngine, IModuleContainer
{
  // other codes...

  
   public void LoadModules<T>(IServiceCollection services, IModuleLoader moduleLoader)
   where T : StartUpModule
   {
      Modules = moduleLoader.LoadModules(services, typeof(T));
   }
  
   // accomplishIModuleContainer定义of属性
   public IReadOnlyList<ISilkyModuleDescriptor> Modules { get; private set; }
}
```

module加载器[ModuleLoader](https://github.com/liuhll/silky/blob/main/framework/src/Silky.Core/Modularity/ModuleLoader.cs)requires two parameters to be passed,One是`IServiceCollection`of对象`services`，One是start upmodule`StartupModule`ofthe type型`typeof(T)`;under我们来描述module加载of过程:

1. pass`SilkyModuleHelper.FindAllModuleTypes(startupModuleType)` 查找到start upmodule`StartupModule`type依赖of所有moduletype;

2. pass反射创建moduleof实例,并pass`IServiceCollection`注册单例ofmodule实例,并创建module描述符`SilkyModuleDescriptor`;

3. 根据moduleof依赖关系对module进行排序;

moduleof依赖关系是pass特性`DependsOnAttribute`指定of,pass[DependsOnAttribute](https://github.com/liuhll/silky/blob/main/framework/src/Silky.Core/Modularity/DependsOnAttribute.cs)exist对modulethe type进行标注,就可以解析到各个moduleof依赖关系,从而accomplishpassmoduleof依赖关系进行排序;

::: tip hint

familiar[APBframe](https://github.com/abpframework/abp)of小伙伴应该可以看出来,Silkymoduleof设计主要是借鉴了APBframeofmodule设计,exist一些细节方面做了调整。

::: 

## Silkyof核心module

pass上面of介绍, 我们知道Onemodule类of最重要of工作主要由两点: 1. accomplishServeof注册; 2. exist应用start up时或是停止时implement指定ofmethod完成初始化任务或是释放资源of任务;

like何判断是否是silkyof核心module呢? 核心module最重要ofOne作用就是exist应用start up时,pass`Initialize()`methodimplement该moduleof初始化资源of任务;

pass查看源码，We found that mostsilkymoduleexist应用start up时并没有重写`Initialize()`method,That is to say,mostsilkymoduleexist应用start up过程时主要是完成各个moduleofServe类of注册并不need做什么工作。

![SilkyModel.png](/assets/imgs/SilkyModel.png)

like上图所示,We sawsilkyframe定义ofmodule,由like上几个module是exist应用start up是完成了主机start up时of关键性作业;

我们再根据moduleof依赖关系,可以看到主机exist应用start up时,passmodule初始化任务ofOneimplement顺序As follows:

```
RpcModule --> DotNettyTcpModule | TransactionModule | WebSocketModule | [RpcMonitorModule] 

--> GeneralHostModule(start upmodule[StartUpModule])[DefaultGeneralHostModule|WebSocketHostModule|DefaultWebSocketHostModule] 

```
pass上述of依赖关系,We can know that:

1. Rpcmoduleexist应用start up时是最早被implement;

2. 然back依次implement: DotNettyTcpModule | TransactionModule | WebSocketModule | [RpcMonitorModule] 等module;

3. 最backimplement应用start upmodule指定of初始化method;


exist上述of过程中,Silky主机existstart up时need完成like下of工作:

1. accomplishRpcmessage monitoringof订阅;

2. 解析应用Serve与Serve条目;

3. start upRpcmessage monitoring;

4. 解析Serve主机和注册该主机实例对应of端点;

5. 向Serve注册中心更新或是获取Serve元数据(应用Serve描述信息和Serve条目描述信息);

6. 向Serve注册中心注册该Serve当前实例of端点以及从Serve注册中心获取该Serve对应of所有实例；

7. pass心跳of方式从Serve注册中心获取最新ofServe元数据以及Serve实例信息;

existunderof篇章中,我们Will着重介绍上述of过程是like何accomplishof。