---
title: passsilky.samplesfamiliarsilkyThe use of microservice frameworks
lang: zh-cn
---


After a period of development and testing,finally releasedSilkyThe first official version of the framework(1.0.0Version),and givessilkySample project for the framework**silky.samples**。本文passright**silky.samples**introduction，简述如何passsilkyThe framework quickly builds a business framework for microservices，and application development。


## silky.samplesBasic introduction to the project

silky.sampleThe project consists of three independent microservice application modules:account、stock、orderand a gateway projectgatewayconstitute。

### business application module

Each independent microservice application adopts a modular design，It is mainly composed of the following parts：

1. **host(Host):** Primarily used to host the microservice application itself，hostpass引用application serviceproject(Implementation of the application interface),Managed Microservice Applications，pass托管application service,existhost启动of过程middle,Register a service route with the service registry。

2. **Application interface layer(Application.Contracts):** Used to define application service interface,passApplication interface,The microservice module communicates with other microservice modules or gatewaysrpcability to communicate。in this project,Except before defining the application service interface,Generally also define the interface related to the application`DTO`object。In addition to being referenced by the microservice application project; the application interface,and before implementing the app service,It can also be referenced by gateways or other microservice modules。网关or其他MicroservicesprojectpassApplication interface生成of代理与该Microservicesmodulepassrpccommunicate。

3. **Application service layer(Application):** application service是该Microservices定义ofImplementation of the application interface。application service andDDDThe concept of the application layer of the traditional layered architecture is consistent。Mainly responsible for the coordination between external communication and the domain layer。normally，Application Services for Business Process Control，But does not include the implementation of business logic。

4. **Domain layer(Domain):** Responsible for expressing business concepts,Business state information and business rules,is the business core of the microservice module。normally,Aggregate roots can be defined at this level、entity、Domain Services等object。

5. **Domain Shared Layer(Domain.Shared):** 该Floor用于定义与领域object相关of模型、entity等相关类型。Does not contain any business implementation，Can be referenced by other microservices。

6. **data access(DataAccess)Floor:** 该Floor一般用于封装data access相关ofobject。E.g：仓库object、 `SqlHelper`、orORMrelated types; etc.。existsilky.samplesmiddle,passefcoreImplement data read and write operations。

![project-arch.jpg](/assets/imgs/project-arch.jpg)

### Service Aggregation and Gateway

silkyframenot allowed服务外部与Microserviceshost直接communication,应用Please求必须passhttpThe request arrives at the gateway,网关passsilky提供ofmiddle间件解析到服务条目,并passrpc与cluster内部ofMicroservicescommunicate。so，如果服务需要与cluster外部communicate,So,开发者定义of网关必须要引用各个MicroservicesmoduleofApplication interface layer；and must usesilky相关ofmiddle间件。


## development environment

1. .netVersion: 5.0.101

2. silkyVersion: 1.0.0

3. IDE: (1) visual studio new (2) Rider(recommend)

## host与应用托管

### hostof创建步骤

passsilkyIt is very convenient for the framework to create a business module,只需要pass如下4steps,you can easily create asilkyApplication business module。

1. Create project

Create a console app(Console Application)project,and cite`Silky.NormHost`Bag。

```
dotnet add package Silky.NormHost --version 1.0.0
```

2. 应用程序入口与host构建

exist`main`方法middle,Universal.netofhost`Host`build and registersilkyMicroservices。exist注册silkyMicroservices时,needs to be specifiedsilkyStarted dependent modules。

normally,If the developer does not need additional dependencies on other modules,也无需exist应用启动或停止时执行方法，So您可by直接指定`NormHostModule`module。

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
                    .RegisterSilkyServices<NormHostModule>()
                ;
        }
    }
```

3. configuration file

silkyFramework support`yml`or`json`格式作forconfiguration file。pass`appsettings.yml`rightsilkyFramework for unified configuration,pass`appsettings.${Environment}.yml`right不同environment variable下ofconfigure项进行set up。

开发者如果直接passprojectof方式启动应用,So可bypass`Properties/launchSettings.json`of`environmentVariables.DOTNET_ENVIRONMENT`environment variable。如果pass`docker-compose`of方式启动应用,So可bypass`.env`set up`DOTNET_ENVIRONMENT`environment variable。

for保证configuration file有效,开发者需要显式ofWillconfiguration file拷贝到project生成Under contents。

4. 引用Application service layeranddata accessFloor

normally,hostproject需要引用该MicroservicesmoduleofApplication service layeranddata accessFloor。只有host引用Application service layer,hostexist启动时,才会生成服务条目ofrouting,并且Will服务routing注册到服务注册middle心。

一个典型ofhostproject文件如下所示:

```xml
<Project Sdk="Microsoft.NET.Sdk">

    <PropertyGroup>
        <OutputType>Exe</OutputType>
        <TargetFramework>net5.0</TargetFramework>
    </PropertyGroup>

    <ItemGroup>
      <PackageReference Include="Silky.NormHost" Version="$(SilkyVersion)" />
    </ItemGroup>

    <ItemGroup>
      <None Update="appsettings.yml">
        <CopyToOutputDirectory>Always</CopyToOutputDirectory>
      </None>
      <None Update="appsettings.Production.yml">
        <CopyToOutputDirectory>Always</CopyToOutputDirectory>
      </None>
      <None Update="appsettings.Development.yml">
        <CopyToOutputDirectory>Always</CopyToOutputDirectory>
      </None>
    </ItemGroup>

    <ItemGroup>
      <ProjectReference Include="..\Silky.Account.Application\Silky.Account.Application.csproj" />
      <ProjectReference Include="..\Silky.Account.EntityFrameworkCore\Silky.Account.EntityFrameworkCore.csproj" />
    </ItemGroup>
</Project>

```

### configure

normally,一个Microservicesmoduleofhost必须要configure:服务注册middle心、Distributed lock link、Distributed cache address、clusterrpccommunicationtoken、Database link address; etc.。

If usingdocker-compose来启动and调试应用of话,So,rpcconfigure节点下ofofhostandportcan be defaulted,因for生成of每个containerof都有自己of地址and端口号。

如果直接passprojectof方式启动and调试应用of话,So,必须要configurerpc节点下ofport,每个Microservicesmoduleofhost应用有自己of端口号。

silkyframeof必要configure如下所示:

```yaml
rpc:
  host: 0.0.0.0
  Port: 2201
  token: ypjdYOzNd4FwENJiEARMLWwK0v7QUHPW
registrycenter:
  connectionStrings: 127.0.0.1:2181,127.0.0.1:2182,127.0.0.1:2183;127.0.0.1:2184,127.0.0.1:2185,127.0.0.1:2186 # use semicolon;来区分不同of服务注册middle心
  registryCenterType: Zookeeper
distributedCache:
  redis:
    isEnabled: true 
    configuration: 127.0.0.1:6379,defaultDatabase=0
connectionStrings:
    default: server=127.0.0.1;port=3306;database=account;uid=root;pwd=qwe!P4ss;
```

## Application interface

### Application interface定义

normally,existApplication interface layer开发者需要Install`Silky.Rpc`Bag。如果该Microservicesmodule还涉及到分布式事务,So还需要Install`Silky.Transaction.Tcc`,certainly，您也可by选择existApplication interface layerInstall`Silky.Transaction`Bag,existApplication service layerInstall`Silky.Transaction.Tcc`Bag。

1. 开发者只需要existApplication interfacepass`ServiceRouteAttribute`特性rightApplication interface进行直接即可。

2. Silky约定Application interface应当by`IXxxAppService`name，so,服务条目生成ofrouting则会by`api/xxx`Form generation。certainly这并不是强制of。

3. 每个Application interfaceof方法都right应着一个服务条目,服务条目ofIdfor: 方法of完全限定名 + parameter name

4. 您可byexistApplication interface layerright方法of缓存、routing、Service Governance、分布式事务进行相关configure。Please refer to this section[official documentation](http://docs.silky-fk.com/)

5. 网关or其他moduleofMicroservicesproject需要引用服务Application interfaceprojectorpassnugetof方式Install服务Application interface生成ofBag。

6. `[Governance(ProhibitExtranet = true)]`可by标识一个方法禁止与cluster外部communicate,pass网关也不会生成swaggerDocumentation。
 
7. Application interface方法生成ofWebApisupportrestful APIstyle。Silkysupportpass方法of约定name生成right应http方法Please求ofWebApi。您certainly开发者也可bypass`HttpMethodAttribute`特性right某个方法进行注解。

### 一个典型ofApplication interfaceof定义

```csharp
    /// <summary>
    /// Account service
    /// </summary>
    [ServiceRoute]
    public interface IAccountAppService
    {
        /// <summary>
        /// Add account
        /// </summary>
        /// <param name="input">account information</param>
        /// <returns></returns>
        Task<GetAccountOutput> Create(CreateAccountInput input);

        /// <summary>
        /// passAccount Name获取account
        /// </summary>
        /// <param name="name">Account Name</param>
        /// <returns></returns>
        [GetCachingIntercept("Account:Name:{0}")]
        [HttpGet("{name:string}")]
        Task<GetAccountOutput> GetAccountByName([CacheKey(0)] string name);

        /// <summary>
        /// passId获取account information
        /// </summary>
        /// <param name="id">accountId</param>
        /// <returns></returns>
        [GetCachingIntercept("Account:Id:{0}")]
        [HttpGet("{id:long}")]
        Task<GetAccountOutput> GetAccountById([CacheKey(0)] long id);

        /// <summary>
        /// 更新account information
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [UpdateCachingIntercept( "Account:Id:{0}")]
        Task<GetAccountOutput> Update(UpdateAccountInput input);

        /// <summary>
        /// 删除account information
        /// </summary>
        /// <param name="id">accountId</param>
        /// <returns></returns>
        [RemoveCachingIntercept("GetAccountOutput","Account:Id:{0}")]
        [HttpDelete("{id:long}")]
        Task Delete([CacheKey(0)]long id);

        /// <summary>
        /// Order debit
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [Governance(ProhibitExtranet = true)]
        [RemoveCachingIntercept("GetAccountOutput","Account:Id:{0}")]
        [Transaction]
        Task<long?> DeductBalance(DeductBalanceInput input);
    }
```


## application service--Implementation of the application interface

1. Application service layer只需要引用application serviceinterfaceFlooras well asDomain ServicesFloor,并实现Application interface相关of方法。

2. 确保该Microservicesmoduleofhost引用了该moduleofApplication service layer,sohost才能够托管该应用本身。

3. Application service layer可bypass引用其他MicroservicesmoduleofApplication interface layerproject(orInstallnugetBag,取决于开发团队ofproject管理方法),与其他Microservicesmodule进行rpccommunication。

4. Application service layer需要依赖Domain Services,pass调用Domain Servicesof相关interface,实现该moduleof核心业务逻辑。

5. DTO到entityobjectorentityrightDTOobjectof映射关系可byexist该Floor指定映射关系。

一个典型ofapplication serviceof实现如下所示:

```csharp
public class AccountAppService : IAccountAppService
    {
        private readonly IAccountDomainService _accountDomainService;

        public AccountAppService(IAccountDomainService accountDomainService)
        {
            _accountDomainService = accountDomainService;
        }

        public async Task<GetAccountOutput> Create(CreateAccountInput input)
        {
            var account = input.MapTo<Domain.Accounts.Account>();
            account = await _accountDomainService.Create(account);
            return account.MapTo<GetAccountOutput>();
        }

        public async Task<GetAccountOutput> GetAccountByName(string name)
        {
            var account = await _accountDomainService.GetAccountByName(name);
            return account.MapTo<GetAccountOutput>();
        }

        public async Task<GetAccountOutput> GetAccountById(long id)
        {
            var account = await _accountDomainService.GetAccountById(id);
            return account.MapTo<GetAccountOutput>();
        }

        public async Task<GetAccountOutput> Update(UpdateAccountInput input)
        {
            var account = await _accountDomainService.Update(input);
            return account.MapTo<GetAccountOutput>();
        }

        public Task Delete(long id)
        {
            return _accountDomainService.Delete(id);
        }

        [TccTransaction(ConfirmMethod = "DeductBalanceConfirm", CancelMethod = "DeductBalanceCancel")]
        public async Task<long?> DeductBalance(DeductBalanceInput input)
        {
            var account = await _accountDomainService.GetAccountById(input.AccountId);
            if (input.OrderBalance > account.Balance)
            {
                throw new BusinessException("account余额不足");
            }
            return await _accountDomainService.DeductBalance(input, TccMethodType.Try);
        }

        public Task DeductBalanceConfirm(DeductBalanceInput input)
        {
            return _accountDomainService.DeductBalance(input, TccMethodType.Confirm);
        }

        public Task DeductBalanceCancel(DeductBalanceInput input)
        {
            return _accountDomainService.DeductBalance(input, TccMethodType.Cancel);
        }
    }
```

## Domain layer--Microservicesof核心业务实现

1. Domain layer是该Microservicesmodule核心业务处理ofmodule,Generally used to target aggregate roots、entity、Domain Services、仓储等业务object。

2. Domain layer引用该MicroservicesmoduleofApplication interface layer,easy to usedtoobject。

3. Domain layer可bypass引用其他MicroservicesmoduleofApplication interface layerproject(orInstallnugetBag,取决于开发团队ofproject管理方法),与其他Microservicesmodule进行rpccommunication。

4. Domain Services必须要直接或间接继承`ITransientDependency`interface,so,该Domain Services才会被注入到ioccontainer。

6. silky.samples projectuse[TanvirArjel.EFCore.GenericRepository](https://github.com/TanvirArjel/EFCore.GenericRepository)BagImplement data read and write operations。

一个典型ofDomain Servicesof实现如下所示:

```csharp
  public class AccountDomainService : IAccountDomainService
    {
        private readonly IRepository _repository;
        private readonly IDistributedCache<GetAccountOutput, string> _accountCache;

        public AccountDomainService(IRepository repository,
            IDistributedCache<GetAccountOutput, string> accountCache)
        {
            _repository = repository;
            _accountCache = accountCache;
        }

        public async Task<Account> Create(Account account)
        {
            var exsitAccountCount = await _repository.GetCountAsync<Account>(p => p.Name == account.Name);
            if (exsitAccountCount > 0)
            {
                throw new BusinessException($"已经存exist{account.Name}名称ofaccount");
            }

            exsitAccountCount = await _repository.GetCountAsync<Account>(p => p.Email == account.Email);
            if (exsitAccountCount > 0)
            {
                throw new BusinessException($"已经存exist{account.Email}Emailofaccount");
            }

            await _repository.InsertAsync<Account>(account);
            return account;
        }

        public async Task<Account> GetAccountByName(string name)
        {
            var accountEntry = _repository.GetQueryable<Account>().FirstOrDefault(p => p.Name == name);
            if (accountEntry == null)
            {
                throw new BusinessException($"不存exist名称for{name}ofaccount");
            }

            return accountEntry;
        }

        public async Task<Account> GetAccountById(long id)
        {
            var accountEntry = _repository.GetQueryable<Account>().FirstOrDefault(p => p.Id == id);
            if (accountEntry == null)
            {
                throw new BusinessException($"不存existIdfor{id}ofaccount");
            }

            return accountEntry;
        }

        public async Task<Account> Update(UpdateAccountInput input)
        {
            var account = await GetAccountById(input.Id);
            if (!account.Email.Equals(input.Email))
            {
                var exsitAccountCount = await _repository.GetCountAsync<Account>(p => p.Email == input.Email);
                if (exsitAccountCount > 0)
                {
                    throw new BusinessException($"系统middle已经存existEmailfor{input.Email}ofaccount");
                }
            }

            if (!account.Name.Equals(input.Name))
            {
                var exsitAccountCount = await _repository.GetCountAsync<Account>(p => p.Name == input.Name);
                if (exsitAccountCount > 0)
                {
                    throw new BusinessException($"系统middle已经存existNamefor{input.Name}ofaccount");
                }
            }

            await _accountCache.RemoveAsync($"Account:Name:{account.Name}");
            account = input.MapTo(account);
            await _repository.UpdateAsync(account);
            return account;
        }

        public async Task Delete(long id)
        {
            var account = await GetAccountById(id);
            await _accountCache.RemoveAsync($"Account:Name:{account.Name}");
            await _repository.DeleteAsync(account);
        }

        public async Task<long?> DeductBalance(DeductBalanceInput input, TccMethodType tccMethodType)
        {
            var account = await GetAccountById(input.AccountId);
            var trans = await _repository.BeginTransactionAsync();
            BalanceRecord balanceRecord = null;
            switch (tccMethodType)
            {
                case TccMethodType.Try:
                    account.Balance -= input.OrderBalance;
                    account.LockBalance += input.OrderBalance;
                    balanceRecord = new BalanceRecord()
                    {
                        OrderBalance = input.OrderBalance,
                        OrderId = input.OrderId,
                        PayStatus = PayStatus.NoPay
                    };
                    await _repository.InsertAsync(balanceRecord);
                    RpcContext.GetContext().SetAttachment("balanceRecordId",balanceRecord.Id);
                    break;
                case TccMethodType.Confirm:
                    account.LockBalance -= input.OrderBalance;
                    var balanceRecordId1 = RpcContext.GetContext().GetAttachment("orderBalanceId")?.To<long>();
                    if (balanceRecordId1.HasValue)
                    {
                        balanceRecord = await _repository.GetByIdAsync<BalanceRecord>(balanceRecordId1.Value);
                        balanceRecord.PayStatus = PayStatus.Payed;
                        await _repository.UpdateAsync(balanceRecord);
                    }
                    break;
                case TccMethodType.Cancel:
                    account.Balance += input.OrderBalance;
                    account.LockBalance -= input.OrderBalance;
                    var balanceRecordId2 = RpcContext.GetContext().GetAttachment("orderBalanceId")?.To<long>();
                    if (balanceRecordId2.HasValue)
                    {
                        balanceRecord = await _repository.GetByIdAsync<BalanceRecord>(balanceRecordId2.Value);
                        balanceRecord.PayStatus = PayStatus.Cancel;
                        await _repository.UpdateAsync(balanceRecord);
                    }
                    break;
            }

           
            await _repository.UpdateAsync(account);
            await trans.CommitAsync();
            await _accountCache.RemoveAsync($"Account:Name:{account.Name}");
            return balanceRecord?.Id;
        }
    }
```

## data access(EntityFrameworkCore)--passefcoreRealize data read and write

1. silky.samplesprojectuseormframeefcoreread and write data。

2. silkyprovided`IConfigureService`,pass继承该interface即可use`IServiceCollection`of实例指定数据上下文objectand注册仓库服务。

```csharp
  public class EfCoreConfigureService : IConfigureService
    {
        public void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<OrderDbContext>(opt =>
                    opt.UseMySql(configuration.GetConnectionString("Default"),
                        ServerVersion.AutoDetect(configuration.GetConnectionString("Default"))))
                .AddGenericRepository<OrderDbContext>(ServiceLifetime.Transient)
                ;
        }

        public int Order { get; } = 1;
    }
```

3. hostproject需要显式of引用该project，只有so,该projectof`ConfigureServices`will be called。

4. data migration,Please[refer to](https://docs.microsoft.com/zh-cn/ef/core/cli/dbcontext-creation?tabs=dotnet-core-cli)

## Application startup and debugging

### Get the source code

1. usegit clonesilkyproject源代码,silky.samples存放exist`samples`Under contents

```cmd
# github
git clone https://github.com/liuhll/silky.git

# gitee
git clone https://gitee.com/liuhll2/silky.git
```

### 必要of前提

1. 服务注册middle心`zookeeper`

2. cache service`redis`

3. mysqldatabase 

If your computer has already installed[docker](https://docs.docker.com/docker-for-windows/install)as well as[docker-compose](https://docs.docker.com/compose/install/)Order,So您只需要进入`samples\docker-compose\infrastr`Under contents,OpenOrder行工作,执行如下Order就可by自动Install`zookeeper`、`redis`、`mysql`and other services:

```cmd
docker-compose -f .\docker-compose.mysql.yml -f .\docker-compose.redis.yml -f .\docker-compose.zookeeper.yml up -d
```

### database迁移

需要分别Enter各个Microservicesmodule下of`EntityFrameworkCore`project(E.g:),执行如下Order:

```cmd
dotnet ef database update
```

E.g: need to migrateaccountmoduleofdatabase如下所示:

![db-migrations.png](/assets/imgs/db-migrations.png)

ordermoduleandstockmodule与accountmodule一致,exist服务运行前都需要passdatabase迁移Order生成相关database。

::: warning Notice

1. database迁移指定database连接地址默认指定of是`appsettings.Development.yml`middleconfigureof,您可bypass修改该configuration filemiddleof`connectionStrings.default`configure项来指定自己ofdatabase服务地址。

2. if there is not`dotnet ef`Order,则需要pass`dotnet tool install --global dotnet-ef`Installeftool,Please[refer to](https://docs.microsoft.com/zh-cn/ef/core/get-started/overview/install)


:::

### byprojectof方式启动and调试

#### usevisual studio作for开发tool

EntersamplesUnder contents,usevisual studioOpen`silky.samples.sln`solution,Willprojectset upfor多启动project,并Will网关and各个moduleofMicroserviceshostset upfor启动project，As shown below:

![visual-studio-debug-1](/assets/imgs/visual-studio-debug-1.png)

set up完成后直接启动即可。

#### userider作for开发tool

1. EntersamplesUnder contents,useriderOpen`silky.samples.sln`solution,Open各个Microservicesmodule下of`Properties/launchSettings.json`,点击图middle绿色of箭头即可启动project。

![rider-debug.png](/assets/imgs/rider-debug.png)

2. 启动网关project后,可by看到Application interfaceof服务条目生成ofwebapiinterface。

![swagger-ui.png](/assets/imgs/swagger-ui.png)

::: warning Notice

1. 默认ofenvironment variablefor: `Development`,如果需要修改environment variableof话,可bypass`Properties/launchSettings.json`下of`environmentVariables`节点修改相关environment variable,Pleaserefer to[exist ASP.NET Core middleuse多个环境](https://docs.microsoft.com/zh-cn/aspnet/core/fundamentals/environments?view=aspnetcore-5.0)。

2. database连接、服务注册middle心地址、as well asredis缓存地址and分布式锁连接等configure项可bypass修改`appsettings.Development.yml`configure项自定义指定。

:::

### bydocker-composeof方式启动and调试

1. EntersamplesUnder contents,usevisual studioOpen`silky.samples.dockercompose.sln`solution,Will**docker-compose**set upfor启动project，即可启动and调式。

2. After the application starts successfully,Open: [http://127.0.0.1/swagger](http://127.0.0.1/swagger),to seeswagger apiDocumentation

![swagger-ui2.png](/assets/imgs/swagger-ui2.png)


::: warning Notice

1. bydocker-composeof方式启动and调试,则指定ofenvironment variablefor:`ContainerDev`

2. database连接、服务注册middle心地址、as well asredis缓存地址and分布式锁连接等configure项可bypass修改`appsettings.ContainerDev.yml`configure项自定义指定,configureof服务连接地址**not allowed**for: `127.0.0.1`or`localhost`

:::


### 测试and调式

After the service starts successfully,您可bypass`/api/account-post`interfaceand`/api/product-post`interfaceAdd accountand产品,然后pass`/api/order-post`interface进行测试and调式。

## open source address

github: [https://github.com/liuhll/silky](https://github.com/liuhll/silky)

gitee: [https://gitee.com/liuhll2/silky](https://gitee.com/liuhll2/silky)
