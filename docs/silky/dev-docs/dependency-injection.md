---
title: dependency injection
lang: zh-cn
---

## 什么是dependency injection

Put dependent classes in the container，Instances of these classes are resolved when used，就是dependency injection。The purpose is to achieve decoupling of classes。

## inject the object intoioccontainer way

1. pass in the module`RegisterServices()`method`ContainerBuilder`registration service
  
   If the developer customizes the custom module,可以pass模块of定义类registration service，Defining classes through modulesioccontainerregistration service,The essence is throughAutofacframed`ContainerBuilder`Implement service registration。

   normally,developers unless custom modules,否则不建议pass该方式registration service。
  
   E.g:
  
   ```csharp
     public class MessagePackModule : SilkyModule
     {
         protected override void RegisterServices(ContainerBuilder builder)
         {
             builder.RegisterType<MessagePackTransportMessageDecoder>().AsSelf().AsImplementedInterfaces().InstancePerDependency();
             builder.RegisterType<MessagePackTransportMessageEncoder>().AsSelf().AsImplementedInterfaces().InstancePerDependency();
         }
     }  
   ```

2. by inheritance`IConfigureService`or`ISilkyStartup`,pass`Configure()`method`IServiceCollection`registration service
  
   by inheritance`IConfigureService`or`ISilkyStartup`How to implement service registration,essence is to.netself-containedioccontainer`IServiceCollection`registration service,After the app starts,.netself-containedioccontainer与autofacofioccontainer会同步更新Serve注册信息和Serve依赖关系。
   
   normally,except with third-party packages(frame)Integrate,E.g开发者需要use[CAPframe](https://github.com/dotnetcore/CAP)or[MediatR](https://github.com/jbogard/MediatR)implement message communication,第三方Bagpass`IServiceCollection`注册相关Serveof情况下,开发者可以pass该方式引入第三方ofBag(frame)ofServe注册。
   
   when the app starts,silkyframe会自动扫描继承了`IConfigureService`接口of类型,并pass`Order`属性对所有of实现类进行排序,execute in sequence`ConfigureServices()`method,pictureioccontainer注册相关Serve。
   
   E.g,The following code:
  
  ```csharp
  // need to be installedCAPframe相关ofBag
  public class CapConfigService : IConfigureService
  {
     public void ConfigureServices(IServiceCollection services, IConfiguration configuration)
     { 
        services.AddDbContext<AppDbContext>(); //Options, If you are using EF as the ORM
        services.AddSingleton<IMongoClient>(new MongoClient("")); //Options, If you are using MongoDB
        services.AddCap(x =>
        {
           // If you are using EF, you need to add the configuration：
           x.UseEntityFramework<AppDbContext>(); //Options, Notice: You don't need to config x.UseSqlServer(""") again! CAP can autodiscovery.

           // If you are using ADO.NET, choose to add configuration you needed：
           x.UseSqlServer("Your ConnectionStrings");
           x.UseMySql("Your ConnectionStrings");
           x.UsePostgreSql("Your ConnectionStrings");

           // If you are using MongoDB, you need to add the configuration：
           x.UseMongoDB("Your ConnectionStrings");  //MongoDB 4.0+ cluster

           // CAP support RabbitMQ,Kafka,AzureService as the MQ, choose to add configuration you needed：
           x.UseRabbitMQ("ConnectionString");
           x.UseKafka("ConnectionString");
           x.UseAzureServiceBus("ConnectionString");
           x.UseAmazonSQS();
       });
     }
     public int Order { get; } = 2;
  }
  ```
  
::: warning

For ordinary microservices,本身底层ofrpc通信是pass`dotnetty`frame,usetcpprotocol进行通信of。Serve于Serve之间of通信,并不passhttpprotocol。所以在与第三方frameIntegrate时,普通微Serve应用无法use第三方framedhttpmiddleware,usehttpmiddlewareofframe也无法与普通微Serve应用Integrate。

:::

3. by inheritancedependency injection标识接口Implement service registration(**recommend**)

   silkyframe提供了三个依赖注册of相关标识接口：`ISingletonDependency`(singleton pattern)、`IScopedDependency`(Regional mode)、`ITransientDependency`(Transient mode)。When the microservice application starts,会扫描继承了这些标识接口of类(Serve),并将其自身和继承of接口注册到Ioccontainer中。
   
   Developers generally,如果需要将自定义of一个类注册到ioccontainer时,可以选择继承相应of标识接口。E.g:自定义of领域Serve、warehouse; etc.。
   
   ```csharp
   public class AccountDomainService : ITransientDependency, IAccountDomainService
   {
   }
   ```

## 获取Serve对象

1. pass构造注入of方式获取Serve对象

   pass以上三种方式被注册ofofServe都可以pass构造注入of方式获取Serve实例对象。
   
   在应用其他微Serve应用of应用接口层(Bag)back,应用Serve接口是pass构造注入of方式获取动态代理对象。
   
   ```csharp
    public class AccountAppService : IAccountAppService
    {
        private readonly IAccountDomainService _accountDomainService;

        public AccountAppService(IAccountDomainService accountDomainService)
        {
            _accountDomainService = accountDomainService;
        }
    }
   ```

3. pass属性注入of方式获取Serve对象

   by inheritance标识接口registration serviceof类,也支持属性注入of方式获取Serve对象。如果pass自定义模块类registration serviceof时候,也可以指定支持pass属性of方式获取Serve。
   
   pass属性of方式获取Serve,设置of属性必须为`public`类型of,and must have`get`、`set`method。
   
   generally,在获取日志记录类of实例时,To avoid null pointer exceptions when executing test cases，可以pass该种方式获取日志记录类of实例。

   E.g:
   
   ```csharp
   public class DotNettyRemoteServiceInvoker : IRemoteServiceInvoker
   {

      private readonly ServiceRouteCache _serviceRouteCache;
      private readonly IRemoteServiceSupervisor _remoteServiceSupervisor;
      private readonly ITransportClientFactory _transportClientFactory;
      private readonly IHealthCheck _healthCheck;
      
      public ILogger<DotNettyRemoteServiceInvoker> Logger { get; set; } //可以pass属性注入of方式替换默认of日志记录实例
      
      public DotNettyRemoteServiceInvoker(ServiceRouteCache serviceRouteCache,
            IRemoteServiceSupervisor remoteServiceSupervisor,
            ITransportClientFactory transportClientFactory,
            IHealthCheck healthCheck)
        {
            _serviceRouteCache = serviceRouteCache;
            _remoteServiceSupervisor = remoteServiceSupervisor;
            _transportClientFactory = transportClientFactory;
            _healthCheck = healthCheck;
            Logger = NullLogger<DotNettyRemoteServiceInvoker>.Instance; //在构造器中设置一个默认of日志记录类实例 
        }
   }
   ```

3. passServe引擎`IEngine`解析Serve实例
   
   silkyframeself-containedServe引擎提供Serve解析method,可以passServe引擎解析获取Serve实例对象。
   
   E.g:
   
   ```csharp
    var accountDomainService = EngineContext.Current.Resolve<IAccountDomainService>();
    ```
