---
title: quick start
lang: zh-cn
---

## necessary premise

1. (**must**) Install .net5 or .net6 sdk。

2. (**must**) you can use itvisual studio orrideras a development tool。 

3. (**must**) 您must准备一个可用of`zookeeper`Service as a Service Registry。

4. (**must**) use selection`redis`Service as a distributed cache service。


## useWebHost builds microservice applications 

Developers can use.netPlatform provides[Web host](https://docs.microsoft.com/zh-cn/aspnet/core/fundamentals/host/web-host?view=aspnetcore-6.0)to buildsilkyMicroservice application。

usewebhostto buildofSilkyMicroservice application，不但可以作为Microservice applicationofServe提供者(The service can be accessed throughSilkyRpcframe to communicate);also availablehttpServe，http请求passapplicationServemethod(Serve条目)Generatedwebapi,passsilky设定ofrouting规则即可访问Microservice application提供of相关Serve。

我们pass如下步骤可以快速ofConstruct一个useWeb hostConstructofSilkyMicroservice application。

1. 新增一个控制台applicationorASP.NET Core Emptyapplication

![quick-start1.png](/assets/imgs/quick-start1.png)

![quick-start1.1.png](/assets/imgs/quick-start1.1.png)

2. Install`Silky.Agent.Host`Bag

pass Nuget Package Manger Install`Silky.Agent.Host`Bag:

![quick-start2.png](/assets/imgs/quick-start2.png)

orpass控制台命令InstallBag:

```powershell
PM> Install-Package Silky.Agent.Host -Version 3.0.2
```

3. exist`Main`built in methodsilkyhost

```csharp
namespace Silky.Sample
{
    using Microsoft.Extensions.Hosting;
    using System.Threading.Tasks;
    class Program
    {
        public static async Task Main(string[] args)
        {
            await CreateHostBuilder(args).Build().RunAsync();
        }

        private static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                    .ConfigureSilkyWebHostDefaults(webBuilder => webBuilder.UseStartup<Startup>());
               
        }
    }
}
```

4. exist启用kind中configureServeand置middleware、routing

```csharp
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Silky.Http.Core;

namespace Silky.Sample
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            // 新增必要ofServe
            services.AddSilkyHttpCore()
                .AddSwaggerDocuments()
                .AddRouting();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // Determine whether the development environment
            if (env.IsDevelopment())
            {
                // 开发环境use开发者异常调式页面
                app.UseDeveloperExceptionPage();
                // 开发环境useSwaggerexist线Documentation
                app.UseSwaggerDocuments();
            }

            // useroutingmiddleware
            app.UseRouting();
            
            // add otherasp.net coremiddleware...

            // configurerouting
            app.UseEndpoints(endpoints => 
              { 
                // configureSilkyRpcrouting
                endpoints.MapSilkyRpcServices(); 
              });
        }
    }
}
```

4. 更新configure

silky支持pass`json`or`yml`格式进行configure。您可以pass`appsettings.json`为公共configure项指定configure信息,也可以pass新增`appsettings.${ENVIRONMENT}.json`文件为指定of环境更新configure属性。

normally,您must指定rpccommunication`token`,Serve注册中心地址等configure项。如果您useredis作为缓存Serve,then you also need to`distributedCache:redis:isEnabled`configure项Set as`true`,and givesredisServe缓存of地址。

exist`appsettings.json`configure文件中新增如下configure属性:

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

Willconfigure文件属性of**Copy to output directory**,Set as: *always copy* or *Copy if newer*。

![quick-start3.png](/assets/imgs/quick-start3.png)

5. createzookeeperServeandredis缓存Serve

exist该示例project中,我们use`Zookeeper`作为Serve注册中心。我们existsilkyof示例project中给出各种基础Serveof[docker-composethe arrangement file](https://github.com/liuhll/silky/tree/main/samples/docker-compose/infrastr),in,也Bag括了zookeeperandredisServeof。

Will[docker-compose.zookeeper.yml](https://raw.githubusercontent.com/liuhll/silky/main/samples/docker-compose/infrastr/docker-compose.zookeeper.yml)and[docker-compose.redis.yml](https://raw.githubusercontent.com/liuhll/silky/main/samples/docker-compose/infrastr/docker-compose.redis.yml)copy to local,Save as a file with the same name,Go to the local directory where the file is saved。

```powershell
# create一个名称为silky_service_netofdockernetwork
docker network create silky_service_net

# usedocker-composecreatezookeeperandredisServe
docker-compose -f docker-compose.zookeeper.yml -f docker-compose.redis.yml up -d
```


6. Microservice applicationof其他层(project)

完成hostproject后,you can add**applicationinterface层**、**application层**、**Domain layer**、**infrastructure layer**等其他project,For more information; please refer to[微Serve架构](#)node。

一个典型of微Serve模块of划分与传统of`DDD`领域模型ofapplication划分基本consistent。需要Willapplicationinterface单独of抽象为一个程序集，方便被其他Microservice applicationquote，其他Microservice applicationpassapplicationinterfacegenerateRPCacting,与该微Serve通信。

一个典型of微Serve模块ofproject结构如下所示:

![quick-start4.png](/assets/imgs/quick-start4.png)

projectofrely关系如下:

(1) hostprojectrely**application层**,从而达到对applicationof托管。

(2) **applicationinterface层**用于定义Serveinterfaceand`DTO`object,**application层**need to depend on**applicationinterface层**,实现定义好ofServeinterface。

(3) **Domain layer**主要used to implement具体of业务逻辑,可以rely自身of**applicationinterface层**以及其他Microservice applicationof**applicationinterface层**(Developers can usenugetBagInstall其他Microservice applicationofapplicationinterfaceprojector直接添加projectof方式进行quote);**Domain layer**rely自身of**applicationinterface层**of原因是为了方便use`DTO`object;quote其他微Serveof**applicationinterface层**可以passinterfaceGenerated动态acting,与其他微Servepass`SilkyRPC`frame to communicate。

(4) **Domain Shared Layer(Domain.Shared)** generally used to defineddd概念中of值kind型以及枚举等,方便被其他Microservice applicationquote。

(5) **EntityFramework**作为基础Serve层,Provide data access capability,certainly,开发者也可以选择use其他ORMframe。


7. applicationinterfaceof定义and实现

**applicationinterface层(Silky.Sample.Application.Contracts)** InstallBag`Silky.Rpc`:

![quick-start5.png](/assets/imgs/quick-start5.png)

orpass控制台命令InstallBag:

```powershell
PM> Install-Package Silky.Rpc -Version 3.0.2
```

新增一个Serveinterface`IGreetingAppService`,and define a`Say()`method,applicationinterface需要use`[ServiceRoute]`feature to identify。

```csharp
[ServiceRoute]
public interface IGreetingAppService
{
    Task<string> Say(string line);
}
```

next,we need to **application层(Silky.Sample.Application)** rely(quote) **applicationinterface层(Silky.Sample.Application.Contracts)**, and add aServekind`GreetingAppService`,pass它实现Serveinterface`IGreetingAppService`。

```csharp
    public class GreetingAppService : IGreetingAppService
    {
        public Task<string> Say(string line)
        {
            return Task.FromResult($"Hello {line}");
        }
    }
```

8. passSwaggerDocumentationexist线调试

运行application程序,to openswaggerexist线Documentation。Developers can useswaggerGeneratedexist线Documentation调试API。

![quick-start6.png](/assets/imgs/quick-start6.png)

## use.NET通用Host builds microservice applications

Developers can use.netPlatform provides[通用host](https://docs.microsoft.com/zh-cn/aspnet/core/fundamentals/host/generic-host?view=aspnetcore-6.0)to buildsilkyMicroservice application。

use.NET 通用Host builds microservice applications只能作为Serve提供者,passSilkyRPCframe与其他Microservice application进行通信;can not providehttpServe，That is to say,集群外部无法直接访问该Microservice application，只能passgatewayor其他提供httpServeofMicroservice application访问该Microservice applicationofServe。

use.NET 通用hostConstructSilkyMicroservice applicationofsteps withuseuseWeb Host builds microservice applicationsof步骤基本consistent,区别exist于无需configure`Startup`kind,也不能configurehttpmiddleware(configure了也无效);Developers can use实现`IConfigureService`interface来完成对Serve注入ofconfigure。

1-2 steps with[usewebHost builds microservice applications](#useWebHost builds microservice applications)consistent。

3. exist`Main`built in methodsilkyhost

```csharp
namespace Silky.Sample
{
    using Microsoft.Extensions.Hosting;
    using System.Threading.Tasks;
    class Program
    {
        public static async Task Main(string[] args)
        {
            await CreateHostBuilder(args).Build().RunAsync();
        }

        private static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                    .ConfigureSilkyGeneralHostDefaults();
               
        }
    }
}
```
create`ConfigureService`kind,used to implement`IConfigureService`interface,exist`ConfigureServices()`method中configureServe注入rely。

```csharp
   public class ConfigureService : IConfigureService
    {
        public void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddSilkySkyApm()
                //其他Serve(Bag括第三方componentsofServeorsilkyframeof其他Serve,E.g:Efcorecomponents,MessagePackCodec,CaporMassTransitetc. Distributed event bus etc.)
                //...
                ;
        }
    }
```

5-7steps with[usewebHost builds microservice applications](#useWebHost builds microservice applications)consistent。

启动application后,我们可以exist控制台看到相关of日志输出,applicationServe启动成功。

![quick-start7.png](/assets/imgs/quick-start7.png)

用户无法直接访问该Microservice application,mustpassgatewayquote该微Serveof **applicationinterface层** ,pass[gateway](#ConstructSilky微Servegateway)of提供ofhttpServe间接of访问该Microservice application提供ofServe。


## Construct具有websocketServe能力ofMicroservice application

开发者passConstruct具有websocketServe能力ofMicroservice application, 这样ofMicroservice application可以除了可以作为Serve提供者之外,also provideswebsocketcommunication能力(websocketThe port defaults to:3000)。可以pass与Serve端进行握手会话(可以passgatewayacting),Serve端实现向客户单推送消息of能力。


Construct具有websocketServe能力ofMicroservice application与[use.NET通用Host builds microservice applications](#use.NET通用Host builds microservice applications)of步骤consistent,只是用于ConstructMicroservice applicationofmethod有差异。

1-2 steps with[usewebHost builds microservice applications](#useWebHost builds microservice applications)consistent。

3. exist`Main`built in methodsilkyhost

```csharp
namespace Silky.Sample
{
    using Microsoft.Extensions.Hosting;
    using System.Threading.Tasks;
    class Program
    {
        public static async Task Main(string[] args)
        {
            await CreateHostBuilder(args).Build().RunAsync();
        }

        private static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureSilkyWebSocketDefaults();
    }
}

```

create`ConfigureService`kind,used to implement`IConfigureService`interface,exist`ConfigureServices()`method中configureServe注入rely。

```csharp
   public class ConfigureService : IConfigureService
    {
        public void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddSilkySkyApm()
                //其他Serve(Bag括第三方componentsofServeorsilkyframeof其他Serve,E.g:Efcorecomponents,MessagePackCodec,CaporMassTransitetc. Distributed event bus etc.)
                //...
                ;
        }
    }
```

5-6steps with[usewebHost builds microservice applications](#useWebHost builds microservice applications)consistent。

7. Construct具有提供websocketServe能力ofServe

applicationServeinterfaceof定义与一般applicationServeofinterface定义一样,只需要exist一个普通ofinterface标识`[ServiceRoute]`feature。

```csharp

[ServiceRoute]
public interface ITestAppService
{
   // 可以定义其他method(Serve条目),定义ofmethod可以与其他Microservice applicationpassRPCframe to communicate
}
```


we need toexist **application层(Silky.Sample.Application)** Install `Silky.WebSocket`Bag。

```powershell
PM> Install-Package Silky.WebSocket -Version 3.0.2
```

and add a `TestAppService`kind, pass它来实现 `ITestAppService`, besides,we need to `TestAppService`kind继承 `WsAppServiceBase`基kind。

```csharp
    public class TestAppService : WsAppServiceBase, ITestAppService
    {
        private readonly ILogger<TestAppService> _logger;

        public TestAppService(ILogger<TestAppService> logger)
        {
            _logger = logger;
        }

        // when establishedwebsocketsession
        protected override void OnOpen()
        {
            base.OnOpen();
            _logger.LogInformation("websocket established a session");
            
        }

        // whenServe端接收到客服端of消息时
        protected override void OnMessage(MessageEventArgs e)
        {
            _logger.LogInformation(e.Data);
        }
        
       // whenwebsocketWhen the session is closed
        protected override void OnClose(CloseEventArgs e)
        {
            base.OnClose(e);
            _logger.LogInformation("websocket disconnected");
        }

        // 其他Servemethod
    }
```

启动application后,我们可以exist控制台看到相关of日志输出,applicationServe启动成功。我们定义ofwebsocketofServeofwebapiaddress is:`/api/test`。

![quick-start8.png](/assets/imgs/quick-start8.png)

8. 客户端透过gatewayacting与websocketServe握手

客户端无法直接与该Microservice application进行握手,mustpassgatewayquote该微Serveof **applicationinterface层** ,pass[gateway](#ConstructSilky微Servegateway)of提供ofwebsocketactingServe与该微Serve进行握手,pass`ws[s]://gateway_ip[:gateway_port]/websocket_webapi`with the previous definitionwebsocketServe进行会话。

我们existConstructofgatewayapplication中quote该微Serveof**applicationinterface层**,并启动gatewayapplication(gatewayServeaddress is`127.0.0.1:5000`),并可pass地址:`ws://127.0.0.1:5000/api/test`with the previous definitionofwebsocketServe进行握手and通信。

client withwebsocketServe进行握手时,需要pass`qstringparameter`or请求头设置`hashkey`，确保每次communicationMicroservice application都是同一个实例。

![quick-start9.png](/assets/imgs/quick-start9.png)

![quick-start10.png](/assets/imgs/quick-start10.png)

## ConstructSilky微Servegateway

Actually,[pass.netPlatform providesWebhost](#useWebHost builds microservice applications)to buildsilkyMicroservice application，也可以认为是一个gateway。我们exist这里专门Constructofgateway与[pass.netPlatform providesWeb host](#useWebHost builds microservice applications)of区别exist于该kind型ofMicroservice application只能作为Serve消费者,cannot act asRPCServe提供者。

总of来说,gateway是对Microservice application集群来说是一个对接外部of流量入口。

Construct过程与[pass.netPlatform providesWeb host](#useWebHost builds microservice applications)consistent,我们只需要Willcreatehostofmethod修改为:

```csharp
 private static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureSilkyGatewayDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); });
```

gatewayprojectpassquote其他Microservice applicationof**applicationinterface层**，就可以作为Serve消费者passSilkyRPCframe调用其他Microservice application提供ofServe,并且passgateway提供ofhttp相关middleware可以实现generateexist线swaggerDocumentation,实现统一ofapiAuthentication,httpLimiting,generatedashboardmanagement side,实现对微Serve集群Serve提供者实例of健康检查等功能。