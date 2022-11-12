---
title: Glossary
lang: zh-cn
---

## host

**host**is an object that encapsulates application resources,For hosting apps and managing app lifecycles。silkyframework uses.net的两种类型的host:

1. for hosting business service applications[通用host](https://docs.microsoft.com/zh-cn/dotnet/core/extensions/generic-host),对于该类型的host,Not availablehttpServe(will not be announced to the outside worldHttpport)。由于业务Serve应用之间主要通信是基于dotnettyrealizedrpccommunication framework(The underlying communication protocol used is`TCP`protocol),对于普通业务应用Serve来说,托管该类型的host并不需要提供httpServe。

2. 用于托管网关Serve应用的[Webhost](https://docs.microsoft.com/zh-cn/aspnet/core/fundamentals/host/web-host?view=aspnetcore-5.0),The gateway itself does not implement specific services,pass依赖其他微Serve应用的应用接口层(Bag)，existhttpAfter the request arrives,pass`webapipath`+`httprequest method`existrouting表中找到相关的Serverouting信息,并pass负载均衡算法,找到相应的Serve提供者,并passrpc通信与相应的Serve提供者实例进行通信,And encapsulate the return result and return it to the caller(front end)。开发者可以exist网关项目引用或是自定义相应的[Asp.net Core middleware](https://docs.microsoft.com/zh-cn/aspnet/core/fundamentals/middleware/?view=aspnetcore-5.0)

## routing

routing是指pass`webapipath`+`httprequest method`或是pass`ServiceId`existrouting表查找相应的Serve条目信息的过程。
  
- exist网关应用,查找Serve条目信息是pass`webapipath`+`httprequest method`。
- exist业务微Serve应用中, pass`ServiceId`查找Serve条目信息。`ServiceId`是passServe调用对应的方法的完全限定名和参数名组合生成的。

## Serve条目

每个业务应用Serve都会将已经realized业务Serve接口方法生成一个对应的Serve条目,每个Serve条目都会根据方法的权限限定名和参数名生成唯一的ServeId(`ServiceId`)。exist应用Serve启动时,每个Serve实例都会想Serveregister中心register或更新Serve条目对应的Serve提供者的地址信息。

## routing表

由Serve条目组成的集合。微Serve集群的routing表会根据Serve实例的启动(register)and stop(log out)get updated。

exist微Serve应用实例启动时,会自动register或更新Serveregister中心的该Serve条目的地址信息。at the same time,订阅了Serve条目的其他微Serve应用也会exist内存中更新相应的routing表。

## Serveregister中心

主要用于保存微Serve集群的routing表信息。会根据Serve提供者实例的启动(register)and stop(log out)更新Serve条目的地址列表。

## RPC

RPC full name Remote Procedure Call——remote procedure call。是为了解决远程调用Serve的一种技术，使得调用者像调用本地Serve一样方便透明。simply put，RPCfrom a machine（client）上pass参数传递的方式调用另一台机器（Serve器）a function or method on（可以统称为Serve）and get the result returned。

## Serve提供者

existrpcduring communication,负责提供Serve接口定义和Serve实现类，existrpcduring communication,作为Serve端的一方。

## Serve消费者/Serve调用者

existrpcduring communication,提供应用Serve接口动态生成本地代理,并使用代理pass网络请求,与Serve提供者进行通信，and return the result to the caller's side。

::: warning

对于一个业务微Serve应用而言，既可以作为Serve提供者,也可能是Serve消费者,主要看exist一个rpcduring communication,是作为提供Serve的一方,还是作为调用Serve的一方。

:::

## Serve治理

Serve治理是主要针对分布式Serve框架，微Serve，处理Serve调用之间的关系，Serve发布和发现（who is the provider，who is the consumer，要register到哪里），who calls who when something goes wrong，Serve的参数都有哪些约束,如何保证Serve的质量？如何Serve降级和熔断？怎么让Serve受到监控，Improve machine utilization?

::: tip

currentsilky框架暂未完全实现Serve治理的所有要求,It will be continuously improved in the later development process

:::

## cache interception

existrpcduring communication,to reduce network requests,Provides the performance of distributed applications,If the data gets cached as well,则直接从缓存Serve中获取相应的数据,并返回给Serve调用者。