---
title: module
lang: zh-cn
---

## module的定义and类型

existsilkyframe,moduleYes应用程序用于服务注册、initialization task、The unit for releasing resources,is defined as an assembly。module具有依赖关系,pass`DependsOn`特性来确定module之间的依赖关系。

silkyframe存exist**two types**的module:

1. 开发者pass继承`SilkyModule`就可以定义一个普通module类;
2. 也可以pass继承`StartUpModule`定义一个服务注册启动module类。


E.g:

普通module类

```csharp
// 普通module类,启动module类必须要直接或间接的依赖该module
[DependsOn(typeof(RpcModule))]
public class CustomModule : SilkyModule
{
}

```

启动module类

```csharp
// 启动module类，只有该类型的module才可以被允许existConstruct服务中被指定为启动module
[DependsOn(typeof(ZookeeperModule),
        typeof(DotNettyTcpModule),
        typeof(MessagePackModule),
        typeof(RpcProxyModule),
        typeof(TransactionTccModule),
        typeof(AutoMapperModule)
)]
public class NormHostModule : StartUpModule
{
}

```
::: tip

1. 开发者想要执行一个module,需要exist微服务时指定该module,或Yespass`DependsOn`特性直接或Yes间接的依赖该module。

2. 只有启动module类才可以exist服务服务注册时指定该module为注册的启动module。
:::

## existmodule中注册服务

module提供了两个服务注册的API,一Yespass`ServiceCollection`Implement service registration,二Yespass`ContainerBuilder`Implement service registration。

### pass`ServiceCollection`Implement service registration

开发者pass重写`ConfigureServices`method,可以pass`IServiceCollection`Implement service registration，E.g:

```csharp
public override void ConfigureServices(IServiceCollection services, IConfiguration configuration)
{
    services.AddOptions<RpcOptions>()
        .Bind(configuration.GetSection(RpcOptions.Rpc));
    services.AddOptions<GovernanceOptions>()
        .Bind(configuration.GetSection(GovernanceOptions.Governance));
    services.AddOptions<WebSocketOptions>()
        .Bind(configuration.GetSection(WebSocketOptions.WebSocket));
    services.AddDefaultMessageCodec();
    services.AddDefaultServiceGovernancePolicy();
}
```

### pass`ContainerBuilder`Implement service registration

`ContainerBuilder` Yes [Autofac](https://github.com/autofac/Autofac) Class that provides service registration,开发者可以pass重写`RegisterServices`methoduse`ContainerBuilder`which providedAPIImplement service registration。use`ContainerBuilder`注册服务的一个优势Yes可以注册命名的服务。

```csharp
protected override void RegisterServices(ContainerBuilder builder)
{
    builder.RegisterType<DefaultExecutor>()
        .As<IExecutor>()
        .InstancePerLifetimeScope()
        .AddInterceptors(
            typeof(CachingInterceptor)
        )
        ;
}
```

## usemoduleinitialization task

exist应用程序启动过程中,Developers can override`Initialize`method来实现module的initialization task。开发者可以pass`applicationContext.ServiceProvider`properties to resolve registered services。

```csharp
public override async Task Initialize(ApplicationContext applicationContext)
{
    var serverRouteRegister =
        applicationContext.ServiceProvider.GetRequiredService<IServerRegister>();
    await serverRouteRegister.RegisterServer();
}
```

## usemodule释放资源

exist应用程序正常停止时,pass重写`Shutdown`method来实现module停止时需要执行的method,E.g：release resources; etc.。

```csharp
public override async Task Shutdown(ApplicationContext applicationContext)
{
    var serverRegister =
        applicationContext.ServiceProvider.GetRequiredService<IServerRegister>();
    await serverRegister.RemoveSelf();
}
```

## module的依赖关系

silkyframe的modulepass`DependsOn`特性指定module的依赖关系,silkyframe支持pass直接或Yes间接的依赖module。E.g: `NormHostModule`module依赖了`DotNettyTcpModule`module,`DotNettyTcpModule`module依赖了`RpcModule`module,Specified during microservice registration`NormHostModule`为启动module。So根据module依赖关系,`RpcModule`module会被应用加载,and precedes`DotNettyTcpModule`and`NormHostModule`执行服务注册methodand初始化method。

开发者只需要pass`DependsOn`特性exist类直接就可以指定该module依赖的module,exist应用启动过程中,会根据module的依赖关系进行排序。并完成服务注册methodand指定的初始化method。

E.g,`NormHostModule`的module依赖关系如下所示:

```csharp
    [DependsOn(typeof(ZookeeperModule),
        typeof(DotNettyTcpModule),
        typeof(MessagePackModule),
        typeof(RpcProxyModule),
        typeof(TransactionTccModule),
        typeof(AutoMapperModule)
    )]
    public class NormHostModule : StartUpModule
    {
    }
```


## Constructhost time指定启动module

开发者如果自定义了module,So,需要existConstruct微服务host time,指定启动module。

E.g:

```csharp

private static IHostBuilder CreateHostBuilder(string[] args)
{
    return Host.CreateDefaultBuilder(args)
            .RegisterSilkyServices<NormHostModule>() //指定启动的module，silkyframe约束了该module类型必须为启动module类(StartUpModule)
        ;
}

```

normally,开发者existConstruct默认host time,并不需要指定启动module。default host for builds,already based on the build host type，指定了默认的启动module。E.g,use`ConfigureSilkyWebHostDefaults`Constructsilkyhost time,already specified`DefaultWebHostModule`作为其中module。

如果开发者有自定义module时,同时也需要自定义一个启动module,pass该启动module依赖开发者自定义的moduleand silky frame定义的module，达到服务注册andinitialization task的目的。

E.g:

```csharp
[DependsOn(typeof(ZookeeperModule),
        typeof(DotNettyTcpModule),
        typeof(MessagePackModule),
        typeof(RpcProxyModule),
        typeof(TransactionTccModule),
        typeof(AutoMapperModule),
        typeof(CustomModule),
)]
public class CustomStartHostModule : StartUpModule
{
}

```

For the convenience of developers,silkyframe根据Construct主机的类型,已经创建了多种启动module,该类型的启动module已经定义好了该module必须的依赖的module:

1. passweb主机Construct微服务应用的`WebHostModule`module
2. pass通用主机Construct微服务应用的`GeneralHostModule`module
3. Constructwebsocketservice host application`WebSocketHostModule`module
4. Construct只能作为服务消费者网关应用的`GatewayHostModule`module

开发者可以选择继承如上的启动module,and configureHostHost providedAPI就可以Construct相应的主机。