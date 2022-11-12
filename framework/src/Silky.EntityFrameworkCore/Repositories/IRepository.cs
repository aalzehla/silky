using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.DependencyInjection;
using Silky.EntityFrameworkCore.Entities;
using Silky.EntityFrameworkCore.Locators;

namespace Silky.EntityFrameworkCore.Repositories
{
    /// <summary>
    /// non-generic warehousing
    /// </summary>
    public partial interface IRepository
    {
        /// <summary>
        /// switch storage
        /// </summary>
        /// <typeparam name="TEntity">entity type</typeparam>
        /// <returns>Warehousing</returns>
        IRepository<TEntity> Change<TEntity>()
            where TEntity : class, IPrivateEntity, new();

        /// <summary>
        /// 切换多database上下文Warehousing
        /// </summary>
        /// <typeparam name="TEntity">entity type</typeparam>
        /// <typeparam name="TDbContextLocator">database context locator</typeparam>
        /// <returns>Warehousing</returns>
        IRepository<TEntity, TDbContextLocator> Change<TEntity, TDbContextLocator>()
            where TEntity : class, IPrivateEntity, new()
            where TDbContextLocator : class, IDbContextLocator;

        /// <summary>
        /// 重新构建并switch storage
        /// </summary>
        /// <remarks>pay attention，Scoped must be released manually</remarks>
        /// <typeparam name="TEntity">entity type</typeparam>
        /// <returns>Warehousing</returns>
        (IRepository<TEntity> Repository, IServiceScope Scoped) BuildChange<TEntity>()
            where TEntity : class, IPrivateEntity, new();

        /// <summary>
        /// 重新构建并切换多database上下文Warehousing
        /// </summary>
        /// <remarks>pay attention，Scoped must be released manually</remarks>
        /// <typeparam name="TEntity">entity type</typeparam>
        /// <typeparam name="TDbContextLocator">database context locator</typeparam>
        /// <returns>Warehousing</returns>
        (IRepository<TEntity, TDbContextLocator> Repository, IServiceScope Scoped) BuildChange<TEntity,
            TDbContextLocator>()
            where TEntity : class, IPrivateEntity, new()
            where TDbContextLocator : class, IDbContextLocator;

        /// <summary>
        /// Obtain Sql 操作Warehousing
        /// </summary>
        /// <returns>ISqlRepository</returns>
        ISqlRepository Sql();

        /// <summary>
        /// Obtain多database上下文 Sql 操作Warehousing
        /// </summary>
        /// <returns>ISqlRepository{TDbContextLocator}</returns>
        ISqlRepository<TDbContextLocator> Sql<TDbContextLocator>()
            where TDbContextLocator : class, IDbContextLocator;
    }

    /// <summary>
    /// Warehousing接口
    /// </summary>
    /// <typeparam name="TEntity">entity type</typeparam>
    public partial interface IRepository<TEntity> : IPrivateRepository<TEntity>
        , IWritableRepository<TEntity>
        , IReadableRepository<TEntity>
        , ISqlRepository
        where TEntity : class, IPrivateEntity, new()
    {
    }

    /// <summary>
    /// 多database上下文Warehousing
    /// </summary>
    /// <typeparam name="TEntity">entity type</typeparam>
    /// <typeparam name="TDbContextLocator">database context locator</typeparam>
    public partial interface IRepository<TEntity, TDbContextLocator> : IPrivateRepository<TEntity>
        , IWritableRepository<TEntity, TDbContextLocator>
        , IReadableRepository<TEntity, TDbContextLocator>
        , ISqlRepository<TDbContextLocator>
        where TEntity : class, IPrivateEntity, new()
        where TDbContextLocator : class, IDbContextLocator
    {
    }

    /// <summary>
    /// private public entity
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public interface IPrivateRepository<TEntity>
        : IPrivateWritableRepository<TEntity>
            , IPrivateReadableRepository<TEntity>
            , IPrivateSqlRepository
        where TEntity : class, IPrivateEntity, new()
    {
        /// <summary>
        /// entity collection
        /// </summary>
        DbSet<TEntity> Entities { get; }

        /// <summary>
        /// not tracked（derail）entity
        /// </summary>
        IQueryable<TEntity> DetachedEntities { get; }

        /// <summary>
        /// 查看entity type
        /// </summary>
        IEntityType EntityType { get; }

        /// <summary>
        /// database connection object
        /// </summary>
        DbConnection DbConnection { get; }

        /// <summary>
        /// entity追综器
        /// </summary>
        ChangeTracker ChangeTracker { get; }

        /// <summary>
        /// entity模型
        /// </summary>
        IModel Model { get; }

        // /// <summary>
        // /// Tenant information
        // /// </summary>
        // Tenant Tenant { get; }

        /// <summary>
        /// database provider name
        /// </summary>
        string ProviderName { get; }

        /// <summary>
        /// service provider
        /// </summary>
        IServiceProvider ServiceProvider { get; }

        /// <summary>
        /// tenantId
        /// </summary>
        Guid? TenantId { get; }

        /// <summary>
        /// Determine if the context has changed
        /// </summary>
        /// <returns>bool</returns>
        bool HasChanges();

        /// <summary>
        /// 将entity加入数据上下文托管
        /// </summary>
        /// <param name="entity">entity</param>
        /// <returns>EntityEntry</returns>
        EntityEntry Entry(object entity);

        /// <summary>
        /// 将entity加入数据上下文托管
        /// </summary>
        /// <param name="entity">entity</param>
        /// <returns></returns>
        EntityEntry<TEntity> Entry(TEntity entity);

        /// <summary>
        /// Obtainentity状态
        /// </summary>
        /// <param name="entity">entity</param>
        /// <returns></returns>
        EntityState EntityEntryState(object entity);

        /// <summary>
        /// Obtainentity状态
        /// </summary>
        /// <param name="entity">entity</param>
        /// <returns>EntityState</returns>
        EntityState EntityEntryState(TEntity entity);

        /// <summary>
        /// 将entity属性加入托管
        /// </summary>
        /// <param name="entity">entity</param>
        /// <param name="propertyName">property name</param>
        /// <returns>PropertyEntry</returns>
        PropertyEntry EntityPropertyEntry(object entity, string propertyName);

        /// <summary>
        /// 将entity属性加入托管
        /// </summary>
        /// <param name="entity">entity</param>
        /// <param name="propertyName">property name</param>
        /// <returns>PropertyEntry</returns>
        PropertyEntry EntityPropertyEntry(TEntity entity, string propertyName);

        /// <summary>
        /// 将entity属性加入托管
        /// </summary>
        /// <typeparam name="TProperty">property type</typeparam>
        /// <param name="entity">entity</param>
        /// <param name="propertyPredicate">property expression</param>
        /// <returns>PropertyEntry</returns>
        PropertyEntry<TEntity, TProperty> EntityPropertyEntry<TProperty>(TEntity entity,
            Expression<Func<TEntity, TProperty>> propertyPredicate);

        /// <summary>
        /// 改变entity状态
        /// </summary>
        /// <param name="entity">entity</param>
        /// <param name="entityState">entity状态</param>
        /// <returns>EntityEntry</returns>
        EntityEntry ChangeEntityState(object entity, EntityState entityState);

        /// <summary>
        /// 改变entity状态
        /// </summary>
        /// <param name="entity">entity</param>
        /// <param name="entityState">entity状态</param>
        /// <returns>EntityEntry{TEntity}</returns>
        EntityEntry<TEntity> ChangeEntityState(TEntity entity, EntityState entityState);

        /// <summary>
        /// 改变entity状态
        /// </summary>
        /// <param name="entityEntry">entity条目</param>
        /// <param name="entityState">entity状态</param>
        /// <returns>EntityEntry</returns>
        EntityEntry ChangeEntityState(EntityEntry entityEntry, EntityState entityState);

        /// <summary>
        /// 改变entity状态
        /// </summary>
        /// <param name="entityEntry">entity条目</param>
        /// <param name="entityState">entity状态</param>
        /// <returns>EntityEntry{TEntity}</returns>
        EntityEntry<TEntity> ChangeEntityState(EntityEntry<TEntity> entityEntry, EntityState entityState);

        /// <summary>
        /// 检查entity跟踪状态
        /// </summary>
        /// <param name="id"></param>
        /// <param name="entityEntry"></param>
        /// <param name="keyName"></param>
        /// <returns></returns>
        bool CheckTrackState(object id, out EntityEntry entityEntry, string keyName = default);

        /// <summary>
        /// 检查entity跟踪状态
        /// </summary>
        /// <typeparam name="TTrackEntity"></typeparam>
        /// <param name="id"></param>
        /// <param name="entityEntry"></param>
        /// <param name="keyName"></param>
        /// <returns></returns>
        bool CheckTrackState<TTrackEntity>(object id, out EntityEntry entityEntry, string keyName = default)
            where TTrackEntity : class, IPrivateEntity, new();

        /// <summary>
        /// determine whether to attach
        /// </summary>
        /// <param name="entity">entity</param>
        /// <returns>bool</returns>
        bool IsAttached(object entity);

        /// <summary>
        /// determine whether to attach
        /// </summary>
        /// <param name="entity">entity</param>
        /// <returns>bool</returns>
        bool IsAttached(TEntity entity);

        /// <summary>
        /// 附加entity
        /// </summary>
        /// <param name="entity">entity</param>
        /// <returns>EntityEntry</returns>
        EntityEntry Attach(object entity);

        /// <summary>
        /// 附加entity
        /// </summary>
        /// <param name="entity">entity</param>
        /// <returns>EntityEntry</returns>
        EntityEntry<TEntity> Attach(TEntity entity);

        /// <summary>
        /// 附加多个entity
        /// </summary>
        /// <param name="entities">多个entity</param>
        void AttachRange(params object[] entities);

        /// <summary>
        /// 附加多个entity
        /// </summary>
        /// <param name="entities">多个entity</param>
        void AttachRange(IEnumerable<TEntity> entities);

        /// <summary>
        /// 取消附加entity
        /// </summary>
        /// <param name="entity">entity</param>
        void Detach(object entity);

        /// <summary>
        /// 取消附加entity
        /// </summary>
        /// <param name="entity">entity</param>
        void Detach(TEntity entity);

        /// <summary>
        /// 取消附加entity
        /// </summary>
        /// <param name="entityEntry">entity条目</param>
        void Detach(EntityEntry entityEntry);

        /// <summary>
        /// 取消附加entity
        /// </summary>
        /// <param name="entityEntry">entity条目</param>
        void Detach(EntityEntry<TEntity> entityEntry);

        /// <summary>
        /// Obtain所有database上下文
        /// </summary>
        /// <returns>ConcurrentBag{DbContext}</returns>
        public ConcurrentDictionary<Guid, DbContext> GetDbContexts();

        /// <summary>
        /// 判断entity是否设置了主键
        /// </summary>
        /// <param name="entity">entity</param>
        /// <returns>bool</returns>
        bool IsKeySet(TEntity entity);

        /// <summary>
        /// delete database
        /// </summary>
        void EnsureDeleted();

        /// <summary>
        /// delete database
        /// </summary>
        Task EnsureDeletedAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// create database
        /// </summary>
        void EnsureCreated();

        /// <summary>
        /// create database
        /// </summary>
        Task EnsureCreatedAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Dynamically change table name
        /// </summary>
        /// <param name="tableName">Table Name</param>
        [Obsolete("This method is obsolete，please call BuildChange<TEntity> method instead。")]
        void ChangeTable(string tableName);

        /// <summary>
        /// Dynamically change the database
        /// </summary>
        /// <param name="connectionString">connection string</param>
        void ChangeDatabase(string connectionString);

        /// <summary>
        /// Dynamically change the database
        /// </summary>
        /// <param name="connectionString">connection string</param>
        /// <param name="cancellationToken">Asynchronous cancellation token</param>
        Task ChangeDatabaseAsync(string connectionString, CancellationToken cancellationToken = default);

        /// <summary>
        /// determine whether SqlServer database
        /// </summary>
        /// <returns>bool</returns>
        bool IsSqlServer();

        /// <summary>
        /// determine whether Sqlite database
        /// </summary>
        /// <returns>bool</returns>
        bool IsSqlite();

        /// <summary>
        /// determine whether Cosmos database
        /// </summary>
        /// <returns>bool</returns>
        bool IsCosmos();

        /// <summary>
        /// determine whether in memory database
        /// </summary>
        /// <returns>bool</returns>
        bool InMemoryDatabase();

        /// <summary>
        /// determine whether MySql database
        /// </summary>
        /// <returns>bool</returns>
        bool IsMySql();

        /// <summary>
        /// determine whether MySql database official package（Update is not timely，only support 8.0.23+ Version， So make a separate category）
        /// </summary>
        /// <returns>bool</returns>
        bool IsMySqlOfficial();

        /// <summary>
        /// determine whether PostgreSQL database
        /// </summary>
        /// <returns>bool</returns>
        bool IsNpgsql();

        /// <summary>
        /// determine whether Oracle database
        /// </summary>
        /// <returns>bool</returns>
        bool IsOracle();

        /// <summary>
        /// determine whether Firebird database
        /// </summary>
        /// <returns>bool</returns>
        bool IsFirebird();

        /// <summary>
        /// determine whether Dm database
        /// </summary>
        /// <returns>bool</returns>
        bool IsDm();

        /// <summary>
        /// determine whether关系型database
        /// </summary>
        /// <returns>bool</returns>
        bool IsRelational();

        /// <summary>
        /// switch storage
        /// </summary>
        /// <typeparam name="TChangeEntity">entity type</typeparam>
        /// <returns>Warehousing</returns>
        new IRepository<TChangeEntity> Change<TChangeEntity>()
            where TChangeEntity : class, IPrivateEntity, new();

        /// <summary>
        /// 切换多database上下文Warehousing
        /// </summary>
        /// <typeparam name="TChangeEntity">entity type</typeparam>
        /// <typeparam name="TChangeDbContextLocator">database context locator</typeparam>
        /// <returns>Warehousing</returns>
        IRepository<TChangeEntity, TChangeDbContextLocator> Change<TChangeEntity, TChangeDbContextLocator>()
            where TChangeEntity : class, IPrivateEntity, new()
            where TChangeDbContextLocator : class, IDbContextLocator;

        /// <summary>
        /// 重新构建并switch storage
        /// </summary>
        /// <remarks>pay attention，Scoped must be released manually</remarks>
        /// <typeparam name="TChangeEntity">entity type</typeparam>
        /// <returns>Warehousing</returns>
        (IRepository<TChangeEntity> Repository, IServiceScope Scoped) BuildChange<TChangeEntity>()
            where TChangeEntity : class, IPrivateEntity, new();

        /// <summary>
        /// 重新构建并切换多database上下文Warehousing
        /// </summary>
        /// <remarks>pay attention，Scoped must be released manually</remarks>
        /// <typeparam name="TChangeEntity">entity type</typeparam>
        /// <typeparam name="TChangeDbContextLocator">database context locator</typeparam>
        /// <returns>Warehousing</returns>
        (IRepository<TChangeEntity, TChangeDbContextLocator> Repository, IServiceScope Scoped) BuildChange<
            TChangeEntity, TChangeDbContextLocator>()
            where TChangeEntity : class, IPrivateEntity, new()
            where TChangeDbContextLocator : class, IDbContextLocator;
    }
}