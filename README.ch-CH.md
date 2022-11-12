<p align="center">
  <img height="200" src="./docs/.vuepress/public/assets/logo/logo.svg">
</p>

# Silky Microservice Framework
[![GitHub license](https://img.shields.io/badge/license-MIT-blue.svg)](./LICENSE)
[![Commit](https://img.shields.io/github/last-commit/liuhll/silky)](https://img.shields.io/github/last-commit/liuhll/silky)
[![NuGet](https://img.shields.io/nuget/v/silky.Core.svg?style=flat-square)](https://www.nuget.org/packages/Silky.Core)
[![MyGet (nightly builds)](https://img.shields.io/myget/silky-preview/vpre/Silky.Core.svg?style=flat-square)](https://www.myget.org/feed/Packages/silky-preview)
[![NuGet Download](https://img.shields.io/nuget/dt/Silky.Core.svg?style=flat-square)](https://www.nuget.org/packages/Silky.Core)
[![Hits](https://hits.seeyoufarm.com/api/count/incr/badge.svg?url=https%3A%2F%2Fgithub.com%2Fliuhll%2Fsilky&count_bg=%2379C83D&title_bg=%23555555&icon=&icon_color=%23E7E7E7&title=hits&edge_flat=false)](https://hits.seeyoufarm.com)

<div align="center">

**Simplified Chinese | [English](./README.en-US.md)**

</div>

## Project Introduction


silkyThe framework is designed to help developers in.netunder the platform,A development framework for quickly building a microservice application with simple code and configuration。It provides **RPCcommunication** and **Microservice Governance** Two key capabilities。this means，use silky Developed microservices，将具备相互之间的远程发现andcommunication能力， use at the same time silky Provides rich service governance capabilities，can implement things like service discovery、load balancing、Service governance requirements such as traffic scheduling。at the same time silky is highly scalable，Users can customize their implementation at almost any function point，Change the default behavior of the framework to meet your business needs。

silkyMicroservices have the following advantages：
- out of the box
  - Simple to use,use[Universal host](https://docs.microsoft.com/zh-cn/aspnet/core/fundamentals/host/generic-host?view=aspnetcore-5.0)or[webhost](https://docs.microsoft.com/zh-cn/aspnet/core/fundamentals/host/web-host?view=aspnetcore-5.0)to build(hosting)Microservice application。
  - Ease of use，The interface-oriented proxy feature can realize local transparent calls。
  - Feature rich，基于原生库或轻量扩展即可实现绝大多数的Microservice Governance能力。

- Ultra-large-scale microservice cluster practice
  - 高性能的跨进程communication协议,use[DotNetty](https://github.com/Azure/DotNetty)communicationframe实现了基于接口代理的RPCframe，Provides high-performance proxy-based remote invocation capabilities，Services are interface-based，Shields the low-level details of remote calls for developers。
  - address discovery、Traffic management level，Easily support scaled cluster instances。

- 企业级Microservice Governance能力
  - pass[Polly](https://github.com/App-vNext/Polly)Implemented Service Governance,Improve service fault tolerance。
  - 内置多种load balancing策略，Intellisense downstream node health status，Significantly reduces call latency，Improve system throughput。
  - Support multiple registry services,Real-time perception of service instance online and offline。

- Data Consistency Guarantee
  - useTCCDistributed transactions ensure data eventual consistency。

## frame特性

![silkyMicroservice Framework.png](./docs/.vuepress/public/assets/imgs/silkyMicroservice Framework.png)

### service engine+Modular design

- Responsiblesilkyhost的初始化过程
- Responsible模块解析、依赖管理and加载
- Serve注册and解析


### RPCcommunication

- use[Dotnetty](https://github.com/Azure/DotNetty)as底层communication组件，useTCPascommunication协议, 采用长链接方式Improve system throughput
- Interface-based dynamic proxy
- supportJSON、MessagePack、ProtoBufCodec method
- RPCcommunication过程中support缓存拦截,提高communication性能
- RPCcall monitoring

### Service Governance

- Service auto-registration and discovery,Service instance online and offline intellisense
- RPCCall failed and retry
- support轮询、random routing、哈希一致性等load balancing路由方式, Intellisense downstream node health status，Significantly reduces call latency，Improve system throughput。
- supportHTTPcurrent limit andRPCCall current limiting
- support熔断保护,When an unfriendly class exception occursnTurn on fuse protection after
- supportRPCcall monitoring
- Service downgrade,whenRPCCalled after a failed call`Fabllback`The specified method achieves the purpose of service fault tolerance
- passconfiguresupport禁止Serve被外部访问

### pass.nethost构建

- usewebhost构建Microservice application
- useUniversal host构建Microservice application
- build withwebsocket能力的Microservice application
- Build gateway applications
  

### safety design

- 网关统一进行身份认证and鉴权
- rpc tokenfor protectionRPCcommunication,Guaranteed that the outside cannot be directly accessedrpcServe
- RPCcommunicationsupportsslencryption

### Multiple configuration methods

- supportJsonformat configuration file
- supportYamlformat configuration file
- supportApolloasconfigureServe中心
- use环境变量

### link tracking

- HTTPask
- RPCtransfer
- TCCDistributed transaction
- other(EFCore)...

### supportDistributed transaction

- RPCcommunication过程中,passTCC分布式frame保证数据最终一致性
- use拦截器+TODOlog implementation
- use RedisasTODOlog repository

### supportwebsocketcommunication

- pass[websocketsharp.core](https://www.nuget.org/packages/websocketsharp.core/)Component buildwebsocketServe
- 透过网关代理and前端进行握手、conversation

## getting Started

- pass[developer documentation](http://docs.silky-fk.com/silky/)studySilkyframe。
- pass[silky.samplesproject](http://docs.silky-fk.com/silky/dev-docs/quick-start.html)熟悉如何useSilkyframe构建一个Microservice application。
- pass[configure](http://docs.silky-fk.com/config/)Documentation familiarSilkyframe的相关configure属性。


## 示例project

### Silky.HeroRights management system

* project地址
https://github.com/liuhll/silky.hero

* demo address
https://hero.silky-fk.com/

* account information(tenantsilky)
  * Administrator account(password): admin(123qweR!)
  * general user: liuhll(123qweR!)
  * other账号password: 123qweR!


## quick start

### 1. 构建host

create a new onewebor控制台project,pass nugetInstall`Silky.Agent.Host`Bag。

```pwsh
PM> Install-Package Silky.Agent.Host
```

exist`Main`方法中pass`HostBuilder`构建host。

```csharp
public class Program
{
  public static Task Main(string[] args)
  {
    return CreateHostBuilder(args).Build().RunAsync();
  }

  private static IHostBuilder CreateHostBuilder(string[] args) =>
    Host.CreateDefaultBuilder(args)
      .ConfigureSilkyWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>();});
   
}
```

exist`Startup`中configureServe依赖注入，以及configure中间件。

```csharp
public void ConfigureServices(IServiceCollection services)
{
  services.AddSilkyHttpCore()
    .AddSwaggerDocuments()
    .AddRouting();
}

public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
{
  if (env.IsDevelopment())
  {
    app.UseDeveloperExceptionPage();
    app.UseSwaggerDocuments();
  }

  app.UseRouting();

  app.UseEndpoints(endpoints => { endpoints.MapSilkyRpcServices(); });
}
```

### 2. 更新configure

existconfigure文件中指定Serve注册中心的类型和Serve注册中心configure属性以及`SilkyRpc`frame的configure。如果useuseDistributed transaction必须要useredisas分布式缓存。

in,exist同一个微Serve集群中,`Rpc:Token`must be the same。`Rpc:Port`The default value of is`2200`,`Rpc:Host`The default is`0.0.0.0`。

exist`appsettings.json`中新增如下configure:

```json
  {
    "RegistryCenter": {
    "Type": "Zookeeper",
    "ConnectionStrings": "127.0.0.1:2181,127.0.0.1:2182,127.0.0.1:2183;127.0.0.1:2184,127.0.0.1:2185,127.0.0.1:2186"
  },
  "DistributedCache": {
    "Redis": {
      "IsEnabled": true,
      "Configuration": "127.0.0.1:6379,defaultDatabase=0"
    }
  },
  "Rpc": {
    "Token": "ypjdYOzNd4FwENJiEARMLWwK0v7QUHPW",
    "Port": 2200
  }
  }
```

### 3. 定义一个Serve接口

normally,我们需要将Serve接口单独定义exist一个project中,方便被Serve消费者引用。

create an interface,并pass`[ServiceRoute]`特性标识为该接口是一个应用Serve。

```csharp
[ServiceRoute]
public interface IGreetingAppService
{   
    Task<string> Get();
}
```

### 4. 提供者实现Serve

create a class,pass继承Serve接口即可实现接口定义的方法。

```csharp
public class GreetingAppService : IGreetingAppService
{
  public Task<string> Get()
  {
    return Task.FromResult("Hello World");
  }
}
```


### 5. 消费者passRPC远程transferServe

otherMicroservice application只需要pass引用应用Serve接口project,pass接口代理andServe提供者pass`SilkyRpc`frame进行communication。

### 6. Swaggerexist线文档

after running the program,Open browser,enter`http://127.0.0.1:5000/index.html` to viewswaggerexist线文档,并且passapito debug。


## passproject模板快速创建应用

silky提供了两个project模板可以快速的创建应用，开发者可以根据需要选择合适的project模板来创建应用。

```pwsh

# 以模块的方式创建Microservice application,适用于将所有的应用放exist同一个仓库
> dotnet new --install Silky.Module.Template

# 以独立应用的方式创建Microservice application,将每个Microservice application单独存放一个仓库
> dotnet new --install Silky.App.Template
```

useproject模板创建Microservice application。

```pwsh

dotnet new silky.app -in -p:i -n Demo

```


## contribute
- contribute的最简单的方法之一就是讨论问题（issue）。你也可以pass提交的 Pull Request 代码变更作出contribute。
