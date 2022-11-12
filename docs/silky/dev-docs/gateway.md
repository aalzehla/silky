---
title: gateway
lang: zh-cn
---

## gateway概述

Different microservices generally have different network addresses，The external client may need to call the interface of multiple services to complete a business requirement，If you let the client communicate directly with each microservice，There will be the following problems：

- The client will request different microservices multiple times，Increased client-side complexity
- There is a cross-domain request，Relatively complex to deal with in certain scenarios
- Authentication is complicated，Each service requires independent certification
- hard to refactor，As the project iterates，May need to repartition microservices。E.g，May combine multiple services into one or split one service into multiple。If the client communicates directly with the microservice，then refactoring will be difficult to implement
- Some microservices may use firewalls / Browser unfriendly protocol，Direct access will be difficult

The above problems can be solved by**gateway**solve。

**gateway**is the middle layer between the client and the server，All external requests go through first gateway这一层。That is to say，API In terms of implementation; more consideration is given to business logic，while safe、performance、Monitoring can be handed over to gateway来做，This increases business flexibility without sacrificing security。

![gateway1.png](/assets/imgs/gateway1.png)

## silky框架gateway

silkyThe framework's normal microservices are designed to use.netof[Universal host](https://docs.microsoft.com/zh-cn/aspnet/core/fundamentals/host/generic-host?view=aspnetcore-5.0)escrow,and customizerpcPorts to communicate with other microservice applications(rpcThe default port number is:`2200`)。为保证微服务应用of安全性,pass`rpc.token`of设计方式,Avoid outside the clusterrpcThe port number communicates directly with the inside of the microservice。

So,outside of service(front end)How to communicate with microservice applications?

silkygateway被设计为对微服务应用集群of聚合，需要Install每个微服务应用of应用接口项目(Bag)，front endpasshttp请求到达gateway,silkymiddlewarepass`webapi`+`httpmethod`Find the application service in the routing tableId,然后passrpcCommunicate with service providers,并将返回结果封装后返回给front end。

existgateway应用,开发者可以增加或自定义middleware实现接口of统一认证与授权,Service current limit,Traffic monitoring and other functions。

## 构建gateway应用

1. passnugetInstall`Silky.WebHost`Bag，Register and build the host in the main function

```csharp
    public class Program
    {
        public async static Task Main(string[] args)
        {
            await CreateHostBuilder(args).Build().RunAsync();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .RegisterSilkyServices<WebHostModule>()
                .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); });
    }
```

2. exist`Startup`class added**swaggerexist线文档**,and configurationsilkyrequest pipeline(自动注册一系列ofsilkymiddleware)。

```csharp
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Silky Gateway Demo", Version = "v1" });
                c.MultipleServiceKey();
                var applicationAssemblies = EngineContext.Current.TypeFinder.GetAssemblies()
                    .Where(p => p.FullName.Contains("Application"));
                foreach (var applicationAssembly in applicationAssemblies)
                {
                    var xmlFile = $"{applicationAssembly.GetName().Name}.xml";
                    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                    if (File.Exists(xmlPath))
                    {
                        c.IncludeXmlComments(xmlPath);
                    }
                }

            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Silky Gateway Demo v1"));
            }
            app.ConfigureSilkyRequestPipeline();
        }
```

3. pass项目引用of方式或是nugetBagof方式Install各个微服务应用of应用服务接口层(Bag)

silkypass引用各个微服务应用of应用接口,can be generated for each application service interfacewebapi,开发者可以passswaggerexist线文档进行开发调式。