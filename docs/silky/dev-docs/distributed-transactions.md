---
title: Distributed transaction
lang: zh-cn
---

## concept

application in microservices(in distributed applications)，Completing a business function may need to span multiple services，Work with multiple databases。这就涉及到到了Distributed transaction，With the resource that needs to be manipulated on multiple resource servers，The application needs to ensure the operation of the data of multiple resource servers，either all succeed，or all fail。Essentially，Distributed transaction就Yes为了保证不同资源服务器的数据consistency。

## 与Distributed transaction相关的theory

### Classic Distributed Systems Theory-CAP

1. consistency

    consistency指**all nodes see the same data at the same time**，That is; the update operation is successful and returns to the client after completion，All nodes have exactly the same data at the same time，no intermediate state。For example; for e-commerce system users to place orders，Inventory reduction、User Fund Account Deduction、Operations such as points increase must be consistent after the user places an order.。Can not appear similar to the inventory has been reduced，And the user's fund account has not been deducted，Points are not increased。If this happens，then it is considered inconsistent。

    关于consistency，If it is true that the data seen by the client can be guaranteed to be consistent at all times as described above，那么称之为强consistency。If an intermediate state is allowed，only after a period of time，Data is eventually consistent，则称之为最终consistency。also，If some data inconsistencies are allowed，那么就称之为弱consistency。

2. Availability

    AvailabilityRefers to系统提供的服务必须一直处于可用的状态，For each operation request of the user; the result can always be returned within a limited time。**for a limited time**Refers to，an operation request from the user，The system must be able to return the corresponding processing result within the specified time，If this time frame is exceeded，then the system is considered unavailable。Just imagine，If an order is placed，为了保证Distributed transaction的consistency，need10minutes to process，Then the user is obviously unbearable。“return result”YesAvailability的另一个非常重要的指标，It requires the system to complete the processing of the user request after，returns a normal response result，whether the result is success or failure。

3. Partition Tolerance

    When a distributed system encounters any network partition failure，仍然need能够保证right外提供满足consistencyandAvailability的服务，Unless the entire network environment fails。


**summary**：既然一个分布式系统无法同时满足consistency、Availability、Partition Tolerance三个特点，我们就need抛弃一个，need明确的一点Yes，For a distributed system，Partition ToleranceYes一个最基本的要求。Because since it is a distributed system，那么分布式系统中的组件必然need被部署到不同的节点，Otherwise; there is no such thing as a distributed system.。And for distributed systems，The network problem is another abnormal situation that is bound to occur，thereforePartition Tolerance也就成为了一个分布式系统必然need面rightand解决的问题。therefore系统架构师往往need把精力花exist如何根据业务特点existC（consistency）andA（Availability）seek balance。And we mentioned earlierX/Open XA 两阶段提交协议的Distributed transaction方案，强调的就Yesconsistency；由于Availability较低，Not much practical application。and based onBASETheoretical Flexibility，强调的YesAvailability，Currently in vogue，Most Internet companies may give priority to adopting this scheme。

### BASEtheory

eBaythe architectDan PritchettFrom the practical summary of large-scale distributed systems，existACMArticles published onBASEtheory。Article link：https://queue.acm.org/detail.cfm?id=1394128

BASEtheoryYesrightCAPtheory的延伸，核心思想Yes即使无法做到强consistency（Strong Consistency，CAP的consistency就Yes强consistency），但应用可以采用适合的方式达到最终consistency（Eventual Consitency）。    


![distributed-transactions1.png](/assets/imgs/distributed-transactions1.png)

BASEYesBasically Available（basically available）、Soft state（soft state）andEventually consistent（最终consistency）abbreviation for three phrases。

1. basically available（Basically Available）

    指分布式系统exist出现不可预知故障的时候，允许损失部分Availability。

2. soft state（ Soft State）

    指允许系统中的数据存exist中间状态，并认为该中间状态的存exist不会影响系统的整体Availability。

3. eventually consistent（ Eventual Consistency）

    强调的Yes所有的数据更新操作，exist经过一段时间的同步之后，eventually reach a consistent state。therefore，最终consistency的本质Yesneed系统保证最终数据能够达到一致，而不need实时保证系统数据的强consistency。

BASEtheory面向的Yes大型高可用可扩展的分布式系统，and传统的事物ACID特性Yes相反的。it is totally different fromACID的强consistency模型，而Yes通过牺牲强consistency来获得Availability，并允许数据exist一段时间内Yes不一致的，but eventually reached a consistent state。But at the same time，exist实际的分布式场景中，不同业务单元and组件right数据consistency的要求Yes不同的，thereforeexist具体的分布式系统架构设计过程中，ACID特性andBASEtheory往往又会结合exist一起。


Typical Flexible Transaction Scenario：

1. best effort notice（unreliable news、Regular proofreading）

2. 可靠消息最终consistency（asynchronous guarantee）

3. TCC（two-stage、Compensation）

## Silky框架Distributed transaction的实现方式

silky框架的Distributed transaction解决方案采用的TCC事务模型实现了Distributed transaction的最终consistency。exist开发过程中参考and借鉴了[hmily](https://github.com/dromara/hmily)。useAOPprogramming ideas,existrpc通信过程中通过拦截器的方式right全局事务或Yes分支事务进行管理and协调。

## 如何use

exist一个Distributed transaction中,参与Distributed transaction的方法可能存exist多个,existuse过程中,right**each**参与Distributed transaction的方法的写法都一致。

1. existneed参与Distributed transaction的应用服务接口中,by feature`TransactionAttribute`label。

```csharp
[Transaction]
Task<string> Delete(string name);
```

2. The application service interface implementation method passes`TccTransactionAttribute`feature callout，and pass the parameter`ConfirmMethod`、`CancelMethod`Specify confirmation、Method executed when a step is canceled。

```csharp
[TccTransaction(ConfirmMethod = "DeleteConfirm", CancelMethod = "DeleteCancel")]
public async Task<string> Delete(string name)
{
    await _anotherAppService.DeleteOne(name);
    await _anotherAppService.DeleteTwo(name);
    return name + " v1";
}

// Distributed transactionComfirmmethod to execute
public async Task<string> DeleteConfirm(string name)
{
    return name + " DeleteConfirm v1";
}

// Distributed transactionCancelmethod to execute
public async Task<string> DeleteCancel(string name)
{
    return name + "DeleteConcel v1";
}

```

关于Distributed transaction的更详细用法,Developers can refer to[silky框架Distributed transactionuse简介](/blog/silky-sample-order.md)。

::: warning

Specified`ConfirmMethod`、`CancelMethod`The method declaration must be`public`,The input parameters are consistent with the parameters defined by the application service interface。

:::

