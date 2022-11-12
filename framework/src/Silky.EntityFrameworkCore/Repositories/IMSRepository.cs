using System;
using Silky.EntityFrameworkCore.Entities;
using Silky.EntityFrameworkCore.Locators;

namespace Silky.EntityFrameworkCore.Repositories
{
    /// <summary>
    /// Random master-slave library storage（The main database is the default database）
    /// </summary>
    public partial interface IMSRepository : IMSRepository<MasterDbContextLocator>
    {
    }

    /// <summary>
    /// Random master-slave library storage
    /// </summary>
    /// <typeparam name="TMasterDbContextLocator">main library</typeparam>
    public partial interface IMSRepository<TMasterDbContextLocator>
        where TMasterDbContextLocator : class, IDbContextLocator
    {
        /// <summary>
        /// 获取main library仓储
        /// </summary>
        /// <typeparam name="TEntity">entity type</typeparam>
        /// <returns></returns>
        IRepository<TEntity, TMasterDbContextLocator> Master<TEntity>()
            where TEntity : class, IPrivateEntity, new();

        /// <summary>
        /// Dynamically fetch from the library（random）
        /// </summary>
        /// <typeparam name="TEntity">entity type</typeparam>
        /// <returns></returns>
        IPrivateReadableRepository<TEntity> Slave<TEntity>()
            where TEntity : class, IPrivateEntity, new();

        /// <summary>
        /// Dynamically fetch from the library（customize）
        /// </summary>
        /// <typeparam name="TEntity">entity type</typeparam>
        /// <returns></returns>
        IPrivateReadableRepository<TEntity> Slave<TEntity>(Func<Type> locatorHandle)
            where TEntity : class, IPrivateEntity, new();
    }

    /// <summary>
    /// Master-slave warehousing
    /// </summary>
    /// <typeparam name="TMasterDbContextLocator">main library</typeparam>
    /// <typeparam name="TSlaveDbContextLocator1">from the library</typeparam>
    public partial interface IMSRepository<TMasterDbContextLocator, TSlaveDbContextLocator1>
        where TMasterDbContextLocator : class, IDbContextLocator
        where TSlaveDbContextLocator1 : class, IDbContextLocator
    {
        /// <summary>
        /// 获取main library仓储
        /// </summary>
        /// <typeparam name="TEntity">entity type</typeparam>
        /// <returns></returns>
        IRepository<TEntity, TMasterDbContextLocator> Master<TEntity>()
            where TEntity : class, IPrivateEntity, new();

        /// <summary>
        /// 获取from the library仓储
        /// </summary>
        /// <typeparam name="TEntity">entity type</typeparam>
        /// <returns></returns>
        IReadableRepository<TEntity, TSlaveDbContextLocator1> Slave1<TEntity>()
            where TEntity : class, IPrivateEntity, new();
    }

    /// <summary>
    /// Master-slave warehousing
    /// </summary>
    /// <typeparam name="TMasterDbContextLocator">main library</typeparam>
    /// <typeparam name="TSlaveDbContextLocator1">from the library</typeparam>
    /// <typeparam name="TSlaveDbContextLocator2">from the library</typeparam>
    public partial interface IMSRepository<TMasterDbContextLocator, TSlaveDbContextLocator1, TSlaveDbContextLocator2>
        : IMSRepository<TMasterDbContextLocator, TSlaveDbContextLocator1>
        where TMasterDbContextLocator : class, IDbContextLocator
        where TSlaveDbContextLocator1 : class, IDbContextLocator
        where TSlaveDbContextLocator2 : class, IDbContextLocator
    {
        /// <summary>
        /// 获取from the library仓储2
        /// </summary>
        /// <typeparam name="TEntity">entity type</typeparam>
        /// <returns></returns>
        IReadableRepository<TEntity, TSlaveDbContextLocator2> Slave2<TEntity>()
            where TEntity : class, IPrivateEntity, new();
    }

    /// <summary>
    /// Master-slave warehousing
    /// </summary>
    /// <typeparam name="TMasterDbContextLocator">main library</typeparam>
    /// <typeparam name="TSlaveDbContextLocator1">from the library</typeparam>
    /// <typeparam name="TSlaveDbContextLocator2">from the library</typeparam>
    /// <typeparam name="TSlaveDbContextLocator3">from the library</typeparam>
    public partial interface IMSRepository<TMasterDbContextLocator, TSlaveDbContextLocator1, TSlaveDbContextLocator2,
            TSlaveDbContextLocator3>
        : IMSRepository<TMasterDbContextLocator, TSlaveDbContextLocator1, TSlaveDbContextLocator2>
        where TMasterDbContextLocator : class, IDbContextLocator
        where TSlaveDbContextLocator1 : class, IDbContextLocator
        where TSlaveDbContextLocator2 : class, IDbContextLocator
        where TSlaveDbContextLocator3 : class, IDbContextLocator
    {
        /// <summary>
        /// 获取from the library仓储3
        /// </summary>
        /// <typeparam name="TEntity">entity type</typeparam>
        /// <returns></returns>
        IReadableRepository<TEntity, TSlaveDbContextLocator3> Slave3<TEntity>()
            where TEntity : class, IPrivateEntity, new();
    }

    /// <summary>
    /// Master-slave warehousing
    /// </summary>
    /// <typeparam name="TMasterDbContextLocator">main library</typeparam>
    /// <typeparam name="TSlaveDbContextLocator1">from the library</typeparam>
    /// <typeparam name="TSlaveDbContextLocator2">from the library</typeparam>
    /// <typeparam name="TSlaveDbContextLocator3">from the library</typeparam>
    /// <typeparam name="TSlaveDbContextLocator4">from the library</typeparam>
    public partial interface IMSRepository<TMasterDbContextLocator, TSlaveDbContextLocator1, TSlaveDbContextLocator2,
            TSlaveDbContextLocator3, TSlaveDbContextLocator4>
        : IMSRepository<TMasterDbContextLocator, TSlaveDbContextLocator1, TSlaveDbContextLocator2,
            TSlaveDbContextLocator3>
        where TMasterDbContextLocator : class, IDbContextLocator
        where TSlaveDbContextLocator1 : class, IDbContextLocator
        where TSlaveDbContextLocator2 : class, IDbContextLocator
        where TSlaveDbContextLocator3 : class, IDbContextLocator
        where TSlaveDbContextLocator4 : class, IDbContextLocator
    {
        /// <summary>
        /// 获取from the library仓储4
        /// </summary>
        /// <typeparam name="TEntity">entity type</typeparam>
        /// <returns></returns>
        IReadableRepository<TEntity, TSlaveDbContextLocator4> Slave4<TEntity>()
            where TEntity : class, IPrivateEntity, new();
    }

    /// <summary>
    /// Master-slave warehousing
    /// </summary>
    /// <typeparam name="TMasterDbContextLocator">main library</typeparam>
    /// <typeparam name="TSlaveDbContextLocator1">from the library</typeparam>
    /// <typeparam name="TSlaveDbContextLocator2">from the library</typeparam>
    /// <typeparam name="TSlaveDbContextLocator3">from the library</typeparam>
    /// <typeparam name="TSlaveDbContextLocator4">from the library</typeparam>
    /// <typeparam name="TSlaveDbContextLocator5">from the library</typeparam>
    public partial interface IMSRepository<TMasterDbContextLocator, TSlaveDbContextLocator1, TSlaveDbContextLocator2,
            TSlaveDbContextLocator3, TSlaveDbContextLocator4, TSlaveDbContextLocator5>
        : IMSRepository<TMasterDbContextLocator, TSlaveDbContextLocator1, TSlaveDbContextLocator2,
            TSlaveDbContextLocator3, TSlaveDbContextLocator4>
        where TMasterDbContextLocator : class, IDbContextLocator
        where TSlaveDbContextLocator1 : class, IDbContextLocator
        where TSlaveDbContextLocator2 : class, IDbContextLocator
        where TSlaveDbContextLocator3 : class, IDbContextLocator
        where TSlaveDbContextLocator4 : class, IDbContextLocator
        where TSlaveDbContextLocator5 : class, IDbContextLocator
    {
        /// <summary>
        /// 获取from the library仓储5
        /// </summary>
        /// <typeparam name="TEntity">entity type</typeparam>
        /// <returns></returns>
        IReadableRepository<TEntity, TSlaveDbContextLocator5> Slave5<TEntity>()
            where TEntity : class, IPrivateEntity, new();
    }

    /// <summary>
    /// Master-slave warehousing
    /// </summary>
    /// <typeparam name="TMasterDbContextLocator">main library</typeparam>
    /// <typeparam name="TSlaveDbContextLocator1">from the library</typeparam>
    /// <typeparam name="TSlaveDbContextLocator2">from the library</typeparam>
    /// <typeparam name="TSlaveDbContextLocator3">from the library</typeparam>
    /// <typeparam name="TSlaveDbContextLocator4">from the library</typeparam>
    /// <typeparam name="TSlaveDbContextLocator5">from the library</typeparam>
    /// <typeparam name="TSlaveDbContextLocator6">from the library</typeparam>
    public partial interface IMSRepository<TMasterDbContextLocator, TSlaveDbContextLocator1, TSlaveDbContextLocator2,
            TSlaveDbContextLocator3, TSlaveDbContextLocator4, TSlaveDbContextLocator5, TSlaveDbContextLocator6>
        : IMSRepository<TMasterDbContextLocator, TSlaveDbContextLocator1, TSlaveDbContextLocator2,
            TSlaveDbContextLocator3, TSlaveDbContextLocator4, TSlaveDbContextLocator5>
        where TMasterDbContextLocator : class, IDbContextLocator
        where TSlaveDbContextLocator1 : class, IDbContextLocator
        where TSlaveDbContextLocator2 : class, IDbContextLocator
        where TSlaveDbContextLocator3 : class, IDbContextLocator
        where TSlaveDbContextLocator4 : class, IDbContextLocator
        where TSlaveDbContextLocator5 : class, IDbContextLocator
        where TSlaveDbContextLocator6 : class, IDbContextLocator
    {
        /// <summary>
        /// 获取from the library仓储6
        /// </summary>
        /// <typeparam name="TEntity">entity type</typeparam>
        /// <returns></returns>
        IReadableRepository<TEntity, TSlaveDbContextLocator6> Slave6<TEntity>()
            where TEntity : class, IPrivateEntity, new();
    }

    /// <summary>
    /// Master-slave warehousing
    /// </summary>
    /// <typeparam name="TMasterDbContextLocator">main library</typeparam>
    /// <typeparam name="TSlaveDbContextLocator1">from the library</typeparam>
    /// <typeparam name="TSlaveDbContextLocator2">from the library</typeparam>
    /// <typeparam name="TSlaveDbContextLocator3">from the library</typeparam>
    /// <typeparam name="TSlaveDbContextLocator4">from the library</typeparam>
    /// <typeparam name="TSlaveDbContextLocator5">from the library</typeparam>
    /// <typeparam name="TSlaveDbContextLocator6">from the library</typeparam>
    /// <typeparam name="TSlaveDbContextLocator7">from the library</typeparam>
    public partial interface IMSRepository<TMasterDbContextLocator, TSlaveDbContextLocator1, TSlaveDbContextLocator2,
            TSlaveDbContextLocator3, TSlaveDbContextLocator4, TSlaveDbContextLocator5, TSlaveDbContextLocator6,
            TSlaveDbContextLocator7>
        : IMSRepository<TMasterDbContextLocator, TSlaveDbContextLocator1, TSlaveDbContextLocator2,
            TSlaveDbContextLocator3, TSlaveDbContextLocator4, TSlaveDbContextLocator5, TSlaveDbContextLocator6>
        where TMasterDbContextLocator : class, IDbContextLocator
        where TSlaveDbContextLocator1 : class, IDbContextLocator
        where TSlaveDbContextLocator2 : class, IDbContextLocator
        where TSlaveDbContextLocator3 : class, IDbContextLocator
        where TSlaveDbContextLocator4 : class, IDbContextLocator
        where TSlaveDbContextLocator5 : class, IDbContextLocator
        where TSlaveDbContextLocator6 : class, IDbContextLocator
        where TSlaveDbContextLocator7 : class, IDbContextLocator
    {
        /// <summary>
        /// 获取from the library仓储7
        /// </summary>
        /// <typeparam name="TEntity">entity type</typeparam>
        /// <returns></returns>
        IReadableRepository<TEntity, TSlaveDbContextLocator7> Slave7<TEntity>()
            where TEntity : class, IPrivateEntity, new();
    }
}