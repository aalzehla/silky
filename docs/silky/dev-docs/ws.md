---
title: websocketcommunication
lang: zh-cn
---

## Introduction

silkyframe pass[WebSocketSharp](https://github.com/sta/websocket-sharp)Support and microservice application establishmentwebsocketconversation。silkyFramework Gateway implements supportwebsocketcommunication的代理middle间件，can be communicated with the gateway addresswebsocketServeEstablishconversation。in withwebsocketServeEstablishconversation时,Need to specify via request header or`qString`set one`bussinessId`parameter,pass该parameterandEstablishconversation的`SessionId`make an association,so,in with其他Serve进行rpccommunication过程middle,you can pass`bussinessId`找到and前端建里conversation的`sessionId`。if found`sessionId`The description is built with the front endwebsocketconversation,The server can push the message to the client。

## Build supportwebsocketcommunication的主机

Microservice applications want to supportwebsocketconversation,When a build host is required，Startup module dependencies`WebSocketModule`module。开发者可以在构造寄宿主机时将启动module直接指定for`WsHostModule`,或是在自定义的启动modulemiddle指定依赖`WebSocketModule`。

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
                    .RegisterSilkyServices<WsHostModule>()
                ;
        }
    }
```

## Establishwebsocketconversation

1. In addition to the need to inherit the application service interface; the application service implementation of microservices,also need to inherit`WsAppServiceBase`base class

  The definition of application service interface is the same as that of other types of application service interface,Application service interface methods can be used with other microservice applicationsrpccommunication。

  Definition of Application Service Interface：
  
  ```csharp
    [ServiceRoute]
    public interface IWsTestAppService
    {
        Task Echo(string businessId,string msg);
    }
  ```

  Application service implementation class:
  
  ```csharp
    public class WsTestAppService : WsAppServiceBase, IWsTestAppService
    {
        public async Task Echo(string businessId, string msg)
        {
            if (BusinessSessionIds.TryGetValue(businessId,out var sessionIds))
            {
                foreach (var sessionId in sessionIds)
                {
                    SessionManager.SendTo($"message:{msg},sessionId:{sessionId}", sessionId);
                }
            }
            else
            {
                throw new BusinessException($"does not existbusinessIdfor{businessId}的conversation");
            }
        }
    }
  ```

2. Gateway application reference application service interface,through the gateway address andwebsocketServeapiand客户端Establishconversation
   
   需要特别强调的是in withServe端Establishwebsocketconversation时,must pass the request header or`qString`set one`bussinessId`parameter,否则无法andServe端Establishconversation。Establishconversation的格式如下所示:
   
   在Establishconversation后,`sessionID`and`businessId`The relationship will be cached in the dictionary`BusinessSessionIds`middle(`key`for`bussinessId`,`value`forEstablish的`sessionId`)。在Establish会后,定义的应用Serve接口可以将`bussinessId`作for方法的parameter,so,you can pass`BusinessSessionIds`字典middle的对应关系找到sessionconversation,passconversation的sessionPush messages to clients。

   

   ```
   ws://gatewayip:port/[api]/websocketName?bussinessId=bussinessId
   ```

   E.g,上述实例middle,andwebsocketServeEstablishconversation的apifor:
   
   ```
   ws://127.0.0.1:5000/api/wstest?businessid=100
   ```

   ![ws1.png](/assets/imgs/ws1.png)

   passwebapi模拟Serve获取消息后推送给websocketclient message：

   ![ws2.png](/assets/imgs/ws2.png)

   websocket接收到Serve端推送的消息：

   ![ws3.png](/assets/imgs/ws3.png)

  如果未Establishconversation,an exception occurs：
  
   ![ws4.png](/assets/imgs/ws4.png)