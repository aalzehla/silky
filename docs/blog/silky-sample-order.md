---
title: silkyIntroduction to the use of framework distributed transactions
lang: zh-cn
---

silkyThe framework's distributed transaction solution adoptsTCCtransaction model。Referenced and borrowed from the development process[hmily](https://github.com/dromara/hmily)。useAOPprogramming ideas,existrpcDuring the communication process; the global transaction or branch transaction is managed and coordinated by means of interceptors。

This article passessilky.samples Introduce the order interface to yousilky框架分布式事务of基本use。


## silky分布式事务ofuse

existsilkyin the frame,exist应用服务接口pass`[Transaction]`The attribute identifies the interface as a distributed transaction interface(Application interface layer requires installation package`Silky.Transaction`)。The implementation of the application service interface must pass`   [TccTransaction(ConfirmMethod = "ConfirmMethod", CancelMethod = "CancelMethod")]`property assignmentConfirmstage andCancelstage method(Reapplication layer installation package is required`Silky.Transaction.Tcc`)。

::: warning Notice

An application interface is distributed transaction`[Transaction]`Feature ID,So这个应用接口of实现也必须要use`TccTransaction`feature to identify。otherwise,应用exist启动时会抛出异常。

:::


exist一个分布式事务处理in the process,会存exist如下两种角色of事务。

### transaction role

1. global transaction

existSilkyin the frame,第一个执行of事务被认for是global transaction(transaction rolefor`TransactionRole.Start`)。in other words,exist一个业务处理in the process,The first one executed is identified as`TccTransaction`(Application interfaces need to be identified as`Transaction`)ofmethodforglobal transaction。

certainly,global transaction也作for事务of一特殊of事务participate者,existglobal transaction开始后,Registering in the transaction context as a transaction participant。

2. branch transaction

exist开始of一个分布式事务中,participaterpccommunication,and is characterized`[Transaction]`Identified App Service,被认for是branch transaction(transaction rolefor:`TransactionRole.Participant`)。

### execution of transactions

1. exist开启一个global transaction之后,existglobal transactionof`try`in the process，first将global transaction作for一个事务participate者添加到事务上下文中。if遇到一个branch transaction,Sofirst会transferbranch transactionof`try`method。if`try`method执行success，Sobranch transaction作for一个事务participate者被注册到事务上下文中,and the transaction status of the branch is changed to`trying`。
 
2. ifexistglobal transactionoftrymethod执行in the process发生异常,Soglobal transactionof`Cancel`methodand被加入事务上下文and the status is`trying`ofbranch transactionparticipate者of`Cancel`method将will be called,exist`Cancel`method中实现数据回滚。That is to say,global transactionof`Cancel`in spite of`try`method是否执行success,global transactionof`Cancel`method都会被执行。branch transaction只有被加入到事务上下文,and the status is`trying`(branch transaction已经执行过`try`method),Sobranch transactionof`Cancel`method才会被执行。
 
3. global transactionoftrymethod执行success,Soglobal transactionof`Confirm`and各个branch transactionof`Confirm`method将会得到执行。

4. in other words,所有global transaction(Transaction master branch)以及branch transactionoftrymethod都执行success,才会依次执行所有事务participate者of`Confirm`method,if分布式事务of`try`Stage execution failed,So主branch transactionof`Cancel`method一定will be called;而branch transaction看是否有被添加到事务上下文中且已经执行success`try`stage method,只有这样ofbranch transaction才会transfer`Cancel`method。

5. ifbranch transaction存existbranch transactionof情况下,This business scenario will be relatively special,这个时候ofbranch transaction相对于它ofbranch transaction就是一个特殊ofglobal transaction。它会exist特殊of`try`stage执行孙子辈ofbranch transactionof`try`and`confirm`(success)or`try`and`cancel`(fail)。并且会将执行success与否返回给父branch transaction(global transaction)。

::: warning Notice

无论是global transaction还是branch transactionof各个stage,if涉及到多个表of操作,So,The corresponding database operations need to be placed in the local transaction for operation。

:::

## Distributed transaction case-- silky.samplesOrder interface

under,we passsilky.samplesofOrder interface来familiarpasssilkyHow the framework implements distributed transactions。

### silky.samples Order interfaceof业务流程介绍

exist上一篇博文[passsilky.samplesfamiliarsilky微服务框架ofuse](https://www.cnblogs.com/bea084100123/p/14631609.html)，Introduced to yousilky.samplesBasics of the sample project。This article passes大家familiarof一个Order interface,familiarsilkyof分布式事务是如何use。

under，给大家梳理一下Order interfaceof业务流程。

1. 判断and锁定Order产品库存: exist下Order之前需要判断是否存exist相应of产品,Whether the remaining quantity of the product is sufficient，if产品数量足够of话,Deduct product inventory,Inventory quantity for locked order(branch transaction)

2. Create an order record,The order status isNoPay(global transaction)

3. 判断用户of账号是否存exist,Is the account balance sufficient?,if账户余额充足of话,you need to lock the order amount，Create an account flow record。

4. if1,2,3都success,Release product-locked order inventory

5. if1,2,3都success,Release the amount locked in the account,Modify the related status of the account flow record

6. if1,2,3都success,修改The order status isPayed

7. ifexist**step1**exception occurs(E.g:产品of库存不足orrpccommunicationfail,orAn exception occurs when accessing the database; etc.),库存branch transaction(`DeductStockCancel`)and账号branch transaction(`DeductBalanceCancel`)Specified`Cancel`method都不会被执行。但是global transactionSpecified`Cancel`method(`OrderCreateCancel`)will be called

8. ifexist**step2**exception occurs(An exception occurred when placing an order to access the database),库存branch transactionSpecified`Cancel`method(`DeductStockCancel`)以及global transactionSpecified`Cancel`method(`OrderCreateCancel`)will be called，账号branch transaction指定(`DeductBalanceCancel`)of`Cancel`method都不会被执行。

9. ifexist**step3**exception occurs(用户ofInsufficient account balance,An exception occurs when accessing the database; etc.),So库存branch transaction(`DeductStockCancel`)and账号branch transaction指定(`DeductBalanceCancel`)global transactionSpecified`Cancel`method(`OrderCreateCancel`)都will be called。

::: tip hint

1. ifexist一个分布式事务处理fail,global transactionof`Cancel`method一定will be called。branch transactionof`Try`method得到执行(branch transactionof状态for`trying`),So将会执行branch transactionSpecified`Cancel`method。ifbranch transactionofbranch transactionof`Try`method没有得到执行(branch transactionof状态for`pretry`),So不会执行branch transactionSpecified`Cancel`method。

2. 上述of业务流程in the process,step1,2,3for`try`stage,step4,5,6for`confirm`stage,step7,8,9for`concel`stage。

:::

### global transaction--Order interface

pass[silky分布式事务ofuse](#silky分布式事务ofuse)节点of介绍,我们知道exist服务之间ofrpccommunicationtransfer中,The first one executed is identified as`Transaction`of应用methodwhich isforglobal transaction(which is:事务of开始)。

first， 我们需要existOrder应用接口中pass`[Transaction]`来标识这是一个分布式事务of应用接口。

```csharp
  [Transaction]
  Task<GetOrderOutput> Create(CreateOrderInput input);
```

Second,exist应用接口of实现pass`[TccTransaction]`property assignment`ConfirmMethod`methodand`CancelMethod`。
- Specified`ConfirmMethod`and`CancelMethod`必须for`public`type,但是不需要exist应用接口中声明。
- global transactionof`ConfirmMethod`and`CancelMethod`one must be executed,iftrymethod(`Create`)执行success,So执行`ConfirmMethod`method,执行fail,So则会执行`CancelMethod`。
- can`try`、`confirm`、`cancel`stage method放到领域服务中实现。
- global transaction可以pass`RpcContext`of`Attachments`向branch transactionor`confirm`、`cancel`stage method传递Attachmentparameter。但是branch transaction不能够pass`RpcContext`of`Attachments`向global transaction传递Attachmentparameter。

```csharp
/// <summary>
/// trystage method
/// </summary>
/// <param name="input"></param>
/// <returns></returns>
[TccTransaction(ConfirmMethod = "OrderCreateConfirm", CancelMethod = "OrderCreateCancel")]
public async Task<GetOrderOutput> Create(CreateOrderInput input)
{
    return await _orderDomainService.Create(input); //具体of业务放到领域层实现
}

// confirmstage method
public async Task<GetOrderOutput> OrderCreateConfirm(CreateOrderInput input)
{
    var orderId = RpcContext.GetContext().GetAttachment("orderId");
    var order = await _orderDomainService.GetById(orderId.To<long>());
    order.Status = OrderStatus.Payed;
    order = await _orderDomainService.Update(order);
    return order.MapTo<GetOrderOutput>();
}

// cancelstage method
public async Task OrderCreateCancel(CreateOrderInput input)
{
    var orderId = RpcContext.GetContext().GetAttachment("orderId");
    // if不for空证明已经创建了Order
    if (orderId != null)
    {
        // 是否保留Order可以根据具体of业务来确定。
        // await _orderDomainService.Delete(orderId.To<long>());
        var order = await _orderDomainService.GetById(orderId.To<long>());
        order.Status = OrderStatus.UnPay;
        await _orderDomainService.Update(order);
    }
}

```

下Orderof具体业务(Ordertrystageof实现)

```csharp
public async Task<GetOrderOutput> Create(CreateOrderInput input)
{
    // deducted inventory
    var product = await _productAppService.DeductStock(new DeductStockInput()
    {
        Quantity = input.Quantity,
        ProductId = input.ProductId
    }); // rpctransfer,DeductStockbe characterized[Transaction]mark,是一个branch transaction

    // 创建Order
    var order = input.MapTo<Domain.Orders.Order>();
    order.Amount = product.UnitPrice * input.Quantity;
    order = await Create(order);
    RpcContext.GetContext().SetAttachment("orderId", order.Id); //branch transactionor主branch transactionofconfirmorcancelstage可以从RpcContextgetAttachmentparameter。

    //Deduct account balance
    var deductBalanceInput = new DeductBalanceInput()
        {OrderId = order.Id, AccountId = input.AccountId, OrderBalance = order.Amount};
    var orderBalanceId = await _accountAppService.DeductBalance(deductBalanceInput); // rpctransfer,DeductStockbe characterized[Transaction]mark,是一个branch transaction
    if (orderBalanceId.HasValue)
    {
        RpcContext.GetContext().SetAttachment("orderBalanceId", orderBalanceId.Value);//branch transactionor主branch transactionofconfirmorcancelstage可以从RpcContextgetAttachmentparameter。
    }

    return order.MapTo<GetOrderOutput>();
}
```


### branch transaction--deducted inventory

first,需要exist应用接口层Identifies that this is a distributed transaction interface。

```csharp
// Identifies that this is a distributed transaction interface
[Transaction]
// 执行success,clear cache data
[RemoveCachingIntercept("GetProductOutput","Product:Id:{0}")]
// This interface is not published outside the cluster
[Governance(ProhibitExtranet = true)]
Task<GetProductOutput> DeductStock(DeductStockInput input);
```

Second,应用接口of实现指定`Confirm`stage and`Cancel`stage method。

```csharp
[TccTransaction(ConfirmMethod = "DeductStockConfirm", CancelMethod = "DeductStockCancel")]
public async Task<GetProductOutput> DeductStock(DeductStockInput input)
{
    var product = await _productDomainService.GetById(input.ProductId);
    if (input.Quantity > product.Stock)
    {
        throw new BusinessException("Order数量超过库存数量,无法完成Order");
    }

    product.LockStock += input.Quantity;
    product.Stock -= input.Quantity;
    product = await _productDomainService.Update(product);
    return product.MapTo<GetProductOutput>();
  
}

public async Task<GetProductOutput> DeductStockConfirm(DeductStockInput input)
{
    //Confirmstageof具体业务放exist领域层实现
    var product = await _productDomainService.DeductStockConfirm(input);
    return product.MapTo<GetProductOutput>();
}

public Task DeductStockCancel(DeductStockInput input)
{
    //Cancelstageof具体业务放exist领域层实现
    return _productDomainService.DeductStockCancel(input);
   
}
```

### branch transaction--Deduct account balance

first,需要exist应用接口层Identifies that this is a distributed transaction interface。

```csharp
 [Governance(ProhibitExtranet = true)]
[RemoveCachingIntercept("GetAccountOutput","Account:Id:{0}")]
[Transaction]
Task<long?> DeductBalance(DeductBalanceInput input);
```


Second,应用接口of实现指定`Confirm`stage and`Cancel`stage method。

```csharp
[TccTransaction(ConfirmMethod = "DeductBalanceConfirm", CancelMethod = "DeductBalanceCancel")]
public async Task<long?> DeductBalance(DeductBalanceInput input)
{
    var account = await _accountDomainService.GetAccountById(input.AccountId);
    if (input.OrderBalance > account.Balance)
    {
        throw new BusinessException("Insufficient account balance");
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
  
```

third, 领域层of业务实现

```csharp
 public async Task<long?> DeductBalance(DeductBalanceInput input, TccMethodType tccMethodType)
 {
    var account = await GetAccountById(input.AccountId);
    //Involves multiple tables,所有每一个stageof都放到一个本地事务中执行  
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
     // 将受影响of缓存数据移除。
     await _accountCache.RemoveAsync($"Account:Name:{account.Name}");
     return balanceRecord?.Id;
 }
```

## Order interface测试

**premise**

存exist如下账号and产品:

![tcc-account.png](/assets/imgs/tcc-account.png)

![tcc-product.png](/assets/imgs/tcc-product.png)

### Simulate low inventory

**请求parameter:**

```json
{
  "accountId": 1,
  "productId": 1,
  "quantity": 11
}
```

**response:**

```json
{
  "data": null,
  "status": 1000,
  "statusCode": "BusinessError",
  "errorMessage": "Order数量超过库存数量,无法完成Order",
  "validErrors": null
}
```

**Database changes**

View database,并没有生成Order信息,账户余额and产品库存也没有修改：

![tcc-account1.png](/assets/imgs/tcc-account1.png)

![tcc-product1.png](/assets/imgs/tcc-product1.png)

**Test Results:**

库存and账户余额均for变化,也未创建Order信息

meet expectations

### 模拟Insufficient account balance

**请求parameter:**

```json
{
  "accountId": 1,
  "productId": 1,
  "quantity": 9
}
```

**response:**

```json
{
  "data": null,
  "status": 1000,
  "statusCode": "BusinessError",
  "errorMessage": "Insufficient account balance",
  "validErrors": null
}
```

**Database changes**

1. 新增了一个产品Order,The order status is未支付状态
![tcc-order2.png](/assets/imgs/tcc-order2.png)

2. 产品库存and账户余额并未变更
![tcc-account2.png](/assets/imgs/tcc-account2.png)

![tcc-product2.png](/assets/imgs/tcc-product2.png)

**Test Results:**

创建了一个新ofOrder,状态for未支付,User account balance,产品Order均未变化。

meet test expectations

### 正常下Order

```json
{
  "accountId": 1,
  "productId": 1,
  "quantity": 2
}
```

**response:**

```json
{
  "data": {
    "id": 2,
    "accountId": 1,
    "productId": 1,
    "quantity": 2,
    "amount": 20,
    "status": 1
  },
  "status": 200,
  "statusCode": "Success",
  "errorMessage": null,
  "validErrors": null
}
```

**Database changes**

1. 创建了一个Order,该The order status is已支付

![tcc-product3.png](/assets/imgs/tcc-order3.png)

2. 库存扣减success

![tcc-product3.png](/assets/imgs/tcc-product3.png)

3. 账户金额扣减success,and create a flow record

![tcc-account3.png](/assets/imgs/tcc-account3.png)

![tcc-balance-record3.png](/assets/imgs/tcc-balance-record3.png)

**Test Results:**

创建了一个新ofOrder,状态for支付,User account balance,产品Order均被扣减,And also created a transaction flow record。

meet expectations结果。