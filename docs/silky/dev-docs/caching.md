---
title: cache
lang: zh-cn
---

## cache拦截

existsilkyin the frame,to improverpcCommunication performance，usecache拦截的设计,reduce networkiooperate,Improve the performance of distributed applications。

要想usecache拦截,必须要exist将Serve治理Attributes的`Governance.CacheEnabled`Set as`true`(The default value of this property is`true`)。if将该Attributes值quiltSet as`false`，Soexistrpcduring communication,cache拦截将无效。

cache拦截exist应用Serve接口方法上通过cache拦截特性进行设置,existsilkyin the frame,存exist如下三中类型的cache特性，分别对数据cache进行新增、renew、delete。

### 设置cache特性--GetCachingInterceptAttribute

existrpcduring communication,if通过cache的`Key`exist分布式cache中能够从分布式cache中命中相应的cache数据,So就就直接从分布式cache中间件中获取数据,and returned to the service caller。if没有命中cache数据,Then the data will be obtained from the service provider through network communication,并且将返回的数据新增到cache中间件,exist下次rpcduring communication,就可以直接从cache中间件中获取数据,so as to reduceiooperate,Improve the performance of distributed applications。

设置cache特性方式如下所示:

```csharp
// exist应用Serve接口方法中通过`GetCachingIntercept`设置cache拦截
[GetCachingIntercept("name:{0}")]
Task<TestOut> Create(TestInput input);


// Placeholders for template parameters are set via input parameters 
public class TestInput  
{
    [CacheKey(0)] 
    [HashKey] 
    [Required(ErrorMessage = "Name is not allowed to be empty")]
    public string Name { get; set; }

    [Required(ErrorMessage = "Address is not allowed to be empty")]
    public string Address { get; set; }

    [Phone(ErrorMessage = "Mobile number format is incorrect")] 
    public string Phone { get; set; }
    
}
```
`GetCachingInterceptAttribute`特性存exist一个参数--`keyTemplete`，The parameter is a string type,String placeholders can be specified for this string,index from0start calculating，The value of the placeholder is passed`CacheKeyAttribute`exist输入参数中specify。If it is a parameter of a simple type,直接exist参数名前面通过`[CacheKey(keyTempleteIndex)]`(`keyTempleteIndex`refers to the index of the placeholder)specify,If the parameter type is a complex parameter,is identified by the properties of the attribute。实际上生成的cache的`key`will be based on`keyTemplete`and the set of input parameters`[CacheKey(keyTempleteIndex)]`value and return result type(`ReturnType`)mutual confirmation。

besides，`GetCachingInterceptAttribute`特性还存exist一个`OnlyCurrentUserData`Attributes，if当前cache数据只与当前登录用户相关,then need to`OnlyCurrentUserData`Attributes值Set as`true`,生成的cache的`key`will add the current userid。

::: warning

have to be aware of is,if`OnlyCurrentUserData`Attributes值Set as`true`,need to ensure,The current interface requires user login authentication。

:::

### renewcache特性--UpdateCachingInterceptAttribute

existrpcduring communication，对数据进行renewoperate后,为保证cacheconsistent性,同时需要对cache数据进行renewoperate。Developers can use`UpdateCachingInterceptAttribute`特性对cache数据进行renew。`UpdateCachingInterceptAttribute`特性的参数与Attributes与`GetCachingInterceptAttribute`consistent,用法基本上也consistent。

with being`GetCachingInterceptAttribute`The difference between the application service interface of the feature annotation is that,existrpcduring communication,quilt`UpdateCachingInterceptAttribute`The method identified by the attribute will be executed,只是将Serve提供者返回的结果renew到cacheServe中。quiltrenew的cache数据与生成的cache的`key`and the return result type(`ReturnType`)related。

```csharp
// exist应用Serve接口方法中通过`GetCachingIntercept`设置cache拦截
[UpdateCachingIntercept("name:{0}")]
Task<TestOut> Update(UpdateTestInput input);

// Placeholders for template parameters are set via input parameters 
public class UpdateTestInput  
{
    public long Id { get; set; }

    [CacheKey(0)] 
    [HashKey] 
    [Required(ErrorMessage = "Name is not allowed to be empty")]
    public string Name { get; set; }

    [Required(ErrorMessage = "Address is not allowed to be empty")]
    public string Address { get; set; }

    [Phone(ErrorMessage = "Mobile number format is incorrect")] 
    public string Phone { get; set; }
    
}
```


### deletecache特性--RemoveCachingInterceptAttribute

existrpcduring communication，对数据进行deleteoperate后,为保证cacheconsistent性,同时需要对cache数据进行deleteoperate。Developers can use`RemoveCachingInterceptAttribute`特性对cache数据进行移除operate。

`RemoveCachingInterceptAttribute`特性除了specify`keyTempalte`outside of template parameters,还需要specifycache名称(`CacheName`),normally,existcache拦截中,`CacheName`与要cache数据(which is:`ReturnType`)的类的完全限定名consistent。

```csharp
[RemoveCachingIntercept("ITestApplication.Test.Dtos.TestOut", "name:{0}")]
[Transaction]
Task<string> Delete([CacheKey(0)] string name);
```

## 分布式cache接口

silky框架提供分布式cache接口`IDistributedCache<T>`,通过分布式cache接口实现对cache数据的增删改查。in,Generics`T`指的是cache数据的类型。exist分布式cache中,`T`与应用Serve接口方法定义的返回值类型consistent。

分布式cache接口可以通过构造器注入,existuse分布式cache接口是,必须要specifyGenerics参数`T`。

```csharp
public class TestAppService : ITestAppService
{

    private readonly IDistributedCache<TestOut> _distributedCache;
    public TestAppService(IDistributedCache<TestOut> distributedCache)  
    {
        // 通过构造器注入分布式cache接口后,可以通过该接口实现对该cache数据的增删改查。
        _distributedCache = distributedCache;
    }
}
```

### cache数据的consistent性

cacheconsistent性的概念: cacheconsistent性的本质是数据consistent性。in other words,就是existcacheServe中的数据与数据库中存储的数据要保证数据consistent性。开发者exist开发过程中,要特别注意对cache数据的consistent性。That is to say,if对某个类型的数据进行cache,So，对其进行renew、deleteoperate时,需要同时对cacheServe中的cache数据进行renew或是、deleteoperate。

existrpcduring communication,usecache拦截,同一数据的cache依据可能会不同(set`KeyTemplate`,E.g:cache依据可能will be based on`Id`、`Name`、`Code`分别进行cache),从而产生不同的cache数据,但是exist对数据进行renew、deleteoperate时,due to not being able to pass`RemoveCachingInterceptAttribute`特性一次性delete该类型数据的所有cache数据,at this time，exist实现业务代码过程中,就需要通过分布式cache接口`IDistributedCache<T>`实现cache数据的renew、deleteoperate。

cache数据的renew、Hit as shown below:

![caching1.png](/assets/imgs/caching1.png)

![caching2.png](/assets/imgs/caching2.png)

## useredis作为cache中间件

silky框架默认use`MemoryCaching`作为cache,但是if开发者需要将微Servecluster部署到多台Serve器,So您需要use`redis`Serve作为cacheServe。

silkyuseredis作为分布式cacheServe非常简单,You only need to provide aredisServe(cluster),通过配置文件就可以将cache中间件替换为redisServe。

The configuration looks like this:

```yml
distributedCache:
  redis:
    isEnabled: true
    configuration: 127.0.0.1:6379,defaultDatabase=0 //rediscache链接配置
```