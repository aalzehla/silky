using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Silky.EntityFrameworkCore.Entities;
using Silky.EntityFrameworkCore.Locators;

namespace Silky.EntityFrameworkCore.Repositories
{
    /// <summary>
    /// Deletable repository interface
    /// </summary>
    /// <typeparam name="TEntity">entity type</typeparam>
    public partial interface IDeletableRepository<TEntity> : IDeletableRepository<TEntity, MasterDbContextLocator>
        where TEntity : class, IPrivateEntity, new()
    {
    }

    /// <summary>
    /// Deletable repository interface
    /// </summary>
    /// <typeparam name="TEntity">entity type</typeparam>
    /// <typeparam name="TDbContextLocator">database context locator</typeparam>
    public partial interface IDeletableRepository<TEntity, TDbContextLocator> : IPrivateDeletableRepository<TEntity>
        where TEntity : class, IPrivateEntity, new()
        where TDbContextLocator : class, IDbContextLocator
    {
    }

    /// <summary>
    /// Deletable repository interface
    /// </summary>
    /// <typeparam name="TEntity">entity type</typeparam>
    public interface IPrivateDeletableRepository<TEntity> : IPrivateRootRepository
        where TEntity : class, IPrivateEntity, new()
    {
        /// <summary>
        /// delete a record
        /// </summary>
        /// <param name="entity">entity</param>
        /// <returns>代理中的entity</returns>
        EntityEntry<TEntity> Delete(TEntity entity);

        /// <summary>
        /// delete multiple records
        /// </summary>
        /// <param name="entities">多个entity</param>
        void Delete(params TEntity[] entities);

        /// <summary>
        /// delete multiple records
        /// </summary>
        /// <param name="entities">多个entity</param>
        void Delete(IEnumerable<TEntity> entities);

        /// <summary>
        /// delete a record
        /// </summary>
        /// <param name="entity">entity</param>
        /// <returns>代理中的entity</returns>
        Task<EntityEntry<TEntity>> DeleteAsync(TEntity entity);

        /// <summary>
        /// delete multiple records
        /// </summary>
        /// <param name="entities">多个entity</param>
        /// <returns>Task</returns>
        Task DeleteAsync(params TEntity[] entities);

        /// <summary>
        /// delete multiple records
        /// </summary>
        /// <param name="entities">多个entity</param>
        /// <returns>Task</returns>
        Task DeleteAsync(IEnumerable<TEntity> entities);

        /// <summary>
        /// delete a record并立即提交
        /// </summary>
        /// <param name="entity">entity</param>
        /// <returns>代理中的entity</returns>
        EntityEntry<TEntity> DeleteNow(TEntity entity);

        /// <summary>
        /// delete a record并立即提交
        /// </summary>
        /// <param name="entity">entity</param>
        /// <param name="acceptAllChangesOnSuccess">accept all changes</param>
        /// <returns></returns>
        EntityEntry<TEntity> DeleteNow(TEntity entity, bool acceptAllChangesOnSuccess);

        /// <summary>
        /// delete multiple records并立即提交
        /// </summary>
        /// <param name="entities">多个entity</param>
        void DeleteNow(params TEntity[] entities);

        /// <summary>
        /// delete multiple records并立即提交
        /// </summary>
        /// <param name="entities">多个entity</param>
        /// <param name="acceptAllChangesOnSuccess">accept all changes</param>
        void DeleteNow(TEntity[] entities, bool acceptAllChangesOnSuccess);

        /// <summary>
        /// delete multiple records并立即提交
        /// </summary>
        /// <param name="entities">多个entity</param>
        void DeleteNow(IEnumerable<TEntity> entities);

        /// <summary>
        /// delete multiple records并立即提交
        /// </summary>
        /// <param name="entities">多个entity</param>
        /// <param name="acceptAllChangesOnSuccess">accept all changes</param>
        void DeleteNow(IEnumerable<TEntity> entities, bool acceptAllChangesOnSuccess);

        /// <summary>
        /// delete a record并立即提交
        /// </summary>
        /// <param name="entity">entity</param>
        /// <param name="cancellationToken">Cancel async token</param>
        /// <returns>代理中的entity</returns>
        Task<EntityEntry<TEntity>> DeleteNowAsync(TEntity entity, CancellationToken cancellationToken = default);

        /// <summary>
        /// delete a record并立即提交
        /// </summary>
        /// <param name="entity">entity</param>
        /// <param name="acceptAllChangesOnSuccess">accept all changes</param>
        /// <param name="cancellationToken">Cancel async token</param>
        /// <returns>代理中的entity</returns>
        Task<EntityEntry<TEntity>> DeleteNowAsync(TEntity entity, bool acceptAllChangesOnSuccess,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// delete multiple records并立即提交
        /// </summary>
        /// <param name="entities">多个entity</param>
        /// <returns>Task</returns>
        Task DeleteNowAsync(params TEntity[] entities);

        /// <summary>
        /// delete multiple records并立即提交
        /// </summary>
        /// <param name="entities">多个entity</param>
        /// <param name="cancellationToken">Cancel async token</param>
        /// <returns>Task</returns>
        Task DeleteNowAsync(TEntity[] entities, CancellationToken cancellationToken = default);

        /// <summary>
        /// delete multiple records并立即提交
        /// </summary>
        /// <param name="entities">多个entity</param>
        /// <param name="acceptAllChangesOnSuccess">accept all changes</param>
        /// <param name="cancellationToken">Cancel async token</param>
        /// <returns>Task</returns>
        Task DeleteNowAsync(TEntity[] entities, bool acceptAllChangesOnSuccess,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// delete multiple records并立即提交
        /// </summary>
        /// <param name="entities">多个entity</param>
        /// <param name="cancellationToken">Cancel async token</param>
        /// <returns>Task</returns>
        Task DeleteNowAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default);

        /// <summary>
        /// delete multiple records并立即提交
        /// </summary>
        /// <param name="entities">多个entity</param>
        /// <param name="acceptAllChangesOnSuccess">accept all changes</param>
        /// <param name="cancellationToken">Cancel async token</param>
        /// <returns>Task</returns>
        Task DeleteNowAsync(IEnumerable<TEntity> entities, bool acceptAllChangesOnSuccess,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// 根据primary keydelete a record
        /// </summary>
        /// <param name="key">primary key</param>
        void Delete(object key);

        /// <summary>
        /// 根据primary keydelete a record
        /// </summary>
        /// <param name="key">primary key</param>
        /// <param name="cancellationToken">Asynchronous cancellation token</param>
        /// <returns>Task</returns>
        Task DeleteAsync(object key, CancellationToken cancellationToken = default);

        /// <summary>
        /// 根据primary keydelete a record并立即提交
        /// </summary>
        /// <param name="key">primary key</param>
        void DeleteNow(object key);

        /// <summary>
        /// 根据primary keydelete a record并立即提交
        /// </summary>
        /// <param name="key">primary key</param>
        /// <param name="acceptAllChangesOnSuccess">accept all changes</param>
        void DeleteNow(object key, bool acceptAllChangesOnSuccess);

        /// <summary>
        /// 根据primary keydelete a record并立即提交
        /// </summary>
        /// <param name="key">primary key</param>
        /// <param name="cancellationToken">Asynchronous cancellation token</param>
        /// <returns></returns>
        Task DeleteNowAsync(object key, CancellationToken cancellationToken = default);

        /// <summary>
        /// 根据primary keydelete a record并立即提交
        /// </summary>
        /// <param name="key">primary key</param>
        /// <param name="acceptAllChangesOnSuccess">accept all changes</param>
        /// <param name="cancellationToken">Asynchronous cancellation token</param>
        /// <returns></returns>
        Task DeleteNowAsync(object key, bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default);
    }
}