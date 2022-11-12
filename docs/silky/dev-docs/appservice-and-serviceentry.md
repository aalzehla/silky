---
title: Application Services and Service Entries
lang: zh-cn
---

## Definition of Service

Service interface is the basic unit of microservice definition service，The defined application service interface can be referenced by other microservices，other microservices viarpcThe framework communicates with the microservice。

pass`ServiceRouteAttribute`A feature identifies an interface to become an application service interface。

E.g:

```csharp
[ServiceRoute]
public interface ITestAppService
{
}

```

虽然我们passuse`[ServiceRoute]`A feature can identify any interface as a service，服务定义of方法会pass应用服务of模板和方法特性of模板生成对应ofwebapi(This method has no service feature identified as disabled external network)。But a good naming convention can save us a lot of unnecessary trouble when building services(In layman's terms:**Agreed about configuration**)。

normally,We recommend using`AppService`as a suffix for the defined service。i.e. it is recommended to use`IXxxxAppService`as the application service interface name,The default generated service template is:`api/{appservice}`,use`XxxxAppService`as the name of the application service implementation class。

Routing Features(`ServiceRouteAttribute`)可bypass`template`Set up the service routing template。路由模板可bypass`{appservice=templateName}`Set the name of the service。

| property name | illustrate   |  Default value  | 
|:---------|:----- |:--------| 
| template | When identifying a service interface as a service route,可bypass`[ServiceRoute("{appservice=templateName}")]`Specifies the route template for the application service interface。`templateName`ofDefault value名称为对应服务of名称 | api/{appservice} | 



## service entry

**service entry(ServiceEntry)**: 服务接口中定义of每一个方法都会生成微服务集群of一个service entry。For the microservice application itself,service entry就相当于MVCmiddle`Action`，application service is equivalent to`Controller`。

### 根据service entry生成WebAPI

The application interface iswebAfter host application or gateway reference,会根据服务应用接口of路由模板和service entry方法ofHttpThe routing information specified by the verb attribute or the method name generates the correspondingwebapi,service entry生成ofWebAPIsupportrestfulAPIstyle。


service entry生成webapiThe rule is:

1. 禁止集群外部访问ofservice entry(`[Governance(ProhibitExtranet = true)]`)won't generatewebapi;

2. 可bypass` [ServiceRoute("{appservice=templateName}")]`Specify a unified routing template for the application interface;

3. like果service entry方法没有被httppredicate attribute identifier,then generatedwebapiofhttprequest verb会根据service entryofmethod name生成,like果没有匹配到相应ofservice entry方法,则会根据service entryof方法参数；

4. service entry方法可bypasshttppredicate attribute to identify,andhttppredicate feature还support对service entry指定路由模板,路由模板of指定还support对路由参数进行约束;

```

service entry生成ofwebAPI = Applying Interface Entry Routing Templates + “method name||Http特性指定ofRouting Features”

```

if not existHttppredicate attribute identifier情况下,生成ofwebapi路由illustrate(E.g,The application interface name is:`ITestAppService`,The route template has not been rewritten):

| method name | 生成ofwebAPIpath | Httprequest verb |
|:--------|:---------------|:------------|
| GetXXX | /api/test | get |
| SearchXXX | /api/test/search | get|
| CreateXXX| /api/test | post |
| UpdateXXX | /api/test | put | 
| DeleteXXX | /api/test | delete | 

existHttppredicate case,生成ofwebapiofrequest verb会根据service entry标识ofhttppredicate properties to determine，开发者还可bypasshttppredicate feature为service entryofof路由进行调整,andsupport路由参数of形式,E.g:

| method name | 生成ofwebAPIpath | httppredicate feature | Httprequest verb |
|:--------|:---------------|:------------|:------------|
| GetXXX | /api/test/{id} | [HttpGet("{id:strig}")] | get |
| DeleteXXX | /api/test/name/{name} | [HttpDelete("name/{name:strig}")] | delete |
| UpdateXXX | /api/test/email | [HttpPatch("email")] | patch |
| CreateXXX | /api/test/user | [HttpPost("user")] | post |


### service entryof治理特性

开发者可bypass配置文件对service entryof治理进行统一配置,除此之外可bypass`Governance`特性为service entry方法进行标识,pass其Attributes对service entry进行治理。pass`Governance`特性对service entry方法注解后,service entryof治理Attributes将会被该特性重写。

service entry治理ofAttributesPlease refer to[service entry治理Attributes配置](#)。

### cache interception

exist服务The application interface is其他微服务应用引用后,可bypasscache interception特性(`GetCachingIntercept`)existrpcduring communication,Read data directly from the distributed cache,避免pass网络通信,while improving system performance。

More关于cache interceptionPlease refer to[cache interception](caching)。

### service entryof例子

```csharp
    [ServiceRoute]
    public interface ITestAppService
    {
        ///  Add interface test([post]/api/test)
        [GetCachingIntercept("name:{0}")]
        Task<TestOut> Create(TestInput input);

        ///  Update interface test([put]/api/test)
        Task<string> Update(TestInput input);

        /// delete interface test([delete]/api/test)
        [RemoveCachingIntercept("ITestApplication.Test.Dtos.TestOut", "name:{0}")]
        [Transaction]
        Task<string> Delete([CacheKey(0)] string name);

        /// Query interface test([get]/api/test/search)
        [HttpGet]
        Task<string> Search([FromQuery] TestInput query);

        /// Submit data in form format([post]/api/test/form)
        [HttpPost]
        string Form([FromForm] TestInput query);

        /// passnameGet a single piece of data([get]/api/test/{name:string},bypathparameter passing,and constrain the parameter type to bestring)
        [HttpGet("{name:string}")]
        [Governance(ShuntStrategy = AddressSelectorMode.HashAlgorithm)]
        [GetCachingIntercept("name:{0}")]
        Task<TestOut> Get([HashKey] [CacheKey(0)] string name);

        /// passidGet a single piece of data([get]/api/test/{id:long},bypathparameter passing,and constrain the parameter type to belong)
        [HttpGet("{id:long}")]
        [Governance(ShuntStrategy = AddressSelectorMode.HashAlgorithm)]
        [GetCachingIntercept("id:{0}")]
        Task<TestOut> GetById([HashKey] [CacheKey(0)] long id);

        ///Update some data，usepatchask ([patch]/api/test)
        [HttpPatch]
        Task<string> UpdatePart(TestInput input);
    }

```

## 服务of实现

normally,开发者应当将定义服务of接口和服务of实现分开定义exist不同of程序集。Application service interface程序集可by被打包成Nuget包或是byprojectof形式被其他微服务应用引用，这样其他微服务就可bypassrpc代理of方式与该微服务应用进行通信。MoreRPC通信方面of文档[Please refer to](rpc)。

一个服务接口可by有一个或多个实现类。只有应用接口exist当前微服务应用中exist实现类,该微服务应用接口对应ofservice entry才会生成相应of服务路由，And register the service routing information to the service registry,同时其他微服务应用ofexample会订阅到微服务集群of路由信息。

应用接口like果exist多个实现类of情况下,So应用接口of实现类,需要pass`ServiceKeyAttribute`feature to identify。`ServiceKeyAttribute`exist两个参数(Attributes)。


| property name | illustrate   |  Remark |
|:---------|:-----|:-----| 
| name | 服务实现类of名称 | passwebapiask时,passask头`serviceKey`make settings;rpcin communication,可bypass`ICurrentServiceKey`ofexample调整要askof应用接口实现。 |
| weight | Weights | like果during communication,not specified`serviceKey`,So,会askWeights最高of应用接口of实现类 |

example:

```csharp
/// Application service interface(like:可定义existITestApplication.csprojproject)
[ServiceRoute]
public interface ITestAppService
{
    Task<string> Create(TestInput input);
   // 其他service entry方法略
}


//------------------------------------//
/// 应用服务example类1 (like:可定义existTestApplication.csprojproject)
[ServiceKey("v1", 3)]
public class TestAppService : ITestAppService
{
  public Task<string> Create(TestInput input)
  {
      return Task.FromResult("create v1")
  }
  // Other interface implementation methods are omitted
}

//------------------------------------//
/// 应用服务example类2  (like:可定义existTestApplication.csprojproject)
[ServiceKey("v2", 1)]
public class TestV2AppService : ITestAppService
{
  public Task<string> Create(TestInput input)
  {
      return Task.FromResult("create v2")
  }
   // Other interface implementation methods are omitted
}

```

生成ofswagger文档like下:

![appservice-and-serviceentry1.jpg](/assets/imgs/appservice-and-serviceentry1.jpg)


existrpcduring communication,可bypass`IServiceKeyExecutor`ofexample设置要askof应用接口of`serviceKey`。

```csharp
private readonly IServiceKeyExecutor _serviceKeyExecutor;

public TestProxyAppService(ITestAppService testAppService,
    IServiceKeyExecutor serviceKeyExecutor)
{
    _testAppService = testAppService;
    _serviceKeyExecutor = serviceKeyExecutor;
}

public async Task<string> CreateProxy(TestInput testInput)
{
   return await _serviceKeyExecutor.Execute(() => _testAppService.Create(testInput), "v2");
}

```