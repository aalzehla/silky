# Changelog
All notable changes to this project will be recorded in this file。

## [2.0.0]
### new
- supportApolloas a service registry

### change
- Optimize package dependencies
- optimizationRpcContext
- optimizationRpcLifecycle Management of Services in Communication
- optimizationremove冗余路由的方式
- optimization分布式锁的use,Avoid deadlock caused by service route registration
- removeIEfCoreDbContextPoolinterface
- optimization心跳检测

### repair
- repairuse内存缓存作为事务参与者导致服务实例存在多个的情况下Cancel and Confirm Stage Unable to Commitbug

## [1.2.0]

### new
- new Serilog Use as a recorder
- new身份认证授权包

### change
- delete Silky.Rpc Unnecessary dependencies of packages
- optimizationswaggerdocument generation

### repair
- repairConfirm阶段andCancelstage does not automatically commit local transactions andTCCdata stored in the transactionbug
- repair分布式事务中use Json as a codec，Cancel and Confirm The problem of abnormal stage parameter conversion
- repair rpc Calling exceptions that return empty results
- repair在rpcused during invocationJsonas a codec,input parameter validation failedbug
  
## [1.1.0]

### new
- 重命名项目名称and一些包的名称
- package EFCore for data access
- use miniProfile Perform performance monitoring
- use SkyApm Implement link tracking
- add viaMapsterA package that implements object mapping

### change
- passServiceCollectionoptimization服务注册的模块加载and模块support
- Refactoring Distributed Transactions
- useFilterImplement input parameter validation

### repair
- repair分布式锁中的bug
- repair客户端可能无法订阅服务注册中心的路由信息​​The problem
- repairzookeeperThe client session timed out and could not subscribe to the routing information of the service registry.bug