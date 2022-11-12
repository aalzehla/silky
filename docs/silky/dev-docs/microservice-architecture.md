---
title: Best Practices for Microservices Modular Architecture & convention
lang: zh-cn
---

## Solution structure

- **recommend** Create a separate solution for each microservice application module。
- **recommend** Name the solution as*CompanyName.MicroServiceName*(forSilkyFor core modules,It's named the way it is*Silky.ModuleName*)。
- **recommend** A per-microservice application is developed as a layered project,because it has several packages(project)are interrelated。

## Floor(layers) & Bag(packages)

### 业务微服务应用模块of分Floor

下图展示了一个普通微服务应用模块ofBag以及他们之间ofrely关系:

![silky-project.png](/assets/imgs/silky-project.png)

The ultimate goal is to allow applications to be developed and referenced by other microservice application modules in a flexible way。Example application。

#### 领域Floor

- **recommend** 将领域Floor划分为两个project:
   - **Domain.Shared** Bag(project) named*CompanyName.MicroServiceName.Domain.Shared*,Bag含常量,enums and other types, 它不能Bag含实体,repository,Domain service or any other business object. 可以安全地与模块中of所有Floor使用. 此Bag也可以与第三方客户端使用。
   - **Domain** Bag(project) named*CompanyName.MicroServiceName.Domain*, Bag含实体, Repository interface,Domain service interfaces and their implementations and other domain objects。
      - **Domain** Bagdepends on *Domain.Shared* Bag。
      - **Domain** Bag可以rely自身微服务应用模块of *Application.Contracts* Bag。
      - **Domain** Bag可以根据实际of业务关系,通过project引用或是nugetBagof方式rely其他微服务模块of *Application.Contracts* Bag。

#### 应用服务Floor
- **recommend** 将应用服务Floor划分为两个project:
   - **Application.Contracts** Bag(project) named*CompanyName.MicroServiceName.Application.Contracts*,Bag含应用服务接口and相关of数据传输right象(DTO)。
      - **Application contract** Bagdepends on *Domain.Shared* Bag。
   - **Application** Bag(project)named*CompanyName.MicroServiceName.Application*,Bag含应用服务实现。
      - **Application** Bagdepends on *Domain* Bagand *Application.Contracts* Bag。

#### 基础设施Floor
- **recommend** For each oform/数据库集成创建一个独立of集成Bag, for exampleEntity Framework Core and MongoDB。
  - **recommend** E.g, create an abstractionEntity Framework CoreIntegratedCompanyName.MicroServiceName.EntityFrameworkCore Bag。 ORM 集成Bagdepends on *Domain* Bag。
  - **不recommend** depends onorm/数据库集成Bag中of其他Floor。
  - **recommend** If usingcode firstway to develop,recommend为project迁移简历一个单独ofproject(Bag)。
- **recommend** For each of主要of库创建一个独立of集成Bag, 在不影响其他Bagof情况下可以被另一个库替换。

#### HostFloor
- **recommend** 创建named**CompanyName.MicroServiceNameHost**of **Host** Bag。Used to host the microservice application itself。
- **must** rely **Application** Bag(project) 只有rely了**Application** Bag,service application method(service entry)才会生成相应of路由信息,and register with the service registry。

### 网关应用模块of分Floor

For gateway applications,并不需要实现具体of业务代码,但是可以在网关应用自定义或是引入第三方框架of中间件。网关应用需要rely其他微服务应用模块of **Application.Contracts** Bag(project)。

下图展示了网关应用ofrely关系：

![silky-gateway-project.png](/assets/imgs/silky-gateway-project.png)

#### HostFloor
- **recommend** 创建named**CompanyName.MicroServiceNameGatewayHost**of **Host** Bag。Used to host the gateway application itself。
- **must** 网关project需要通过rely其他微服务应用模块of**Application Contract** Bag。
- **recommend** If the developer needs to customize the middleware,开发将其封装为一个单独ofBag。
- **recommend** Third-party middleware can also be referenced in gateway applications,righthttpRequest for unified processing。