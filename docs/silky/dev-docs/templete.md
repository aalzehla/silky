---
title: scaffold
lang: zh-cn
---

## Introduction to Templates

use `dotnet new` command can create[template](https://docs.microsoft.com/zh-cn/dotnet/core/tools/custom-templates),也就是我们常说ofscaffold工具。silky框架提供了两种类型oftemplate,开发者可以选择合适oftemplate构建微Serve应用。

## 构建独立应用oftemplate[Silky.App.Template](https://www.nuget.org/packages/Silky.App.Template/)

If the developer needs independent development、Manage microservice applications(Put microservice applications in a separate warehouse for management),可以use[Silky.App.Template](https://www.nuget.org/packages/Silky.App.Template/)template构建微Serve应用。

1. Install **Silky.App.Template** template

```powershell
dotnet new --install Silky.App.Template::3.0.3
```

2. Create a microservice application

Create a new microservice application with the following command：

```powershell
dotnet new silky.app --hosttype webhost -p:i --includeinfrastr -n Demo
```

**Silky.App.Template** template参数:

| short command | long command | illustrate  | Default value |
|:-----|:-----|:------|:-----|
| -r|--rpcToken | set uprpctoken | ypjdYOzNd4FwENJiEARMLWwK0v7QUHPW |
| -rp|--rpcPort | set uprpcport | 2200 |
| -re|--registrycentertype | Service registry type | Zookeeper |
| -p:r|--registrycenterconnections | Service registration center link address | 127.0.0.1:2181,127.0.0.1:2182,127.0.0.1:2183;127.0.0.1:2184,127.0.0.1:2185,127.0.0.1:2186 |
| -p:re|--redisenable | it's usable or notredisServe | true |
| | --redisconfiguration | redisServeconfigure |  127.0.0.1:6379,defaultDatabase=0 |
| -d | --dockersupport | supportdocker |  true |
| -do | --dotnetenv | set up运行开发环境 |  Development |
| -in | --includeinfrastr | 是否包含基础ServeArrange files |  true |
|  -p:i | --includesln | Whether to include resolving access files |  true |
|  -p:d | --dockernetwork | set updocker network |  silky_service_net |
|  -ho | --hosttype | set up主机类型:  webhost, generalhost ,websockethost, gateway |  webhost |

use **Silky.App.Template** templatecreateof微Serve应用ofTable of contents结构:

```
.
├─docker-compose          // docker-composeArrange files
│  ├─Demo                 // Demo微Serve应用Serve编排
│  └─infrastr             // 基础ServeArrange files
│      └─sql
└─src                     // source code directory
    ├─Demo.Application    // application layer
    │  └─System
    ├─Demo.Application.Contracts  // Application interface layer,用于定义Serve,可被其他微Serve应用引用
    │  └─System
    │      └─Dtos
    ├─Demo.Database.Migrations   // Data Migration Project(belong to the infrastructure layer),for storageefMigration files
    ├─Demo.Domain                // Domain layer,Implement core business applications
    ├─Demo.Domain.Shared         // Domain Shared Layer,for storage通用of值类型,enum type,可被其他微Serve应用
    ├─Demo.EntityFrameworkCore   // data access layer(belong to the infrastructure layer),provided byefcoreProvide data access capability
    │  └─DbContexts
    └─DemoHost                   // host project,for application boarding,manage应用Serve生命周期
        └─Properties             // Application launch configuration
```

3. Startup project

Enter **./docker-compose/infrastr** Table of contents,Created with the following command`zookeeper`and`redis`Serve:

```powershell
# Create a namedsilky_service_netofdockernetwork
docker network create silky_service_net

# usedocker-composecreatezookeeperandredisServe
docker-compose -f docker-compose.zookeeper.yml -f docker-compose.redis.yml up -d
```

usevisual studio or rider Open Demo.sln solution,Will **DemoHost** set up为Startup project,After restoring the project,according to`F5`Startup project。

After the project starts,通过浏览器Open地址 `https://localhost:5001/index.html`, 即可OpenswaggerOnline document address:

![templete1.png](/assets/imgs/templete1.png)


## 构建模块化应用oftemplate[Silky.Module.Template](https://www.nuget.org/packages/Silky.Module.Template)

如果开发者Will所有of微Serve应用统一开发、manage(Will所有微Serve应用存放在一个仓库中集中manage)，可以use[Silky.Module.Template](https://www.nuget.org/packages/Silky.Module.Template)template构建微Serve应用。

1. Install **Silky.Module.Template** template

```powershell
dotnet new --install Silky.Module.Template::3.0.3
```

2. Create a microservice application

```powershell
dotnet new silky.module --hosttype webhost -p:i --includeinfrastr --newsln -n Demo
```

**Silky.Module.Template** template参数:

| short command | long command | illustrate  | Default value |
|:-----|:-----|:------|:-----|
| -r|--rpcToken | set uprpctoken | ypjdYOzNd4FwENJiEARMLWwK0v7QUHPW |
| -rp|--rpcPort | set uprpcport | 2200 |
| -re|--registrycentertype | Service registry type | Zookeeper |
| -p:r|--registrycenterconnections | Service registration center link address | 127.0.0.1:2181,127.0.0.1:2182,127.0.0.1:2183;127.0.0.1:2184,127.0.0.1:2185,127.0.0.1:2186 |
| -p:re|--redisenable | it's usable or notredisServe | true |
| | --redisconfiguration | redisServeconfigure |  127.0.0.1:6379,defaultDatabase=0 |
| -d | --dockersupport | supportdocker |  true |
| -do | --dotnetenv | set up运行开发环境 |  Development |
| -in | --includeinfrastr | 是否包含基础ServeArrange files |  true |
|  -p:i | --includesln | Whether to include resolving access files |  true |
|  -p:d | --dockernetwork | set updocker network |  silky_service_net |
|  -ho | --hosttype | set up主机类型:  webhost, generalhost ,websockethost, gateway |  webhost |
|  -ne | --newsln|  是否是一个新ofsolution |  false |

use **Silky.Module.Template** templatecreateof微Serve应用ofTable of contents结构:

```
.
├─docker-compose               // docker-composeArrange files
│  ├─Demo                      // Demo微Serve应用Serve编排
│  └─infrastr                  // 基础ServeArrange files
│      └─sql
└─microservices                // 各个微Serve应用模块      
    └─Demo                     // Demo微Serve应用
        ├─Demo.Application     // application layer
        │  └─System
        ├─Demo.Application.Contracts   // Application interface layer,用于定义Serve,可被其他微Serve应用引用
        │  └─System
        │      └─Dtos
        ├─Demo.Database.Migrations     // Data Migration Project(belong to the infrastructure layer),for storageefMigration files
        ├─Demo.Domain                  // Domain layer,Implement core business applications
        ├─Demo.Domain.Shared           // Domain Shared Layer,for storage通用of值类型,enum type,可被其他微Serve应用
        ├─Demo.EntityFrameworkCore     // data access layer(belong to the infrastructure layer),provided byefcoreProvide data access capability
        │  └─DbContexts
        └─DemoHost                     // host project,for application boarding,manage应用Serve生命周期
            └─Properties               // Application launch configuration
```

3. 新增一个微Serve应用模块

```powershell
dotnet new silky.module --hosttype webhost -p:i -n Demo1
```

Will新createof微Serve应用从 **Demo1/microservices/Demo1** copy to **Demo/microservices/Demo1**,  **Demo1/docker-compose/Demo1** copy to **Demo/docker-compose/Demo1**,并Will新模块of微Serve应用添加到solution中。

![templete2.png](/assets/imgs/templete2.png)

4. debug

如果开发者需要同时start updebug多个微Serve,need to be updated`rpc:port`configure(`rpc:port`cannot be repeated),and by updating`launchSettings.json`更新应用Servestart upofhttpServe地址。WillStartup projectset up为 **多个Startup project**,Will **Demo1Host** and **DemoHost** set up为 **start up**。so, 我们就可以同时debug **Demo1Host** and **DemoHost** These two applications。

![templete3.png](/assets/imgs/templete3.png)

![templete4.png](/assets/imgs/templete4.png)

![templete5.png](/assets/imgs/templete5.png)