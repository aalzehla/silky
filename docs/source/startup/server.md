---
title: Silkyservice host
lang: zh-cn
---

## Silkyservice host的概念

existSilkyin the microservice framework,[host](host.html)For hosting microservice applications,exist微service host启动时,The most important task is to build the service provider,并WillServesupply者hostInformation以元数据的形式注册到 **service registration**,Each microservice application in the cluster can be **heartbeat** or **subscription** 的方式fromservice registration中心Obtain整个Microservices集群最新的元数据信息。

existSilkyin the frame,we pass [Server](https://github.com/liuhll/silky/blob/main/framework/src/Silky.Rpc/Runtime/Server/Server.cs) to defineSilkyhost(service provider)。The main properties of a microservice provider are shown in the following table:

| property name | name  | Remark           |
|:-------|:------|:--------------|
| HostName | 微service hostname  |  等于application程序的启动程序集的name    |
| Endpoints | Endpoints owned by the microservice provider  | exist构建support不同agreement service时,动态的添加该service host的endpoint   |
| Services | Service descriptors owned by the microservice provider  |    |

existservice registration过程中,cannot register directly`Server`Information,So we define **service host描述符** ,pass`service host描述符`[ServerDescriptor](https://github.com/liuhll/silky/blob/main/framework/src/Silky.Rpc/Runtime/Server/ServerDescriptor.cs) to describeSilkyhost信息,并pass其WillhostInformation注册到service registration中心,service host`Server`与service host描述符`ServerDescriptor`can be converted into each other。

## createSilkyservice host

Silkyservice host的解析Depend on默认hostServesupply者`DefaultServerProvider`负责create和维护;

```csharp
    public class DefaultServerProvider : IServerProvider
    {
        public ILogger<DefaultServerProvider> Logger { get; set; }
        private readonly IServer _server;
        private readonly IServiceManager _serviceManager;
        private readonly ISerializer _serializer;

        public DefaultServerProvider(IServiceManager serviceManager,
            ISerializer serializer)
        {
            _serviceManager = serviceManager;
            _serializer = serializer;
            Logger = EngineContext.Current.Resolve<ILogger<DefaultServerProvider>>();
            _server = new Server(EngineContext.Current.HostName);
        }

        public void AddRpcServices()
        {
            var rpcEndpoint = EndpointHelper.GetLocalRpcEndpoint();
            _server.Endpoints.Add(rpcEndpoint);
            var rpcServices = _serviceManager.GetLocalService(ServiceProtocol.Rpc);
            foreach (var rpcService in rpcServices)
            {
                _server.Services.Add(rpcService.ServiceDescriptor);
            }
        }

        public void AddHttpServices()
        {
            var webEndpoint = RpcEndpointHelper.GetLocalWebEndpoint();
            if (webEndpoint == null)
            {
                throw new SilkyException("Failed to obtain http service rpcEndpoint");
            }

            _server.Endpoints.Add(webEndpoint);
        }

        public void AddWsServices()
        {
            var wsEndpoint = RpcEndpointHelper.GetWsEndpoint();
            _server.Endpoints.Add(wsEndpoint);
            var wsServices = _serviceManager.GetLocalService(ServiceProtocol.Ws);
            foreach (var wsService in wsServices)
            {
                _server.Services.Add(wsService.ServiceDescriptor);
            }
        }

        public IServer GetServer()
        {
            Logger.LogDebug("server endpoints:" + _serializer.Serialize(_server.Endpoints.Select(p => p.ToString())));
            if (_server.HasHttpProtocolServiceEntry() && !_server.Endpoints.Any(p =>
                    p.ServiceProtocol == ServiceProtocol.Http || p.ServiceProtocol == ServiceProtocol.Https))
            {
                throw new SilkyException(
                    "A server that supports file upload and download or ActionResult must be built through the http protocol host",
                    StatusCode.ServerError);
            }
            return _server;
        }
    }
```

From the above code we can see that:

1. Depend onServerhostsupply者的构造器create`Server`host;

2. Serverhostsupply者的构造器中注入Serve管理器`IServiceManager`,Depend on此,We can also know:existapplication启动时Obtainhostsupply者的时候,Achieved[Resolution of Services and Service Entries](service-serviceentry.html);

3. hostServesupply者`DefaultServerProvider`Provides three core methods`AddRpcServices()`、`AddHttpServices()`、`AddWsServices()`; existapplication启动时,exist指定的时刻查找指定agreement service和相应serviceendpoint;
  
  3.1 Depend on[webhost](https://learn.microsoft.com/zh-cn/aspnet/core/fundamentals/host/web-host?view=aspnetcore-6.0)create的SilkyMicroservice application,mapSilky路Depend on的时候,transfer`AddHttpServices()`method,existapplication启动成功时,添加该Microservice application的Httpendpoint;
  
  ```csharp
  public static class SilkyEndpointRouteBuilderExtensions
  {
        public static ServiceEntryEndpointConventionBuilder MapSilkyRpcServices(this IEndpointRouteBuilder endpoints)
        {
            if (endpoints == null)
            {
                throw new ArgumentNullException(nameof(endpoints));
            }

            var hostApplicationLifetime = endpoints.ServiceProvider.GetRequiredService<IHostApplicationLifetime>();
            // existapplication启动后注册RegisterSilkyWebServer()method
            hostApplicationLifetime.ApplicationStarted.Register(async () =>
            {
                // Sign up for supportWebServicesSilkyMicroservices
                await RegisterSilkyWebServer(endpoints.ServiceProvider);
            });
            return GetOrCreateServiceEntryDataSource(endpoints).DefaultBuilder;
        }

        private static async Task RegisterSilkyWebServer(IServiceProvider serviceProvider)
        {
            // ObtainhostServesupply者实例
            var serverRegisterProvider =
                serviceProvider.GetRequiredService<IServerProvider>();

            serverRegisterProvider.AddHttpServices();
        }      
  }
  ``` 

```csharp

public class DefaultServerProvider : IServerProvider
{
    public void AddHttpServices()
    {
        // Obtain http endpoint
        var webEndpoint = RpcEndpointHelper.GetLocalWebEndpoint();
        if (webEndpoint == null) // Obtain失败则抛出异常
        {
            throw new SilkyException("Failed to obtain http service rpcEndpoint");
        }
        // Willhttp endpoint添加servicesupply者的endpoint列表中
        _server.Endpoints.Add(webEndpoint);
    }

   // other codes...
}

```

  From the above code we can see,only usewebhost构建(hosting)application的host，existServe启动过程中才会有Willhttpendpoint添加到silkyServesupply者的endpoint列表中；SilkyServe内部之间的communication是Depend ondotnettyaccomplishrpcframe,httpendpoint的用途是supply了对Serve外部访问的入口;

::: tip Remark

如果是Depend on[Webhost](https://learn.microsoft.com/zh-cn/aspnet/core/fundamentals/host/web-host?view=aspnetcore-6.0) hosting的Silkyapplication,那么existexist此时才会首次Obtain`DefaultServerProvider`instance of,也就是exist此时才会进行Serve与Serve条目的解析;
:::

  3.2  exist模块`DotNettyTcpModule`in the process of initializing the task,fromIoc容器中Obtain到消息监听者`DotNettyTcpServerMessageListener`after instance,After completing the monitoring task，add supportRPC
  agreement service;
  
  ```csharp
   [DependsOn(typeof(RpcModule), typeof(DotNettyModule))]
    public class DotNettyTcpModule : SilkyModule
    {
        // other codes...
    
        public override async Task Initialize(ApplicationContext applicationContext)
        {
            //Obtain消息监听者实例
            var messageListener =
                applicationContext.ServiceProvider.GetRequiredService<DotNettyTcpServerMessageListener>();
            // Complete the message monitoring task
            await messageListener.Listen();
            // ObtainsilkyhostServesupply者实例
            var serverProvider =
                applicationContext.ServiceProvider.GetRequiredService<IServerProvider>();
            // add supportTCPagreement service
            serverProvider.AddRpcServices();
        }
    }
  ```

  pass上面的代码我们看到,只有exist完成Serve端消息监听任务之后,Silkyservice host才会完成add supportRPCagreement service,supportRPCservices are as described above[applicationServe](service-serviceentry.html#applicationServices解析);SilkyMicroservices之间的communication主要是Depend ondotnettyaccomplish的RPCframe完成的。
  
  ```csharp
  public class DefaultServerProvider : IServerProvider
  {
    public void AddRpcServices()
    {
        var rpcEndpoint = EndpointHelper.GetLocalRpcEndpoint();
        _server.Endpoints.Add(rpcEndpoint);
        var rpcServices = _serviceManager.GetLocalService(ServiceProtocol.Rpc);
        foreach (var rpcService in rpcServices)
        {
            _server.Services.Add(rpcService.ServiceDescriptor);
        }
    }

   // other codes...
  }
  ```

::: tip Remark
如果是Depend on[通用host](https://learn.microsoft.com/zh-cn/aspnet/core/fundamentals/host/generic-host?view=aspnetcore-6.0) hosting的Silkyapplication,那么existexist此时才会首次Obtain`DefaultServerProvider`instance of,也就是exist此时才会进行Serve与Serve条目的解析;
:::

   3.3  exist模块`WebSocketModule`in the process of initializing the task,查找到所有support`WebSocket`service,并pass`WebSocketServerBootstrap`instance of完成createwsServe，这些ServeWill会supply`WebSocket`Serve,After the task is completed,WillpassSilkyhostServesupply者`DefaultServerProvider`instance of添加对`wsServe`;
 
   ```csharp
    [DependsOn(typeof(RpcModule))]
    public class WebSocketModule : SilkyModule
    {

        // other codes...

        public override async Task Initialize(ApplicationContext applicationContext)
        {
            var typeFinder = applicationContext.ServiceProvider.GetRequiredService<ITypeFinder>();
            var webSocketServices = GetWebSocketServices(typeFinder);
            var webSocketServerBootstrap =
                applicationContext.ServiceProvider.GetRequiredService<WebSocketServerBootstrap>();
            webSocketServerBootstrap.Initialize(webSocketServices);
            var serverProvider =
                applicationContext.ServiceProvider.GetRequiredService<IServerProvider>();

            serverProvider.AddWsServices();
        }

        private (Type, string)[] GetWebSocketServices(ITypeFinder typeFinder)
        {
            var wsServicesTypes = ServiceHelper.FindServiceLocalWsTypes(typeFinder);
            return wsServicesTypes.Select(p => (p, WebSocketResolverHelper.ParseWsPath(p))).ToArray();
        }
    }
   ```

```csharp
public class DefaultServerProvider : IServerProvider
{
    public void AddWsServices()
    {
        var wsEndpoint = RpcEndpointHelper.GetWsEndpoint();
        _server.Endpoints.Add(wsEndpoint);
        var wsServices = _serviceManager.GetLocalService(ServiceProtocol.Ws);
        foreach (var wsService in wsServices)
        {
            _server.Services.Add(wsService.ServiceDescriptor);
        }
    }

 // other codes...
}
```

websocketServe是如何解析,如何createsupportwebsocketservice这个我们Will会exist之后的文档中介绍;

::: tip Remark

1. only depend on`WebSocketModule`modularSilkyapplication,才supportsupply`WebSocket`Serve,supply`WebSocket`Serve必须要求继承`Silky.WebSocket.WsAppServiceBase`；

2. silkyframe的websocket是pass网关accomplish代理的,pass代理再与具体的SilkyapplicationServesupply者进行连接;

3. websocketServe是Depend onframe[websocket-sharp-core](https://github.com/ImoutoChan/websocket-sharp-core)supply的；

4. websocketServesupply的method也会被解析为Serve条目,也可以与其他Microservices实例accomplishRPCcommunication;
:::