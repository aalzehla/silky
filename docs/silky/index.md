---
title: silkyFramework introduction
lang: zh-cn
---


## silkyIntroduction

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

![silky微Serveframe.png](/assets/imgs/silky微Serveframe.png)

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
- pass配置support禁止Serve被外部访问

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
- supportApolloas配置Serve中心
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

## open source address

- github: [https://github.com/liuhll/silky](https://github.com/liuhll/silky)
- gitee: [https://gitee.com/liuhll2/silky](https://gitee.com/liuhll2/silky)

## contribute

- contribute的最简单的方法之一就是是参and讨论和讨论问题（issue）。你也可以pass提交的 Pull Request 代码变更作出contribute。
- you can also joinQQgroup(934306776)参andsilkyframe的学习讨论。 

  ![qq-group.jpg](/assets/imgs/qq-group.jpg)