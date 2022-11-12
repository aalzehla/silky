---
title: Distributed lock
lang: zh-cn
---

## introduce

In order to ensure that a method or property can only be executed by the same thread at the same time under high concurrency，In the case of traditional single application single machine deployment，Mutual exclusion control can be performed using concurrent processing related functions。but，As business needs grow，After the original single-machine deployment system is evolved into a distributed cluster system，Due to the multi-threading of distributed systems、Multiprocess and distributed on different machines，This will invalidate the concurrency control lock strategy in the original stand-alone deployment，单纯的应用并不能提供Distributed lock的能力。In order to solve this problem; a cross-machine mutual exclusion mechanism is needed to control access to shared resources，这就是Distributed lock要解决的问题。

Distributed lock应该具备哪些条件？

1. in a distributed system environment，A method can only be executed by one thread of one machine at a time；
2. Highly available lock acquisition and lock release；
3. High-performance lock acquisition and lock release；
4. Reentrant；
5. With lock failure mechanism，prevent deadlock；
6. Has a non-blocking lock feature，That is; if the lock is not acquired; it will directly return the failure to acquire the lock。

## silky框架的Distributed lock

silkyframe usage[RedLock.net](https://github.com/samcook/RedLock.net)实现Distributed lock,RedLock.netuseredis服务实现的Distributed lock。

silkyThe framework is in the process of service entry registration,use到了Distributed lock,Avoid the simultaneous registration of unified service entries due to multiple service instances,The problem that caused the service address not to be updated。由于在框架层面use了Distributed lock,so,In common business application services,开发者必须要对Distributed lockuse到的`redis`service to configure。

