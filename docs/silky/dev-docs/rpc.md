---
title: rpccommunication
lang: zh-cn
---

## what isRPC

RPC full name Remote Procedure Call——remote procedure call。It is a technology to solve remote invocation services，Make the caller as convenient and transparent as calling a local service。simply put，RPCfrom a machine（client）On calling another machine by passing parameters（server）a function or method on（may be collectively referred to as services）and get the result returned。

RPCThe goal of the framework is to allow remote processes(Serve)call is easier、transparent，RPCThe framework is responsible for shielding the underlying transmission method(TCPorUDP)、Serialization method(XML/JSON/binary)和communication细节。frame usage者只需要了解谁exist设么位置提供了什么样of远程Serveinterface即可，开发者不需要关心底层communication细节和调用过程。

RPCcommunication有如下特点：

- RPC Will hide the underlying communication details（No need for direct processingSocketcommunicationorHttpcommunication）
- RPC is a request-response model。client发起请求，server返回响应（similar toHttpway of working）
- RPC Use the form like calling a local function（or方法）same as calling remote functions（or方法）。

OneRPCThe frame consists of three parts：

1. Serve提供者: it runs onServe端,负责提供Serveinterface定义和Serve实现kind。

2. Serve发布者：it runs onRPCServe端,复杂将本地Serve发布成远程Serve,for other consumers to call。

3. 本地Serve代理: it runs onRPCKefuduan,pass代理调用远程Serve提供者,Then encapsulate the result to the local consumer。



RPCcommunication模型如下图所示:

![rpc1.jpg](/assets/imgs/rpc1.jpg)

## SilkyofrpcFramework implementation

Silkyframe usagerpc协议实现Serve之间ofcommunication。we know,要实现OneRPCframe,The following technical points need to be solved:

1. 远程Serve提供者：需要以某种形式提供Serve调用相关of信息，Bag括但不限于Serve连interface定义、Data interface、or者中间态ofServe定义文件；existSilkyframe中,Serve提供者以Serve应用interfaceof方式提供Serve调用of相关信息，Serve调用者pass引用Serve提供者of应用Serveinterface层(project、Bag)of方式获取远程Serve调用of相关信息。

2. remote proxy object：Serve调用者调用ofServe实际上是远程Serveof本地代理，forSilkyframe而言，is through`Autofac.Extras.DynamicProxy`组件实现of动态代理，将本地调用封装成远程Serve调用,隐藏了底层ofcommunication细节。

3. communication：RPCframe于具体ofcommunication协议无关；Silkyframe usagedotnettyframe作为底层ofcommunicationframe。

4. Serialization：远程communication，需要将对象转换成binary码流进行网络传输，不同ofSerializationframe，支持of数据kind型/数据Bag大小、Exception types and performance are different。Silkyframe默认使用json格式作为Serialization格式，also supports`MessagePack`or是`Protobuff`作为Serialization method。

## how to use

exist开发过程中,我们一般需要将Serveof定义(interface)和Serveof实现(kind)分别定义exist两个不同of程序集，方便Serveofinterface被其他微Serve应用引用，existSilkyframe中,Serve调用者(consumer)pass引用Serve提供者of应用interface层(Bag),就可以pass动态代理代理机制为Serve应用interface生成本地代理,pass该代理与应用Serve提供者进行communication,并将执行of结果返回给consumer，The steps are as follows：

1. 将Serve提供者of应用interface单独定义为One应用程序集(Bag)。定义of应用interface需要pass`ServiceRouteAttribute`对Serve应用interfaceof路由进行标识，Serve提供者需要实现应用interface。关于应用interfaceof定义和实现[please check](appservice-and-serviceentry)。

2. Serve调用者Serve需要passproject(oris throughnugetBag安装)of方式引用Serve提供者of应用interface所定义ofproject(Bag)。

3. 开发者可以pass构造注入of方式使用应用Serveinterface，Serve调用者就可以passServe调用者of应用interface生成of动态代理与Serve提供者进行communication。

```csharp
public class TestProxyAppService : ITestProxyAppService
{
    private readonly ITestAppService _testAppServiceProxy; // 应用提供者of应用interface，pass其生成Serve调用者of本地动态代理
    private readonly ICurrentServiceKey _currentServiceKey;

    public TestProxyAppService(ITestAppService testAppService,
        ICurrentServiceKey currentServiceKey)
    {
        _testAppServiceProxy = testAppService;
        _currentServiceKey = currentServiceKey;
    }

    public async Task<TestOut> CreateProxy(TestInput testInput)
    {
        // _currentServiceKey.Change("v2");
        // pass应用interface生成of本地动态代理与Serve提供者进行rpccommunication
        return await _testAppServiceProxy.Create(testInput);
    }
}

```

::: warning

existrpccommunication过程中,pass指定of`ServiceKey`来指定Serve提供者of应用interfaceof实现kind,可以existServe调用前,pass`IServiceKeyExecutor`to specify therpccommunicationof`serviceKey`of值。   

:::