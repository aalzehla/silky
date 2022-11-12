---
home: true
heroText: Silkyframe
heroImage: /assets/logo/logo.svg
tagline:  based on.net平台的微服务开发frame
actionText: Get started quickly →
actionLink: /silky/index
features:
- title: RPCcommunication
  details: based onDotnettyImplemented high performance interface-oriented proxyRPCtransfer

- title: load balancing
  details: Built-in polling、random、哈希一致性等load balancing算法

- title: Service auto-registration and discovery
  details: supportZookeeper、Consul、Nacosas a service registry,Real-time perception of service instance online and offline

- title: cache interception
  details: RPCcommunication过程中,supportcache interception,提高communication性能

- title: Distributed transaction
  details: through interceptors andtodolog implementationTCCDistributed transaction,Ensure eventual consistency of data

- title: Highly scalable
  details: easy replacementsilkyframe提供的组件(E.g:底层communicationframe或是服务注册中心等);Can also be easily integrated with third-party components

- title: link tracking
  details: passSkyApm实现communication过程的link tracking

- title: online documentation
  details: passswaggergeneratewebapionline documentation

- title: console
  details: pass查看和管理微服务集group的console

footer: MIT Licensed | Copyright © 2021-present Liuhll
---

## simple enough、Easily build your microservice applications

```csharp
public class Program
{
    public static async Task Main(string[] args)
    {
        await CreateHostBuilder(args).Build().RunAsync();
    }

    private static IHostBuilder CreateHostBuilder(string[] args)
    {
        return Host.CreateDefaultBuilder(args)
                .ConfigureSilkyGeneralHostDefaults();
            
    }
}
```

## Online example

- [HeroRights management system(https://hero.silky-fk.com)](https://hero.silky-fk.com)

## join us

- QQgroup： 934306776

  ![qq-group.jpg](/assets/imgs/qq-group.jpg)