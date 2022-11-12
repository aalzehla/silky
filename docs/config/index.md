---
title: configure
lang: zh-cn
---

## introduce

silkyFramework support via`json`or`yml`格式ofconfiguredocument,开发者可以根据需要对下面configure节点进行调整。E.g:pass`appsettings.yml`对微服务应用进行统一configure,pass`appsettings.${Environment}.yml`对不同环境变量下ofconfigure项进行设置。

## RPCcommunication(Rpc)

| Attributes | illustrate | Default value |  Remark |
|:-----|:-----|:-----|:------|
| Host | hostIp | 0.0.0.0 | Setting up a microservice applicationipaddress,"0.0.0.0"自动获取当前hostIpaddress |
| Port |  RPCThe port number | 2200 | RPCcommunicationuseofThe port number |
| UseLibuv | Whether to enableLibuv | true |  |
| IsSsl | Whether to openSsl | false |  |
| SslCertificateName | SslCertificate(document)name |  |  |
| SslCertificatePassword | SslCertificate密码 |  |  |
| SoBacklog | dotNettycommunicationofSoBacklogparameter | 8192  |  |
| Token | RPCcommunicationofkey |  | Empty is not allowed,And the same microservice application cluster,configureoftokenmust agree |
| ConnectTimeout | RPCcommunication过程中建立链接of超时时间 | 500 | unit:ms |

## useZookeeper作为服务中心ofconfigure(RegistryCenter)

| Attributes | illustrate | Default value |  Remark |
|:-----|:-----|:-----|:------|
| Type | Service registry type | Zookeeper  | configure值为`Zookeeper`,标识use Zookeeperas a service registry |
| ConnectionTimeout | Connection timed out | 5000 | unit(ms)  |
| SessionTimeout | sessionsession timeout | 8000 | unit(ms)  |
| OperatingTimeout | Operation timed out | 10000 | unit(ms)  |
| ConnectionStrings | 服务注册中心address |  | Support for multi-service registries,通一个集群ofaddressuse逗号(,)segmentation，多个服务注册中心use(;)进行segmentation。E.g:`zookeeper1:2181,zookeeper1:2182,zookeeper1:2183;zookeeper2:2181,zookeeper2:2182,zookeeper2:2183` |
| RoutePath | 服务元数据of路由address | /services/serviceroutes | 当服务启动时会更新服务元数据和终结点address信息 |

## useNacos作为服务中心ofconfigure(RegistryCenter)

## useConsul作为服务中心ofconfigure(RegistryCenter)

## Service Governance(Governance)

| Attributes | illustrate | Default value |  Remark |
|:-----|:-----|:-----|:------|
| ShuntStrategy | load balancing strategy | Polling(polling) | Load balancing algorithm support：Polling(polling)、Random(random)、HashAlgorithm(hash consistency) |
| TimeoutMillSeconds | execution timeout | 3000(ms) | unit为(ms),Fuse on timeout，-1expressed inrpccommunication过程中不会超时 |
| EnableCachingInterceptor | Whether to support cache interception | true | 如果configure为`false`，then cache interception will not take effect |
| EnableCircuitBreaker | Whether to open熔断保护 | true |  |
| ExceptionsAllowedBeforeBreaking | The number of exceptions allowed before the fuse protection is turned on | 3 | Only non-business exceptions |
| BreakerSeconds | Fusing protection time | 3 | 60 |
| AddressFuseSleepDurationSeconds | communication异常发生时,The length of time the service instance goes to sleep | 60 | After the allowed number of sleeps,The service instance will be taken offline |
| UnHealthAddressTimesAllowedBeforeRemoving | The number of times an unhealthy service instance is allowed to go to sleep | 0 |  |
| RetryIntervalMillSeconds | Service retry interval | 50 |  |
| MaxConcurrentHandlingCount | Maximum allowed concurrent processing  | 50 | The service instance reaches the maximum concurrent processing capacity,will throw an exception |
| EnableHeartbeat | Whether to allow heartbeat detection  | false | After enabling heartbeat detection,After the link is established between the services,将会pass发送心跳包保证dotnettyThe link survives |
| HeartbeatWatchIntervalSeconds | Heartbeat packet interval  | 300 | The minimum value is`60`s，if less than60,will be set to60 |


## Distributed cache(DistributedCache)

| Attributes | illustrate | Default value |  Remark |
|:-----|:-----|:-----|:------|
| HideErrors | usecache移除时,whether to hide errors | false |  |
| KeyPrefix | cachekeyprefix | "" | empty string |

Redis子节点有如下configure:


| Attributes | illustrate | Default value |  Remark |
|:-----|:-----|:-----|:------|
| IsEnabled | 是否useredis服务作为cache服务 | false |  |
| Configuration | rediscache服务链接字符串 |  | Redis.IsEnabledSet as`true`valid when |

GlobalCacheEntryOptions子节点有如下configure:

| Attributes | illustrate | Default value |  Remark |
|:-----|:-----|:-----|:------|
| AbsoluteExpiration |  Absolute expiration date |  | 获取或设置cache项ofAbsolute expiration date  |
| AbsoluteExpirationRelativeToNow | Relative expiry time  |  | Gets or sets the absolute expiration time relative to the current time |
| SlidingExpiration |  | 20min | 获取或设置cache项在被删除之前可以处于停用状态（E.g不被访问）duration。 This does not extend the item lifetime beyond the absolute expiration time（if set）  |


## gateway(Gateway)

| Attributes | illustrate | Default value |  Remark |
|:-----|:-----|:-----|:------|
| ResponseContentType | httpRequested response content type |  |  |
| JwtSecret | issuetokenofjwtkey |  | 必须与issuetoken应用configureof保持一致  |
| IgnoreWrapperPathPatterns |  不需要包装of返回值of接口or请求路径 | Ignore static resource related  | data type is array  |
               


::: warning

该configure只有configure在gateway应用才会生效。

:::