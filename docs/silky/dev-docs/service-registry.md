---
title:  Service Registry
lang: zh-cn
---

## Service Registry简介

in a distributed system,Service Registryof作用是将deployServe实例of机器addressas well as其它元数据记录到注册middle心,Serve consumers when they need it，By querying the registry，Enter the provided service name，get the address，to initiate a call。

Under the microservice architecture，There are three main roles：**service provider（RPC Server）**、**service consumers（RPC Client）** and **Service Registry（Registry）**，Please see the following picture for the interaction between the three:

![service-registry1.png](/assets/imgs/service-registry1.png)

**RPC Server** Provide services，at startup，根据配置文件指定ofService Registry配置信息，Towards **Registry** Register your own service route，并Towards**Registry**Periodically send heartbeats to report survival status。

**RPC Client** call service，at startup，根据配置文件指定ofService Registry配置信息，Towards **Registry** Subscription service，Bundle**Registry**The returned list of service nodes is cached in local memory，and with **RPC Sever** establish connection。

when **RPC Server** When the node changes，**Registry**changes will be synchronized，**RPC Client** After sensing; the list of service nodes cached in the local memory will be refreshed。

**RPC Client** from a locally cached list of service nodes，Choose one based on the load balancing algorithm **RPC Sever** make a call。

::: tip hint

1. For a microservice application,in a cluster,it can either be used as**RPC Sever**,also possible as**RPC Client**，mainly look atrpcduring communication，是Provide servicesof一方，还是call serviceof一方。

:::

when前, silkyThe microservice framework supports the use of **Zookeeper** 、 **Nacos** 、 **Consul** 作为Service Registry,开发者可以选择熟悉ofServemiddle间件作为Service Registry。

## service metadata

existsilkyin the frame,service providerTowardsService Registry注册of数据被称为: **service metadata** 。service metadata主要四部分组成:

1. **hostName** : 用于描述service providerof名称,The package name for the build host
2. **services** : 该service provider所提供of应用Serve信息,is an array,include：ServeId,Serve名称,Serve协议,Serve条目，Metadata and other information
3. **timeStamp** : 更新service metadataoftimestamp
4. **endpoints** : 该Serve实例ofendpoint,is an array。不同Service Registry注册Serve实例ofendpoint不同


If using **Zookeeper** 作为Service Registry, 所有ofservice metadata将会被维护到该主机所existnode下, register to **Zookeeper** Service Registryofservice metadatalike下:

```json
{
    "hostName":"DemoHost",
    "services":[
        {
            "id":"Demo.Application.Contracts.System.ISystemAppService",
            "serviceName":"SystemAppService",
            "serviceProtocol":0,
            "serviceEntries":[
                {
                    "id":"Demo.Application.Contracts.System.ISystemAppService.GetInfo_Get",
                    "serviceId":"Demo.Application.Contracts.System.ISystemAppService",
                    "serviceName":"SystemAppService",
                    "method":"GetInfo",
                    "webApi":"api/system/demo/info",
                    "httpMethod":0,
                    "serviceProtocol":0,
                    "metadatas":{

                    },
                    "prohibitExtranet":false,
                    "isAllowAnonymous":true,
                    "isDistributeTransaction":false
                }
            ],
            "metadatas":{

            }
        }
    ],
    "endpoints":[
        {
            "host":"172.26.144.1",
            "port":2200,
            "processorTime":2578.125,
            "timeStamp":1636464575,
            "serviceProtocol":0
        }
    ],
    "timeStamp":1636464576
}
```

like果Service Registry是 **Consul** or **Nacos**, Serve实例ofendpoint则会单独注册and维护, service metadata格式like下:

```json
{
    "hostName":"DemoHost",
    "services":[
        {
            "id":"Demo.Application.Contracts.System.ISystemAppService",
            "serviceName":"SystemAppService",
            "serviceProtocol":0,
            "serviceEntries":[
                {
                    "id":"Demo.Application.Contracts.System.ISystemAppService.GetInfo_Get",
                    "serviceId":"Demo.Application.Contracts.System.ISystemAppService",
                    "serviceName":"SystemAppService",
                    "method":"GetInfo",
                    "webApi":"api/system/demo/info",
                    "httpMethod":0,
                    "serviceProtocol":0,
                    "metadatas":{

                    },
                    "prohibitExtranet":false,
                    "isAllowAnonymous":true,
                    "isDistributeTransaction":false
                }
            ],
            "metadatas":{

            }
        }
    ],
    "timeStamp":1636464576
}
```

### hostname(hostName)

**hostName** 用于描述service providerof名称,existTowardsService Registry注册Serveof过程middle,应用会判断Service Registry是否存exist该应用ofservice metadata,like果不存exist,create the corresponding node,并添加相应ofservice metadata;like果已经存exist相应ofServenode,则会更新service metadata,其他service providerof实例从Service Registry获取到service metadata,并更新本地内存ofservice metadata。

### Serve列表(services)

该Attributes包含该应用所支持ofServe列表,like果Service RegistryofServe列表被更新,其他Serve实例也会从Service Registry获取,and update to local memory。

Serve列表include：ServeId,Serve名称,Serve协议,Serve条目，Metadata and other information

| field | illustrate | Remark |
|:-----|:-----|:-----|
| id | ServeId | unique;Serve接口定义of完全限定名 |
| serviceName | Serve名称 |  |
| serviceProtocol | Serve通信协议 | rpc通信in the frame,Communication protocol used  |
| serviceEntries | 该Serve支持ofServe条目(which is：应用Serve定义of方法) | data type is array  |
| serviceEntries.id | Serve条目Id | fully qualified name of the method + parameter name + Httpmethod name  |
| serviceEntries.serviceId | ServeId |  |
| serviceEntries.serviceName | Serve名称 |  |
| serviceEntries.method | Serve条目对应ofmethod name称 |  |
| serviceEntries.webApi | Generatedwebapi address | Empty if access to the external network is prohibited  |
| serviceEntries.httpMethod | Generatedwebapiof请求address | Empty if access to the external network is prohibited  |
| serviceEntries.serviceProtocol |  rpc通信in the frame,Communication protocol used |   |
| serviceEntries.metadatas |  Serve条目of元数据 | 可以为Serve条目写入(k,v)format metadata   |
| metadatas | Serveof其他元数据 | 可以为Serve写入(k,v)format metadata  |

::: warning Notice

1. exist一个微Serve集群middle,Serve条目unique。That is to say,不允许exist同一个微Serve集群middle, 不同微Serve应用middle不允许出现两个一模一样of方法(应用Serve接口of完全限定名andmethod name、parameter name一致);
2. 只有被实现of应用Serve才会被register toService Registry。
::: 

### endpoint

**endpoints** 是用来描述该微ServeofServe实例ofaddress信息。

一个Serve实例可能存exist多个endpoint,like：usewebhost构建of微Serve应用(存existwebaddressendpointandrpcendpointaddress)；build supportwebsocketServeof微Serve应用(存existwebsocketServeaddressendpointandrpcendpointaddress)。

use不同ofService Registry,注册endpoint可能会做不同of处理。

1. If using **Zookeeper** 作为Service Registry, Serve实例ofendpoint将被更新到该service provider对应node数据of `endpoints` Attributes,That is to say,`endpoints`将作为service metadataof一个Attributes。

```json
{
    "hostName": "DemoHost",
    "services": ["..."],
    "timeStamp": 1636464576,
    "endpoints":[
        {
            "host":"localhost",
            "port":5000,
            "processorTime":2984.375,
            "timeStamp":1636464576,
            "serviceProtocol":4
        },
        {
            "host":"172.26.144.1",
            "port":2200,
            "processorTime":2578.125,
            "timeStamp":1636464575,
            "serviceProtocol":0
        }
    ]
}
```

2. If using **Consul** 作为Service Registry, Serve实例将会被register to **Services** node,and will only register the protocol as`TCP`ofendpoint,Serve实例of其他协议of将会以元数据of方式添加到元数据

![service-registry2.png](/assets/imgs/service-registry2.png)

![service-registry3.png](/assets/imgs/service-registry3.png)

3. If using **Nacos** 作为Service Registry, Serve将会被register toServe列表node,与use **Consul** 作为Service Registry相同,will only register the agreement as`TCP`ofendpoint,Serve实例of其他协议of将会以元数据of方式添加到元数据

![service-registry4.png](/assets/imgs/service-registry4.png)

![service-registry5.png](/assets/imgs/service-registry5.png)

endpointofAttributeslike下所述:

| field | illustrate | Remark |
|:-----|:-----|:-----|
| host | 对应of主机address | 微Serve应用ofIp内网address |
| port | The port number |  |
| processorTime | CPU使use率  |  |
| timeStamp | registration timestamp  |  |
| serviceProtocol | Serve协议  |  |

### timestamp

`timeStamp`是指TowardsService Registry更新service metadataoftimestamp。

## useZookeeper作为Service Registry

silky支持use **Zookeeper** 作为Service Registry。

silky支持为微Serve集群配置多个 **Zookeeper** Service Registry，您只需要exist配置Service Registryof链接字符串`registrycenter.connectionStrings`middle,use分号`;`就可以指定微Serve框架of多个Service Registry。

use **Zookeeper** 作为Service Registry需要exist配置文件middle,exist`registrycenter`配置node下,将Service Registryof类型`type`Set as: `Zookeeper`,pass`connectionStrings`Attributes配置Servemiddle心of链接。同时可以pass其他Attributes配置Service Registryof链接Attributes。

```yml

registrycenter: // Service Registry配置node
  type: Zookeeper
  connectionStrings: 127.0.0.1:2181,127.0.0.1:2182,127.0.0.1:2183;127.0.0.1:2184,127.0.0.1:2185,127.0.0.1:2186 // Serve配置middle心链接
  connectionTimeout: 1000 // link timeout(unit:ms)
  sessionTimeout: 2000 // session timeout(unit:ms)
  operatingTimeout: 4000 // Operation timeout(unit:ms)
  routePath: /services/serviceroutes

```
normally,use **Zookeeper** 作为Service Registry我们只需要指定Service Registry类型and链接字符串which is可,其他Attributes(likelink timeout、session time、Operation timeout、注册of路由address等)default values ​​are provided。

```yml

registrycenter: // Service Registry配置node
  type: Zookeeper
  connectionStrings: 127.0.0.1:2181,127.0.0.1:2182,127.0.0.1:2183;127.0.0.1:2184,127.0.0.1:2185,127.0.0.1:2186 // Serve配置middle心链接

```

silky框架提供了use docker-compose to arrange Zookeeper of [Arrange files](https://raw.githubusercontent.com/liuhll/silky/main/samples/docker-compose/infrastr/docker-compose.zookeeper.yml), 开发者exist获取 silkyafter the source code,Enter `./samples/docker-compose/infrastr` after the directory,执行like下命令,can build a **Zookeeper** Serve集群。

```powershell
docker-compose -f docker-compose.zookeeper.yml up -d
```

## useNacos作为Service Registry

silky支持use **Nacos** 作为Service Registry。

If using **Nacos** 作为Service Registry，need to be `registrycenter:type` 配置Set as:`Nacos`,Plus **Nacos** of其他配置Attributes。Developers can refer to[NacosDocumentation](https://nacos.io/zh-cn/docs/what-is-nacos.html)as well as[nacos-sdk-csharp](https://.readthedocs.io/en/latest/introduction/gettingstarted.html) to get acquainted **Nacos** of配置anduse。

use **Nacos** 作为Service Registryof配置like下:

```yml
registrycenter:
  type: Nacos
  namespace: silky
  serverAddresses:
    - http://127.0.0.1:8848/
    - http://127.0.0.1:8849/
    - http://127.0.0.1:8850/
  userName: nacos
  password: nacos
  groupName: DEFAULT_GROUP
  clusterName: DEFAULT
  registerEnabled: true
  instanceEnabled: true
  namingUseRpc: true
```

about NacasServeofbuild可以refer to[Nacos Docker quick start](https://nacos.io/zh-cn/docs/quick-start-docker.html)。when然, silky框架也提供了use **docker-compose** to arrange **Nacos** Serveof文件，开发者exist获取after the source code,Enter `./samples/docker-compose/infrastr` after the directory,执行like下命令,can build a **Nacos** Serve集群。

```powershell
docker-compose -f docker-compose.nacos.cluster-hostname.yaml up -d
```

## useConsul作为Service Registry

silky支持use **Consul** 作为Service Registry。

If using **Consul** 作为Service Registry，need to be `registrycenter:type` 配置Set as:`Consul`,Plus **Consul** of其他配置Attributes。Developers can refer to[ConsulDocumentation](https://www.consul.io/docs)as well as[consuldotnet](https://github.com/G-Research/consuldotnet) to get acquainted **Consul** of配置anduse。

use **Consul** 作为Service Registryof配置like下:

```yml
registrycenter:
  type: Consul
  address: http://127.0.0.1:8500
  datacenter: dc1 # Default is dc1
  token：""  # like果consulServe设置了token，you need to configuretoken
  waitTime: 1000 # Default is空
  heartBeatInterval: 10 # Default is10，unit为秒
```

buildconsulServe集群of方式开发者可以[refer to](https://learn.hashicorp.com/tutorials/consul/docker-compose-datacenter)。Similarly,silky框架提供了use docker-compose deploy **Consul** 集群ofArrange files，开发者exist获取源码后,Enter `./samples/docker-compose/infrastr` after the directory,执行like下命令,can build a **Consul** Serve集群。

```powershell
docker-compose -f docker-compose.consul.yaml up -d
```