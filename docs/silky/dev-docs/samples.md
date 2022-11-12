---
title: Example
lang: zh-cn
---

## Introduction

**Silky.SampleExampleproject** in the project root directory**samples**Under contents。mainly forsilkyLearn and demonstrate to users how to passsilkyFramework to quickly build a distributed microservice application。

**Silky.SampleExampleproject** Mainly consists of two parts:(1)is a business microservice application module;(2)Gateway application。

A business microservice consists of three independent microservice applications:

1. account(Account)Microservice Application Module

2. Order(Order)Microservice Application Module

3. in stock(Stock)Microservice Application Module

Three independent microservice applications mainly demonstratesilkyThe following features of the framework:

1. A sample project hierarchy for a microservice application

2. How to define a service application interface and implement the service application interface

3. how to passrpcCommunicate within a microservice cluster

4. How to use cache interception and use of distributed cache interface

5. pass下Order的方式演示了分布式事务的use

The gateway project mainly relies on the application of various microservice modules.**Application.Contract**Bag,So as to realize the external publication of the clusterwebapiinterface。

Developers can learnSilky.SampleExampleproject更好的学习and入门silkyframe。

Developers can use the blog post[passsilky.samplesfamiliarsilky微服务frame的use](/blog/silky-sample)and[silkyframe分布式事务useIntroduction](/blog/silky-sample-order)学习andfamiliarSilky.SampleExampleproject。

## 如何运行Exampleproject

1. Enter`silky\samples\docker-compose\infrastr`Table of contents,pass如下命令，WillSilky.SampleExampleproject需要依赖的基础服务**Zookeeper**and**redis**as well as**Mysql**up and running。

```powershell
docker-compose -f .\docker-compose.mysql.yml -f .\docker-compose.redis.yml -f .\docker-compose.zookeeper.yml up -d
```

2. Implement database migration

需要分别Enter各个微服务模块下的EntityFrameworkCoreproject(E.g:),Execute the following command:

```powershell
dotnet ef database update
```

3. usevisual studioorriderdevelopment mode

