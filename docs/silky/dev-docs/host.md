---
title: host
lang: zh-cn
---

## hostof概念

silkyofhostand.netof[host](https://docs.microsoft.com/zh-cn/aspnet/core/fundamentals/host/generic-host?view=aspnetcore-3.1)Conceptual agreement。是封装应用资源of对象,用于托管应用和管理应用of生命周期。

### 通用host

如果用于托管普通of业务应用,The microservice module itself does not need to provide access to the outside of the cluster directly。So,you can use it[.netof通用host](https://docs.microsoft.com/zh-cn/dotnet/core/extensions/generic-host)registersilkyservice framework。.netof通用host无法supplyhttpask,also cannot be configuredhttpofask管道(which is:**middleware**)。

existregistersilkyafter the frame,silkyframe会registerdotnettyofServe监听者,and will exposerpcThe port number。But due tosilkyframeof安全机制,Outside the cluster is not allowed to pass`tcp`Agreement passedrpcThe port number直接访问该微Serve模块of应用接口。

### webhost

如果您需要访问该Serve模块of应用接口,you must pass.netof[webhost](https://docs.microsoft.com/zh-cn/aspnet/core/fundamentals/host/web-host?view=aspnetcore-3.1)registersilkyframe,and configuresilkyframeofask管道。so,web构建ofhostpass引用某个微Serveof应用接口项目(Bag)，pass应用接口of代理and微Serve集群内部实现rpccommunication。

## 业务hostkind型

silky微service frameworksupply了多种kind型of业务host,开发者可以选择合适ofhost来托管应用Serve。

### usewebhost构建微Serve应用

usewebhost构建ofsilkyThe application has the following characteristics:

1. supplyhttpservice andRPCServe,exposedhttpport andRPCport
2. 可以as外部流量of入口,pass outside the clusterhttpServe访问微Serve应用集群
3. asRPCServesupply者,passRPCframeand其他微Serve进行communication

![host0.png](/assets/imgs/host0.png)


normally,如果我们希望该Serve应用既可以asRPCServesupply者,也希望外部能够直接passhttpProtocol access application,So我们就可以passwebhost构建微Serve应用。soof方式适用于Will微Serve应用拆分给不同of团队进行开发,开发者也无需要额外ofBuild a gateway,就可以访问微Serve应用Serve。

usewebhost构建Silky微Serve应用只需要开发者安装`Silky.Agent.Host`Bag后,exist`Main()`方法中pass`Host`supplyofAPI`ConfigureSilkyWebHostDefaults`which is可。The developer needs to specify`Startup`kind,exist`Startup`中registerservice andconfigurehttpmiddleware。

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

certainly,我们也可以exist构建hostof时候,Also specify the startup module:

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
                    .ConfigureSilkyWebHost<DemoModule>(webBuilder => webBuilder.UseStartup<Startup>());
               
        }
    }
}
```

自定义of启动模块`DemoModule`need to inherit`WebHostModule`,开发者可以exist自定义of启动模块中,定义应用启动和停止需要执行of业务方法和configureServeregister,也可以依赖开发者扩展of自定义模块。

```csharp
    // 依赖开发者自定义of模块
    // [DependsOn(typeof("UserDefinedModule"))]
    public class DemoModule : WebHostModule
    {
        public override Task Initialize(ApplicationContext applicationContext)
        {
            // 开发者可以定义应用程序启动时执行of业务方法
            return Task.CompletedTask;
        }

        public override Task Shutdown(ApplicationContext applicationContext)
        {
            // 开发者可以定义应用程序停止时执行of业务方法
            return Task.CompletedTask;d
        }

        public override void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            // 开发者可以configureServeregister,role andStartupkindConfigureServicesconsistent
            
        }

        protected override void RegisterServices(ContainerBuilder builder)
        {
            // 开发者可以pass Autofac ofContainerBuilderregisterServe,
            // E.g: IServiceCollection无法register命名Serve,ContainerBuilder支持register命名Serve
        }
    }
```

exist启动kind`Startup`kind中configureServeregister和middleware:

```csharp
 public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services) 
        {
            services
                .AddSilkyHttpCore()
                .AddResponseCaching()
                .AddHttpContextAccessor()
                .AddRouting()
                .AddSilkyIdentity()
                .AddSilkyMiniProfiler()
                .AddSwaggerDocuments();
            
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwaggerDocuments();
                app.UseMiniProfiler();
            }
            app.UseRouting();
            app.UseResponseCaching();
            app.UseSilkyWebSocketsProxy();
            app.UseSilkyIdentity();
            app.UseSilkyHttpServer();           
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapSilkyRpcServices();
            });
        }
    }
```

so,我们就可以得到一个既可以supplyhttpServe,也asrpcServesupply者of应用。

### use通用host构建微Serve应用

use通用host构建ofsilkyThe application has the following characteristics:

1. 只supplyRPCServe,不supplyhttpServe,微Serve集群外部无法直接访问应用
2. 可以pass网关或是havehttpServeof应用间接of访问该微ServesupplyofServe

![host1.png](/assets/imgs/host1.png)

normally,如果只是as普通of业务应用,只需要asRPCServesupply者,Serve内部passRPCframe进行communication,并不需要对外supplyhttpServe,existsoof情况下,我们考虑use通用host构建微Serve应用。

开发者exist安装`Silky.Agent.Host`Bag后,exist`Main()`方法中pass`Host`supplyofAPI`ConfigureSilkyGeneralHostDefaults`which is可pass通用host构建silky微Serve应用。

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
                .ConfigureSilkyGeneralHostDefaults();
    }
}
```

Similarly,我们也可以exist构建hostof时候,Also specify the startup module:

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
                .ConfigureSilkyGeneralHost<DemoModule>();
    }
}
```

existhere,我们需要自定义of启动模块`DemoModule`need to inherit`GeneralHostModule`,开发者可以exist自定义of启动模块中,定义应用启动和停止需要执行of业务方法和configureServeregister,也可以依赖开发者扩展of自定义模块。

```csharp
  // [DependsOn(typeof("UserDefinedModule"))]
    public class DemoModule : GeneralHostModule
    {
        public override Task Initialize(ApplicationContext applicationContext)
        {
            // 开发者可以定义应用程序启动时执行of业务方法
            return Task.CompletedTask;
        }

        public override Task Shutdown(ApplicationContext applicationContext)
        {
            // 开发者可以定义应用程序停止时执行of业务方法
            return Task.CompletedTask;
        }

        public override void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            // 开发者可以configureServeregister,role andStartupkindConfigureServicesconsistent
            
        }

        protected override void RegisterServices(ContainerBuilder builder)
        {
            // 开发者可以pass Autofac ofContainerBuilderregisterServe,
            // E.g: IServiceCollection无法register命名Serve,ContainerBuilder支持register命名Serve
        }
    }
```

::: warning Notice

andwebhost构建微Serve应用自定义启动模块继承of基kind不同,but作用和use上consistent

:::

通用host构建of微Serve应用,不supplyHTTPServe,all without(there is no need)configurehttpmiddleware。but,开发者可以pass继承`IConfigureService`来configureServeofregister,从而自身Serveregister,Or introduce third-party components。

```csharp
    public class ConfigureService : IConfigureService
    {
        public void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddSilkyCaching()
                .AddSilkySkyApm()
                .AddMessagePackCodec();
             
            services.AddDatabaseAccessor(
                options => { options.AddDbPool<DefaultContext>(); },
                "Demo.Database.Migrations");

             // 可以passServeregister引入第三方组件,E.g:CAP,MassTransitWait
        }
    }
```

### build withwebsocket能力of微Serve应用

havewebsocketServe能力of微Serve应用除了能够supplyRPCServe,还可以supplywebsocketServe。

1. supplyRPCServe,也supplyWebSocketServe
2. 可以pass网关ofwebsocket代理middlewareand该微ServeofwebsocketServe进行握手

![host2.png](/assets/imgs/host2.png)

开发者exist安装`Silky.Agent.Host`Bag后,exist`Main()`方法中pass`Host`supplyofAPI`ConfigureSilkyGeneralHostDefaults`which is可pass通用host构建支持websocketServeof微Serve应用。

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

Similarly,我们也可以exist构建hostof时候,Also specify the startup module:

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
                .ConfigureSilkyWebSocket<DemoModule>();
    }
}
```

existhere,我们需要自定义of启动模块`DemoModule`need to inherit`WebSocketHostModule`,开发者可以exist自定义of启动模块中,定义应用启动和停止需要执行of业务方法和configureServeregister,也可以依赖开发者扩展of自定义模块。

```csharp
// [DependsOn(typeof("UserDefinedModule"))]
    public class DemoModule : WebSocketHostModule
    {
        public override Task Initialize(ApplicationContext applicationContext)
        {
            // 开发者可以定义应用程序启动时执行of业务方法
            return Task.CompletedTask;
        }

        public override Task Shutdown(ApplicationContext applicationContext)
        {
            // 开发者可以定义应用程序停止时执行of业务方法
            return Task.CompletedTask;
        }

        public override void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            // 开发者可以configureServeregister,role andStartupkindConfigureServicesconsistent
            
        }

        protected override void RegisterServices(ContainerBuilder builder)
        {
            // 开发者可以pass Autofac ofContainerBuilderregisterServe,
            // E.g: IServiceCollection无法register命名Serve,ContainerBuilder支持register命名Serve
        }
    }
```

::: warning Notice

andwebhost构建微Serve应用自定义启动模块继承of基kind不同,but作用和use上consistent

:::

build withwebsocket能力ofServe,实现应用Serve接口ofkindneed to inherit`WsAppServiceBase`。existand前端建立会话后,就可以pass`SessionManager`Send a message to the front end。


```csharp
    public class TestAppService : WsAppServiceBase, ITestAppService
    {
        private readonly ILogger<TestAppService> _logger;

        public TestAppService(ILogger<TestAppService> logger)
        {
            _logger = logger;
        }

        protected override void OnOpen()
        {
            base.OnOpen();
            _logger.LogInformation("websocket established a session");
            
        }

        protected override void OnMessage(MessageEventArgs e)
        {
            _logger.LogInformation(e.Data);
        }

        protected override void OnClose(CloseEventArgs e)
        {
            base.OnClose(e);
            _logger.LogInformation("websocket disconnected");
        }
    }
```

前端需要pass网关ofwebsocket代理middleware，and具体ofwebsocketServe实例建立会话时,The following requirements need to be met:

1. 需要passask头或是`qString`parameter specification`bussinessId`，pass该值use哈希算法,路由到具体ofwebsocketServe实例。
2. In order to ensure that each time can be routed to the samewebsocketServe实例,websocketServe对应of网关实例只能有一个。
3. The gateway must be referencedwebsocketServe代理middleware。

```csharp
// 需要exist网关ofConfigure()configurewebsocket代理middleware
app.UseSilkyWebSocketsProxy();
```

::: warning Notice

1. Developers can consider,普通业务Serve对应一组网关应用(Support for deploying multiple instances),websocketAn application corresponds to a set of gateway applications(只允许一个Serve实例)

:::

### Build a gateway

here,网关of作用只是as集群流量of入口,Willhttpask转发到集群内部,交个各个微Serve应用ofServe进行处理,并不asrpcServesupply者。That is to say,here构建of网关只能asServe消费者。

1. 只supplyhttpServe,as集群流量入口
2. 不supplyRPCServe,不可以asrpcServesupply者

![host3.png](/assets/imgs/host3.png)

::: warning Notice

网关andwebhost构建业务hostof区别exist于,网关只能asServe消费者,转发外部ofhttpask,而后者除了have转发httpaskof能力之外,还能asRPCServesupply者。

:::

开发者exist安装`Silky.Agent.Host`Bag后,exist`Main()`方法中pass`Host`supplyofAPI`ConfigureSilkyGatewayDefaults`which is可pass通用host构建支持websocketServeof微Serve应用。

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
                .ConfigureSilkyGatewayDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); });
    }
}
```

certainly,You can also customize the startup module,只需要Will自定义of启动模块`DemoModule`继承of基kind修改为`GatewayHostModule`:

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
                .ConfigureSilkyGateway<DemoModule>(webBuilder => { webBuilder.UseStartup<Startup>(); });
    }
}
```
