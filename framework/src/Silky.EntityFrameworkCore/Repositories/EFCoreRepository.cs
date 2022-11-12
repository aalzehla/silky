using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.DependencyInjection;
using Silky.Core;
using Silky.Core.DbContext;
using Silky.EntityFrameworkCore.ContextPool;
using Silky.EntityFrameworkCore.Entities;
using Silky.EntityFrameworkCore.Locators;

namespace Silky.EntityFrameworkCore.Repositories
{
    /// <summary>
    /// non-genericEF CoreWarehousing implementation
    /// </summary>
    public partial class EFCoreRepository : IRepository
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public EFCoreRepository()
        {
        }

        /// <summary>
        /// switch storage
        /// </summary>
        /// <typeparam name="TEntity">entity type</typeparam>
        /// <returns>Warehousing</returns>
        public virtual IRepository<TEntity> Change<TEntity>()
            where TEntity : class, IPrivateEntity, new()
        {
            return EngineContext.Current.Resolve<IRepository<TEntity>>();
        }

        /// <summary>
        /// 切换多database上下文Warehousing
        /// </summary>
        /// <typeparam name="TEntity">entity type</typeparam>
        /// <typeparam name="TDbContextLocator">database context locator</typeparam>
        /// <returns>Warehousing</returns>
        public virtual IRepository<TEntity, TDbContextLocator> Change<TEntity, TDbContextLocator>()
            where TEntity : class, IPrivateEntity, new()
            where TDbContextLocator : class, IDbContextLocator
        {
            return EngineContext.Current.Resolve<IRepository<TEntity, TDbContextLocator>>();
        }

        /// <summary>
        /// 重新构建并switch storage
        /// </summary>
        /// <remarks>pay attention，Scoped must be released manually</remarks>
        /// <typeparam name="TEntity">entity type</typeparam>
        /// <returns>Warehousing</returns>
        public virtual (IRepository<TEntity> Repository, IServiceScope Scoped) BuildChange<TEntity>()
            where TEntity : class, IPrivateEntity, new()
        {
            var scoped = EngineContext.Current.ServiceProvider.CreateScope();
            var repository = scoped.ServiceProvider.GetService<IRepository<TEntity>>();

            // Add unmanaged object
            // App.UnmanagedObjects.Add(scoped);

            return (repository, scoped);
        }

        /// <summary>
        /// 重新构建并切换多database上下文Warehousing
        /// </summary>
        /// <remarks>pay attention，Scoped must be released manually</remarks>
        /// <typeparam name="TEntity">entity type</typeparam>
        /// <typeparam name="TDbContextLocator">database context locator</typeparam>
        /// <returns>Warehousing</returns>
        public virtual (IRepository<TEntity, TDbContextLocator> Repository, IServiceScope Scoped) BuildChange<TEntity,
            TDbContextLocator>()
            where TEntity : class, IPrivateEntity, new()
            where TDbContextLocator : class, IDbContextLocator
        {
            var scoped = EngineContext.Current.ServiceProvider.CreateScope();
            var repository = scoped.ServiceProvider.GetService<IRepository<TEntity, TDbContextLocator>>();

            // Add unmanaged object
            // App.UnmanagedObjects.Add(scoped);

            return (repository, scoped);
        }

        /// <summary>
        /// Obtain Sql 操作Warehousing
        /// </summary>
        /// <returns>ISqlRepository</returns>
        public virtual ISqlRepository Sql()
        {
            return EngineContext.Current.Resolve<ISqlRepository>();
        }

        /// <summary>
        /// Obtain多database上下文 Sql 操作Warehousing
        /// </summary>
        /// <returns>ISqlRepository{TDbContextLocator}</returns>
        public virtual ISqlRepository<TDbContextLocator> Sql<TDbContextLocator>()
            where TDbContextLocator : class, IDbContextLocator
        {
            return EngineContext.Current.Resolve<ISqlRepository<TDbContextLocator>>();
        }
    }

    /// <summary>
    /// EF CoreWarehousing implementation
    /// </summary>
    /// <typeparam name="TEntity">entity type</typeparam>
    public partial class EFCoreRepository<TEntity> : EFCoreRepository<TEntity, MasterDbContextLocator>
        , IRepository<TEntity>
        where TEntity : class, IPrivateEntity, new()
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public EFCoreRepository()
        {
        }
    }

    /// <summary>
    /// 多database上下文Warehousing
    /// </summary>
    public partial class EFCoreRepository<TEntity, TDbContextLocator> : PrivateRepository<TEntity>
        , IRepository<TEntity, TDbContextLocator>
        where TEntity : class, IPrivateEntity, new()
        where TDbContextLocator : class, IDbContextLocator
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public EFCoreRepository() : base(typeof(TDbContextLocator))
        {
        }
    }

    /// <summary>
    /// 私有Warehousing
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public partial class PrivateRepository<TEntity> : PrivateSqlRepository, IPrivateRepository<TEntity>
        where TEntity : class, IPrivateEntity, new()
    {
        /// <summary>
        /// non-genericWarehousing
        /// </summary>
        private readonly IRepository _repository;

        /// <summary>
        /// database context pool
        /// </summary>
        private readonly EfCoreDbContextPool _silkyDbContextPool;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="dbContextLocator"></param>
        public PrivateRepository(Type dbContextLocator) : base(dbContextLocator)
        {
            DbConnection = Database.GetDbConnection();
            ChangeTracker = Context.ChangeTracker;
            Model = Context.Model;
            
            // set provider name
            ProviderName = Database.ProviderName;

            //Initialize entity
            Entities = Context.Set<TEntity>();
            DetachedEntities = Entities.AsNoTracking();
            EntityType = Entities.EntityType;

            // Initialize the data context pool
            _silkyDbContextPool = (EngineContext.Current.Resolve<ISilkyDbContextPool>() as EfCoreDbContextPool);

            // non-genericWarehousing
            _repository = EngineContext.Current.Resolve<IRepository>();
        }

        /// <summary>
        /// entity collection
        /// </summary>
        public virtual DbSet<TEntity> Entities { get; }

        /// <summary>
        /// not tracked（derail）entity
        /// </summary>
        public virtual IQueryable<TEntity> DetachedEntities { get; }

        /// <summary>
        /// 查看entity type
        /// </summary>
        public virtual IEntityType EntityType { get; }

        /// <summary>
        /// database connection object
        /// </summary>
        public virtual DbConnection DbConnection { get; }

        /// <summary>
        /// entity追综器
        /// </summary>
        public virtual ChangeTracker ChangeTracker { get; }

        /// <summary>
        /// entity模型
        /// </summary>
        public virtual IModel Model { get; }

        
        /// <summary>
        /// database provider name
        /// </summary>
        public virtual string ProviderName { get; }

        /// <summary>
        /// service provider
        /// </summary>
        public virtual IServiceProvider ServiceProvider { get; }

        /// <summary>
        /// tenantId
        /// </summary>
        public virtual Guid? TenantId { get; }

        /// <summary>
        /// Determine if the context has changed
        /// </summary>
        /// <returns>bool</returns>
        public virtual bool HasChanges()
        {
            return ChangeTracker.HasChanges();
        }

        /// <summary>
        /// 将entity加入数据上下文托管
        /// </summary>
        /// <param name="entity">entity</param>
        /// <returns>EntityEntry</returns>
        public virtual EntityEntry Entry(object entity)
        {
            return Context.Entry(entity);
        }

        /// <summary>
        /// 将entity加入数据上下文托管
        /// </summary>
        /// <param name="entity">entity</param>
        /// <returns></returns>
        public virtual EntityEntry<TEntity> Entry(TEntity entity)
        {
            return Context.Entry(entity);
        }

        /// <summary>
        /// Obtainentity状态
        /// </summary>
        /// <param name="entity">entity</param>
        /// <returns></returns>
        public virtual EntityState EntityEntryState(object entity)
        {
            return Entry(entity).State;
        }

        /// <summary>
        /// Obtainentity状态
        /// </summary>
        /// <param name="entity">entity</param>
        /// <returns>EntityState</returns>
        public virtual EntityState EntityEntryState(TEntity entity)
        {
            return Entry(entity).State;
        }

        /// <summary>
        /// 将entity属性加入托管
        /// </summary>
        /// <param name="entity">entity</param>
        /// <param name="propertyName">property name</param>
        /// <returns>PropertyEntry</returns>
        public virtual PropertyEntry EntityPropertyEntry(object entity, string propertyName)
        {
            return Entry(entity).Property(propertyName);
        }

        /// <summary>
        /// 将entity属性加入托管
        /// </summary>
        /// <param name="entity">entity</param>
        /// <param name="propertyName">property name</param>
        /// <returns>PropertyEntry</returns>
        public virtual PropertyEntry EntityPropertyEntry(TEntity entity, string propertyName)
        {
            return Entry(entity).Property(propertyName);
        }

        /// <summary>
        /// 将entity属性加入托管
        /// </summary>
        /// <typeparam name="TProperty">property type</typeparam>
        /// <param name="entity">entity</param>
        /// <param name="propertyPredicate">property expression</param>
        /// <returns>PropertyEntry</returns>
        public virtual PropertyEntry<TEntity, TProperty> EntityPropertyEntry<TProperty>(TEntity entity,
            Expression<Func<TEntity, TProperty>> propertyPredicate)
        {
            return Entry(entity).Property(propertyPredicate);
        }

        /// <summary>
        /// 改变entity状态
        /// </summary>
        /// <param name="entity">entity</param>
        /// <param name="entityState">entity状态</param>
        /// <returns>EntityEntry</returns>
        public virtual EntityEntry ChangeEntityState(object entity, EntityState entityState)
        {
            var entityEntry = Entry(entity);
            entityEntry.State = entityState;
            return entityEntry;
        }

        /// <summary>
        /// 改变entity状态
        /// </summary>
        /// <param name="entity">entity</param>
        /// <param name="entityState">entity状态</param>
        /// <returns>EntityEntry{TEntity}</returns>
        public virtual EntityEntry<TEntity> ChangeEntityState(TEntity entity, EntityState entityState)
        {
            var entityEntry = Entry(entity);
            entityEntry.State = entityState;
            return entityEntry;
        }

        /// <summary>
        /// 改变entity状态
        /// </summary>
        /// <param name="entityEntry">entity条目</param>
        /// <param name="entityState">entity状态</param>
        /// <returns>EntityEntry</returns>
        public virtual EntityEntry ChangeEntityState(EntityEntry entityEntry, EntityState entityState)
        {
            entityEntry.State = entityState;
            return entityEntry;
        }

        /// <summary>
        /// 改变entity状态
        /// </summary>
        /// <param name="entityEntry">entity条目</param>
        /// <param name="entityState">entity状态</param>
        /// <returns>EntityEntry{TEntity}</returns>
        public virtual EntityEntry<TEntity> ChangeEntityState(EntityEntry<TEntity> entityEntry, EntityState entityState)
        {
            entityEntry.State = entityState;
            return entityEntry;
        }

        /// <summary>
        /// 检查entity跟踪状态
        /// </summary>
        /// <param name="id"></param>
        /// <param name="entityEntry"></param>
        /// <param name="keyName"></param>
        /// <returns></returns>
        public virtual bool CheckTrackState(object id, out EntityEntry entityEntry, string keyName = default)
        {
            return CheckTrackState<TEntity>(id, out entityEntry, keyName);
        }

        /// <summary>
        /// 检查entity跟踪状态
        /// </summary>
        /// <typeparam name="TTrackEntity"></typeparam>
        /// <param name="id"></param>
        /// <param name="entityEntry"></param>
        /// <param name="keyName"></param>
        /// <returns></returns>
        public virtual bool CheckTrackState<TTrackEntity>(object id, out EntityEntry entityEntry,
            string keyName = default)
            where TTrackEntity : class, IPrivateEntity, new()
        {
            // Obtain主键名
            keyName ??= (typeof(TTrackEntity) == typeof(TEntity) ? EntityType : Context.Set<TTrackEntity>().EntityType)
                .FindPrimaryKey()?.Properties?.AsEnumerable()?.FirstOrDefault()?.PropertyInfo?.Name;

            // Check if tracked
            entityEntry = ChangeTracker.Entries().FirstOrDefault(u => u.Entity.GetType() == typeof(TTrackEntity)
                                                                      && u.CurrentValues[keyName].ToString()
                                                                          .Equals(id.ToString()));

            return entityEntry != null;
        }

        /// <summary>
        /// determine whether to attach
        /// </summary>
        /// <param name="entity">entity</param>
        /// <returns>bool</returns>
        public virtual bool IsAttached(object entity)
        {
            return EntityEntryState(entity) != EntityState.Detached;
        }

        /// <summary>
        /// determine whether to attach
        /// </summary>
        /// <param name="entity">entity</param>
        /// <returns>bool</returns>
        public virtual bool IsAttached(TEntity entity)
        {
            return EntityEntryState(entity) != EntityState.Detached;
        }

        /// <summary>
        /// 附加entity
        /// </summary>
        /// <param name="entity">entity</param>
        /// <returns>EntityEntry</returns>
        public virtual EntityEntry Attach(object entity)
        {
            return Context.Attach(entity);
        }

        /// <summary>
        /// 附加entity
        /// </summary>
        /// <param name="entity">entity</param>
        /// <returns>EntityEntry</returns>
        public virtual EntityEntry<TEntity> Attach(TEntity entity)
        {
            return Context.Attach(entity);
        }

        /// <summary>
        /// 附加多个entity
        /// </summary>
        /// <param name="entities">多个entity</param>
        public virtual void AttachRange(params object[] entities)
        {
            Context.AttachRange(entities);
        }

        /// <summary>
        /// 附加多个entity
        /// </summary>
        /// <param name="entities">多个entity</param>
        public virtual void AttachRange(IEnumerable<TEntity> entities)
        {
            Context.AttachRange(entities);
        }

        /// <summary>
        /// 取消附加entity
        /// </summary>
        /// <param name="entity">entity</param>
        public virtual void Detach(object entity)
        {
            ChangeEntityState(entity, EntityState.Detached);
        }

        /// <summary>
        /// 取消附加entity
        /// </summary>
        /// <param name="entity">entity</param>
        public virtual void Detach(TEntity entity)
        {
            ChangeEntityState(entity, EntityState.Detached);
        }

        /// <summary>
        /// 取消附加entity
        /// </summary>
        /// <param name="entityEntry">entity条目</param>
        public virtual void Detach(EntityEntry entityEntry)
        {
            ChangeEntityState(entityEntry, EntityState.Detached);
        }

        /// <summary>
        /// 取消附加entity
        /// </summary>
        /// <param name="entityEntry">entity条目</param>
        public virtual void Detach(EntityEntry<TEntity> entityEntry)
        {
            ChangeEntityState(entityEntry, EntityState.Detached);
        }

        /// <summary>
        /// Obtain所有database上下文
        /// </summary>
        /// <returns>ConcurrentBag{DbContext}</returns>
        public ConcurrentDictionary<Guid, DbContext> GetDbContexts()
        {
            return _silkyDbContextPool.GetDbContexts();
        }

        /// <summary>
        /// 判断entity是否设置了主键
        /// </summary>
        /// <param name="entity">entity</param>
        /// <returns>bool</returns>
        public virtual bool IsKeySet(TEntity entity)
        {
            return Entry(entity).IsKeySet;
        }

        /// <summary>
        /// delete database
        /// </summary>
        public virtual void EnsureDeleted()
        {
            Context.Database.EnsureDeleted();
        }

        /// <summary>
        /// delete database
        /// </summary>
        public virtual Task EnsureDeletedAsync(CancellationToken cancellationToken = default)
        {
            return Context.Database.EnsureDeletedAsync(cancellationToken);
        }

        /// <summary>
        /// create database
        /// </summary>
        public virtual void EnsureCreated()
        {
            Context.Database.EnsureCreated();
        }

        /// <summary>
        /// create database
        /// </summary>
        public virtual Task EnsureCreatedAsync(CancellationToken cancellationToken = default)
        {
            return Context.Database.EnsureCreatedAsync(cancellationToken);
        }

        /// <summary>
        /// Dynamically change table name
        /// </summary>
        /// <param name="tableName">Table Name</param>
        [Obsolete("This method is obsolete，please call BuildChange<TEntity> method instead。")]
        public virtual void ChangeTable(string tableName)
        {
            if (EntityType is IConventionEntityType convention)
            {
                convention.SetTableName(tableName);
            }
        }

        /// <summary>
        /// Dynamically change the database
        /// </summary>
        /// <param name="connectionString">connection string</param>
        public virtual void ChangeDatabase(string connectionString)
        {
            if (DbConnection.State == ConnectionState.Open) DbConnection.ChangeDatabase(connectionString);
            else DbConnection.ConnectionString = connectionString;
        }

        /// <summary>
        /// Dynamically change the database
        /// </summary>
        /// <param name="connectionString">connection string</param>
        /// <param name="cancellationToken">Asynchronous cancellation token</param>
        public virtual async Task ChangeDatabaseAsync(string connectionString,
            CancellationToken cancellationToken = default)
        {
            if (DbConnection.State == ConnectionState.Open)
            {
                await DbConnection.ChangeDatabaseAsync(connectionString, cancellationToken);
            }
            else
            {
                DbConnection.ConnectionString = connectionString;
            }
        }

        /// <summary>
        /// determine whether SqlServer database
        /// </summary>
        /// <returns>bool</returns>
        public virtual bool IsSqlServer()
        {
            return DbProvider.IsDatabaseFor(ProviderName, DbProvider.SqlServer);
        }

        /// <summary>
        /// determine whether Sqlite database
        /// </summary>
        /// <returns>bool</returns>
        public virtual bool IsSqlite()
        {
            return DbProvider.IsDatabaseFor(ProviderName, DbProvider.Sqlite);
        }

        /// <summary>
        /// determine whether Cosmos database
        /// </summary>
        /// <returns>bool</returns>
        public virtual bool IsCosmos()
        {
            return DbProvider.IsDatabaseFor(ProviderName, DbProvider.Cosmos);
        }

        /// <summary>
        /// determine whether in memory database
        /// </summary>
        /// <returns>bool</returns>
        public virtual bool InMemoryDatabase()
        {
            return DbProvider.IsDatabaseFor(ProviderName, DbProvider.InMemoryDatabase);
        }

        /// <summary>
        /// determine whether MySql database
        /// </summary>
        /// <returns>bool</returns>
        public virtual bool IsMySql()
        {
            return DbProvider.IsDatabaseFor(ProviderName, DbProvider.MySql);
        }

        /// <summary>
        /// determine whether MySql database official package（Update is not timely，only support 8.0.23+ Version， So make a separate category）
        /// </summary>
        /// <returns>bool</returns>
        public virtual bool IsMySqlOfficial()
        {
            return DbProvider.IsDatabaseFor(ProviderName, DbProvider.MySqlOfficial);
        }

        /// <summary>
        /// determine whether PostgreSQL database
        /// </summary>
        /// <returns>bool</returns>
        public virtual bool IsNpgsql()
        {
            return DbProvider.IsDatabaseFor(ProviderName, DbProvider.Npgsql);
        }

        /// <summary>
        /// determine whether Oracle database
        /// </summary>
        /// <returns>bool</returns>
        public virtual bool IsOracle()
        {
            return DbProvider.IsDatabaseFor(ProviderName, DbProvider.Oracle);
        }

        /// <summary>
        /// determine whether Firebird database
        /// </summary>
        /// <returns>bool</returns>
        public virtual bool IsFirebird()
        {
            return DbProvider.IsDatabaseFor(ProviderName, DbProvider.Firebird);
        }

        /// <summary>
        /// determine whether Dm database
        /// </summary>
        /// <returns>bool</returns>
        public virtual bool IsDm()
        {
            return DbProvider.IsDatabaseFor(ProviderName, DbProvider.Dm);
        }

        /// <summary>
        /// determine whether关系型database
        /// </summary>
        /// <returns>bool</returns>
        public virtual bool IsRelational()
        {
            return Database.IsRelational();
        }

        /// <summary>
        /// switch storage
        /// </summary>
        /// <typeparam name="TChangeEntity">entity type</typeparam>
        /// <returns>Warehousing</returns>
        public virtual new IRepository<TChangeEntity> Change<TChangeEntity>()
            where TChangeEntity : class, IPrivateEntity, new()
        {
            return _repository.Change<TChangeEntity>();
        }

        /// <summary>
        /// 切换多database上下文Warehousing
        /// </summary>
        /// <typeparam name="TChangeEntity">entity type</typeparam>
        /// <typeparam name="TChangeDbContextLocator">database context locator</typeparam>
        /// <returns>Warehousing</returns>
        public virtual IRepository<TChangeEntity, TChangeDbContextLocator> Change<TChangeEntity,
            TChangeDbContextLocator>()
            where TChangeEntity : class, IPrivateEntity, new()
            where TChangeDbContextLocator : class, IDbContextLocator
        {
            return _repository.Change<TChangeEntity, TChangeDbContextLocator>();
        }

        /// <summary>
        /// 重新构建并switch storage
        /// </summary>
        /// <remarks>pay attention，Scoped must be released manually</remarks>
        /// <typeparam name="TChangeEntity">entity type</typeparam>
        /// <returns>Warehousing</returns>
        public virtual (IRepository<TChangeEntity> Repository, IServiceScope Scoped) BuildChange<TChangeEntity>()
            where TChangeEntity : class, IPrivateEntity, new()
        {
            return _repository.BuildChange<TChangeEntity>();
        }

        /// <summary>
        /// 重新构建并切换多database上下文Warehousing
        /// </summary>
        /// <remarks>pay attention，Scoped must be released manually</remarks>
        /// <typeparam name="TChangeEntity">entity type</typeparam>
        /// <typeparam name="TChangeDbContextLocator">database context locator</typeparam>
        /// <returns>Warehousing</returns>
        public virtual (IRepository<TChangeEntity, TChangeDbContextLocator> Repository, IServiceScope Scoped)
            BuildChange<TChangeEntity, TChangeDbContextLocator>()
            where TChangeEntity : class, IPrivateEntity, new()
            where TChangeDbContextLocator : class, IDbContextLocator
        {
            return _repository.BuildChange<TChangeEntity, TChangeDbContextLocator>();
        }
    }
}