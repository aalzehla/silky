---
title: Service Governance
lang: zh-cn
---

## Service Governanceof概念

Service Governance是主要against分布式服务框架、Microservices，Handling relationships between service calls，Service Publishing and Discovery（who is the provider，who is the consumer，where to register），who calls who when something goes wrong，What are the constraints on the parameters of the service,How to ensure the quality of service？How to service downgrade and circuit breaker？How to make a service monitored，Improve machine utilization?

Microservices有哪些问题需要治理？

1. **Service registration and discovery**: 单体服务拆分为Microservices后，ifMicroservices之间存existtransfer依赖，You need to get the service address of the target service，也就是微Service Governanceof **service discovery** 。要完成service discovery，It is necessary to store the service information in a carrier，载体本身即是微Service Governanceof*Service Registry*，And the action of storing to the carrier is*service registration*。

2. **observability**: Microservices由于较单体应用有了更多of部署载体，Need to call relationship between many services、Status has clear control。observability就包括了transfer拓扑关系、monitor（Metrics）、log（Logging）、call tracing（Trace）Wait。

3. **Traffic management**: 由于Microservices本身存exist不同版本，During the version change process，需要对Microservices间transfer进行control，以完成Microservices版本更迭of平滑。This process needs to be based on the characteristics of the traffic（访问参数Wait）、Percentage distributed to different versions of the service，This also hatched the grayscale release、blue-green release、A/B测试WaitService Governanceof细分主题。

4. **Service fault tolerance**: No service is guaranteed100%no problem，The production environment is complex and changeable，Various failures inevitably occur during service operation（downtime、过载WaitWait），What engineers can do is to minimize the scope of impact when a failure occurs、Return to normal service as soon as possible,need to be introduced「fuse、isolation、throttling and downgrades、timeout mechanism」Wait「Service fault tolerance」mechanism to ensure continuous service availability。

4. **Safety**: 不同Microservices承载自身独有of业务职责，对于业务敏感ofMicroservices，Requires authentication and authentication for access to other services，也就是Safety问题。

5. **control**： 对Service Governance能力充分建设后，就需要有足够ofcontrol能力，能实时进行Service Governance策略向Microservices分发。

6. **Governance of the service itself**: 确保Microservices主机of健康,有能力将不健康node从Microservices集群中移除。

## Service registration and discovery

silkyAutomatic registration and discovery of support services,support the use of **Zookeeper** 、**Nacos** 、**Consul** 作为Service Registry。Service instance online、Offline IntelliSense。

1. When the service instance starts,会向Service Registry新增or更新服务元数据(*If there is no new service metadata、Update if service metadata exists*);at the same time,更新Service Registry该实例of终结点(Instance address information)。

2. use **Zookeeper** or **Nacos** 作为Service Registry,will pass **release-subscription** of方式从Service Registry获取Up-to-date service metadata and endpoints for service instances(instance address)information,and update to local memory;

3. ifuse **Consul** 作为Service Registry，则will pass心跳of方式从Service Registry **Pull** Up-to-date service metadata and endpoints for service instances(instance address)information。whenService Registryof终结点(地址information)change,服务实例of内存中服务路由表information也将得到更新。

4. when inRPCoccurs during communicationIOabnormalor通信abnormal时,The service instance will be inn(The configuration property is:`Governance:UnHealthAddressTimesAllowedBeforeRemoving`)after times从Service Registry移除。(`UnHealthAddressTimesAllowedBeforeRemoving`ifof值Wait于0,the service instance will be removed immediately)。

5. existRPCduring communication,Use long links, Support heartbeat detection。exist服务之间建立连接后,if`Governance:EnableHeartbeat`configured as`true`，then it will be timed(by configuration`Governance:HeartbeatWatchIntervalSeconds`)Send a heartbeat packet,So as to ensure the reliability of the session link。if心跳检测到通信abnormal,will be based on configuration properties(`Governance:UnHealthAddressTimesAllowedBeforeRemoving`)nafter times,从Service Registry移除。

## load balancing

existRPCduring communication,silkyFramework support **polling(Polling)**、 **random(Random)** 、 **hash consistency(HashAlgorithm)** Waitload balancing算法。load balancingofDefault is **polling(Polling)** ,开发者可以by configurationAttributes `Governance:ShuntStrategy` 来统一指定load balancing算法。at the same time,Developers can also`GovernanceAttribute`Trait to reset application service methods(service entry)ofload balancing算法。

E.g:

```csharp
[HttpGet("{name}")]
[Governance(ShuntStrategy = ShuntStrategy.HashAlgorithm)]
Task<TestOut> Get([HashKey]string name);
```

if选择use **hash consistency(HashAlgorithm)** 作为load balancing算法,则需要use`[HashKey]`identify a parameter,so,request with the same parameters,existRPCduring communication,will be routed to a service instance。

## time out

existRPCin communication,ifexist给定ofconfigure时长没有返回结果,则will throwtime outabnormal。normally,开发者可以by configurationAttributes`Governance:TimeoutMillSeconds`to a unified configurationRPCtransfertime out时长,Default is`5000`ms。Similarly,Developers can also`GovernanceAttribute`Trait to reset application service methodsoftime out时长。


```csharp
[HttpGet("{name}")]
[Governance(TimeoutMillSeconds = 1000)]
Task<TestOut> Get([HashKey]string name);
```


if将time out时长configured as`0`，则表示existRPCduring the call,不会出现time outabnormal,untilRPCtransfer返回结果or抛出RPCcall throws other exception。

::: tip hint

建议exist开发环境中, will configure properties`Governance:TimeoutMillSeconds`Set as`0`,Easy for developers to debug。

:::

## failover(retry on failure)

existRPCduring communication,if发生IOabnormal(`IOException`)、通信abnormal(`CommunicationException`)、or找不到本地service entry(service provider throws`NotFindLocalServiceEntryException`abnormal)、Exceeds the maximum processing concurrency allowed by the service provider(`NotFindLocalServiceEntryException`),Then the service consumer will select another service instance to call again according to the configured number of times。

1. ifRPCduring the call发生of是IOabnormal(`IOException`)or通信abnormal(`CommunicationException`)orservice provider throws`NotFindLocalServiceEntryException`abnormal，will change the status of the selected service instance to unavailable,exist`Governance:UnHealthAddressTimesAllowedBeforeRemoving`after identification,The service instance will go offline(*将服务提供者ofinstance address从Service Registry移除*)。

2. if超出服务提供者实例允许of最大并发量,will select another service instance to call,but does not change the state of the service instance。(in other words,That is; the service provider triggers the current limiting protection.)

3. 其他类型ofabnormal不会导致retry on failure。

developer through`Governance:RetryTimes`configure项来确定retry on failureofSecond-rate数，缺省值Wait于`3`。Similarly,Developers can also`GovernanceAttribute`特性来重置retry on failureSecond-rate数。if`RetryTimes`被Set as小于Wait于`0`,则不会发生retry on failure。pass`Governance:RetryIntervalMillSeconds` 可以configureretry on failureof间隔时间。

```csharp
[HttpGet("{name}")]
[Governance(RetryTimes = 3)]
Task<TestOut> Get([HashKey]string name);
```


开发者也可以自定义retry on failure策略,just inherit`InvokeFailoverPolicyProviderBase`base class,pass重写`Create`Method Build Failure Policy。

下面of例子演示了定义time out重试of策略:

```csharp
public class TimeoutFailoverPolicyProvider : InvokeFailoverPolicyProviderBase
{
       
    private readonly IServerManager _serverManager;

    public TimeoutFailoverPolicyProvider( IServerManager serverManager)  
    {
        _serverManager = serverManager;
    }

    public override IAsyncPolicy<object> Create(string serviceEntryId, object[] parameters)
    {
        IAsyncPolicy<object> policy = null;
        var serviceEntryDescriptor = _serverManager.GetServiceEntryDescriptor(serviceEntryId);

        if (serviceEntryDescriptor?.GovernanceOptions.RetryTimes > 0) 
        {
            policy = Policy<object>
                        .Handle<Timeoutxception>()
                        .Or<SilkyException>(ex => ex.GetExceptionStatusCode() == StatusCode.Timeout)
                        .WaitAndRetryAsync(serviceEntryDescriptor.GovernanceOptions.RetryTimes,
                            retryAttempt =>
                                TimeSpan.FromMilliseconds(serviceEntryDescriptor.GovernanceOptions.RetryIntervalMillSeconds),
                            (outcome, timeSpan, retryNumber, context)
                                => OnRetry(retryNumber, outcome, context));
        }
        return policy;
    }
}

```

## fuse保护(breaker)

RPCduring communication,exist开启fuse保护of情况下,ifexist连续发生nSecond-rate **非业务类abnormal** (*非业务类abnormal包括友好类abnormal、鉴权类abnormal、参数校验类abnormalWait*)，则会触发fuse保护,exist一段时间内,该service entry将不可用。

developer through`Governance:EnableCircuitBreaker`configure项来确定是否要开启fuse保护，pass`Governance:ExceptionsAllowedBeforeBreaking`configure项来确定existfuse保护触发前允许ofabnormalSecond-rate数(这里ofabnormal为非业务类abnormal)，pass`Governance:BreakerSeconds`configure项来确定fuseof时长(Unit is:second)。Similarly,fuse保护也可以pass`GovernanceAttribute`to configure。


```csharp
[HttpGet("{name}")]
[Governance(EnableCircuitBreaker = true,ExceptionsAllowedBeforeBreaking = 2, BreakerSeconds = 120)]
Task<TestOut> Get([HashKey]string name);
```

## Limiting

Limitingof目of是pass对并发访问/Request a speed limit，或者对一个时间窗口内ofRequest a speed limit来保护系统，Denial of service once the limit rate is reached、排队或Wait待、降级Wait处理。

SilkyMicroservices框架ofLimiting分为两部分,part of the serviceRPCcommunication between,part is rightHTTPaskof进行Limiting。

### RPCLimiting

When the service provider receivesRPCafter request,ifwhen前服务实例并发处理量大于configureof`Governance:MaxConcurrentHandlingCount`,The current instance cannot handle the request,will throw`OverflowMaxServerHandleException`abnormal。The service consumer will retry other instances of the service as configured，Can refer to[failover(retry on failure)](#fuse保护-breaker)node。

`Governance:MaxConcurrentHandlingCount`ofconfigureDefault is`50`,ifconfigure小于Wait于`0`，it means wrongrpc通信进行Limiting。The configuration here is aimed at the ability of all concurrent processing of the service instance,并不是against某个service entryof并发量configure,所以开发者无法pass`GovernanceAttribute`feature to modify the configuration of concurrent processing。

### HTTPLimiting

SilkyIn addition to supporting services within the frameworkRPCtransferofLimiting之外,还支持pass[AspNetCoreRateLimit](https://github.com/stefanprodan/AspNetCoreRateLimit)realize pairHttpaskofLimiting。**AspNetCoreRateLimit** 支持passIporagainstIP进行Limiting。

下面我们来简述如何use **AspNetCoreRateLimit** reach righthttpaskofLimiting。

#### 1. add configuration

网关应用新增Limitingconfigure文件 **ratelimit.json**。exist`RateLimiting:Client`configurenodeconfigureagainst客户端ofLimiting通用规则,pass`RateLimiting:Client:Policies`configurenode重写against特定客户端ofLimiting策略。Developers can refer to[ClientRateLimitMiddleware](https://github.com/stefanprodan/AspNetCoreRateLimit/wiki/ClientRateLimitMiddleware)Familiarize yourself with relevant configuration properties。exist`RateLimiting:Ip`configurenodeconfigureagainstIPofLimiting通用规则,pass`RateLimiting:Ip:Policies`configurenode重写against特定IPofLimiting策略。Developers can refer to[IpRateLimitMiddleware](https://github.com/stefanprodan/AspNetCoreRateLimit/wiki/IpRateLimitMiddleware)Familiarize yourself with relevant configuration properties。

if网关采用分布式部署,可以pass`RateLimiting:RedisConfiguration`property configurationredisservice as storage service。

E.g:

```json
{
  "RateLimiting": {
    "Client": {
      "EnableEndpointRateLimiting": false,
      "StackBlockedRequests": false,
      "ClientIdHeader": "X-ClientId",
      "HttpStatusCode": 429,
      "EndpointWhitelist": [
        "get:/api/license",
        "*:/api/status"
      ],
      "ClientWhitelist": [
        "dev-id-1",
        "dev-id-2"
      ],
      "GeneralRules": [
        {
          "Endpoint": "*",
          "Period": "1s",
          "Limit": 5
        }
      ],
      "QuotaExceededResponse": {
        "Content": "{{ \"data\":null,\"errorMessage\": \"Whoa! Calm down, cowboy! Quota exceeded. Maximum allowed: {0} per {1}. Please try again in {2} second(s).\",\"status\":\"514\",\"statusCode\":\"OverflowMaxRequest\" }}",
        "ContentType": "application/json",
        "StatusCode": 429
      },
      "Policies": {
        "ClientRules": [
        ]
      }
    }
  },
  "RedisConfiguration": "127.0.0.1:6379,defaultDatabase=1"
}
```
#### 2. registration service

exist `Startup` in startup class, Add to **AspNetCoreRateLimit** related services。

```csharp
public override void ConfigureServices(IServiceCollection services, IConfiguration configuration)
{
    var redisOptions = configuration.GetRateLimitRedisOptions();
    services.AddClientRateLimit(redisOptions);
    services.AddIpRateLimit(redisOptions);
}
```

certainly,if开发者是pass `AddSilkyHttpServices()` 进行service registration,exist这个过程中,已经at the same timeAdd to了 **AspNetCoreRateLimit** related services

#### 3.enable **AspNetCoreRateLimit** of Related middleware,accomplishHTTPLimiting。

```csharp
public void Configure(IApplicationBuilder app, IWebHostEnvironment env) 
{
   app.UseClientRateLimiting(); // against客户端进行Limiting
  // app.UseIpRateLimiting(); // againstIP进行Limiting

}
```

## service fallback(Service downgrade)

existRPCduring the call,if执行失败,我们passtransfer`Fallbackmethod`,从而达到Service downgrade操作。

想要accomplishservice fallbackof处理，我们需要exist定义服务methodof时候pass`FallbackAttribute`特性指定of要回退of接口类型。给定of接口need to define a服务method参数相同ofmethod。

`FallbackAttribute`需要指定回退接口接口of类型,method名称(if缺省,则回退接口定义ofmethod名称与service entrymethod一致)，if定义了多个回退method,Also give weight configuration。

| Attributes | configure | Remark |
|:-----|:-----|:-----|
| Type | Fallback interface type | must be specified,一般与服务接口定义exist一起 |
| MethodName | 指定of回退method | if不configureof话,则与service entrymethod一致,定义ofmethodof参数必须一致 |

```csharp
  [HttpPatch]
  [Fallback(typeof(IUpdatePartFallBack))]
  Task<string> UpdatePart(TestInput input);
```

`IUpdatePartFallBack`need to define a`UpdatePart`参数相同ofmethod。

```csharp
public interface IUpdatePartFallBack
{
    Task<string> UpdatePart(TestInput input);
}
    
```


定义of回退接口和method只有exist被accomplish后,才会existRPCcall failed后执行定义of`Fallbackmethod`。service fallbackofaccomplish方式有两种方式,一种是exist服务端accomplish回退,一种是exist客户端accomplish。

### exist服务端accomplish回退method

ifexist服务端accomplish回退method,whenRPCexist服务端执行业务method失败,if服务端有存exist定义of回退接口ofaccomplish,那么会降级执行回退method。

举一个比较适用of场景，E.g: exist一个短信收发of业务场景中,ifuse阿里云作为服务提供商相对更便宜,但是if欠费后我们希望能够平滑of切换到腾讯云提供商。existsoof业务场景下,if我们默认选择use阿里云作为服务提供商, 我们就可以passexist服务端accomplish一个定义of`Fallbackmethod`从而达到平滑ofuse备用短信服务提供商of作用。

### existconsumeraccomplish回退method

certainly, 我们也可以existtransfer端(consumer)accomplish定义of`Fallbackmethod`，ifRPCcall failed,那么existtransfer端就会执行accomplish了of`Fallbackmethod`。返回给前端of是降级后of数据,并不会发生abnormal。

```csharp
public class TestUpdatePartFallBack : IUpdatePartFallBack, IScopedDependency
  {
      public async Task<string> UpdatePart(TestInput input)
      {
          return "this is a fallback method for update part";
      }
  }
```

## link tracking

silky框架use[SkyAPM](https://github.com/SkyAPM/SkyAPM-dotnet)accomplish了link tracking,developer through引入相应ofconfigure和服务,即可realize pairhttpask、RPCtransfer、TCCDistributed transaction execution process andEFCore数据访问oftransferlink tracking。

开发者可以pass查看[link tracking](link-tracking)node熟悉如何进行configure和引入相应of服务以及如何部署 **skywalking**，并pass **skywalking** 查看transferof链路。

## Safety

existsilkyin the frame,非常重视对Safety模块of设计。

1. pass`rpc:token`ofconfigure,保证外部无法passRPCThe port directly accesses the application service。服务内部之间oftransfer均需要对`rpc:token`check,if`rpc:token`inconsistent，则不允许进行transfer。
2. exist网关处统一accomplishAuthentication and Authorization。开发者可以pass查看[Authentication and Authorization](identity)node查看相关Documentation。
3. 开发者可以pass`GovernanceAttribute`特性来禁止外部访问某个应用服务method。被外部禁止访问of应用服务method只允许服务内部之间passRPCway to communicate。

```csharp
[Governance(ProhibitExtranet = true)]
Task<string> Delete(string name);
```

## cache interception

existRPCduring communication,pass引入cache interception,极大of提高了系统性能。

开发者可以pass[cache](caching.html#cache interception)Documentation，熟悉cache interceptionofusemethod。