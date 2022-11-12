using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Silky.EntityFrameworkCore.Entities;

namespace Silky.EntityFrameworkCore.Repositories
{
    /// <summary>
    /// Updatable warehouse division class
    /// </summary>
    public partial class PrivateRepository<TEntity>
        where TEntity : class, IPrivateEntity, new()
    {
        /// <summary>
        /// update a record
        /// </summary>
        /// <param name="entity">entity</param>
        /// <param name="ignoreNullValues"></param>
        /// <returns>代理中的entity</returns>
        public virtual EntityEntry<TEntity> Update(TEntity entity, bool? ignoreNullValues = null)
        {
            var entityEntry = Entities.Update(entity);

            // Ignore null values
            IgnoreNullValues(ref entity, ignoreNullValues);

            return entityEntry;
        }

        /// <summary>
        /// Update multiple records
        /// </summary>
        /// <param name="entities">多个entity</param>
        public virtual void Update(params TEntity[] entities)
        {
            Entities.UpdateRange(entities);
        }

        /// <summary>
        /// Update multiple records
        /// </summary>
        /// <param name="entities">多个entity</param>
        public virtual void Update(IEnumerable<TEntity> entities)
        {
            Entities.UpdateRange(entities);
        }

        /// <summary>
        /// update a record
        /// </summary>
        /// <param name="entity">entity</param>
        /// <param name="ignoreNullValues"></param>
        /// <returns>代理中的entity</returns>
        public virtual Task<EntityEntry<TEntity>> UpdateAsync(TEntity entity, bool? ignoreNullValues = null)
        {
            return Task.FromResult(Update(entity, ignoreNullValues));
        }

        /// <summary>
        /// Update multiple records
        /// </summary>
        /// <param name="entities">多个entity</param>
        /// <returns>Task</returns>
        public virtual Task UpdateAsync(params TEntity[] entities)
        {
            Update(entities);
            return Task.CompletedTask;
        }

        /// <summary>
        /// Update multiple records
        /// </summary>
        /// <param name="entities">多个entity</param>
        /// <returns>Task</returns>
        public virtual Task UpdateAsync(IEnumerable<TEntity> entities)
        {
            Update(entities);
            return Task.CompletedTask;
        }

        /// <summary>
        /// update a record并立即提交
        /// </summary>
        /// <param name="entity">entity</param>
        /// <param name="ignoreNullValues"></param>
        /// <returns>数据库中的entity</returns>
        public virtual EntityEntry<TEntity> UpdateNow(TEntity entity, bool? ignoreNullValues = null)
        {
            var entityEntry = Update(entity, ignoreNullValues);
            SaveNow();
            return entityEntry;
        }

        /// <summary>
        /// update a record并立即提交
        /// </summary>
        /// <param name="entity">entity</param>
        /// <param name="acceptAllChangesOnSuccess">accept all changes</param>
        /// <param name="ignoreNullValues"></param>
        /// <returns>数据库中的entity</returns>
        public virtual EntityEntry<TEntity> UpdateNow(TEntity entity, bool acceptAllChangesOnSuccess,
            bool? ignoreNullValues = null)
        {
            var entityEntry = Update(entity, ignoreNullValues);
            SaveNow(acceptAllChangesOnSuccess);
            return entityEntry;
        }

        /// <summary>
        /// Update multiple records并立即提交
        /// </summary>
        /// <param name="entities">多个entity</param>
        public virtual void UpdateNow(params TEntity[] entities)
        {
            Update(entities);
            SaveNow();
        }

        /// <summary>
        /// Update multiple records并立即提交
        /// </summary>
        /// <param name="entities">多个entity</param>
        /// <param name="acceptAllChangesOnSuccess">accept all changes</param>
        public virtual void UpdateNow(TEntity[] entities, bool acceptAllChangesOnSuccess)
        {
            Update(entities);
            SaveNow(acceptAllChangesOnSuccess);
        }

        /// <summary>
        /// Update multiple records并立即提交
        /// </summary>
        /// <param name="entities">多个entity</param>
        public virtual void UpdateNow(IEnumerable<TEntity> entities)
        {
            Update(entities);
            SaveNow();
        }

        /// <summary>
        /// Update multiple records并立即提交
        /// </summary>
        /// <param name="entities">多个entity</param>
        /// <param name="acceptAllChangesOnSuccess">accept all changes</param>
        public virtual void UpdateNow(IEnumerable<TEntity> entities, bool acceptAllChangesOnSuccess)
        {
            Update(entities);
            SaveNow(acceptAllChangesOnSuccess);
        }

        /// <summary>
        /// update a record并立即提交
        /// </summary>
        /// <param name="entity">entity</param>
        /// <param name="cancellationToken">Cancel async token</param>
        /// <param name="ignoreNullValues"></param>
        /// <returns>数据库中的entity</returns>
        public virtual async Task<EntityEntry<TEntity>> UpdateNowAsync(TEntity entity, bool? ignoreNullValues = null,
            CancellationToken cancellationToken = default)
        {
            var entityEntry = await UpdateAsync(entity, ignoreNullValues);
            await SaveNowAsync(cancellationToken);
            return entityEntry;
        }

        /// <summary>
        /// update a record并立即提交
        /// </summary>
        /// <param name="entity">entity</param>
        /// <param name="acceptAllChangesOnSuccess">accept all changes</param>
        /// <param name="ignoreNullValues"></param>
        /// <param name="cancellationToken">Cancel async token</param>
        /// <returns>数据库中的entity</returns>
        public virtual async Task<EntityEntry<TEntity>> UpdateNowAsync(TEntity entity, bool acceptAllChangesOnSuccess,
            bool? ignoreNullValues = null, CancellationToken cancellationToken = default)
        {
            var entityEntry = await UpdateAsync(entity, ignoreNullValues);
            await SaveNowAsync(acceptAllChangesOnSuccess, cancellationToken);
            return entityEntry;
        }

        /// <summary>
        /// Update multiple records并立即提交
        /// </summary>
        /// <param name="entities">多个entity</param>
        /// <returns>Task</returns>
        public virtual async Task UpdateNowAsync(params TEntity[] entities)
        {
            await UpdateAsync(entities);
            await SaveNowAsync();
        }

        /// <summary>
        /// Update multiple records并立即提交
        /// </summary>
        /// <param name="entities">多个entity</param>
        /// <param name="cancellationToken">Cancel async token</param>
        /// <returns></returns>
        public virtual async Task UpdateNowAsync(TEntity[] entities, CancellationToken cancellationToken = default)
        {
            await UpdateAsync(entities);
            await SaveNowAsync(cancellationToken);
        }

        /// <summary>
        /// Update multiple records并立即提交
        /// </summary>
        /// <param name="entities">多个entity</param>
        /// <param name="acceptAllChangesOnSuccess">accept all changes</param>
        /// <param name="cancellationToken">Cancel async token</param>
        /// <returns>Task</returns>
        public virtual async Task UpdateNowAsync(TEntity[] entities, bool acceptAllChangesOnSuccess,
            CancellationToken cancellationToken = default)
        {
            await UpdateAsync(entities);
            await SaveNowAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        /// <summary>
        /// Update multiple records并立即提交
        /// </summary>
        /// <param name="entities">多个entity</param>
        /// <param name="cancellationToken">Cancel async token</param>
        /// <returns>Task</returns>
        public virtual async Task UpdateNowAsync(IEnumerable<TEntity> entities,
            CancellationToken cancellationToken = default)
        {
            await UpdateAsync(entities);
            await SaveNowAsync(cancellationToken);
        }

        /// <summary>
        /// Update multiple records并立即提交
        /// </summary>
        /// <param name="entities">多个entity</param>
        /// <param name="acceptAllChangesOnSuccess">accept all changes</param>
        /// <param name="cancellationToken">Cancel async token</param>
        /// <returns>Task</returns>
        public virtual async Task UpdateNowAsync(IEnumerable<TEntity> entities, bool acceptAllChangesOnSuccess,
            CancellationToken cancellationToken = default)
        {
            await UpdateAsync(entities);
            await SaveNowAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        /// <summary>
        /// update a record中特定属性
        /// </summary>
        /// <param name="entity">entity</param>
        /// <param name="propertyNames">property name</param>
        /// <param name="ignoreNullValues"></param>
        /// <returns>代理中的entity</returns>
        public virtual EntityEntry<TEntity> UpdateInclude(TEntity entity, string[] propertyNames,
            bool? ignoreNullValues = null)
        {
            var entityEntry = ChangeEntityState(entity, EntityState.Detached);
            foreach (var propertyName in propertyNames)
            {
                EntityPropertyEntry(entity, propertyName).IsModified = true;
            }

            // Ignore null values
            IgnoreNullValues(ref entity, ignoreNullValues);

            return entityEntry;
        }

        /// <summary>
        /// update a record中特定属性
        /// </summary>
        /// <param name="entity">entity</param>
        /// <param name="propertyPredicates">property expression</param>
        /// <param name="ignoreNullValues"></param>
        /// <returns>代理中的entity</returns>
        public virtual EntityEntry<TEntity> UpdateInclude(TEntity entity,
            Expression<Func<TEntity, object>>[] propertyPredicates, bool? ignoreNullValues = null)
        {
            // Judging whether there is only one parameter，and is an anonymous type
            if (propertyPredicates?.Length == 1 && propertyPredicates[0].Body is NewExpression newExpression)
            {
                var propertyNames = newExpression.Members.Select(u => u.Name);
                return UpdateInclude(entity, propertyNames, ignoreNullValues);
            }
            else
            {
                var entityEntry = ChangeEntityState(entity, EntityState.Detached);
                foreach (var propertyPredicate in propertyPredicates)
                {
                    EntityPropertyEntry(entity, propertyPredicate).IsModified = true;
                }

                // Ignore null values
                IgnoreNullValues(ref entity, ignoreNullValues);

                return entityEntry;
            }
        }

        /// <summary>
        /// update a record中特定属性
        /// </summary>
        /// <param name="entity">entity</param>
        /// <param name="propertyNames">property name</param>
        /// <param name="ignoreNullValues"></param>
        /// <returns>代理中的entity</returns>
        public virtual EntityEntry<TEntity> UpdateInclude(TEntity entity, IEnumerable<string> propertyNames,
            bool? ignoreNullValues = null)
        {
            return UpdateInclude(entity, propertyNames.ToArray(), ignoreNullValues);
        }

        /// <summary>
        /// update a record中特定属性
        /// </summary>
        /// <param name="entity">entity</param>
        /// <param name="propertyPredicates">property expression</param>
        /// <param name="ignoreNullValues"></param>
        /// <returns>代理中的entity</returns>
        public virtual EntityEntry<TEntity> UpdateInclude(TEntity entity,
            IEnumerable<Expression<Func<TEntity, object>>> propertyPredicates, bool? ignoreNullValues = null)
        {
            return UpdateInclude(entity, propertyPredicates.ToArray(), ignoreNullValues);
        }

        /// <summary>
        /// update a record中的特定属性
        /// </summary>
        /// <param name="entity">entity</param>
        /// <param name="propertyNames">property name</param>
        /// <param name="ignoreNullValues"></param>
        /// <returns>代理中的entity</returns>
        public virtual Task<EntityEntry<TEntity>> UpdateIncludeAsync(TEntity entity, string[] propertyNames,
            bool? ignoreNullValues = null)
        {
            return Task.FromResult(UpdateInclude(entity, propertyNames, ignoreNullValues));
        }

        /// <summary>
        /// update a record中的特定属性
        /// </summary>
        /// <param name="entity">entity</param>
        /// <param name="propertyPredicates">property expression</param>
        /// <param name="ignoreNullValues"></param>
        /// <returns>代理中的entity</returns>
        public virtual Task<EntityEntry<TEntity>> UpdateIncludeAsync(TEntity entity,
            Expression<Func<TEntity, object>>[] propertyPredicates, bool? ignoreNullValues = null)
        {
            return Task.FromResult(UpdateInclude(entity, propertyPredicates, ignoreNullValues));
        }

        /// <summary>
        /// update a record中的特定属性
        /// </summary>
        /// <param name="entity">entity</param>
        /// <param name="propertyNames">property expression</param>
        /// <param name="ignoreNullValues"></param>
        /// <returns>代理中的entity</returns>
        public virtual Task<EntityEntry<TEntity>> UpdateIncludeAsync(TEntity entity, IEnumerable<string> propertyNames,
            bool? ignoreNullValues = null)
        {
            return Task.FromResult(UpdateInclude(entity, propertyNames, ignoreNullValues));
        }

        /// <summary>
        /// update a record中的特定属性
        /// </summary>
        /// <param name="entity">entity</param>
        /// <param name="propertyPredicates">property expression</param>
        /// <param name="ignoreNullValues"></param>
        /// <returns>代理中的entity</returns>
        public virtual Task<EntityEntry<TEntity>> UpdateIncludeAsync(TEntity entity,
            IEnumerable<Expression<Func<TEntity, object>>> propertyPredicates, bool? ignoreNullValues = null)
        {
            return Task.FromResult(UpdateInclude(entity, propertyPredicates, ignoreNullValues));
        }

        /// <summary>
        /// update a record中的特定属性并立即提交
        /// </summary>
        /// <param name="entity">entity</param>
        /// <param name="propertyNames">property name</param>
        /// <param name="ignoreNullValues"></param>
        /// <returns>数据库中的entity</returns>
        public virtual EntityEntry<TEntity> UpdateIncludeNow(TEntity entity, string[] propertyNames,
            bool? ignoreNullValues = null)
        {
            var entityEntry = UpdateInclude(entity, propertyNames, ignoreNullValues);
            SaveNow();
            return entityEntry;
        }

        /// <summary>
        /// update a record中的特定属性并立即提交
        /// </summary>
        /// <param name="entity">entity</param>
        /// <param name="propertyNames">property name</param>
        /// <param name="acceptAllChangesOnSuccess">accept all changes</param>
        /// <param name="ignoreNullValues"></param>
        /// <returns>数据库中的entity</returns>
        public virtual EntityEntry<TEntity> UpdateIncludeNow(TEntity entity, string[] propertyNames,
            bool acceptAllChangesOnSuccess, bool? ignoreNullValues = null)
        {
            var entityEntry = UpdateInclude(entity, propertyNames, ignoreNullValues);
            SaveNow(acceptAllChangesOnSuccess);
            return entityEntry;
        }

        /// <summary>
        /// update a record中的特定属性并立即提交
        /// </summary>
        /// <param name="entity">entity</param>
        /// <param name="propertyPredicates">property expression</param>
        /// <param name="ignoreNullValues"></param>
        /// <returns>数据库中entity</returns>
        public virtual EntityEntry<TEntity> UpdateIncludeNow(TEntity entity,
            Expression<Func<TEntity, object>>[] propertyPredicates, bool? ignoreNullValues = null)
        {
            var entityEntry = UpdateInclude(entity, propertyPredicates, ignoreNullValues);
            SaveNow();
            return entityEntry;
        }

        /// <summary>
        /// update a record中的特定属性并立即提交
        /// </summary>
        /// <param name="entity">entity</param>
        /// <param name="propertyPredicates">property expression</param>
        /// <param name="acceptAllChangesOnSuccess">accept all changes</param>
        /// <param name="ignoreNullValues"></param>
        /// <returns>数据库中的entity</returns>
        public virtual EntityEntry<TEntity> UpdateIncludeNow(TEntity entity,
            Expression<Func<TEntity, object>>[] propertyPredicates, bool acceptAllChangesOnSuccess,
            bool? ignoreNullValues = null)
        {
            var entityEntry = UpdateInclude(entity, propertyPredicates, ignoreNullValues);
            SaveNow(acceptAllChangesOnSuccess);
            return entityEntry;
        }

        /// <summary>
        /// update a record中的特定属性并立即提交
        /// </summary>
        /// <param name="entity">entity</param>
        /// <param name="propertyNames">property name</param>
        /// <param name="ignoreNullValues"></param>
        /// <returns>数据库中的entity</returns>
        public virtual EntityEntry<TEntity> UpdateIncludeNow(TEntity entity, IEnumerable<string> propertyNames,
            bool? ignoreNullValues = null)
        {
            var entityEntry = UpdateInclude(entity, propertyNames, ignoreNullValues);
            SaveNow();
            return entityEntry;
        }

        /// <summary>
        /// update a record中的特定属性并立即提交
        /// </summary>
        /// <param name="entity">entity</param>
        /// <param name="propertyNames">property name</param>
        /// <param name="acceptAllChangesOnSuccess">accept all changes</param>
        /// <param name="ignoreNullValues"></param>
        /// <returns>数据库中的entity</returns>
        public virtual EntityEntry<TEntity> UpdateIncludeNow(TEntity entity, IEnumerable<string> propertyNames,
            bool acceptAllChangesOnSuccess, bool? ignoreNullValues = null)
        {
            var entityEntry = UpdateInclude(entity, propertyNames, ignoreNullValues);
            SaveNow(acceptAllChangesOnSuccess);
            return entityEntry;
        }

        /// <summary>
        /// update a record中的特定属性并立即提交
        /// </summary>
        /// <param name="entity">entity</param>
        /// <param name="propertyPredicates">property expression</param>
        /// <param name="ignoreNullValues"></param>
        /// <returns>数据库中的entity</returns>
        public virtual EntityEntry<TEntity> UpdateIncludeNow(TEntity entity,
            IEnumerable<Expression<Func<TEntity, object>>> propertyPredicates, bool? ignoreNullValues = null)
        {
            var entityEntry = UpdateInclude(entity, propertyPredicates, ignoreNullValues);
            SaveNow();
            return entityEntry;
        }

        /// <summary>
        /// update a record中的特定属性并立即提交
        /// </summary>
        /// <param name="entity">entity</param>
        /// <param name="propertyPredicates">property expression</param>
        /// <param name="acceptAllChangesOnSuccess">accept all changes</param>
        /// <param name="ignoreNullValues"></param>
        /// <returns>数据库中的entity</returns>
        public virtual EntityEntry<TEntity> UpdateIncludeNow(TEntity entity,
            IEnumerable<Expression<Func<TEntity, object>>> propertyPredicates, bool acceptAllChangesOnSuccess,
            bool? ignoreNullValues = null)
        {
            var entityEntry = UpdateInclude(entity, propertyPredicates, ignoreNullValues);
            SaveNow(acceptAllChangesOnSuccess);
            return entityEntry;
        }

        /// <summary>
        /// update a record中的特定属性并立即提交
        /// </summary>
        /// <param name="entity">entity</param>
        /// <param name="propertyNames">property name</param>
        /// <param name="ignoreNullValues"></param>
        /// <param name="cancellationToken">Cancel async token</param>
        /// <returns>数据库中的entity</returns>
        public virtual async Task<EntityEntry<TEntity>> UpdateIncludeNowAsync(TEntity entity, string[] propertyNames,
            bool? ignoreNullValues = null, CancellationToken cancellationToken = default)
        {
            var entityEntry = await UpdateIncludeAsync(entity, propertyNames, ignoreNullValues);
            await SaveNowAsync(cancellationToken);
            return entityEntry;
        }

        /// <summary>
        /// update a record中的特定属性并立即提交
        /// </summary>
        /// <param name="entity">entity</param>
        /// <param name="propertyNames">property name</param>
        /// <param name="acceptAllChangesOnSuccess">accept all changes</param>
        /// <param name="ignoreNullValues"></param>
        /// <param name="cancellationToken">Cancel async token</param>
        /// <returns>数据库中的entity</returns>
        public virtual async Task<EntityEntry<TEntity>> UpdateIncludeNowAsync(TEntity entity, string[] propertyNames,
            bool acceptAllChangesOnSuccess, bool? ignoreNullValues = null,
            CancellationToken cancellationToken = default)
        {
            var entityEntry = await UpdateIncludeAsync(entity, propertyNames, ignoreNullValues);
            await SaveNowAsync(acceptAllChangesOnSuccess, cancellationToken);
            return entityEntry;
        }

        /// <summary>
        /// update a record中的特定属性并立即提交
        /// </summary>
        /// <param name="entity">entity</param>
        /// <param name="propertyPredicates">property expression</param>
        /// <param name="ignoreNullValues"></param>
        /// <param name="cancellationToken">Cancel async token</param>
        /// <returns>数据库中的entity</returns>
        public virtual async Task<EntityEntry<TEntity>> UpdateIncludeNowAsync(TEntity entity,
            Expression<Func<TEntity, object>>[] propertyPredicates, bool? ignoreNullValues = null,
            CancellationToken cancellationToken = default)
        {
            var entityEntry = await UpdateIncludeAsync(entity, propertyPredicates, ignoreNullValues);
            await SaveNowAsync(cancellationToken);
            return entityEntry;
        }

        /// <summary>
        /// update a record中的特定属性并立即提交
        /// </summary>
        /// <param name="entity">entity</param>
        /// <param name="propertyPredicates">property expression</param>
        /// <param name="acceptAllChangesOnSuccess">accept all changes</param>
        /// <param name="ignoreNullValues"></param>
        /// <param name="cancellationToken">Cancel async token</param>
        /// <returns>数据库中的entity</returns>
        public virtual async Task<EntityEntry<TEntity>> UpdateIncludeNowAsync(TEntity entity,
            Expression<Func<TEntity, object>>[] propertyPredicates, bool acceptAllChangesOnSuccess,
            bool? ignoreNullValues = null, CancellationToken cancellationToken = default)
        {
            var entityEntry = await UpdateIncludeAsync(entity, propertyPredicates, ignoreNullValues);
            await SaveNowAsync(acceptAllChangesOnSuccess, cancellationToken);
            return entityEntry;
        }

        /// <summary>
        /// update a record中的特定属性并立即提交
        /// </summary>
        /// <param name="entity">entity</param>
        /// <param name="propertyNames">property name</param>
        /// <param name="ignoreNullValues"></param>
        /// <param name="cancellationToken">Cancel async token</param>
        /// <returns>数据库中的entity</returns>
        public virtual async Task<EntityEntry<TEntity>> UpdateIncludeNowAsync(TEntity entity,
            IEnumerable<string> propertyNames, bool? ignoreNullValues = null,
            CancellationToken cancellationToken = default)
        {
            var entityEntry = await UpdateIncludeAsync(entity, propertyNames, ignoreNullValues);
            await SaveNowAsync(cancellationToken);
            return entityEntry;
        }

        /// <summary>
        /// update a record中的特定属性并立即提交
        /// </summary>
        /// <param name="entity">entity</param>
        /// <param name="propertyNames">property name</param>
        /// <param name="acceptAllChangesOnSuccess">accept all changes</param>
        /// <param name="ignoreNullValues"></param>
        /// <param name="cancellationToken">Cancel async token</param>
        /// <returns>数据库中的entity</returns>
        public virtual async Task<EntityEntry<TEntity>> UpdateIncludeNowAsync(TEntity entity,
            IEnumerable<string> propertyNames, bool acceptAllChangesOnSuccess, bool? ignoreNullValues = null,
            CancellationToken cancellationToken = default)
        {
            var entityEntry = await UpdateIncludeAsync(entity, propertyNames, ignoreNullValues);
            await SaveNowAsync(acceptAllChangesOnSuccess, cancellationToken);
            return entityEntry;
        }

        /// <summary>
        /// update a record中的特定属性并立即提交
        /// </summary>
        /// <param name="entity">entity</param>
        /// <param name="propertyPredicates">property expression</param>
        /// <param name="ignoreNullValues"></param>
        /// <param name="cancellationToken">Cancel async token</param>
        /// <returns>数据库中的entity</returns>
        public virtual async Task<EntityEntry<TEntity>> UpdateIncludeNowAsync(TEntity entity,
            IEnumerable<Expression<Func<TEntity, object>>> propertyPredicates, bool? ignoreNullValues = null,
            CancellationToken cancellationToken = default)
        {
            var entityEntry = await UpdateIncludeAsync(entity, propertyPredicates, ignoreNullValues);
            await SaveNowAsync(cancellationToken);
            return entityEntry;
        }

        /// <summary>
        /// update a record中的特定属性并立即提交
        /// </summary>
        /// <param name="entity">entity</param>
        /// <param name="propertyPredicates">property expression</param>
        /// <param name="acceptAllChangesOnSuccess">accept all changes</param>
        /// <param name="ignoreNullValues"></param>
        /// <param name="cancellationToken">Cancel async token</param>
        /// <returns>数据库中的entity</returns>
        public virtual async Task<EntityEntry<TEntity>> UpdateIncludeNowAsync(TEntity entity,
            IEnumerable<Expression<Func<TEntity, object>>> propertyPredicates, bool acceptAllChangesOnSuccess,
            bool? ignoreNullValues = null, CancellationToken cancellationToken = default)
        {
            var entityEntry = await UpdateIncludeAsync(entity, propertyPredicates, ignoreNullValues);
            await SaveNowAsync(acceptAllChangesOnSuccess, cancellationToken);
            return entityEntry;
        }

        /// <summary>
        /// update a record并排除属性
        /// </summary>
        /// <param name="entity">entity</param>
        /// <param name="propertyNames">property name</param>
        /// <param name="ignoreNullValues"></param>
        /// <returns>代理中的entity</returns>
        public virtual EntityEntry<TEntity> UpdateExclude(TEntity entity, string[] propertyNames,
            bool? ignoreNullValues = null)
        {
            var entityEntry = ChangeEntityState(entity, EntityState.Modified);
            foreach (var propertyName in propertyNames)
            {
                EntityPropertyEntry(entity, propertyName).IsModified = false;
            }

            // Ignore null values
            IgnoreNullValues(ref entity, ignoreNullValues);

            return entityEntry;
        }

        /// <summary>
        /// update a record并排除属性
        /// </summary>
        /// <param name="entity">entity</param>
        /// <param name="propertyPredicates">property expression</param>
        /// <param name="ignoreNullValues"></param>
        /// <returns>代理中的entity</returns>
        public virtual EntityEntry<TEntity> UpdateExclude(TEntity entity,
            Expression<Func<TEntity, object>>[] propertyPredicates, bool? ignoreNullValues = null)
        {
            // Judging whether there is only one parameter，and is an anonymous type
            if (propertyPredicates?.Length == 1 && propertyPredicates[0].Body is NewExpression newExpression)
            {
                var propertyNames = newExpression.Members.Select(u => u.Name);
                return UpdateExclude(entity, propertyNames, ignoreNullValues);
            }
            else
            {
                var entityEntry = ChangeEntityState(entity, EntityState.Modified);
                foreach (var propertyPredicate in propertyPredicates)
                {
                    EntityPropertyEntry(entity, propertyPredicate).IsModified = false;
                }

                // Ignore null values
                IgnoreNullValues(ref entity, ignoreNullValues);

                return entityEntry;
            }
        }

        /// <summary>
        /// update a record并排除属性
        /// </summary>
        /// <param name="entity">entity</param>
        /// <param name="propertyNames">property name</param>
        /// <param name="ignoreNullValues"></param>
        /// <returns>代理中的entity</returns>
        public virtual EntityEntry<TEntity> UpdateExclude(TEntity entity, IEnumerable<string> propertyNames,
            bool? ignoreNullValues = null)
        {
            return UpdateExclude(entity, propertyNames.ToArray(), ignoreNullValues);
        }

        /// <summary>
        /// update a record并排除属性
        /// </summary>
        /// <param name="entity">entity</param>
        /// <param name="propertyPredicates">property expression</param>
        /// <param name="ignoreNullValues"></param>
        /// <returns>代理中的entity</returns>
        public virtual EntityEntry<TEntity> UpdateExclude(TEntity entity,
            IEnumerable<Expression<Func<TEntity, object>>> propertyPredicates, bool? ignoreNullValues = null)
        {
            return UpdateExclude(entity, propertyPredicates.ToArray(), ignoreNullValues);
        }

        /// <summary>
        /// update a record并排除属性
        /// </summary>
        /// <param name="entity">entity</param>
        /// <param name="propertyNames">property name</param>
        /// <param name="ignoreNullValues"></param>
        /// <returns>代理中的entity</returns>
        public virtual Task<EntityEntry<TEntity>> UpdateExcludeAsync(TEntity entity, string[] propertyNames,
            bool? ignoreNullValues = null)
        {
            return Task.FromResult(UpdateExclude(entity, propertyNames, ignoreNullValues));
        }

        /// <summary>
        /// update a record并排除属性
        /// </summary>
        /// <param name="entity">entity</param>
        /// <param name="propertyPredicates">property expression</param>
        /// <param name="ignoreNullValues"></param>
        /// <returns>代理中的entity</returns>
        public virtual Task<EntityEntry<TEntity>> UpdateExcludeAsync(TEntity entity,
            Expression<Func<TEntity, object>>[] propertyPredicates, bool? ignoreNullValues = null)
        {
            return Task.FromResult(UpdateExclude(entity, propertyPredicates, ignoreNullValues));
        }

        /// <summary>
        /// update a record并排除属性
        /// </summary>
        /// <param name="entity">entity</param>
        /// <param name="propertyNames">property expression</param>
        /// <param name="ignoreNullValues"></param>
        /// <returns>代理中的entity</returns>
        public virtual Task<EntityEntry<TEntity>> UpdateExcludeAsync(TEntity entity, IEnumerable<string> propertyNames,
            bool? ignoreNullValues = null)
        {
            return Task.FromResult(UpdateExclude(entity, propertyNames, ignoreNullValues));
        }

        /// <summary>
        /// update a record并排除属性
        /// </summary>
        /// <param name="entity">entity</param>
        /// <param name="propertyPredicates">property expression</param>
        /// <param name="ignoreNullValues"></param>
        /// <returns>代理中的entity</returns>
        public virtual Task<EntityEntry<TEntity>> UpdateExcludeAsync(TEntity entity,
            IEnumerable<Expression<Func<TEntity, object>>> propertyPredicates, bool? ignoreNullValues = null)
        {
            return Task.FromResult(UpdateExclude(entity, propertyPredicates, ignoreNullValues));
        }

        /// <summary>
        /// update a record并排除属性并立即提交
        /// </summary>
        /// <param name="entity">entity</param>
        /// <param name="propertyNames">property name</param>
        /// <param name="ignoreNullValues"></param>
        /// <returns>数据库中的entity</returns>
        public virtual EntityEntry<TEntity> UpdateExcludeNow(TEntity entity, string[] propertyNames,
            bool? ignoreNullValues = null)
        {
            var entityEntry = UpdateExclude(entity, propertyNames, ignoreNullValues);
            SaveNow();
            return entityEntry;
        }

        /// <summary>
        /// update a record并排除属性并立即提交
        /// </summary>
        /// <param name="entity">entity</param>
        /// <param name="propertyNames">property name</param>
        /// <param name="acceptAllChangesOnSuccess">accept all changes</param>
        /// <param name="ignoreNullValues"></param>
        /// <returns>数据库中的entity</returns>
        public virtual EntityEntry<TEntity> UpdateExcludeNow(TEntity entity, string[] propertyNames,
            bool acceptAllChangesOnSuccess, bool? ignoreNullValues = null)
        {
            var entityEntry = UpdateExclude(entity, propertyNames, ignoreNullValues);
            SaveNow(acceptAllChangesOnSuccess);
            return entityEntry;
        }

        /// <summary>
        /// update a record并排除属性并立即提交
        /// </summary>
        /// <param name="entity">entity</param>
        /// <param name="propertyPredicates">property expression</param>
        /// <param name="ignoreNullValues"></param>
        /// <returns>数据库中entity</returns>
        public virtual EntityEntry<TEntity> UpdateExcludeNow(TEntity entity,
            Expression<Func<TEntity, object>>[] propertyPredicates, bool? ignoreNullValues = null)
        {
            var entityEntry = UpdateExclude(entity, propertyPredicates, ignoreNullValues);
            SaveNow();
            return entityEntry;
        }

        /// <summary>
        /// update a record并排除属性并立即提交
        /// </summary>
        /// <param name="entity">entity</param>
        /// <param name="propertyPredicates">property expression</param>
        /// <param name="acceptAllChangesOnSuccess">accept all changes</param>
        /// <param name="ignoreNullValues"></param>
        /// <returns>数据库中的entity</returns>
        public virtual EntityEntry<TEntity> UpdateExcludeNow(TEntity entity,
            Expression<Func<TEntity, object>>[] propertyPredicates, bool acceptAllChangesOnSuccess,
            bool? ignoreNullValues = null)
        {
            var entityEntry = UpdateExclude(entity, propertyPredicates, ignoreNullValues);
            SaveNow(acceptAllChangesOnSuccess);
            return entityEntry;
        }

        /// <summary>
        /// update a record并排除属性并立即提交
        /// </summary>
        /// <param name="entity">entity</param>
        /// <param name="propertyNames">property name</param>
        /// <param name="ignoreNullValues"></param>
        /// <returns>数据库中的entity</returns>
        public virtual EntityEntry<TEntity> UpdateExcludeNow(TEntity entity, IEnumerable<string> propertyNames,
            bool? ignoreNullValues = null)
        {
            var entityEntry = UpdateExclude(entity, propertyNames, ignoreNullValues);
            SaveNow();
            return entityEntry;
        }

        /// <summary>
        /// update a record并排除属性并立即提交
        /// </summary>
        /// <param name="entity">entity</param>
        /// <param name="propertyNames">property name</param>
        /// <param name="acceptAllChangesOnSuccess">accept all changes</param>
        /// <param name="ignoreNullValues"></param>
        /// <returns>数据库中的entity</returns>
        public virtual EntityEntry<TEntity> UpdateExcludeNow(TEntity entity, IEnumerable<string> propertyNames,
            bool acceptAllChangesOnSuccess, bool? ignoreNullValues = null)
        {
            var entityEntry = UpdateExclude(entity, propertyNames, ignoreNullValues);
            SaveNow(acceptAllChangesOnSuccess);
            return entityEntry;
        }

        /// <summary>
        /// update a record并排除属性并立即提交
        /// </summary>
        /// <param name="entity">entity</param>
        /// <param name="propertyPredicates">property expression</param>
        /// <param name="ignoreNullValues"></param>
        /// <returns>数据库中的entity</returns>
        public virtual EntityEntry<TEntity> UpdateExcludeNow(TEntity entity,
            IEnumerable<Expression<Func<TEntity, object>>> propertyPredicates, bool? ignoreNullValues = null)
        {
            var entityEntry = UpdateExclude(entity, propertyPredicates, ignoreNullValues);
            SaveNow();
            return entityEntry;
        }

        /// <summary>
        /// update a record并排除属性并立即提交
        /// </summary>
        /// <param name="entity">entity</param>
        /// <param name="propertyPredicates">property expression</param>
        /// <param name="acceptAllChangesOnSuccess">accept all changes</param>
        /// <param name="ignoreNullValues"></param>
        /// <returns>数据库中的entity</returns>
        public virtual EntityEntry<TEntity> UpdateExcludeNow(TEntity entity,
            IEnumerable<Expression<Func<TEntity, object>>> propertyPredicates, bool acceptAllChangesOnSuccess,
            bool? ignoreNullValues = null)
        {
            var entityEntry = UpdateExclude(entity, propertyPredicates, ignoreNullValues);
            SaveNow(acceptAllChangesOnSuccess);
            return entityEntry;
        }

        /// <summary>
        /// update a record并排除属性并立即提交
        /// </summary>
        /// <param name="entity">entity</param>
        /// <param name="propertyNames">property name</param>
        /// <param name="ignoreNullValues"></param>
        /// <param name="cancellationToken">Cancel async token</param>
        /// <returns>数据库中的entity</returns>
        public virtual async Task<EntityEntry<TEntity>> UpdateExcludeNowAsync(TEntity entity, string[] propertyNames,
            bool? ignoreNullValues = null, CancellationToken cancellationToken = default)
        {
            var entityEntry = await UpdateExcludeAsync(entity, propertyNames, ignoreNullValues);
            await SaveNowAsync(cancellationToken);
            return entityEntry;
        }

        /// <summary>
        /// update a record并排除属性并立即提交
        /// </summary>
        /// <param name="entity">entity</param>
        /// <param name="propertyNames">property name</param>
        /// <param name="acceptAllChangesOnSuccess">accept all changes</param>
        /// <param name="ignoreNullValues"></param>
        /// <param name="cancellationToken">Cancel async token</param>
        /// <returns>数据库中的entity</returns>
        public virtual async Task<EntityEntry<TEntity>> UpdateExcludeNowAsync(TEntity entity, string[] propertyNames,
            bool acceptAllChangesOnSuccess, bool? ignoreNullValues = null,
            CancellationToken cancellationToken = default)
        {
            var entityEntry = await UpdateExcludeAsync(entity, propertyNames, ignoreNullValues);
            await SaveNowAsync(acceptAllChangesOnSuccess, cancellationToken);
            return entityEntry;
        }

        /// <summary>
        /// update a record并排除属性并立即提交
        /// </summary>
        /// <param name="entity">entity</param>
        /// <param name="propertyPredicates">property expression</param>
        /// <param name="ignoreNullValues"></param>
        /// <param name="cancellationToken">Cancel async token</param>
        /// <returns>数据库中的entity</returns>
        public virtual async Task<EntityEntry<TEntity>> UpdateExcludeNowAsync(TEntity entity,
            Expression<Func<TEntity, object>>[] propertyPredicates, bool? ignoreNullValues = null,
            CancellationToken cancellationToken = default)
        {
            var entityEntry = await UpdateExcludeAsync(entity, propertyPredicates, ignoreNullValues);
            await SaveNowAsync(cancellationToken);
            return entityEntry;
        }

        /// <summary>
        /// update a record并排除属性并立即提交
        /// </summary>
        /// <param name="entity">entity</param>
        /// <param name="propertyPredicates">property expression</param>
        /// <param name="acceptAllChangesOnSuccess">accept all changes</param>
        /// <param name="ignoreNullValues"></param>
        /// <param name="cancellationToken">Cancel async token</param>
        /// <returns>数据库中的entity</returns>
        public virtual async Task<EntityEntry<TEntity>> UpdateExcludeNowAsync(TEntity entity,
            Expression<Func<TEntity, object>>[] propertyPredicates, bool acceptAllChangesOnSuccess,
            bool? ignoreNullValues = null, CancellationToken cancellationToken = default)
        {
            var entityEntry = await UpdateExcludeAsync(entity, propertyPredicates, ignoreNullValues);
            await SaveNowAsync(acceptAllChangesOnSuccess, cancellationToken);
            return entityEntry;
        }

        /// <summary>
        /// update a record并排除属性并立即提交
        /// </summary>
        /// <param name="entity">entity</param>
        /// <param name="propertyNames">property name</param>
        /// <param name="ignoreNullValues"></param>
        /// <param name="cancellationToken">Cancel async token</param>
        /// <returns>数据库中的entity</returns>
        public virtual async Task<EntityEntry<TEntity>> UpdateExcludeNowAsync(TEntity entity,
            IEnumerable<string> propertyNames, bool? ignoreNullValues = null,
            CancellationToken cancellationToken = default)
        {
            var entityEntry = await UpdateExcludeAsync(entity, propertyNames, ignoreNullValues);
            await SaveNowAsync(cancellationToken);
            return entityEntry;
        }

        /// <summary>
        /// update a record并排除属性并立即提交
        /// </summary>
        /// <param name="entity">entity</param>
        /// <param name="propertyNames">property name</param>
        /// <param name="acceptAllChangesOnSuccess">accept all changes</param>
        /// <param name="ignoreNullValues"></param>
        /// <param name="cancellationToken">Cancel async token</param>
        /// <returns>数据库中的entity</returns>
        public virtual async Task<EntityEntry<TEntity>> UpdateExcludeNowAsync(TEntity entity,
            IEnumerable<string> propertyNames, bool acceptAllChangesOnSuccess, bool? ignoreNullValues = null,
            CancellationToken cancellationToken = default)
        {
            var entityEntry = await UpdateExcludeAsync(entity, propertyNames, ignoreNullValues);
            await SaveNowAsync(acceptAllChangesOnSuccess, cancellationToken);
            return entityEntry;
        }

        /// <summary>
        /// update a record并排除属性并立即提交
        /// </summary>
        /// <param name="entity">entity</param>
        /// <param name="propertyPredicates">property expression</param>
        /// <param name="ignoreNullValues"></param>
        /// <param name="cancellationToken">Cancel async token</param>
        /// <returns>数据库中的entity</returns>
        public virtual async Task<EntityEntry<TEntity>> UpdateExcludeNowAsync(TEntity entity,
            IEnumerable<Expression<Func<TEntity, object>>> propertyPredicates, bool? ignoreNullValues = null,
            CancellationToken cancellationToken = default)
        {
            var entityEntry = await UpdateExcludeAsync(entity, propertyPredicates, ignoreNullValues);
            await SaveNowAsync(cancellationToken);
            return entityEntry;
        }

        /// <summary>
        /// update a record并排除属性并立即提交
        /// </summary>
        /// <param name="entity">entity</param>
        /// <param name="propertyPredicates">property expression</param>
        /// <param name="acceptAllChangesOnSuccess">accept all changes</param>
        /// <param name="ignoreNullValues"></param>
        /// <param name="cancellationToken">Cancel async token</param>
        /// <returns>数据库中的entity</returns>
        public virtual async Task<EntityEntry<TEntity>> UpdateExcludeNowAsync(TEntity entity,
            IEnumerable<Expression<Func<TEntity, object>>> propertyPredicates, bool acceptAllChangesOnSuccess,
            bool? ignoreNullValues = null, CancellationToken cancellationToken = default)
        {
            var entityEntry = await UpdateExcludeAsync(entity, propertyPredicates, ignoreNullValues);
            await SaveNowAsync(acceptAllChangesOnSuccess, cancellationToken);
            return entityEntry;
        }

        /// <summary>
        /// Ignore null values属性
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="ignoreNullValues"></param>
        private void IgnoreNullValues(ref TEntity entity, bool? ignoreNullValues = null)
        {
            var isIgnore = ignoreNullValues ?? DynamicContext.InsertOrUpdateIgnoreNullValues;
            if (isIgnore == false) return;

            // get all properties
            var properties = EntityType?.GetProperties();
            if (properties == null) return;

            foreach (var propety in properties)
            {
                var entityProperty = EntityPropertyEntry(entity, propety.Name);
                var propertyValue = entityProperty?.CurrentValue;
                var propertyType = entityProperty?.Metadata?.PropertyInfo?.PropertyType;

                // Determine whether it is an invalid value，such as null，default time，and empty Guid value
                var isInvalid = propertyValue == null
                                || (propertyType == typeof(DateTime) &&
                                    propertyValue?.ToString() == new DateTime().ToString())
                                || (propertyType == typeof(DateTimeOffset) &&
                                    propertyValue?.ToString() == new DateTimeOffset().ToString())
                                || (propertyType == typeof(Guid) && propertyValue?.ToString() == Guid.Empty.ToString());

                if (isInvalid && entityProperty != null)
                {
                    entityProperty.IsModified = false;
                }
            }
        }
    }
}