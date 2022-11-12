using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Silky.EntityFrameworkCore.Entities;

namespace Silky.EntityFrameworkCore.Repositories
{
    /// <summary>
    /// Repository division classes can be deleted
    /// </summary>
    public partial class PrivateRepository<TEntity>
        where TEntity : class, IPrivateEntity, new()
    {
        /// <summary>
        /// delete a record
        /// </summary>
        /// <param name="entity">entity</param>
        /// <returns>代理中的entity</returns>
        public virtual EntityEntry<TEntity> Delete(TEntity entity)
        {
            return Entities.Remove(entity);
        }

        /// <summary>
        /// delete multiple records
        /// </summary>
        /// <param name="entities">多个entity</param>
        public virtual void Delete(params TEntity[] entities)
        {
            Entities.RemoveRange(entities);
        }

        /// <summary>
        /// delete multiple records
        /// </summary>
        /// <param name="entities">多个entity</param>
        public virtual void Delete(IEnumerable<TEntity> entities)
        {
            Entities.RemoveRange(entities);
        }

        /// <summary>
        /// delete a record
        /// </summary>
        /// <param name="entity">entity</param>
        /// <returns>代理中的entity</returns>
        public virtual Task<EntityEntry<TEntity>> DeleteAsync(TEntity entity)
        {
            return Task.FromResult(Delete(entity));
        }

        /// <summary>
        /// delete multiple records
        /// </summary>
        /// <param name="entities">多个entity</param>
        /// <returns>Task</returns>
        public virtual Task DeleteAsync(params TEntity[] entities)
        {
            Delete(entities);
            return Task.CompletedTask;
        }

        /// <summary>
        /// delete multiple records
        /// </summary>
        /// <param name="entities">多个entity</param>
        /// <returns>Task</returns>
        public virtual Task DeleteAsync(IEnumerable<TEntity> entities)
        {
            Delete(entities);
            return Task.CompletedTask;
        }

        /// <summary>
        /// delete a record并立即提交
        /// </summary>
        /// <param name="entity">entity</param>
        /// <returns>代理中的entity</returns>
        public virtual EntityEntry<TEntity> DeleteNow(TEntity entity)
        {
            var entityEntry = Delete(entity);
            SaveNow();
            return entityEntry;
        }

        /// <summary>
        /// delete a record并立即提交
        /// </summary>
        /// <param name="entity">entity</param>
        /// <param name="acceptAllChangesOnSuccess">accept all changes</param>
        /// <returns></returns>
        public virtual EntityEntry<TEntity> DeleteNow(TEntity entity, bool acceptAllChangesOnSuccess)
        {
            var entityEntry = Delete(entity);
            SaveNow(acceptAllChangesOnSuccess);
            return entityEntry;
        }

        /// <summary>
        /// delete multiple records并立即提交
        /// </summary>
        /// <param name="entities">多个entity</param>
        public virtual void DeleteNow(params TEntity[] entities)
        {
            Delete(entities);
            SaveNow();
        }

        /// <summary>
        /// delete multiple records并立即提交
        /// </summary>
        /// <param name="entities">多个entity</param>
        /// <param name="acceptAllChangesOnSuccess">accept all changes</param>
        public virtual void DeleteNow(TEntity[] entities, bool acceptAllChangesOnSuccess)
        {
            Delete(entities);
            SaveNow(acceptAllChangesOnSuccess);
        }

        /// <summary>
        /// delete multiple records并立即提交
        /// </summary>
        /// <param name="entities">多个entity</param>
        public virtual void DeleteNow(IEnumerable<TEntity> entities)
        {
            Delete(entities);
            SaveNow();
        }

        /// <summary>
        /// delete multiple records并立即提交
        /// </summary>
        /// <param name="entities">多个entity</param>
        /// <param name="acceptAllChangesOnSuccess">accept all changes</param>
        public virtual void DeleteNow(IEnumerable<TEntity> entities, bool acceptAllChangesOnSuccess)
        {
            Delete(entities);
            SaveNow(acceptAllChangesOnSuccess);
        }

        /// <summary>
        /// delete a record并立即提交
        /// </summary>
        /// <param name="entity">entity</param>
        /// <param name="cancellationToken">Cancel async token</param>
        /// <returns>代理中的entity</returns>
        public virtual async Task<EntityEntry<TEntity>> DeleteNowAsync(TEntity entity,
            CancellationToken cancellationToken = default)
        {
            var entityEntry = await DeleteAsync(entity);
            await SaveNowAsync(cancellationToken);
            return entityEntry;
        }

        /// <summary>
        /// delete a record并立即提交
        /// </summary>
        /// <param name="entity">entity</param>
        /// <param name="acceptAllChangesOnSuccess">accept all changes</param>
        /// <param name="cancellationToken">Cancel async token</param>
        /// <returns>代理中的entity</returns>
        public virtual async Task<EntityEntry<TEntity>> DeleteNowAsync(TEntity entity, bool acceptAllChangesOnSuccess,
            CancellationToken cancellationToken = default)
        {
            var entityEntry = await DeleteAsync(entity);
            await SaveNowAsync(acceptAllChangesOnSuccess, cancellationToken);
            return entityEntry;
        }

        /// <summary>
        /// delete multiple records并立即提交
        /// </summary>
        /// <param name="entities">多个entity</param>
        /// <returns>Task</returns>
        public virtual async Task DeleteNowAsync(params TEntity[] entities)
        {
            await DeleteAsync(entities);
            await SaveNowAsync();
        }

        /// <summary>
        /// delete multiple records并立即提交
        /// </summary>
        /// <param name="entities">多个entity</param>
        /// <param name="cancellationToken">Cancel async token</param>
        /// <returns>Task</returns>
        public virtual async Task DeleteNowAsync(TEntity[] entities, CancellationToken cancellationToken = default)
        {
            await DeleteAsync(entities);
            await SaveNowAsync(cancellationToken);
        }

        /// <summary>
        /// delete multiple records并立即提交
        /// </summary>
        /// <param name="entities">多个entity</param>
        /// <param name="acceptAllChangesOnSuccess">accept all changes</param>
        /// <param name="cancellationToken">Cancel async token</param>
        /// <returns>Task</returns>
        public virtual async Task DeleteNowAsync(TEntity[] entities, bool acceptAllChangesOnSuccess,
            CancellationToken cancellationToken = default)
        {
            await DeleteAsync(entities);
            await SaveNowAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        /// <summary>
        /// delete multiple records并立即提交
        /// </summary>
        /// <param name="entities">多个entity</param>
        /// <param name="cancellationToken">Cancel async token</param>
        /// <returns>Task</returns>
        public virtual async Task DeleteNowAsync(IEnumerable<TEntity> entities,
            CancellationToken cancellationToken = default)
        {
            await DeleteAsync(entities);
            await SaveNowAsync(cancellationToken);
        }

        /// <summary>
        /// delete multiple records并立即提交
        /// </summary>
        /// <param name="entities">多个entity</param>
        /// <param name="acceptAllChangesOnSuccess">accept all changes</param>
        /// <param name="cancellationToken">Cancel async token</param>
        /// <returns>Task</returns>
        public virtual async Task DeleteNowAsync(IEnumerable<TEntity> entities, bool acceptAllChangesOnSuccess,
            CancellationToken cancellationToken = default)
        {
            await DeleteAsync(entities);
            await SaveNowAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        /// <summary>
        /// 根据primary keydelete a record
        /// </summary>
        /// <param name="key">primary key</param>
        public virtual void Delete(object key)
        {
            var deletedEntity = BuildDeletedEntity(key);
            if (deletedEntity != null) return;

            // 如果primary key不存在，then use Find Inquire
            var entity = FindOrDefault(key);
            if (entity != null) Delete(entity);
        }

        /// <summary>
        /// 根据primary keydelete a record
        /// </summary>
        /// <param name="key">primary key</param>
        /// <param name="cancellationToken">Asynchronous cancellation token</param>
        /// <returns>Task</returns>
        public virtual async Task DeleteAsync(object key, CancellationToken cancellationToken = default)
        {
            var deletedEntity = BuildDeletedEntity(key);
            if (deletedEntity != null) return;

            // 如果primary key不存在，then use FindAsync Inquire
            var entity = await FindOrDefaultAsync(key, cancellationToken);
            if (entity != null) await DeleteAsync(entity);
        }

        /// <summary>
        /// 根据primary keydelete a record并立即提交
        /// </summary>
        /// <param name="key">primary key</param>
        public virtual void DeleteNow(object key)
        {
            Delete(key);
            SaveNow();
        }

        /// <summary>
        /// 根据primary keydelete a record并立即提交
        /// </summary>
        /// <param name="key">primary key</param>
        /// <param name="acceptAllChangesOnSuccess">accept all changes</param>
        public virtual void DeleteNow(object key, bool acceptAllChangesOnSuccess)
        {
            Delete(key);
            SaveNow(acceptAllChangesOnSuccess);
        }

        /// <summary>
        /// 根据primary keydelete a record并立即提交
        /// </summary>
        /// <param name="key">primary key</param>
        /// <param name="cancellationToken">Asynchronous cancellation token</param>
        /// <returns></returns>
        public virtual async Task DeleteNowAsync(object key, CancellationToken cancellationToken = default)
        {
            await DeleteAsync(key, cancellationToken);
            await SaveNowAsync(cancellationToken);
        }

        /// <summary>
        /// 根据primary keydelete a record并立即提交
        /// </summary>
        /// <param name="key">primary key</param>
        /// <param name="acceptAllChangesOnSuccess">accept all changes</param>
        /// <param name="cancellationToken">Asynchronous cancellation token</param>
        /// <returns></returns>
        public virtual async Task DeleteNowAsync(object key, bool acceptAllChangesOnSuccess,
            CancellationToken cancellationToken = default)
        {
            await DeleteAsync(key, cancellationToken);
            await SaveNowAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        /// <summary>
        /// 构建被删除的entity
        /// </summary>
        /// <param name="key">primary key</param>
        /// <param name="isRealDelete">really delete</param>
        /// <returns></returns>
        private TEntity BuildDeletedEntity(object key, bool isRealDelete = true)
        {
            // 读取primary key
            var keyProperty = EntityType.FindPrimaryKey().Properties.AsEnumerable().FirstOrDefault()?.PropertyInfo;
            if (keyProperty == null) return default;

            // 判断当前primary key是否被跟踪了
            var tracking = CheckTrackState(key, out var entityEntry, keyProperty.Name);
            if (tracking)
            {
                // 设置entity状态为已删除
                if (isRealDelete) ChangeEntityState(entityEntry, EntityState.Deleted);

                return entityEntry.Entity as TEntity;
            }

            // if not tracked，创建entity对象并设置primary key值
            var entity = Activator.CreateInstance<TEntity>();
            keyProperty.SetValue(entity, key);

            // 设置entity状态为已删除
            if (isRealDelete) ChangeEntityState(entity, EntityState.Deleted);

            return entity;
        }
    }
}