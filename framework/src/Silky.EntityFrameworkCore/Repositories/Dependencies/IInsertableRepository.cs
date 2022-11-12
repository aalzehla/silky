using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Silky.EntityFrameworkCore.Entities;
using Silky.EntityFrameworkCore.Locators;

namespace Silky.EntityFrameworkCore.Repositories
{
    /// <summary>
    /// Pluggable storage interface
    /// </summary>
    /// <typeparam name="TEntity">entity type</typeparam>
    public partial interface IInsertableRepository<TEntity> : IInsertableRepository<TEntity, MasterDbContextLocator>
        where TEntity : class, IPrivateEntity, new()
    {
    }

    /// <summary>
    /// Pluggable storage interface
    /// </summary>
    /// <typeparam name="TEntity">entity type</typeparam>
    /// <typeparam name="TDbContextLocator">database context locator</typeparam>
    public partial interface IInsertableRepository<TEntity, TDbContextLocator> : IPrivateInsertableRepository<TEntity>
        where TEntity : class, IPrivateEntity, new()
        where TDbContextLocator : class, IDbContextLocator
    {
    }

    /// <summary>
    /// Pluggable storage interface
    /// </summary>
    /// <typeparam name="TEntity">entity type</typeparam>
    public interface IPrivateInsertableRepository<TEntity> : IPrivateRootRepository
        where TEntity : class, IPrivateEntity, new()
    {
        /// <summary>
        /// add a record
        /// </summary>
        /// <param name="entity">entity</param>
        /// <param name="ignoreNullValues"></param>
        /// <returns>代理的entity</returns>
        EntityEntry<TEntity> Insert(TEntity entity, bool? ignoreNullValues = null);

        /// <summary>
        /// Add multiple records
        /// </summary>
        /// <param name="entities">多个entity</param>
        void Insert(params TEntity[] entities);

        /// <summary>
        /// Add multiple records
        /// </summary>
        /// <param name="entities">多个entity</param>
        void Insert(IEnumerable<TEntity> entities);

        /// <summary>
        /// add a record
        /// </summary>
        /// <param name="entity">entity</param>
        /// <param name="ignoreNullValues"></param>
        /// <param name="cancellationToken">Cancel async token</param>
        /// <returns>代理的entity</returns>
        Task<EntityEntry<TEntity>> InsertAsync(TEntity entity, bool? ignoreNullValues = null,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Add multiple records
        /// </summary>
        /// <param name="entities">多个entity</param>
        /// <returns>Task</returns>
        Task InsertAsync(params TEntity[] entities);

        /// <summary>
        /// Add multiple records
        /// </summary>
        /// <param name="entities">多个entity</param>
        /// <param name="cancellationToken">Cancel async token</param>
        /// <returns></returns>
        Task InsertAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default);

        /// <summary>
        /// add a record并立即提交
        /// </summary>
        /// <param name="entity">entity</param>
        /// <param name="ignoreNullValues"></param>
        /// <returns>数据库中返回的entity</returns>
        EntityEntry<TEntity> InsertNow(TEntity entity, bool? ignoreNullValues = null);

        /// <summary>
        /// add a record并立即提交
        /// </summary>
        /// <param name="entity">entity</param>
        /// <param name="acceptAllChangesOnSuccess">accept all changes</param>
        /// <param name="ignoreNullValues"></param>
        /// <returns>数据库中返回的entity</returns>
        EntityEntry<TEntity> InsertNow(TEntity entity, bool acceptAllChangesOnSuccess, bool? ignoreNullValues = null);

        /// <summary>
        /// Add multiple records
        /// </summary>
        /// <param name="entities">多个entity</param>
        void InsertNow(params TEntity[] entities);

        /// <summary>
        /// Add multiple records
        /// </summary>
        /// <param name="entities">多个entity</param>
        /// <param name="acceptAllChangesOnSuccess">accept all changes</param>
        void InsertNow(TEntity[] entities, bool acceptAllChangesOnSuccess);

        /// <summary>
        /// Add multiple records
        /// </summary>
        /// <param name="entities">多个entity</param>
        void InsertNow(IEnumerable<TEntity> entities);

        /// <summary>
        /// Add multiple records
        /// </summary>
        /// <param name="entities">多个entity</param>
        /// <param name="acceptAllChangesOnSuccess">accept all changes</param>
        void InsertNow(IEnumerable<TEntity> entities, bool acceptAllChangesOnSuccess);

        /// <summary>
        /// add a record并立即提交
        /// </summary>
        /// <param name="entity">entity</param>
        /// <param name="cancellationToken">Cancel async token</param>
        /// <param name="ignoreNullValues"></param>
        /// <returns>数据库中返回的entity</returns>
        Task<EntityEntry<TEntity>> InsertNowAsync(TEntity entity, bool? ignoreNullValues = null,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// add a record并立即提交
        /// </summary>
        /// <param name="entity">entity</param>
        /// <param name="acceptAllChangesOnSuccess">accept all changes</param>
        /// <param name="ignoreNullValues"></param>
        /// <param name="cancellationToken">Cancel async token</param>
        /// <returns>数据库中返回的entity</returns>
        Task<EntityEntry<TEntity>> InsertNowAsync(TEntity entity, bool acceptAllChangesOnSuccess,
            bool? ignoreNullValues = null, CancellationToken cancellationToken = default);

        /// <summary>
        /// Add multiple records并立即提交
        /// </summary>
        /// <param name="entities">多个entity</param>
        /// <returns>Task</returns>
        Task InsertNowAsync(params TEntity[] entities);

        /// <summary>
        /// Add multiple records并立即提交
        /// </summary>
        /// <param name="entities">多个entity</param>
        /// <param name="cancellationToken">Cancel async token</param>
        /// <returns>Task</returns>
        Task InsertNowAsync(TEntity[] entities, CancellationToken cancellationToken = default);

        /// <summary>
        /// Add multiple records并立即提交
        /// </summary>
        /// <param name="entities">多个entity</param>
        /// <param name="acceptAllChangesOnSuccess">accept all changes</param>
        /// <param name="cancellationToken">Cancel async token</param>
        /// <returns>Task</returns>
        Task InsertNowAsync(TEntity[] entities, bool acceptAllChangesOnSuccess,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Add multiple records并立即提交
        /// </summary>
        /// <param name="entities">多个entity</param>
        /// <param name="cancellationToken">Cancel async token</param>
        /// <returns>Task</returns>
        Task InsertNowAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default);

        /// <summary>
        /// Add multiple records并立即提交
        /// </summary>
        /// <param name="entities">多个entity</param>
        /// <param name="acceptAllChangesOnSuccess">accept all changes</param>
        /// <param name="cancellationToken">Cancel async token</param>
        /// <returns>Task</returns>
        Task InsertNowAsync(IEnumerable<TEntity> entities, bool acceptAllChangesOnSuccess,
            CancellationToken cancellationToken = default);
    }
}