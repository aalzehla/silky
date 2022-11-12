---
title: object-to-object mapping
lang: zh-cn
---

## Object Mapping Concept

Batch map data from one object to another object according to specific rules，Reduce manual operations and reduce human error rates。as will DTO object maps to Entity in entity，vice versa。

silkyframe usage[AutoMapper](https://github.com/AutoMapper/AutoMapper)Packages as Object Mapping Tools。

late,silkyThe framework is also ready to be extended using[Mapster](https://github.com/MapsterMapper/Mapster)Packages as Object Mapping Tools。

## usage

### useAutoMapperas a mapping tool

1. starting the module(StartUpModule)middle，explicit dependencies`AutoMapperModule`module。
   
   如果启动module指定的是`NormHostModule`,So,该module已经指定依赖`AutoMapperModule`module。

2. by inheritance`Profile`base class,在其构造器middle指定`source`and`Purpose`The type mapping relationship of。
  
  E.g:

```csharp
    public class AccountProfile : Profile
    {
        public AccountProfile()
        {
            CreateMap<CreateAccountInput, Domain.Accounts.Account>();
            CreateMap<Domain.Accounts.Account, GetAccountOutput>();
            CreateMap<UpdateAccountInput, Domain.Accounts.Account>().AfterMap((src, dest) =>
            {
                dest.UpdateTime = DateTime.Now;
                dest.UpdateBy = NullSession.Instance.UserId;
            });
        }
    }
```

3. pass`MapTo`method to implement object property mapping

```csharp
public async Task<GetAccountOutput> Create(CreateAccountInput input)
{
   // Input type objects are mapped to entities
   var account = input.MapTo<Domain.Accounts.Account>();
   account = await _accountDomainService.Create(account);
   // Entity objects are mapped to output objects   
   return account.MapTo<GetAccountOutput>();
}
```

```csharp
public async void Update(UpdateAccountInput input)
{
   var account = await GetAccountById(input.Id);
   account = input.MapTo(account); //pass输入对象更新实体属性
}
```
