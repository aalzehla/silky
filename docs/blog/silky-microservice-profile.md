---
title: silkyIntroduction to Microservices
lang: zh-cn
---

## proxy host

silky微Serve定义了三种kind型ofproxy host,Developers can choose the appropriatesilkyproxy host托管微Serve应用。proxy host定义了一个`Startup`module，该module给出了use该种kind型主机所必须依赖ofmodule。

### 通用proxy host

This type of host is generally used to host business applications,between servicesrpccommunicate,不支持and微Serve集群and外部communicate,webproxy host可以pass引用该kind型of微Serveof应用接口,pass应用接口Generatedagent with该微Servecommunicate。This type of microservice uses.netGeneric host for managed references。Defined`Startup`moduleAs follows:

```csharp
  [DependsOn(typeof(ZookeeperModule),
        typeof(DotNettyTcpModule),
        typeof(RpcProxyModule),
        typeof(TransactionTccModule),
        typeof(ValidationModule),
        typeof(FluentValidationModule),
        typeof(RedisCachingModule),
        typeof(TransactionRepositoryRedisModule)
    )]
    public abstract class GeneralHostModule : StartUpModule
    {
    }
```

If the developer needs to create a microservice application,Just create a console application,passnugetPackage management tool installation`Silky.Agent.GeneralHost`Bag,register in the main function`SilkyServices`,and specify启动moduleJust。

```csharp

   public static async Task Main(string[] args)
   {
       await CreateHostBuilder(args).Build().RunAsync();
   }

    private static IHostBuilder CreateHostBuilder(string[] args)
    {
        return Host.CreateDefaultBuilder(args)
            .RegisterSilkyServices<AccountHostModule>()
        ;
    }

```

开发者pass继承`GeneralHostModule`module定义启动module。可以pass`DependsOn`依赖自DefinedmoduleorSilky提供ofmodule。

启动moduleAs follows:

```csharp
// 
//  [DependsOn(typeof(SilkySkyApmAgentModule),
//         typeof(JwtModule),
//         typeof(MessagePackModule))]
public class AccountHostModule : GeneralHostModule
{

}
```

### webproxy host

该kind型of主机可以passhttp端口and外部passhttp协议communicate,pass引用其他业务微Serve应用of应用接口,Generated from a routing templaterestfulStylewebapi,开发者可以passconfigure生成exist线of`swagger`Documentation。Intuitively see onlineapiDocumentationandconduct调试。GeneratedswaggerDocumentation可以according to引用of应用接口conduct分组。

![silky-ms1.png](/assets/imgs/silky-ms1.png)


webproxy host预Defined`Startup`module指定了webproxy host必须依赖ofsilkymodule,As follows:

```csharp
    [DependsOn(typeof(RpcProxyModule),
       typeof(ZookeeperModule),
       typeof(SilkyHttpCoreModule),
       typeof(DotNettyModule),
       typeof(ValidationModule),
       typeof(FluentValidationModule),
       typeof(RedisCachingModule)
   )]
    public abstract class WebHostModule : StartUpModule
    {

    }
```

This type of host is generally used for gateways，provided外部and微Serve集群communicateof桥梁,This type of host uses.netofwebhost to host the application。Developers can create aaspnetcoreproject,passInstall`Silky.Agent.WebHost`BagJust创建webproxy host，需要同时指定启动moduleand`Startup`kind。

```csharp
    public async static Task Main(string[] args)
    {
        await CreateHostBuilder(args).Build().RunAsync();
    }

    public static IHostBuilder CreateHostBuilder(string[] args)
    {
        var hostBuilder = Host.CreateDefaultBuilder(args)
            .RegisterSilkyServices<GatewayHostModule>()
            .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); });

        return hostBuilder;
    }
```

webproxy hostof启动module需要继承`WebHostModule`，启动module`GatewayHostModule`As follows:

```csharp
public class GatewayHostModule : WebHostModule
{
    
}
```

need in`Startup`kind注册`Silky`request pipeline，`Startup`kindAs follows:

```csharp
public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
        
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment() || env.EnvironmentName == "ContainerDev")
        {
            app.UseDeveloperExceptionPage();
        }
        app.ConfigureSilkyRequestPipeline();
    }
}
```

### websocketproxy host

websocketproxy hostand通用proxy host基本一致,websocketproxy host具体提供wsServeof能力,web主机可以passwsagent withwebsocketproxy hostofwsServecommunicate。

websocketproxy hostof启动moduleAs follows:

```csharp
    [DependsOn(typeof(ZookeeperModule),
        typeof(DotNettyTcpModule),
        typeof(RpcProxyModule),
        typeof(TransactionTccModule),
        typeof(WebSocketModule),
        typeof(ValidationModule),
        typeof(FluentValidationModule),
        typeof(RedisCachingModule),
        typeof(TransactionRepositoryRedisModule)
        )]
    public abstract class WebSocketHostModule : StartUpModule
    {
    }
```

开发者可以pass`WebSocket`Configure Node Pairswsservice to configure,wsServeof默认端口for`3000`，But generally,andwsWhen the service establishes the handshake,不应该andwsThe service handshakes directly,而是应该passwebproxy hostof代理middle间件conduct握手，So if the developer useswsServe,must be inwebproxy hostInstall`Silky.WebSocket.Middleware`。

wsServeof创建and通用proxy hostof创建一致,只需要Will启动module继承of基kind修改for`WebSocketHostModule`Just。

wsServeof定义如下:

```csharp
    public class WsTestAppService : WsAppServiceBase, IWsTestAppService
    {
        public async Task Echo(string businessId, string msg)
        {
            if (BusinessSessionIds.TryGetValue(businessId, out var sessionIds))
            {
                foreach (var sessionId in sessionIds)
                {
                    SessionManager.SendTo($"message:{msg},sessionId:{sessionId}", sessionId);
                }
            }
            else
            {
                throw new BusinessException($"does not existbusinessIdfor{businessId}of会话");
            }
        }
    }
```

需要注意of时,During the establishment of the handshake,must be specified`hashkey`从而保证每次回话of微Serve实例都是同一个,more aboutwsServe[please check](#)。

## Distributed transaction

silky微Serveuse拦截器andfilterAchievedTCCDistributed transaction,existtccDistributed transaction过程middle,Will事务参and者oftransferparameter作forundologLogs are saved to the data warehouse(when前Achievedredis作forundo日志of数据存储器),并exist后台执行作业检查Distributed transactionof执行情况,从而保证数据of最终一致性。

### Distributed transactionofuse

1. exist应用接口middlepass`[Transaction]`Characteristics to identify the interface is a distributed application method。

```csharp
[Transaction]
Task<GetOrderOutput> Create(CreateOrderInput input);
```

2. 应用Serveaccomplishpass`[TccTransaction]`Feature ID,and specify`ConfirmMethod`Methods and`CancelMethod`,指定accomplishof`ConfirmMethod`Methods and`CancelMethod`必须for`public`，methodparameterand应用accomplishmethodof保持一致。trymethod if required to`ConfirmMethod`Methods and`CancelMethod`传递parameterpass`RpcContext.Context`conduct。

```csharp
        [TccTransaction(ConfirmMethod = "OrderCreateConfirm", CancelMethod = "OrderCreateCancel")]
        [UnitOfWork]
        public async Task<GetOrderOutput> Create(CreateOrderInput input)
        {
            var orderOutput = await _orderDomainService.Create(input);
            return orderOutput;
        }

        [UnitOfWork]
        public async Task<GetOrderOutput> OrderCreateConfirm(CreateOrderInput input)
        {
            var orderId = RpcContext.Context.GetAttachment("orderId");
            var order = await _orderDomainService.GetById(orderId.To<long>());
            order.Status = OrderStatus.Payed;
            order.UpdateTime = DateTime.Now;
            order = await _orderDomainService.Update(order);
            return order.Adapt<GetOrderOutput>();
        }
        
        [UnitOfWork]
        public async Task OrderCreateCancel(CreateOrderInput input)
        {
            var orderId = RpcContext.Context.GetAttachment("orderId");
            if (orderId != null)
            {
                // await _orderDomainService.Delete(orderId.To<long>());
                var order = await _orderDomainService.GetById(orderId.To<long>());
                order.Status = OrderStatus.UnPay;
                await _orderDomainService.Update(order);
            }
        }
```

## Serve定义andRPCcommunication

### 应用接口of定义

silkyofServe定义非常简单,exist这里ofServe指of是应用Serve,and传统MVCof`Controller`of概念一致。

您只need in一个业务微Serve应用middle,Added application interface layer,normally,我们可以命名for`Project.IApplication`or`Project.Application.Contracts`,and add application interface,exist应用接口middlepass`[ServiceRoute]`特性conduct标识,并exist`Project.Application`projectmiddleaccomplishthe interface。

您可以pass`[ServiceRoute]`指定该应用Serveof路由模板, and whether multiple implementations are allowed。

E.g:

```csharp

namespace Silky.Account.Application.Contracts.Accounts
{
    /// <summary>
    /// 账号Serve
    /// </summary>
    [ServiceRoute]
    public interface IAccountAppService
    {
        /// <summary>
        /// Add account
        /// </summary>
        /// <param name="input">account information</param>
        /// <returns></returns>
        Task<GetAccountOutput> Create(CreateAccountInput input);
    }
}

```
exist应用层middleaccomplishthe interface:

```csharp
namespace Silky.Account.Application.Accounts
{
    public class AccountAppService : IAccountAppService
    {
        private readonly IAccountDomainService _accountDomainService;


        public AccountAppService(IAccountDomainService accountDomainService)
        {
            _accountDomainService = accountDomainService;
        }

        public async Task<GetAccountOutput> Create(CreateAccountInput input)
        {
            var account = await _accountDomainService.Create(input);
            return account.Adapt<GetAccountOutput>();
        }
    }
}
```

### RPCcommunication

应用接口可以被其他微Serve应用or被网关应用引用。其他微Serve可以pass应用接口生成代理,并pass内部accomplishofrpc框架and该微Servecommunicate。silkyofrpccommunication支持tcc方式ofDistributed transaction，See the previous section for details。

The application interface is referenced by the gateway,web host主机可以pass该应用Serve接口生成相应ofwebapi,and can generateswaggerexist线Documentation。passhttpask,从而accomplishServeand集群外部communicate,whenhttpask到达webhostafter the host,silkymiddle间件passwebapiandaskmethod路由到Serve条目,然后pass内部accomplishofrpccommunicationand微Serve应用communicate。

**RPCoffilter**: rpccommunication支持两种kind型offilter,exist客户端发起ask过程middle,will be called sequentially开发者Defined`IClientFilter`filter,Serve端收到ask后,will be called sequentially`IServerFilter`Then execute the apply method itself。

**RpcContext**: 可以pass`RpcContext.Context`添加or获取本次rpctransferof`Attachments`parameter。when然,开发者也可以pass注入`IRpcContextAccessor`获取本次communicationof上线文parameter`RpcContext`。

**获取when前登录用户**: 开发者可以pass`NullSession.Instance`获取when前登录用户,If you are already logged in to the system,那么passthe interface可以获取到when前登录用户of`userId`、`userName`other information。


## Serve治理

针right每个Serve条目(应用Serve接口method),都AchievedServe治理,开发者可以pass`governance`or`[Governance()]`特性rightServeof最大并发量、load balancing algorithm、execution timeout、Whether to use cache interception、failure callback interface、接口是否right外网屏蔽等等Attributesconductconfigure。

以下描述了以Serve条目for治理unitofAttributes表单：

| Attributes | illustrate | Default value |  Remark |
|:-----|:-----|:-----|:------|
| AddressSelectorMode | load balancing strategy | Polling(polling) | load balancing algorithm支持：Polling(polling)、Random(random)、HashAlgorithm(hash consistency，according torpcparameterof第一个parameter值) |
| ExecutionTimeout | execution timeout | 3000(ms) | unitfor(ms),Fuse on timeout，-1表示existrpccommunication过程middle不会超时 |
| CacheEnabled | Whether to enable cache interception | true | rpccommunicationmiddleWhether to enable cache interception |
| MaxConcurrent | 允许of最大并发量 | 100 |  |
| FuseProtection | Whether to open the fuse protection  | true |  |
| FuseSleepDuration | fuse sleep duration  | 60(s) | After a fuse,多少时长后再次重试该Serve实例 |
| FuseTimes | Serve提供者允许of熔断次数  | 3 | Serve实例连续nfuse terminal,Serve实例Will被标识for不健康 |
| FailoverCount | Failover times  | 0 | rpccommunication异常情况下,允许of重新路由Serve实例of次数,0表示有几个Serve实例就转移几次 |
| ProhibitExtranet | Whether to prohibit external network access  | false | 该Attributes只允许pass`GovernanceAttribute`特性conduct设置 |
| FallBackType | 失败回调指定ofkind型  | null | kind型for`Type`,如果指定了失败回调kind型,那么existServe执行失败,则会执行该kind型of`Invoke`method,该kind型,must inherit`IFallbackInvoker`the interface |

开发者还可以pass`[Governance()]`特性right某个Servemethodconduct标识,被该Feature IDof治理AttributesWill覆盖微Serveofconfigure/Default value。

## cache interception

for提高应用of响应,silky支持cache interception。cache interceptionexist应用Serve接口method上passcache interception特性conduct设置,existsilkyin the frame,存exist如下三middlekind型of缓存特性，分别right数据缓存conduct新增、renew、delete。

1. Set cache properties--`GetCachingInterceptAttribute`

2. renew缓存特性--`UpdateCachingInterceptAttribute`

3. delete缓存特性--`RemoveCachingInterceptAttribute`

usecache interception,Cache coherence must be guaranteed。existrpccommunication过程middle,usecache interception,同一数据of缓存依据可能会不同(设置of`KeyTemplate`,E.g:缓存依据可能会according to`Id`、`Name`、`Code`分别conduct缓存),从而产生不同of缓存数据,但是existright数据conductrenew、delete操作时,由于无法pass`RemoveCachingInterceptAttribute`特性一次性delete该kind型数据of所有缓存数据,at this time，existaccomplish业务code过程middle,就需要pass分布式缓存接口`IDistributedCache<T>`accomplish缓存数据ofrenew、delete操作。

## Serve注册middle心

silkyusezookeeper作for默认Serveof注册middle心。when前还未扩展支持其他ofServe注册middle心。

silky支持for微Serve集群configure多个Serve注册middle心，您只need inconfigureServe注册middle心of链接字符串`registrycenter:connectionStrings`middle,use分号`;`就可以指定微Serve框架of多个Serve注册middle心。

for微ServeconfigureServe注册middle心As follows:

```shell
registrycenter: // Serve注册middle心configure节点
  connectionStrings: 127.0.0.1:2181,127.0.0.1:2182,127.0.0.1:2183;127.0.0.1:2184,127.0.0.1:2185,127.0.0.1:2186 // Serveconfiguremiddle心链接
  registryCenterType: Zookeeper // 注册middle心kind型
  connectionTimeout: 1000 // link timeout(unit:ms)
  sessionTimeout: 2000 // session timeout(unit:ms)
  operatingTimeout: 4000 // Operation timeout(unit:ms)
  routePath: /services/serviceroutes
```

## module化管理

silky框架存exist两种kind型ofmodule:

1. 开发者pass继承`SilkyModule`就可以定义一个普通modulekind;
2. 也可以pass继承`StartUpModule`定义一个Serve注册启动modulekind;开发者也可以pass继承`StartUpModule`,选择合适of依赖Bag,accomplish自己ofproxy host。

**moduleof依赖关系:** silky框架ofmodulepass`DependsOn`特性指定moduleof依赖关系,silky框架支持pass直接or间接of依赖module。

## dependency injection(Serve注册and解析)

1. pass继承dependency injection标识接口accomplishServeof注册**(recommend)**
silkyframework provides三个依赖注册of相关标识接口：`ISingletonDependency`(singleton pattern)、`IScopedDependency`(Regional mode)、`ITransientDependency`(Transient mode)。exist微Serve应用启动时,会扫描继承了这些标识接口ofkind(Serve),and will其自身and继承of接口注册到Ioccontainermiddle。

2. 定义module,并existmodulemiddlepass`RegisterServices()`methodof`ContainerBuilder`注册Serve(autofac),orpass`ConfigureServices()`methodof`IServiceCollection`注册Serve(微软官方自带ofioccontainer)

3. pass继承`IConfigureService`or`ISilkyStartup`,pass`Configure()`methodof`IServiceCollection`注册Serve

Silky因for支持pass`IServiceCollection`conductServe注册,所以可以很方便ofand第三方Serve(components)conduct整合,E.g:`CAP`or`MassTransit`Equal Distributed Event Framework。


## useSerilog作for日志记录器

silkyframework provides`serilog`作for日志记录器。只need in构建主机时,Increase`UseSerilogDefault()`,and add`Serilog`相关configureJust。

code:

```csharp
public static async Task Main(string[] args)
{
    await CreateHostBuilder(args).Build().RunAsync();
}

private static IHostBuilder CreateHostBuilder(string[] args)
{
    var hostBuilder = Host.CreateDefaultBuilder(args)
        .RegisterSilkyServices<OrderHostModule>()
        .UseSerilogDefault();
    return hostBuilder;
}
```

configure:

```yml
serilog:
  minimumLevel:
    default: Information
    override:
      Microsoft: Warning
      Microsoft.Hosting.Lifetime: Information
      Silky: Debug
  writeTo:
    - name: File
      args:
        path: "./logs/log-.log"
        rollingInterval: Day
    - name: Console
      args:
        outputTemplate: "[{Timestamp:yyyy/MM/dd HH:mm:ss} {Level:u11}] {Message:lj} {NewLine}{Exception}{NewLine}"
        theme: "Serilog.Sinks.SystemConsole.Themes.SystemConsoleTheme::Literate, Serilog.Sinks.Console"
```

## useMiniprofilerighthttpaskconduct性能监控

要求必须existweb主机project(一般for网关project)Install`Silky.Http.MiniProfiler`Bag,and will`swaggerDocument:injectMiniProfiler`configure项ofAttributes设置for`true`。

```yml
swaggerDocument:
  injectMiniProfiler: true
```
![silky-ms2.png](/assets/imgs/silky-ms2.png)


## useskywalkingView link trace

要求微Serveexist启动module依赖`SilkySkyApmAgentModule`module,并configure`skyWalking`相关Attributes。

```csharp
 [DependsOn(typeof(SilkySkyApmAgentModule))]
public class AccountHostModule : GeneralHostModule
{
}
```

```yaml
skyWalking:
  serviceName: AccountHost
  headerVersions:
    - sw8
  sampling:
    samplePer3Secs: -1
    percentage: -1.0
  logging:
    level: Debug
    filePath: "logs/skyapm-{Date}.log"
  transport:
    interval: 3000
    protocolVersion: v8
    queueSize: 30000
    batchSize: 3000
    gRPC:
      servers: "127.0.0.1:11800"
      timeout: 10000
      connectTimeout: 10000
      reportTimeout: 600000
```
existsilkyof实例projectmiddle,provided`skyWalking`pass`docker-compose`快速启动ofServe编排文件`samples\docker-compose\infrastr\docker-compose.skywalking.yml`,Developers only need to enter`samples\docker-compose\infrastr`Table of contentsmiddle,pass如下命令Just开始of启动一个skyWalkingServe。

```shell
cd samples\docker-compose\infrastr
docker-compose -f docker-compose.skywalking.yml
```

Open`http://127.0.0.1:8180`Just看到微Serve集群of运行情况：

network topology:

![silky-ms3.png](/assets/imgs/silky-ms3.png)


link tracking:

![silky-ms4.png](/assets/imgs/silky-ms4.png)

dash board:

![silky-ms5.png](/assets/imgs/silky-ms5.png)

## useApollo作forServeconfiguremiddle心

### deployApolloServe

必要前提是开发者已经deploy了一套ApolloServe。Developers can refer to[Apollodeploy节点](https://www.apolloconfig.com/#/zh/deployment/distributed-deployment-guide),deploy一套ApolloServe。

exist开发过程middle,更简单of做法是usesilky实例projectmiddleusedocker-compose已经编排好of文件` docker-compose.apollo.yml`。

Enter`samples\docker-compose\infrastr`Table of contents,Will`.env`设置of环境variable`EUREKA_INSTANCE_IP_ADDRESS`修改for您**when前本机ofIPaddress,不允许for`127.0.0.1`**。

Run the following command,wait1~2分钟Just启动apolloconfigureServe。

```powershell
docker-compose -f docker-compose.apollo.yml up -d
```

### useApollo作for微Serveofconfiguremiddle心

1. exist主机projectpassnugetInstall`Silky.Apollo`Bag。(这是一个空Bag,您也可以直接Install`Com.Ctrip.Framework.Apollo.AspNetCoreHosting`and`Com.Ctrip.Framework.Apollo.Configuration`Bag)

2. existServe注册时,添加rightAppoServeconfiguremiddle心of支持

```csharp
private static IHostBuilder CreateHostBuilder(string[] args)
{
    return Host.CreateDefaultBuilder(args)
        .RegisterSilkyServices<AccountHostModule>()
        .AddApollo();
}
```

如果您您想exist指定of运行环境middleuseApollo作for微Serveofconfiguremiddle心，而exist另外其他运行环境middleuse本地configure,那么您也可以pass如下when时处理：

```csharp
private static IHostBuilder CreateHostBuilder(string[] args)
{
    var hostBuilder = Host.CreateDefaultBuilder(args)
        .RegisterSilkyServices<AccountHostModule>()
        .UseSerilogDefault();
    if (EngineContext.Current.IsEnvironment("Apollo"))
    {
        hostBuilder.AddApollo();
    }

    return hostBuilder;
}
```

> Remark 
> 运行环境您可以pass修改`Properties\launchSettings.json`of`DOTNET_ENVIRONMENT`variable(local development mode)
> orpass`.env`环境variable文件指定`DOTNET_ENVIRONMENT`variable(docker-composedevelopment mode)

3. existApolloServeconfiguremiddle心新建相关of应用,并新增相关ofconfigure

Openaddress:http://127.0.0.1:8070 (ApploServeofwebmanagement tools:portal),新建相关of应用。As shown below:

![silky-ms6.png](/assets/imgs/silky-ms6.png)

for应用添加相应ofconfigure:

![silky-ms7.png](/assets/imgs/silky-ms7.png)

普通业务微Serveofconfigure如下:

```properties
# Application
rpc:port = 2201
connectionStrings:default = server=127.0.0.1;port=3306;database=order;uid=root;pwd=qwe!P4ss;
jwtSettings:secret = jv1PZkwjLVCEygM7faLLvEhDGWmFqRUW

# TEST1.silky.sample
registrycenter:registryCenterType = Zookeeper
registrycenter:connectionStrings = 127.0.0.1:2181,127.0.0.1:2182,127.0.0.1:2183;127.0.0.1:2184,127.0.0.1:2185,127.0.0.1:2186

distributedCache:redis:isEnabled = true
distributedCache:redis:configuration = 127.0.0.1:6379,defaultDatabase=0

rpc:token = ypjdYOzNd4FwENJiEARMLWwK0v7QUHPW

governance:executionTimeout = -1

cap:rabbitmq:hostName = 127.0.0.1
cap:rabbitmq:userName = rabbitmq
cap:rabbitmq:password = rabbitmq
```
web网关ofconfigure如下:
```properties

# TEST1.silky.sample.gateway
gateway:injectMiniProfiler = true
gateway:enableSwaggerDoc = true
gateway:wrapResult = true
gateway:jwtSecret = jaoaNPA1fo1rcPfK23iNufsQKkhTy8eh
swaggerDocument:organizationMode = Group
swaggerDocument:injectMiniProfiler = true
swaggerDocument:termsOfService = https://www.github.com/liuhll/silky

# TEST1.silky.sample
registrycenter:registryCenterType = Zookeeper
registrycenter:connectionStrings = 127.0.0.1:2181,127.0.0.1:2182,127.0.0.1:2183;127.0.0.1:2184,127.0.0.1:2185,127.0.0.1:2186

distributedCache:redis:isEnabled = true
distributedCache:redis:configuration = 127.0.0.1:6379,defaultDatabase=0

rpc:token = ypjdYOzNd4FwENJiEARMLWwK0v7QUHPW

governance:executionTimeout = -1
```

4. IncreaseApolloconfiguremiddle心相关configure(read by default`appsettings.yml`),如果指定运行环境variable则读取`appsettings.{Environment}.yml`middleofconfigure

E.g:

```yml
apollo:
  appId: "silky-stock-host"
  cluster: default
  metaServer: "http://127.0.0.1:8080/"
  #  secret: "ffd9d01130ee4329875ac3441c0bedda"
  namespaces:
    - application
    - TEST1.silky.sample
  env: DEV
  meta:
    DEV: "http://127.0.0.1:8080/"
    PRO: "http://127.0.0.1:8080/"
```


## Distributed lock

silkyuse[DistributedLock](https://github.com/madelson/DistributedLock)作forDistributed lock,existServe路由注册andDistributed transaction作业middle均use了Distributed lock.

## AuthenticationandAuthorize

silkyAuthenticationandAuthorizepassBag`Silky.Http.Identity`，passwebhostexist网关accomplish统一ofAuthenticationandAuthorize。

### 用户登陆andissuetoken

silkypass`Silky.Jwt`Bag提供of`IJwtTokenGenerator`accomplishjwt格式oftoken。issuetokenof微Serve应用需要passnugetInstall`Silky.Jwt`Bag，并exist启动modulemiddle依赖`JwtModule`module。开发者可以rightissueoftokenof密钥、tokenValidity period、JwtSignature algorithm、issue者、受众等Attributespassconfigure节点`jwtSettings`conductconfigure。开发者至少需要right`jwtSettings:secret`conductconfigure。

configure如下:
```yaml
jwtSettings:
  secret: jv1PZkwjLVCEygM7faLLvEhDGWmFqRUW
```

User login interface is as follows:

```csharp
        public async Task<string> Login(LoginInput input)
        {
            var userInfo = await _accountRepository.FirstOrDefaultAsync(p => p.UserName == input.Account
                                                                             || p.Email == input.Account);
            if (userInfo == null)
            {
                throw new AuthenticationException($"does not exist账号for{input.Account}of用户");
            }

            if (!userInfo.Password.Equals(_passwordHelper.EncryptPassword(userInfo.UserName, input.Password)))
            {
                throw new AuthenticationException("The password is incorrect");
            }

            var payload = new Dictionary<string, object>()
            {
                { ClaimTypes.UserId, userInfo.Id },
                { ClaimTypes.UserName, userInfo.UserName },
                { ClaimTypes.Email, userInfo.Email },
            };
            return _jwtTokenGenerator.Generate(payload);
        }
```

### Authentication

1. 网关project(WebHost)of启动module需要依赖`IdentityModule`module

```csharp
    [DependsOn(typeof(IdentityModule))]
    public class GatewayHostModule : WebHostModule
    {
        
    }
```

2. `gateway:jwtSecret`configureofAttributes必须andissuetokenof微Serve应用configureofAttributes`jwtSettings:secret`of值保持一致。

```yaml
gateway:
  jwtSecret: jv1PZkwjLVCEygM7faLLvEhDGWmFqRUW
```

3. anonymous access

开发者只need in应用接口or应用接口methodmiddle标注`[AllowAnonymous]`特性Just，This does not require the user to log in,也可以访问the interface。

```csharp
[AllowAnonymous]
Task<string> Login(LoginInput input);
```

### Authorize

开发者可以exist网关应用pass继承`SilkyAuthorizationHandler`基kind,and rewrite`PipelineAsync`methodJustaccomplishright自定义Authorize。

```csharp
 public class TestAuthorizationHandler : SilkyAuthorizationHandler
    {
        private readonly ILogger<TestAuthorizationHandler> _logger;
        private readonly IAuthorizationAppService _authorizationAppService;

        public TestAuthorizationHandler(ILogger<TestAuthorizationHandler> logger,
        IAuthorizationAppService authorizationAppService)
        {
            _logger = logger;
           _authorizationAppService = authorizationAppService;
        }

        public async override Task<bool> PipelineAsync(AuthorizationHandlerContext context,
            DefaultHttpContext httpContext)
        {
            // 获取访问ofServe条目
            var serviceEntry = httpContext.GetServiceEntry();
           
            // 可以passrpctransferIdentifyApp,accomplish自DefinedAuthorize 
           return _authorizationAppService.Authorization(sserviceEntry.ServiceDescriptor.Id);
           
        }
    }
```

## right象Attributes映射
silkyAchieved基于[AutoMapper](https://github.com/AutoMapper/AutoMapper)and[Mapster](https://github.com/MapsterMapper/Mapster)ofright象Attributes映射ofBag。accomplishofproxy host默认依赖`MapsterModule`Bag,useMapster作forright象映射ofcomponents。

只需要pass`Adapt`methodJustaccomplishright象Attributes映射。


## useefcore作for数据访问components

efcore数据访问components主要参考了[furion](https://dotnetchina.gitee.io/furion/docs/dbcontext-start)ofaccomplish。provided数据仓库、database locator、多租户等accomplish方式。use方式and其基本保持一致。