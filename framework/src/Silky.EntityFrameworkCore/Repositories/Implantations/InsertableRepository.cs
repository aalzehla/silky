using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Silky.EntityFrameworkCore.Entities;

namespace Silky.EntityFrameworkCore.Repositories
{
    /// <summary>
    /// Pluggable warehouse subclasses
    /// </summary>
    public partial class PrivateRepository<TEntity>
        where TEntity : class, IPrivateEntity, new()
    {
        /// <summary>
        /// add a record
        /// </summary>
        /// <param name="entity">entity</param>
        /// <param name="ignoreNullValues"></param>
        /// <returns>代理的entity</returns>
        public virtual EntityEntry<TEntity> Insert(TEntity entity, bool? ignoreNullValues = null)
        {
            var entryEntity = Entities.Add(entity);

            // Ignore null values
            IgnoreNullValues(ref entity, ignoreNullValues);

            return entryEntity;
        }

        /// <summary>
        /// Add multiple records
        /// </summary>
        /// <param name="entities">多个entity</param>
        public virtual void Insert(params TEntity[] entities)
        {
            Entities.AddRange(entities);
        }

        /// <summary>
        /// Add multiple records
        /// </summary>
        /// <param name="entities">多个entity</param>
        public virtual void Insert(IEnumerable<TEntity> entities)
        {
            Entities.AddRange(entities);
        }

        /// <summary>
        /// add a record
        /// </summary>
        /// <param name="entity">entity</param>
        /// <param name="ignoreNullValues"></param>
        /// <param name="cancellationToken">Cancel async token</param>
        /// <returns>代理的entity</returns>
        public virtual async Task<EntityEntry<TEntity>> InsertAsync(TEntity entity, bool? ignoreNullValues = null,
            CancellationToken cancellationToken = default)
        {
            var entityEntry = await Entities.AddAsync(entity, cancellationToken);

            // Ignore null values
            IgnoreNullValues(ref entity, ignoreNullValues);

            return entityEntry;
        }

        /// <summary>
        /// Add multiple records
        /// </summary>
        /// <param name="entities">多个entity</param>
        /// <returns>Task</returns>
        public virtual Task InsertAsync(params TEntity[] entities)
        {
            return Entities.AddRangeAsync(entities);
        }

        /// <summary>
        /// Add multiple records
        /// </summary>
        /// <param name="entities">多个entity</param>
        /// <param name="cancellationToken">Cancel async token</param>
        /// <returns></returns>
        public virtual Task InsertAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
        {
            return Entities.AddRangeAsync(entities, cancellationToken);
        }

        /// <summary>
        /// add a record并立即提交
        /// </summary>
        /// <param name="entity">entity</param>
        /// <param name="ignoreNullValues"></param>
        /// <returns>数据库中返回的entity</returns>
        public virtual EntityEntry<TEntity> InsertNow(TEntity entity, bool? ignoreNullValues = null)
        {
            var entityEntry = Insert(entity, ignoreNullValues);
            SaveNow();
            return entityEntry;
        }

        /// <summary>
        /// add a record并立即提交
        /// </summary>
        /// <param name="entity">entity</param>
        /// <param name="acceptAllChangesOnSuccess">accept all changes</param>
        /// <param name="ignoreNullValues"></param>
        /// <returns>数据库中返回的entity</returns>
        public virtual EntityEntry<TEntity> InsertNow(TEntity entity, bool acceptAllChangesOnSuccess,
            bool? ignoreNullValues = null)
        {
            var entityEntry = Insert(entity, ignoreNullValues);
            SaveNow(acceptAllChangesOnSuccess);
            return entityEntry;
        }

        /// <summary>
        /// Add multiple records
        /// </summary>
        /// <param name="entities">多个entity</param>
        public virtual void InsertNow(params TEntity[] entities)
        {
            Insert(entities);
            SaveNow();
        }

        /// <summary>
        /// Add multiple records
        /// </summary>
        /// <param name="entities">多个entity</param>
        /// <param name="acceptAllChangesOnSuccess">accept all changes</param>
        public virtual void InsertNow(TEntity[] entities, bool acceptAllChangesOnSuccess)
        {
            Insert(entities);
            SaveNow(acceptAllChangesOnSuccess);
        }

        /// <summary>
        /// Add multiple records
        /// </summary>
        /// <param name="entities">多个entity</param>
        public virtual void InsertNow(IEnumerable<TEntity> entities)
        {
            Insert(entities);
            SaveNow();
        }

        /// <summary>
        /// Add multiple records
        /// </summary>
        /// <param name="entities">多个entity</param>
        /// <param name="acceptAllChangesOnSuccess">accept all changes</param>
        public virtual void InsertNow(IEnumerable<TEntity> entities, bool acceptAllChangesOnSuccess)
        {
            Insert(entities);
            SaveNow(acceptAllChangesOnSuccess);
        }

        /// <summary>
        /// add a record并立即提交
        /// </summary>
        /// <param name="entity">entity</param>
        /// <param name="ignoreNullValues"></param>
        /// <param name="cancellationToken">Cancel async token</param>
        /// <returns>数据库中返回的entity</returns>
        public virtual async Task<EntityEntry<TEntity>> InsertNowAsync(TEntity entity, bool? ignoreNullValues = null,
            CancellationToken cancellationToken = default)
        {
            var entityEntry = await InsertAsync(entity, ignoreNullValues, cancellationToken);
            await SaveNowAsync(cancellationToken);
            return entityEntry;
        }

        /// <summary>
        /// add a record并立即提交
        /// </summary>
        /// <param name="entity">entity</param>
        /// <param name="acceptAllChangesOnSuccess">accept all changes</param>
        /// <param name="ignoreNullValues"></param>
        /// <param name="cancellationToken">Cancel async token</param>
        /// <returns>数据库中返回的entity</returns>
        public virtual async Task<EntityEntry<TEntity>> InsertNowAsync(TEntity entity, bool acceptAllChangesOnSuccess,
            bool? ignoreNullValues = null, CancellationToken cancellationToken = default)
        {
            var entityEntry = await InsertAsync(entity, ignoreNullValues, cancellationToken);
            await SaveNowAsync(acceptAllChangesOnSuccess, cancellationToken);
            return entityEntry;
        }

        /// <summary>
        /// Add multiple records并立即提交
        /// </summary>
        /// <param name="entities">多个entity</param>
        /// <returns>Task</returns>
        public virtual async Task InsertNowAsync(params TEntity[] entities)
        {
            await InsertAsync(entities);
            await SaveNowAsync();
        }

        /// <summary>
        /// Add multiple records并立即提交
        /// </summary>
        /// <param name="entities">多个entity</param>
        /// <param name="cancellationToken">Cancel async token</param>
        /// <returns>Task</returns>
        public virtual async Task InsertNowAsync(TEntity[] entities, CancellationToken cancellationToken = default)
        {
            await InsertAsync(entities);
            await SaveNowAsync(cancellationToken);
        }

        /// <summary>
        /// Add multiple records并立即提交
        /// </summary>
        /// <param name="entities">多个entity</param>
        /// <param name="acceptAllChangesOnSuccess">accept all changes</param>
        /// <param name="cancellationToken">Cancel async token</param>
        /// <returns>Task</returns>
        public virtual async Task InsertNowAsync(TEntity[] entities, bool acceptAllChangesOnSuccess,
            CancellationToken cancellationToken = default)
        {
            await InsertAsync(entities);
            await SaveNowAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        /// <summary>
        /// Add multiple records并立即提交
        /// </summary>
        /// <param name="entities">多个entity</param>
        /// <param name="cancellationToken">Cancel async token</param>
        /// <returns>Task</returns>
        public virtual async Task InsertNowAsync(IEnumerable<TEntity> entities,
            CancellationToken cancellationToken = default)
        {
            await InsertAsync(entities, cancellationToken);
            await SaveNowAsync(cancellationToken);
        }

        /// <summary>
        /// Add multiple records并立即提交
        /// </summary>
        /// <param name="entities">多个entity</param>
        /// <param name="acceptAllChangesOnSuccess">accept all changes</param>
        /// <param name="cancellationToken">Cancel async token</param>
        /// <returns>Task</returns>
        public virtual async Task InsertNowAsync(IEnumerable<TEntity> entities, bool acceptAllChangesOnSuccess,
            CancellationToken cancellationToken = default)
        {
            await InsertAsync(entities, cancellationToken);
            await SaveNowAsync(acceptAllChangesOnSuccess, cancellationToken);
        }
    }
}