---
title: Resolution of Services and Service Entries
lang: zh-cn
---

## concept

### Services that provide external access interfaces

here **Serve** as referred to in the preceding paragraph through`ServiceCollection` or through`ContainerBuilder`Implement dependency injection for each class as described **Serve注册** middleServeofconcept是不一样of。

referred to here **Serve** means in aSilkyin application,An interface that provides access to outside the application，it and traditionalMVC框架middle控制器concept相对应。

existsilkyin application,We add through the interface`[ServiceRoute]`characteristic,can be defined **应用Serve**,exist该Servemiddle定义ofmethod我们称之为 **Serve条目**, **Serve条目** 与之correspondingconcept是传统MVCmiddle **Action**,existsilkyin application,we passRPCtransferof方式与其他微Serve应用ofServe条目进行远程communication;

**应用Serve** The main properties of are as follows:

| property name | name  | Remark           |
|:-------|:------|:--------------|
| Id | ServeId  | 由该Serveof完全限定名generate |
| ServiceDescriptor | ServeDescriptor  | 将会以Serve元数据of方式注册到Serve注册middle心 |
| IsLocal | 是否是本地Serve  | exist该应用内是否存exist实现类,exist运行时确定是由本地Serve执行器执行还is throughRPCtransfer |
| ServiceType | 应用Servecorresponding类型`Type`  |  |
| ServiceProtocol | Serve协议  | 该ServecorrespondingServe协议 |
| ServiceEntries | 该Servecorresponding所有ofServe条目  | 该Serve定义of所有method |

**Serve条目**The main properties of are as follows:

| property name | name  | Remark           |
|:-------|:------|:--------------|
| Id | Serve条目Id  | 该Serve条目对应methodof完全限定名 + parameter name + correspondingHttprequest method name |
| ServiceId | ServeId  | correspondingServeId |
| ServiceType | 应用Servecorresponding类型`Type`  |
| IsLocal | 是否是本地Serve条目  | |  |
| MethodExecutor | method executor  | 该Serve条目对应methodofObjectMethodExecutor |
| FallbackProvider | Failed retry provider  | nullable,当Serve条目transfer失败后,The method through which you can get the configured failure callback |
| SupportedRequestMediaTypes | supportedhttprequested media type  |  |
| SupportedResponseMediaTypes |  supportedhttpresponse media type |  |
| Router |  correspondingHttprouting | 主要包括routing模板、correspondingHttpmethod、Httppath(WebAPI)、and parsinghttp Pathparameter  |
| MethodInfo |  correspondingmethod |   |
| ReturnType |  Serve条目对应method返回值of类型 |   |
| ParameterDescriptors |  parameterDescriptor | 用于描述和解释该methodcorrespondingparameter说明  |
| CustomAttributes |  该Serve条目所有ofcharacteristic |   |
| ClientFilters |  client filter |  非本地Serve条目exist执行RPC远程transfer时根据client filterof排序依次执行过滤器method |
| ClientFilters |  Serve端过滤器 |  本地Serve条目exist执行实现of业务method时根据Serve端过滤器排序依次执行过滤器method |
| AuthorizeData |  Authentication data |   |
| GovernanceOptions |  Serve条目exist执行过程middle实现Serve治理ofparameter配置 |  E.g: overtime time、load balancing strategy、是否允许Serve熔断、A non-business exception occurredNfuse after、Fusing time、number of retries、Whether to disable the external network; etc.  |
| ServiceEntryDescriptor |  Serve条目Descriptor | 该Serve条目correspondingServe条目Descriptor,将会以元数据of方式注册到Serve注册middle心  |






exist本节middle,我们主要叙述exist应用启动时,Silky是如何对应用内定义ofServe以及Serve条目进行解析。

## 应用Serveof解析

### 应用Servemanager

1. Silkyin the frame由Servemanager`ServiceManager`Responsible应用Serveof解析和Obtain,Servemanager被注册为 **singleton** of,That is to say,应用Servemanagerexist整个应用of生命周期middlewill only be created once,existServemanager`DefaultServiceManager`of构造器middle,将会transferServeproviderof所有实现类，解析应用Serve;

```csharp
public class DefaultServiceManager : IServiceManager
{
   private IEnumerable<Service> m_localServices; // 用于缓存本地应用Serve
   private IEnumerable<Service> m_allServices; // 用于缓存所有应用Serve

   public DefaultServiceManager(IEnumerable<IServiceProvider> providers) // existServemanagerof构造器注入所有ofServeprovider
   {
       UpdateServices(providers); // 使用注入所有ofServeprovider实现解析应用Serve
   }

   private void UpdateServices(IEnumerable<IServiceProvider> providers)
   {
       var allServices = new List<Service>();
       foreach (var provider in providers)
       {
           var services = provider.GetServices();
           allServices.AddRange(services);
       }
       if (allServices.GroupBy(p => p.Id).Any(p => p.Count() > 1))
       {
           throw new SilkyException(
               "There is duplicate service information, please check the service you set");
       }
       m_allServices = allServices.ToList();
       m_localServices = allServices.Where(p => p.IsLocal).ToList();
   }

// other codes...

}


```

从上面of代码of代码我们可以看出,pass遍历所有ofServeprovider,pass其method`provider.GetServices()`解析该Serveprovider规定of应用Serve;silky框架提供了默认ofServeprovider`DefaultServiceProvider`Identified`[ServiceRoute]`ofinterface进行解析；

certainly,开发者也可以根据需要对应用Serveprovider进行扩展(by implementing`IServiceProvider`interface),实现对开发者对自己定义of应用Serve进行解析;

passServeId进行分组判断应用Serve是否重复，exist一个用于middle,应用Serve时不允许重复of，如果定义了相同of应用Serveinterface,那么应用将会exist启动时抛出异常;

Servemanagermiddle定义了两个全局变量：一个用于缓存本地应用Serve,一个用于缓存所有of应用Serve; 

### 默认ofServeprovider

2. Silkyin the frame,pass默认ofServeprovider`DefaultServiceProvider`Scan the logo`[ServiceRoute]`ofinterface,then pass遍历所有ofServe类型,pass默认ofServegenerate器`DefaultServiceGenerator`实现创建应用Serveobject;

```csharp
public class DefaultServiceGenerator : IServiceGenerator
{
    private readonly IIdGenerator _idGenerator;
    private readonly ITypeFinder _typeFinder;
    private readonly IServiceEntryManager _serviceEntryManager;
    public DefaultServiceGenerator(IIdGenerator idGenerator,
        ITypeFinder typeFinder,
        IServiceEntryManager serviceEntryManager)
    {
        _idGenerator = idGenerator;
        _typeFinder = typeFinder;
        _serviceEntryManager = serviceEntryManager;
    }

   public IReadOnlyCollection<Service> GetServices()
   {
       var serviceTypes = ServiceHelper.FindAllServiceTypes(_typeFinder);
       if (!EngineContext.Current.IsContainHttpCoreModule()) // if not includedHttpCoreModulemodule,then ignore the identifier`[DashboardAppService]`of应用Serve
       {
           serviceTypes = serviceTypes.Where(p =>
               p.Item1.GetCustomAttributes().OfType<DashboardAppServiceAttribute>().FirstOrDefault() == null);
       }
       var services = new List<Service>();
       foreach (var serviceTypeInfo in serviceTypes)
       {
           services.Add(_serviceGenerator.CreateService(serviceTypeInfo));
       }
       if (EngineContext.Current.IsContainWebSocketModule())
       {
           var wsServiceTypes = ServiceHelper.FindServiceLocalWsTypes(_typeFinder);
           foreach (var wsServiceType in wsServiceTypes)
           {
               services.Add(_serviceGenerator.CreateWsService(wsServiceType));
           }
       }
       return services;
   }
}

// other codes...

```
pass上述of代码We saw：

2.1 passServe帮助者类提供of`ServiceHelper.FindAllServiceTypes(_typeFinder)`查找到所有of应用Serveof类型`serviceTypes`,if not includedHttpCoreModulemodule,then ignore the identifier`[DashboardAppService]`of应用Serve,然后遍历所有ofServe类型,passServegenerate器`_serviceGenerator.CreateService(serviceTypeInfo)`generate应用Serveobject;

查找应用Serve类型ofmethod`ServiceHelper.FindAllServiceTypes(_typeFinder)`As follows:

```csharp
public static IEnumerable<(Type, bool)> FindAllServiceTypes(ITypeFinder typeFinder)
{
    var serviceTypes = new List<(Type, bool)>();
    var exportedTypes = typeFinder.GetaAllExportedTypes();
    var serviceInterfaces = exportedTypes
            .Where(p => p.IsInterface
                        && p.GetCustomAttributes().Any(a => a is ServiceRouteAttribute)
                        && !p.IsGenericType
            )
        ;
    foreach (var entryInterface in serviceInterfaces)
    {
        serviceTypes.Add(
            exportedTypes.Any(t => entryInterface.IsAssignableFrom(t) && t.IsClass && !t.IsAbstract)
                ? (entryInterface, true)
                : (entryInterface, false));
    }
    return serviceTypes;
}
```

从上面of代码我们可以看到框架是如何扫描应用Serveof：First scan from the system all identified`[ServiceRoute]`characteristicofinterface,然后遍历所有ofinterface,分析出该interface是否存exist实现类,从而得到应用Serveof类型信息`(TypeInfo,IsLocal)`;


2.2 If the current application contains`WebSocketModule`,则passServe帮助类提供of`ServiceHelper.FindServiceLocalWsTypes(_typeFinder)`查找所有ofsupportwebsocketof`wsServiceTypes`,然后遍历所有of`wsServiceTypes`，then pass`_serviceGenerator.CreateWsService(wsServiceType)`generatewsServeobject；

Find supportwebsocketof应用Serveofmethod`ServiceHelper.FindServiceLocalWsTypes(_typeFinder)`As follows:

```csharp
public static IEnumerable<Type> FindServiceLocalWsTypes(ITypeFinder typeFinder)
{
    var types = typeFinder.GetaAllExportedTypes()
            .Where(p => p.IsClass
                        && !p.IsAbstract
                        && !p.IsGenericType
                        && p.GetInterfaces().Any(i =>
                            i.GetCustomAttributes().Any(a => a is ServiceRouteAttribute))
                        && p.BaseType?.FullName == ServiceConstant.WebSocketBaseTypeName
            )
            .OrderBy(p =>
                p.GetCustomAttributes().OfType<ServiceKeyAttribute>().Select(q => q.Weight).FirstOrDefault()
            )
        ;
    return types;
}
```
从上面ofmethod我们可以看出,a supportwebsocket应用Serve必须为一个类, 其interface需要pass`[ServiceRoute]`characteristiclogo,and must be derived from`Silky.WebSocket.WsAppServiceBase`；

### Servegenerate器(creator)

3. Servegenerate器`DefaultServiceGenerator`会passServe类型generate两种不同类型ofServe:

3.1 普通of应用Serve(able to passRPC协议与Serve内部实现communicationor throughHttpIntroduction to the protocol and communication with external implementations),普通of应用Servepass如下代码generate：

```csharp

public Service CreateService((Type, bool) serviceTypeInfo)
{
    var serviceId = _idGenerator.GenerateServiceId(serviceTypeInfo.Item1);
    var serviceInfo = new Service()
    {
        Id = serviceId,
        ServiceType = serviceTypeInfo.Item1,
        IsLocal = serviceTypeInfo.Item2,
        ServiceProtocol = ServiceHelper.GetServiceProtocol(serviceTypeInfo.Item1, serviceTypeInfo.Item2, true),
        ServiceEntries =  _serviceEntryManager.GetServiceEntries(serviceId)
    };
    serviceInfo.ServiceDescriptor = CreateServiceDescriptor(serviceInfo);
    return serviceInfo;
}

```

We saw，应用Serveof输入parameter是一个元组`(Type, bool) serviceTypeInfo`,元组of第一个parameter表示Serveof类型,第二个parameter表示是否是本地Serve;exist应用内实现了应用Serveinterface则表示是一个本地应用Serve，如果没有实现应用Serveinterface,则logo该Serve时一个远程应用Serve,exist使用该Serve提供ofmethod时，able to pass该Servegenerateof代理与具体ofServeprovider进行RPCcommunication;

We sawServeIdis throughIdgenerate器(`IIdGenerator`)generateof,ServeIdofgenerate规则是该Serve类型of完全限定名:

```csharp
public string GenerateServiceId(Type serviceType)
{
    Check.NotNull(serviceType, nameof(serviceType));
    return serviceType.FullName;
}
```

应用Serve所持有ofServe条目会根据Serve条目manager`IServiceEntryManager`passServeIdget,Serve条目如何generate;Serve条目如何generate,我们将会exist下一节进行叙述;

in，比较重要of一点就是如果pass应用ServegenerateServe条目Descriptor`ServiceDescriptor`,ServeDescriptor是一个POJOobject,可以被注册到Serve注册middle心,we pass`CreateServiceDescriptor(serviceInfo)`generate该ServecorrespondingServeDescriptor;

```csharp
        private ServiceDescriptor CreateServiceDescriptor(Service service)
        {
           
            var serviceBundleProvider = ServiceDiscoveryHelper.GetServiceBundleProvider(service.ServiceType);
            var serviceDescriptor = new ServiceDescriptor
            {
                ServiceProtocol = service.ServiceProtocol,
                Id = service.Id,
                ServiceName = serviceBundleProvider.GetServiceName(service.ServiceType),
                ServiceEntries = service.ServiceEntries.Select(p => p.ServiceEntryDescriptor).ToArray()
            };

            if (service.IsLocal)
            {
                var implementTypes = ServiceHelper.FindLocalServiceImplementTypes(_typeFinder, service.ServiceType);
                var serviceKeys = new Dictionary<string, int>();
                foreach (var implementType in implementTypes)
                {
                    var serviceKeyProvider = implementType.GetCustomAttributes().OfType<IServiceKeyProvider>()
                        .FirstOrDefault();
                    if (serviceKeyProvider != null)
                    {
                        if (serviceKeys.ContainsKey(serviceKeyProvider.Name))
                        {
                            throw new SilkyException(
                                $"The {service.ServiceType.FullName} set ServiceKey is not allowed to be repeated");
                        }

                        serviceKeys.Add(serviceKeyProvider.Name, serviceKeyProvider.Weight);
                    }
                }

                if (serviceKeys.Any())
                {
                    serviceDescriptor.Metadatas.Add(ServiceConstant.ServiceKey, serviceKeys);
                }
            }

            var metaDataList = service.ServiceType.GetCustomAttributes<MetadataAttribute>();
            foreach (var metaData in metaDataList)
            {
                serviceDescriptor.Metadatas.Add(metaData.Key, metaData.Value);
            }

            if (service.ServiceProtocol == ServiceProtocol.Ws)
            {
                serviceDescriptor.Metadatas.Add(ServiceConstant.WsPath,
                    WebSocketResolverHelper.ParseWsPath(service.ServiceType));
            }

            return serviceDescriptor;
        }
```

We saw如果该Serve是本地Serve，则会查找该应用ServeofcorrespondingServe类,we know,一个应用Serveinterface是可以存exist多个实现类of;其实现类able to passcharacteristic`[ServiceKey]`logo,该characteristic有两个parameter,一个是实现类ofname,一个是该实现类of权重，As follows:

```csharp

[ServiceKey("v1", 3)]
public class TestAppService : ITestAppService
{
    
}

```

```csharp
    [AttributeUsage(AttributeTargets.Class, Inherited = true, AllowMultiple = false)]
    public class ServiceKeyAttribute : Attribute, IServiceKeyProvider
    {
        public ServiceKeyAttribute(string name, int weight)
        {
            Name = name;
            Weight = weight;
        }

        public string Name { get; }

        public int Weight { get; }
    }
```

前端exist发送http请求of时候,able to passexist请求头携带`ServiceKey`来指定transferof实现类,If not specified`ServiceKey`of话,则执行权重大of实现类;

besides,应用Serve还able to passcharacteristic`[Metadata(key,value)]`来logo应用Serve,pass其来追加该Serveof元数据;

3.2 supportwebsocketofServe，exist上一节middle我们介绍了怎么Find supportwebsocketofServe；supportwebsocketofServe要求该主机必须包含`WebSocketModule`,After finding the implementation class，passServegenerate器of`CreateWsService(wsServiceType)`创建supportwebsocketofServe:

```csharp
public Service CreateWsService(Type wsServiceType)
{
    var wsPath = WebSocketResolverHelper.ParseWsPath(wsServiceType);
    var serviceId = WebSocketResolverHelper.Generator(wsPath);
      
    var serviceInfo = new Service()
    {
        Id = serviceId,
        ServiceType = wsServiceType,
        IsLocal = true,
        ServiceProtocol = ServiceProtocol.Ws,
        ServiceEntries =  _serviceEntryManager.GetServiceEntries(serviceId)
    };
    serviceInfo.ServiceDescriptor = CreateServiceDescriptor(serviceInfo);
    return serviceInfo;
}
```

websocketServegenerateServeIdofmethod不一样,is through该ServecorrespondingwebsocketcorrespondingwebAPIofpathgenerateof,其他of属性赋值与普通应用Serveof方式一致。

## Serve条目of解析

exist上面of源码解读middle,We saw,exist应用Serve解析过程middle,应用Serve所持有ofServe条目是根据Serve条目manager根据`serviceId`提供of`_serviceEntryManager.GetServiceEntries(serviceId)`Obtain。

Serve条目如何解析和Obtain是由默认ofServe条目manager`DefaultServiceEntryManager`Responsible,Serve条目manager跟Servemanager一样,are registered as **singletonof**, exist整个应用生命周期,will only be created once。并且exist构造器middle实现Serve条目of创建并对Serve条目进行缓存。

```csharp
public class DefaultServiceEntryManager : IServiceEntryManager
{
    private IEnumerable<ServiceEntry> m_localServiceEntries;  //用于缓存本地Serve条目
    private IEnumerable<ServiceEntry> m_allServiceEntries;  // 用于缓存全部Serve条目
    private IChangeToken? _changeToken;

    public DefaultServiceEntryManager(IEnumerable<IServiceEntryProvider> providers)
    {
        UpdateEntries(providers);
    }

    private void UpdateEntries(IEnumerable<IServiceEntryProvider> providers)
    {
        var allServiceEntries = new List<ServiceEntry>();
        foreach (var provider in providers) // 遍历所有Serve条目provider,有Serve条目Serve者创建Serve条目
        {
            var entries = provider.GetEntries();
            foreach (var entry in entries)
            {
                if (allServiceEntries.Any(p => p.ServiceEntryDescriptor.Id == entry.ServiceEntryDescriptor.Id))
                {
                    throw new InvalidOperationException(
                        $"Locally contains multiple service entries with Id: {entry.ServiceEntryDescriptor.Id}");
                }
                allServiceEntries.Add(entry);
            }
        }
        if (allServiceEntries.GroupBy(p => p.Router).Any(p => p.Count() > 1))
        {
            throw new SilkyException(
                "There is duplicate routing information, please check the service routing you set");
        }
        m_allServiceEntries = allServiceEntries;
        m_localServiceEntries = allServiceEntries.Where(p => p.IsLocal);
    }

    public void Update(ServiceEntry serviceEntry)
    {
        m_allServiceEntries = m_allServiceEntries
            .Where(p => !p.ServiceEntryDescriptor.Id.Equals(serviceEntry.ServiceEntryDescriptor.Id))
            .Append(serviceEntry);
        if (serviceEntry.IsLocal)
        {
            m_localServiceEntries = m_localServiceEntries
                .Where(p => !p.ServiceEntryDescriptor.Id.Equals(serviceEntry.ServiceEntryDescriptor.Id))
                .Append(serviceEntry);
        }
        OnUpdate?.Invoke(this, serviceEntry);
    }

   // other codes...
}
```

从上面of源码我们可以看到,existServe条目manager`DefaultServiceEntryManager`when created,会transfer`UpdateEntries(providers)`pass遍历所有ofServe条目providergenerateServe条目，并对Serve条目进行缓存，Serve条目不允许重复。

### Serve条目provider

与Serveprovider一样,开发者也可以根据自己of约定实现自己ofServe条目provider,Silky框架实现了默认ofServeprovider`DefaultServiceEntryProvider`。

```csharp
public class DefaultServiceEntryProvider : IServiceEntryProvider
{
    public IReadOnlyList<ServiceEntry> GetEntries()
    {
        var serviceTypeInfos = ServiceHelper.FindAllServiceTypes(_typeFinder);
        if (!EngineContext.Current.IsContainHttpCoreModule())
        {
            serviceTypeInfos = serviceTypeInfos.Where(p =>
                p.Item1.GetCustomAttributes().OfType<DashboardAppServiceAttribute>().FirstOrDefault() == null);
        }
        var entries = new List<ServiceEntry>();
        foreach (var serviceTypeInfo in serviceTypeInfos)
        {
            Logger.LogDebug("The Service were be found,type:{0},IsLocal:{1}", serviceTypeInfo.Item1.FullName,
                serviceTypeInfo.Item2);
            entries.AddRange(_serviceEntryGenerator.CreateServiceEntry(serviceTypeInfo));
        }

        return entries;
    }
   
// other codes...

}
```

Serve条目provider创建Serve条目of过程如下:

1. 查找到所有ofServe类型`IEnumerable<(Type, bool)> serviceTypeInfos`，in：元组of第一个parameter表示Serve类型，第二个parameter表示是否是本地Serve;
2. Does it contain`HttpCoreModule`module,if not included,那么忽略logo了`[DashboardAppService]`ofServe条目;
3. 遍历所有ofServe类型，passServe条目generate器`IServiceEntryGenerator`创建该Serve定义of所有Serve条目;


### Serve条目generate器

Serve条目generate器`DefaultServiceEntryGenerator`pass遍历Serve类型定义of所有method,以及该methodlogoof`HttpMethod`,并且依次创建Serve条目;

```csharp
public class DefaultServiceEntryGenerator : IServiceEntryGenerator
{
   public IEnumerable<ServiceEntry> CreateServiceEntry((Type, bool) serviceType)
   {
        var serviceBundleProvider = ServiceDiscoveryHelper.GetServiceBundleProvider(serviceType.Item1);
        var methods = serviceType.Item1.GetTypeInfo().GetMethods();
        foreach (var method in methods)
        {
            var httpMethodInfos = method.GetHttpMethodInfos();
            foreach (var httpMethodInfo in httpMethodInfos)
            {
                yield return Create(method,
                    serviceType.Item1,
                    serviceType.Item2,
                    serviceBundleProvider,
                    httpMethodInfo);
            }
        }
    }

    private ServiceEntry Create(MethodInfo method,
            Type serviceType,
            bool isLocal,
            IRouteTemplateProvider routeTemplateProvider,
            HttpMethodInfo httpMethodInfo)
        {
            var serviceName = serviceType.Name;
            var serviceEntryId = _idGenerator.GenerateServiceEntryId(method, httpMethodInfo.HttpMethod);
            var serviceId = _idGenerator.GenerateServiceId(serviceType);
            var parameterDescriptors = _parameterProvider.GetParameterDescriptors(method, httpMethodInfo);
            if (parameterDescriptors.Count(p => p.IsHashKey) > 1)
            {
                throw new SilkyException(
                    $"It is not allowed to specify multiple HashKey,Method is {serviceType.FullName}.{method.Name}");
            }
            
            var serviceEntryTemplate =
                TemplateHelper.GenerateServerEntryTemplate(routeTemplateProvider.Template, parameterDescriptors,
                    httpMethodInfo, _governanceOptions.ApiIsRESTfulStyle,
                    method.Name);

            var router = new Router(serviceEntryTemplate, serviceName, method, httpMethodInfo.HttpMethod);
            Debug.Assert(method.DeclaringType != null);
            var serviceEntryDescriptor = new ServiceEntryDescriptor()
            {
                Id = serviceEntryId,
                ServiceId = serviceId,
                ServiceName = routeTemplateProvider.GetServiceName(serviceType),
                ServiceProtocol = ServiceHelper.GetServiceProtocol(serviceType, isLocal, false),
                Method = method.Name,
            };

            var metaDataList = method.GetCustomAttributes<MetadataAttribute>();

            foreach (var metaData in metaDataList)
            {
                serviceEntryDescriptor.Metadatas.Add(metaData.Key, metaData.Value);
            }

            var serviceEntry = new ServiceEntry(router,
                serviceEntryDescriptor,
                serviceType,
                method,
                parameterDescriptors,
                isLocal,
                _governanceOptions);

            if (serviceEntry.NeedHttpProtocolSupport())
            {
                serviceEntryDescriptor.Metadatas.Add(ServiceEntryConstant.NeedHttpProtocolSupport, true);
            }
            
            return serviceEntry;
        }

   // other codes...

   
}
```

如果一个method被logo了多个`HttpMethod`,那么将会generate两个不同ofServe条目,如果没有被logo`HttpMethod`characteristic,那么将会根据命名of规则默认返回corresponding`HttpMethod`method:

```csharp
        public static ICollection<HttpMethodInfo> GetHttpMethodInfos(this MethodInfo method)
        {
            var httpMethodAttributeInfo = method.GetHttpMethodAttributeInfos();
            var httpMethods = new List<HttpMethodInfo>();

            foreach (var httpMethodAttribute in httpMethodAttributeInfo.Item1)
            {
                var httpMethod = httpMethodAttribute.HttpMethods.First().To<HttpMethod>();
                if (!httpMethodAttributeInfo.Item2)
                {
                    if (method.Name.StartsWith("Create"))
                    {
                        httpMethod = HttpMethod.Post;
                    }

                    if (method.Name.StartsWith("Update"))
                    {
                        httpMethod = HttpMethod.Put;
                    }

                    if (method.Name.StartsWith("Delete"))
                    {
                        httpMethod = HttpMethod.Delete;
                    }

                    if (method.Name.StartsWith("Search"))
                    {
                        httpMethod = HttpMethod.Get;
                    }

                    if (method.Name.StartsWith("Query"))
                    {
                        httpMethod = HttpMethod.Get;
                    }

                    if (method.Name.StartsWith("Get"))
                    {
                        httpMethod = HttpMethod.Get;
                    }
                }

                httpMethods.Add(new HttpMethodInfo()
                {
                    IsSpecify = httpMethodAttributeInfo.Item2,
                    Template = httpMethodAttribute.Template,
                    HttpMethod = httpMethod
                });
            }

            return httpMethods;
        }
```

创建Serve条目of过程如下所述:

1. passIdgenerate器`IIdGenerator`依次generateServeId和Serve条目Id;
2. passparameterproviderObtain该methodcorrespondingparameterDescriptor` _parameterProvider.GetParameterDescriptors(method, httpMethodInfo)`;
3. pass`TemplateHelper.GenerateServerEntryTemplate()`为该methodgeneraterouting模板，并创建该methodcorrespondingrouting器`router`;
4. 创建Serve条目Descriptor,并根据methodofcharacteristic`[Metadata]`更新ServeDescriptorof元数据;
5. transferServe条目of构造method创建Serve条目;
6. 更新Serve条目Descriptorof元数据;

Serve条目治理构造methodAs follows,existServe条目构造器middle完成了如下一系列of任务:

```csharp
    internal ServiceEntry(IRouter router,
            ServiceEntryDescriptor serviceEntryDescriptor,
            Type serviceType,
            MethodInfo methodInfo,
            IReadOnlyList<ParameterDescriptor> parameterDescriptors,
            bool isLocal,
            GovernanceOptions governanceOptions)
        {
            Router = router;
            _serviceEntryDescriptor = serviceEntryDescriptor;
            ParameterDescriptors = parameterDescriptors;
            _serviceType = serviceType;
            IsLocal = isLocal;
            MethodInfo = methodInfo;
            CustomAttributes = MethodInfo.GetCustomAttributes(true);
            (IsAsyncMethod, ReturnType) = MethodInfo.ReturnTypeInfo();
            GovernanceOptions = new ServiceEntryGovernance(governanceOptions); // 根据Serve治理配置创建Serve条目治理属性

            var governanceProvider = CustomAttributes.OfType<IGovernanceProvider>().FirstOrDefault();
            ReConfiguration(governanceProvider); // 更新Serve条目ofServe治理配置属性

            _methodExecutor = methodInfo.CreateExecutor(serviceType); // 创建method执行器
            Executor = CreateExecutor();  //创建Serve条目执行器
            AuthorizeData = CreateAuthorizeData(); // 解析Serve条目of身份认证元数据

            ClientFilters = CreateClientFilters();  // 解析client filter
            ServerFilters = CreateServerFilters();  // 解析Serve端过滤器
            CreateFallBackExecutor();   // Create a failure callback executor
            CreateDefaultSupportedRequestMediaTypes();  // 创建默认of请求媒体类型 
            CreateDefaultSupportedResponseMediaTypes(); // 创建默认of响应媒体类型
            CreateCachingInterceptorDescriptors();  // Create a cache interception descriptor
        }
```

## Resolution of Services and Service Entries过程

pass上文所述,we knowServe与Serve条目是exist其 **manager** 创建of时候进行构造解析of, **manager**是singletonof,That is to sayexist整个应用of生命周期middle,Serve与Serve条目都will only be created once，并存exist于应用of内存middle。那么Serve与Serve条目是existServe条目of什么时候进行解析of呢?

1. We sawexistServegenerate器`DefaultServiceGenerator`see in,pass构造注入Serve条目managerinterface`IServiceEntryManager`,That is to say,existgenerateServe之前必须要先创建Serve条目managerof实例，exist解析Serve之前需要先解析Serve条目,It is because of this,所以可以existexist解析Serveof时候passServe条目manager`IServiceEntryManager`Obtain该ServecorrespondingServe条目;

```csharp
public class DefaultServiceGenerator : IServiceGenerator
{
    private readonly IIdGenerator _idGenerator;
    private readonly ITypeFinder _typeFinder;
    private readonly IServiceEntryManager _serviceEntryManager;
    
    public DefaultServiceGenerator(IIdGenerator idGenerator,
        ITypeFinder typeFinder,
        IServiceEntryManager serviceEntryManager)
    {
        _idGenerator = idGenerator;
        _typeFinder = typeFinder;
        _serviceEntryManager = serviceEntryManager;
    }
}
```

2. existSilkyServe主机provider`DefaultServerProvider` middle,We sawpass构造注入应用Servemanager`IServiceManager`,That is to sayexist第一次ObtainSilkyServe主机providerof时候,需要创建应用Servemanagerof实例,实现应用Serveof解析；

```csharp
public class DefaultServerProvider : IServerProvider
{
    public ILogger<DefaultServerProvider> Logger { get; set; }
    private readonly IServer _server;
    private readonly IServiceManager _serviceManager;
    private readonly ISerializer _serializer;

    public DefaultServerProvider(IServiceManager serviceManager,
        ISerializer serializer)
    {
        _serviceManager = serviceManager;
        _serializer = serializer;
        Logger = EngineContext.Current.Resolve<ILogger<DefaultServerProvider>>();
        _server = new Server(EngineContext.Current.HostName);
    }
}
```

pass上述of描述,we can learn,exist应用启动过程middle,exist首次解析SilkyServe主机provider实例`DefaultServerProvider`of时候,Silky框架会首先进行Serve条目of解析,然后再解析应用Serve;由于其相应ofServemanager都是 **singletonof**，exist整个应用of生命周期middle,Serve与Serve条目都只会被解析一次;

Serve和Serve条目被解析成功后，也会存exist相应of**Descriptor**,随着应用of启动,Descriptor将会作为silkyServe主机of一部分,将会随着应用Serve主机ofDescriptor注册到Serve注册middle心，Serve注册middle心将会更新整个微Serve集群of注册信息(包括新增微Serve主机信息、supportedServe与Serve条目、以及主机实例ofof终结点等等元数据信息)，集群of其他微Serve主机实例将会pass心跳或是订阅of方式get整个集群最新of元数据,并pass更新到内存middle;

next,我们将继续介绍exist应用启动时,how to buildSilkyServe主机(provider)[Server](https://github.com/liuhll/silky/blob/main/framework/src/Silky.Rpc/Runtime/Server/Server.cs),并将其信息注册到Serve注册middle心;
