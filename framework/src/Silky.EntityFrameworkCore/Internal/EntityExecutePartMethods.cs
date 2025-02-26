using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Silky.Core;
using Silky.EntityFrameworkCore.Entities;
using Silky.EntityFrameworkCore.Repositories;

namespace Silky.EntityFrameworkCore
{
    /// <summary>
    /// entity executive component
    /// </summary>
    public sealed partial class EntityExecutePart<TEntity>
        where TEntity : class, IPrivateEntity, new()
    {
        /// <summary>
        /// get entity like（ethnic group）
        /// </summary>
        /// <returns>DbSet{TEntity}</returns>
        public DbSet<TEntity> Ethnics()
        {
            return GetRepository().Entities;
        }

        /// <summary>
        /// add a record
        /// </summary>
        /// <param name="ignoreNullValues"></param>
        /// <returns>proxy entity</returns>
        public EntityEntry<TEntity> Insert(bool? ignoreNullValues = null)
        {
            return GetRepository().Insert(Entity, ignoreNullValues);
        }

        /// <summary>
        /// add a record
        /// </summary>
        /// <param name="ignoreNullValues"></param>
        /// <param name="cancellationToken">Asynchronous cancellation token</param>
        /// <returns>proxy entity</returns>
        public Task<EntityEntry<TEntity>> InsertAsync(bool? ignoreNullValues = null,
            CancellationToken cancellationToken = default)
        {
            return GetRepository().InsertAsync(Entity, ignoreNullValues, cancellationToken);
        }

        /// <summary>
        /// add a record并立即提交
        /// </summary>
        /// <param name="ignoreNullValues"></param>
        /// <returns>entities in the database</returns>
        public EntityEntry<TEntity> InsertNow(bool? ignoreNullValues = null)
        {
            return GetRepository().InsertNow(Entity, ignoreNullValues);
        }

        /// <summary>
        /// add a record并立即提交
        /// </summary>
        /// <param name="acceptAllChangesOnSuccess">Accept all commit changes</param>
        /// <param name="ignoreNullValues"></param>
        /// <returns>entities in the database</returns>
        public EntityEntry<TEntity> InsertNow(bool acceptAllChangesOnSuccess, bool? ignoreNullValues = null)
        {
            return GetRepository().InsertNow(Entity, acceptAllChangesOnSuccess, ignoreNullValues);
        }

        /// <summary>
        /// add a record并立即提交
        /// </summary>
        /// <param name="cancellationToken">Asynchronous cancellation token</param>
        /// <param name="ignoreNullValues"></param>
        /// <returns>entities in the database</returns>
        public Task<EntityEntry<TEntity>> InsertNowAsync(bool? ignoreNullValues = null,
            CancellationToken cancellationToken = default)
        {
            return GetRepository().InsertNowAsync(Entity, ignoreNullValues, cancellationToken);
        }

        /// <summary>
        /// add a record并立即提交
        /// </summary>
        /// <param name="acceptAllChangesOnSuccess">Accept all commit changes</param>
        /// <param name="ignoreNullValues"></param>
        /// <param name="cancellationToken">Asynchronous cancellation token</param>
        /// <returns>entities in the database</returns>
        public Task<EntityEntry<TEntity>> InsertNowAsync(bool acceptAllChangesOnSuccess, bool? ignoreNullValues = null,
            CancellationToken cancellationToken = default)
        {
            return GetRepository()
                .InsertNowAsync(Entity, acceptAllChangesOnSuccess, ignoreNullValues, cancellationToken);
        }

        /// <summary>
        /// update a record
        /// </summary>
        /// <param name="ignoreNullValues"></param>
        /// <returns>Entity in the proxy</returns>
        public EntityEntry<TEntity> Update(bool? ignoreNullValues = null)
        {
            return GetRepository().Update(Entity, ignoreNullValues);
        }

        /// <summary>
        /// update a record
        /// </summary>
        /// <param name="ignoreNullValues"></param>
        /// <returns>Entity in the proxy</returns>
        public Task<EntityEntry<TEntity>> UpdateAsync(bool? ignoreNullValues = null)
        {
            return GetRepository().UpdateAsync(Entity, ignoreNullValues);
        }

        /// <summary>
        /// update a record并立即提交
        /// </summary>
        /// <param name="ignoreNullValues"></param>
        /// <returns>entities in the database</returns>
        public EntityEntry<TEntity> UpdateNow(bool? ignoreNullValues = null)
        {
            return GetRepository().UpdateNow(Entity, ignoreNullValues);
        }

        /// <summary>
        /// update a record并立即提交
        /// </summary>
        /// <param name="acceptAllChangesOnSuccess">accept all changes</param>
        /// <param name="ignoreNullValues"></param>
        /// <returns>entities in the database</returns>
        public EntityEntry<TEntity> UpdateNow(bool acceptAllChangesOnSuccess, bool? ignoreNullValues = null)
        {
            return GetRepository().UpdateNow(Entity, acceptAllChangesOnSuccess, ignoreNullValues);
        }

        /// <summary>
        /// update a record并立即提交
        /// </summary>
        /// <param name="ignoreNullValues"></param>
        /// <param name="cancellationToken">Cancel async token</param>
        /// <returns>entities in the database</returns>
        public Task<EntityEntry<TEntity>> UpdateNowAsync(bool? ignoreNullValues = null,
            CancellationToken cancellationToken = default)
        {
            return GetRepository().UpdateNowAsync(Entity, ignoreNullValues, cancellationToken);
        }

        /// <summary>
        /// update a record并立即提交
        /// </summary>
        /// <param name="acceptAllChangesOnSuccess">accept all changes</param>
        /// <param name="ignoreNullValues"></param>
        /// <param name="cancellationToken">Cancel async token</param>
        /// <returns>entities in the database</returns>
        public Task<EntityEntry<TEntity>> UpdateNowAsync(bool acceptAllChangesOnSuccess, bool? ignoreNullValues = null,
            CancellationToken cancellationToken = default)
        {
            return GetRepository()
                .UpdateNowAsync(Entity, acceptAllChangesOnSuccess, ignoreNullValues, cancellationToken);
        }

        /// <summary>
        /// update a record中特定属性
        /// </summary>
        /// <param name="propertyNames">property name</param>
        /// <param name="ignoreNullValues"></param>
        /// <returns>Entity in the proxy</returns>
        public EntityEntry<TEntity> UpdateInclude(string[] propertyNames, bool? ignoreNullValues = null)
        {
            return GetRepository().UpdateInclude(Entity, propertyNames, ignoreNullValues);
        }

        /// <summary>
        /// update a record中特定属性
        /// </summary>
        /// <param name="propertyPredicates">property expression</param>
        /// <param name="ignoreNullValues"></param>
        /// <returns>Entity in the proxy</returns>
        public EntityEntry<TEntity> UpdateInclude(Expression<Func<TEntity, object>>[] propertyPredicates,
            bool? ignoreNullValues = null)
        {
            return GetRepository().UpdateInclude(Entity, propertyPredicates, ignoreNullValues);
        }

        /// <summary>
        /// update a record中特定属性
        /// </summary>
        /// <param name="propertyNames">property name</param>
        /// <param name="ignoreNullValues"></param>
        /// <returns>Entity in the proxy</returns>
        public EntityEntry<TEntity> UpdateInclude(IEnumerable<string> propertyNames, bool? ignoreNullValues = null)
        {
            return GetRepository().UpdateInclude(Entity, propertyNames, ignoreNullValues);
        }

        /// <summary>
        /// update a record中特定属性
        /// </summary>
        /// <param name="propertyPredicates">property expression</param>
        /// <param name="ignoreNullValues"></param>
        /// <returns>Entity in the proxy</returns>
        public EntityEntry<TEntity> UpdateInclude(IEnumerable<Expression<Func<TEntity, object>>> propertyPredicates,
            bool? ignoreNullValues = null)
        {
            return GetRepository().UpdateInclude(Entity, propertyPredicates, ignoreNullValues);
        }

        /// <summary>
        /// update a record中的特定属性
        /// </summary>
        /// <param name="propertyNames">property name</param>
        /// <param name="ignoreNullValues"></param>
        /// <returns>Entity in the proxy</returns>
        public Task<EntityEntry<TEntity>> UpdateIncludeAsync(string[] propertyNames, bool? ignoreNullValues = null)
        {
            return GetRepository().UpdateIncludeAsync(Entity, propertyNames, ignoreNullValues);
        }

        /// <summary>
        /// update a record中的特定属性
        /// </summary>
        /// <param name="propertyPredicates">property expression</param>
        /// <param name="ignoreNullValues"></param>
        /// <returns>Entity in the proxy</returns>
        public Task<EntityEntry<TEntity>> UpdateIncludeAsync(Expression<Func<TEntity, object>>[] propertyPredicates,
            bool? ignoreNullValues = null)
        {
            return GetRepository().UpdateIncludeAsync(Entity, propertyPredicates, ignoreNullValues);
        }

        /// <summary>
        /// update a record中的特定属性
        /// </summary>
        /// <param name="propertyNames">property expression</param>
        /// <param name="ignoreNullValues"></param>
        /// <returns>Entity in the proxy</returns>
        public Task<EntityEntry<TEntity>> UpdateIncludeAsync(IEnumerable<string> propertyNames,
            bool? ignoreNullValues = null)
        {
            return GetRepository().UpdateIncludeAsync(Entity, propertyNames, ignoreNullValues);
        }

        /// <summary>
        /// update a record中的特定属性
        /// </summary>
        /// <param name="propertyPredicates">property expression</param>
        /// <param name="ignoreNullValues"></param>
        /// <returns>Entity in the proxy</returns>
        public Task<EntityEntry<TEntity>> UpdateIncludeAsync(
            IEnumerable<Expression<Func<TEntity, object>>> propertyPredicates, bool? ignoreNullValues = null)
        {
            return GetRepository().UpdateIncludeAsync(Entity, propertyPredicates, ignoreNullValues);
        }

        /// <summary>
        /// update a record中的特定属性并立即提交
        /// </summary>
        /// <param name="propertyNames">property name</param>
        /// <param name="ignoreNullValues"></param>
        /// <returns>entities in the database</returns>
        public EntityEntry<TEntity> UpdateIncludeNow(string[] propertyNames, bool? ignoreNullValues = null)
        {
            return GetRepository().UpdateIncludeNow(Entity, propertyNames, ignoreNullValues);
        }

        /// <summary>
        /// update a record中的特定属性并立即提交
        /// </summary>
        /// <param name="propertyNames">property name</param>
        /// <param name="acceptAllChangesOnSuccess">accept all changes</param>
        /// <param name="ignoreNullValues"></param>
        /// <returns>entities in the database</returns>
        public EntityEntry<TEntity> UpdateIncludeNow(string[] propertyNames, bool acceptAllChangesOnSuccess,
            bool? ignoreNullValues = null)
        {
            return GetRepository().UpdateIncludeNow(Entity, propertyNames, acceptAllChangesOnSuccess, ignoreNullValues);
        }

        /// <summary>
        /// update a record中的特定属性并立即提交
        /// </summary>
        /// <param name="propertyPredicates">property expression</param>
        /// <param name="ignoreNullValues"></param>
        /// <returns>entities in the database</returns>
        public EntityEntry<TEntity> UpdateIncludeNow(Expression<Func<TEntity, object>>[] propertyPredicates,
            bool? ignoreNullValues = null)
        {
            return GetRepository().UpdateIncludeNow(Entity, propertyPredicates, ignoreNullValues);
        }

        /// <summary>
        /// update a record中的特定属性并立即提交
        /// </summary>
        /// <param name="propertyPredicates">property expression</param>
        /// <param name="acceptAllChangesOnSuccess">accept all changes</param>
        /// <param name="ignoreNullValues"></param>
        /// <returns>entities in the database</returns>
        public EntityEntry<TEntity> UpdateIncludeNow(Expression<Func<TEntity, object>>[] propertyPredicates,
            bool acceptAllChangesOnSuccess, bool? ignoreNullValues = null)
        {
            return GetRepository()
                .UpdateIncludeNow(Entity, propertyPredicates, acceptAllChangesOnSuccess, ignoreNullValues);
        }

        /// <summary>
        /// update a record中的特定属性并立即提交
        /// </summary>
        /// <param name="propertyNames">property name</param>
        /// <param name="ignoreNullValues"></param>
        /// <returns>entities in the database</returns>
        public EntityEntry<TEntity> UpdateIncludeNow(IEnumerable<string> propertyNames, bool? ignoreNullValues = null)
        {
            return GetRepository().UpdateIncludeNow(Entity, propertyNames, ignoreNullValues);
        }

        /// <summary>
        /// update a record中的特定属性并立即提交
        /// </summary>
        /// <param name="propertyNames">property name</param>
        /// <param name="acceptAllChangesOnSuccess">accept all changes</param>
        /// <param name="ignoreNullValues"></param>
        /// <returns>entities in the database</returns>
        public EntityEntry<TEntity> UpdateIncludeNow(IEnumerable<string> propertyNames, bool acceptAllChangesOnSuccess,
            bool? ignoreNullValues = null)
        {
            return GetRepository().UpdateIncludeNow(Entity, propertyNames, acceptAllChangesOnSuccess, ignoreNullValues);
        }

        /// <summary>
        /// update a record中的特定属性并立即提交
        /// </summary>
        /// <param name="propertyPredicates">property expression</param>
        /// <param name="ignoreNullValues"></param>
        /// <returns>entities in the database</returns>
        public EntityEntry<TEntity> UpdateIncludeNow(IEnumerable<Expression<Func<TEntity, object>>> propertyPredicates,
            bool? ignoreNullValues = null)
        {
            return GetRepository().UpdateIncludeNow(Entity, propertyPredicates, ignoreNullValues);
        }

        /// <summary>
        /// update a record中的特定属性并立即提交
        /// </summary>
        /// <param name="propertyPredicates">property expression</param>
        /// <param name="acceptAllChangesOnSuccess">accept all changes</param>
        /// <param name="ignoreNullValues"></param>
        /// <returns>entities in the database</returns>
        public EntityEntry<TEntity> UpdateIncludeNow(IEnumerable<Expression<Func<TEntity, object>>> propertyPredicates,
            bool acceptAllChangesOnSuccess, bool? ignoreNullValues = null)
        {
            return GetRepository()
                .UpdateIncludeNow(Entity, propertyPredicates, acceptAllChangesOnSuccess, ignoreNullValues);
        }

        /// <summary>
        /// update a record中的特定属性并立即提交
        /// </summary>
        /// <param name="propertyNames">property name</param>
        /// <param name="ignoreNullValues"></param>
        /// <param name="cancellationToken">Cancel async token</param>
        /// <returns>entities in the database</returns>
        public Task<EntityEntry<TEntity>> UpdateIncludeNowAsync(string[] propertyNames, bool? ignoreNullValues = null,
            CancellationToken cancellationToken = default)
        {
            return GetRepository().UpdateIncludeNowAsync(Entity, propertyNames, ignoreNullValues, cancellationToken);
        }

        /// <summary>
        /// update a record中的特定属性并立即提交
        /// </summary>
        /// <param name="propertyNames">property name</param>
        /// <param name="acceptAllChangesOnSuccess">accept all changes</param>
        /// <param name="ignoreNullValues"></param>
        /// <param name="cancellationToken">Cancel async token</param>
        /// <returns>entities in the database</returns>
        public Task<EntityEntry<TEntity>> UpdateIncludeNowAsync(string[] propertyNames, bool acceptAllChangesOnSuccess,
            bool? ignoreNullValues = null, CancellationToken cancellationToken = default)
        {
            return GetRepository().UpdateIncludeNowAsync(Entity, propertyNames, acceptAllChangesOnSuccess,
                ignoreNullValues, cancellationToken);
        }

        /// <summary>
        /// update a record中的特定属性并立即提交
        /// </summary>
        /// <param name="propertyPredicates">property expression</param>
        /// <param name="ignoreNullValues"></param>
        /// <param name="cancellationToken">Cancel async token</param>
        /// <returns>entities in the database</returns>
        public Task<EntityEntry<TEntity>> UpdateIncludeNowAsync(Expression<Func<TEntity, object>>[] propertyPredicates,
            bool? ignoreNullValues = null, CancellationToken cancellationToken = default)
        {
            return GetRepository()
                .UpdateIncludeNowAsync(Entity, propertyPredicates, ignoreNullValues, cancellationToken);
        }

        /// <summary>
        /// update a record中的特定属性并立即提交
        /// </summary>
        /// <param name="propertyPredicates">property expression</param>
        /// <param name="acceptAllChangesOnSuccess">accept all changes</param>
        /// <param name="ignoreNullValues"></param>
        /// <param name="cancellationToken">Cancel async token</param>
        /// <returns>entities in the database</returns>
        public Task<EntityEntry<TEntity>> UpdateIncludeNowAsync(Expression<Func<TEntity, object>>[] propertyPredicates,
            bool acceptAllChangesOnSuccess, bool? ignoreNullValues = null,
            CancellationToken cancellationToken = default)
        {
            return GetRepository().UpdateIncludeNowAsync(Entity, propertyPredicates, acceptAllChangesOnSuccess,
                ignoreNullValues, cancellationToken);
        }

        /// <summary>
        /// update a record中的特定属性并立即提交
        /// </summary>
        /// <param name="propertyNames">property name</param>
        /// <param name="ignoreNullValues"></param>
        /// <param name="cancellationToken">Cancel async token</param>
        /// <returns>entities in the database</returns>
        public Task<EntityEntry<TEntity>> UpdateIncludeNowAsync(IEnumerable<string> propertyNames,
            bool? ignoreNullValues = null, CancellationToken cancellationToken = default)
        {
            return GetRepository().UpdateIncludeNowAsync(Entity, propertyNames, ignoreNullValues, cancellationToken);
        }

        /// <summary>
        /// update a record中的特定属性并立即提交
        /// </summary>
        /// <param name="propertyNames">property name</param>
        /// <param name="acceptAllChangesOnSuccess">accept all changes</param>
        /// <param name="ignoreNullValues"></param>
        /// <param name="cancellationToken">Cancel async token</param>
        /// <returns>entities in the database</returns>
        public Task<EntityEntry<TEntity>> UpdateIncludeNowAsync(IEnumerable<string> propertyNames,
            bool acceptAllChangesOnSuccess, bool? ignoreNullValues = null,
            CancellationToken cancellationToken = default)
        {
            return GetRepository().UpdateIncludeNowAsync(Entity, propertyNames, acceptAllChangesOnSuccess,
                ignoreNullValues, cancellationToken);
        }

        /// <summary>
        /// update a record中的特定属性并立即提交
        /// </summary>
        /// <param name="propertyPredicates">property expression</param>
        /// <param name="ignoreNullValues"></param>
        /// <param name="cancellationToken">Cancel async token</param>
        /// <returns>entities in the database</returns>
        public Task<EntityEntry<TEntity>> UpdateIncludeNowAsync(
            IEnumerable<Expression<Func<TEntity, object>>> propertyPredicates, bool? ignoreNullValues = null,
            CancellationToken cancellationToken = default)
        {
            return GetRepository()
                .UpdateIncludeNowAsync(Entity, propertyPredicates, ignoreNullValues, cancellationToken);
        }

        /// <summary>
        /// update a record中的特定属性并立即提交
        /// </summary>
        /// <param name="propertyPredicates">property expression</param>
        /// <param name="acceptAllChangesOnSuccess">accept all changes</param>
        /// <param name="ignoreNullValues"></param>
        /// <param name="cancellationToken">Cancel async token</param>
        /// <returns>entities in the database</returns>
        public Task<EntityEntry<TEntity>> UpdateIncludeNowAsync(
            IEnumerable<Expression<Func<TEntity, object>>> propertyPredicates, bool acceptAllChangesOnSuccess,
            bool? ignoreNullValues = null, CancellationToken cancellationToken = default)
        {
            return GetRepository().UpdateIncludeNowAsync(Entity, propertyPredicates, acceptAllChangesOnSuccess,
                ignoreNullValues, cancellationToken);
        }

        /// <summary>
        /// update a record中特定属性
        /// </summary>
        /// <param name="propertyNames">property name</param>
        /// <param name="ignoreNullValues"></param>
        /// <returns>Entity in the proxy</returns>
        public EntityEntry<TEntity> UpdateExclude(string[] propertyNames, bool? ignoreNullValues = null)
        {
            return GetRepository().UpdateExclude(Entity, propertyNames, ignoreNullValues);
        }

        /// <summary>
        /// update a record中特定属性
        /// </summary>
        /// <param name="propertyPredicates">property expression</param>
        /// <param name="ignoreNullValues"></param>
        /// <returns>Entity in the proxy</returns>
        public EntityEntry<TEntity> UpdateExclude(Expression<Func<TEntity, object>>[] propertyPredicates,
            bool? ignoreNullValues = null)
        {
            return GetRepository().UpdateExclude(Entity, propertyPredicates, ignoreNullValues);
        }

        /// <summary>
        /// update a record中特定属性
        /// </summary>
        /// <param name="propertyNames">property name</param>
        /// <param name="ignoreNullValues"></param>
        /// <returns>Entity in the proxy</returns>
        public EntityEntry<TEntity> UpdateExclude(IEnumerable<string> propertyNames, bool? ignoreNullValues = null)
        {
            return GetRepository().UpdateExclude(Entity, propertyNames, ignoreNullValues);
        }

        /// <summary>
        /// update a record中特定属性
        /// </summary>
        /// <param name="propertyPredicates">property expression</param>
        /// <param name="ignoreNullValues"></param>
        /// <returns>Entity in the proxy</returns>
        public EntityEntry<TEntity> UpdateExclude(IEnumerable<Expression<Func<TEntity, object>>> propertyPredicates,
            bool? ignoreNullValues = null)
        {
            return GetRepository().UpdateExclude(Entity, propertyPredicates, ignoreNullValues);
        }

        /// <summary>
        /// update a record并排除属性
        /// </summary>
        /// <param name="propertyNames">property name</param>
        /// <param name="ignoreNullValues"></param>
        /// <returns>Entity in the proxy</returns>
        public Task<EntityEntry<TEntity>> UpdateExcludeAsync(string[] propertyNames, bool? ignoreNullValues = null)
        {
            return GetRepository().UpdateExcludeAsync(Entity, propertyNames, ignoreNullValues);
        }

        /// <summary>
        /// update a record并排除属性
        /// </summary>
        /// <param name="propertyPredicates">property expression</param>
        /// <param name="ignoreNullValues"></param>
        /// <returns>Entity in the proxy</returns>
        public Task<EntityEntry<TEntity>> UpdateExcludeAsync(Expression<Func<TEntity, object>>[] propertyPredicates,
            bool? ignoreNullValues = null)
        {
            return GetRepository().UpdateExcludeAsync(Entity, propertyPredicates, ignoreNullValues);
        }

        /// <summary>
        /// update a record并排除属性
        /// </summary>
        /// <param name="propertyNames">property expression</param>
        /// <param name="ignoreNullValues"></param>
        /// <returns>Entity in the proxy</returns>
        public Task<EntityEntry<TEntity>> UpdateExcludeAsync(IEnumerable<string> propertyNames,
            bool? ignoreNullValues = null)
        {
            return GetRepository().UpdateExcludeAsync(Entity, propertyNames, ignoreNullValues);
        }

        /// <summary>
        /// update a record并排除属性
        /// </summary>
        /// <param name="propertyPredicates">property expression</param>
        /// <param name="ignoreNullValues"></param>
        /// <returns>Entity in the proxy</returns>
        public Task<EntityEntry<TEntity>> UpdateExcludeAsync(
            IEnumerable<Expression<Func<TEntity, object>>> propertyPredicates, bool? ignoreNullValues = null)
        {
            return GetRepository().UpdateExcludeAsync(Entity, propertyPredicates, ignoreNullValues);
        }

        /// <summary>
        /// update a record并排除属性并立即提交
        /// </summary>
        /// <param name="propertyNames">property name</param>
        /// <param name="ignoreNullValues"></param>
        /// <returns>entities in the database</returns>
        public EntityEntry<TEntity> UpdateExcludeNow(string[] propertyNames, bool? ignoreNullValues = null)
        {
            return GetRepository().UpdateExcludeNow(Entity, propertyNames, ignoreNullValues);
        }

        /// <summary>
        /// update a record并排除属性并立即提交
        /// </summary>
        /// <param name="propertyNames">property name</param>
        /// <param name="acceptAllChangesOnSuccess">accept all changes</param>
        /// <param name="ignoreNullValues"></param>
        /// <returns>entities in the database</returns>
        public EntityEntry<TEntity> UpdateExcludeNow(string[] propertyNames, bool acceptAllChangesOnSuccess,
            bool? ignoreNullValues = null)
        {
            return GetRepository().UpdateExcludeNow(Entity, propertyNames, acceptAllChangesOnSuccess, ignoreNullValues);
        }

        /// <summary>
        /// update a record并排除属性并立即提交
        /// </summary>
        /// <param name="propertyPredicates">property expression</param>
        /// <param name="ignoreNullValues"></param>
        /// <returns>entities in the database</returns>
        public EntityEntry<TEntity> UpdateExcludeNow(Expression<Func<TEntity, object>>[] propertyPredicates,
            bool? ignoreNullValues = null)
        {
            return GetRepository().UpdateExcludeNow(Entity, propertyPredicates, ignoreNullValues);
        }

        /// <summary>
        /// update a record并排除属性并立即提交
        /// </summary>
        /// <param name="propertyPredicates">property expression</param>
        /// <param name="acceptAllChangesOnSuccess">accept all changes</param>
        /// <param name="ignoreNullValues"></param>
        /// <returns>entities in the database</returns>
        public EntityEntry<TEntity> UpdateExcludeNow(Expression<Func<TEntity, object>>[] propertyPredicates,
            bool acceptAllChangesOnSuccess, bool? ignoreNullValues = null)
        {
            return GetRepository()
                .UpdateExcludeNow(Entity, propertyPredicates, acceptAllChangesOnSuccess, ignoreNullValues);
        }

        /// <summary>
        /// update a record并排除属性并立即提交
        /// </summary>
        /// <param name="propertyNames">property name</param>
        /// <param name="ignoreNullValues"></param>
        /// <returns>entities in the database</returns>
        public EntityEntry<TEntity> UpdateExcludeNow(IEnumerable<string> propertyNames, bool? ignoreNullValues = null)
        {
            return GetRepository().UpdateExcludeNow(Entity, propertyNames, ignoreNullValues);
        }

        /// <summary>
        /// update a record并排除属性并立即提交
        /// </summary>
        /// <param name="propertyNames">property name</param>
        /// <param name="acceptAllChangesOnSuccess">accept all changes</param>
        /// <param name="ignoreNullValues"></param>
        /// <returns>entities in the database</returns>
        public EntityEntry<TEntity> UpdateExcludeNow(IEnumerable<string> propertyNames, bool acceptAllChangesOnSuccess,
            bool? ignoreNullValues = null)
        {
            return GetRepository().UpdateExcludeNow(Entity, propertyNames, acceptAllChangesOnSuccess, ignoreNullValues);
        }

        /// <summary>
        /// update a record并排除属性并立即提交
        /// </summary>
        /// <param name="propertyPredicates">property expression</param>
        /// <param name="ignoreNullValues"></param>
        /// <returns>entities in the database</returns>
        public EntityEntry<TEntity> UpdateExcludeNow(IEnumerable<Expression<Func<TEntity, object>>> propertyPredicates,
            bool? ignoreNullValues = null)
        {
            return GetRepository().UpdateExcludeNow(Entity, propertyPredicates, ignoreNullValues);
        }

        /// <summary>
        /// update a record并排除属性并立即提交
        /// </summary>
        /// <param name="propertyPredicates">property expression</param>
        /// <param name="acceptAllChangesOnSuccess">accept all changes</param>
        /// <param name="ignoreNullValues"></param>
        /// <returns>entities in the database</returns>
        public EntityEntry<TEntity> UpdateExcludeNow(IEnumerable<Expression<Func<TEntity, object>>> propertyPredicates,
            bool acceptAllChangesOnSuccess, bool? ignoreNullValues = null)
        {
            return GetRepository()
                .UpdateExcludeNow(Entity, propertyPredicates, acceptAllChangesOnSuccess, ignoreNullValues);
        }

        /// <summary>
        /// update a record并排除属性并立即提交
        /// </summary>
        /// <param name="propertyNames">property name</param>
        /// <param name="ignoreNullValues"></param>
        /// <param name="cancellationToken">Cancel async token</param>
        /// <returns>entities in the database</returns>
        public Task<EntityEntry<TEntity>> UpdateExcludeNowAsync(string[] propertyNames, bool? ignoreNullValues = null,
            CancellationToken cancellationToken = default)
        {
            return GetRepository().UpdateExcludeNowAsync(Entity, propertyNames, ignoreNullValues, cancellationToken);
        }

        /// <summary>
        /// update a record并排除属性并立即提交
        /// </summary>
        /// <param name="propertyNames">property name</param>
        /// <param name="acceptAllChangesOnSuccess">accept all changes</param>
        /// <param name="ignoreNullValues"></param>
        /// <param name="cancellationToken">Cancel async token</param>
        /// <returns>entities in the database</returns>
        public Task<EntityEntry<TEntity>> UpdateExcludeNowAsync(string[] propertyNames, bool acceptAllChangesOnSuccess,
            bool? ignoreNullValues = null, CancellationToken cancellationToken = default)
        {
            return GetRepository().UpdateExcludeNowAsync(Entity, propertyNames, acceptAllChangesOnSuccess,
                ignoreNullValues, cancellationToken);
        }

        /// <summary>
        /// update a record并排除属性并立即提交
        /// </summary>
        /// <param name="propertyPredicates">property expression</param>
        /// <param name="ignoreNullValues"></param>
        /// <param name="cancellationToken">Cancel async token</param>
        /// <returns>entities in the database</returns>
        public Task<EntityEntry<TEntity>> UpdateExcludeNowAsync(Expression<Func<TEntity, object>>[] propertyPredicates,
            bool? ignoreNullValues = null, CancellationToken cancellationToken = default)
        {
            return GetRepository()
                .UpdateExcludeNowAsync(Entity, propertyPredicates, ignoreNullValues, cancellationToken);
        }

        /// <summary>
        /// update a record并排除属性并立即提交
        /// </summary>
        /// <param name="propertyPredicates">property expression</param>
        /// <param name="acceptAllChangesOnSuccess">accept all changes</param>
        /// <param name="ignoreNullValues"></param>
        /// <param name="cancellationToken">Cancel async token</param>
        /// <returns>entities in the database</returns>
        public Task<EntityEntry<TEntity>> UpdateExcludeNowAsync(Expression<Func<TEntity, object>>[] propertyPredicates,
            bool acceptAllChangesOnSuccess, bool? ignoreNullValues = null,
            CancellationToken cancellationToken = default)
        {
            return GetRepository().UpdateExcludeNowAsync(Entity, propertyPredicates, acceptAllChangesOnSuccess,
                ignoreNullValues, cancellationToken);
        }

        /// <summary>
        /// update a record并排除属性并立即提交
        /// </summary>
        /// <param name="propertyNames">property name</param>
        /// <param name="ignoreNullValues"></param>
        /// <param name="cancellationToken">Cancel async token</param>
        /// <returns>entities in the database</returns>
        public Task<EntityEntry<TEntity>> UpdateExcludeNowAsync(IEnumerable<string> propertyNames,
            bool? ignoreNullValues = null, CancellationToken cancellationToken = default)
        {
            return GetRepository().UpdateExcludeNowAsync(Entity, propertyNames, ignoreNullValues, cancellationToken);
        }

        /// <summary>
        /// update a record并排除属性并立即提交
        /// </summary>
        /// <param name="propertyNames">property name</param>
        /// <param name="acceptAllChangesOnSuccess">accept all changes</param>
        /// <param name="ignoreNullValues"></param>
        /// <param name="cancellationToken">Cancel async token</param>
        /// <returns>entities in the database</returns>
        public Task<EntityEntry<TEntity>> UpdateExcludeNowAsync(IEnumerable<string> propertyNames,
            bool acceptAllChangesOnSuccess, bool? ignoreNullValues = null,
            CancellationToken cancellationToken = default)
        {
            return GetRepository().UpdateExcludeNowAsync(Entity, propertyNames, acceptAllChangesOnSuccess,
                ignoreNullValues, cancellationToken);
        }

        /// <summary>
        /// update a record并排除属性并立即提交
        /// </summary>
        /// <param name="propertyPredicates">property expression</param>
        /// <param name="ignoreNullValues"></param>
        /// <param name="cancellationToken">Cancel async token</param>
        /// <returns>entities in the database</returns>
        public Task<EntityEntry<TEntity>> UpdateExcludeNowAsync(
            IEnumerable<Expression<Func<TEntity, object>>> propertyPredicates, bool? ignoreNullValues = null,
            CancellationToken cancellationToken = default)
        {
            return GetRepository()
                .UpdateExcludeNowAsync(Entity, propertyPredicates, ignoreNullValues, cancellationToken);
        }

        /// <summary>
        /// update a record并排除属性并立即提交
        /// </summary>
        /// <param name="propertyPredicates">property expression</param>
        /// <param name="acceptAllChangesOnSuccess">accept all changes</param>
        /// <param name="ignoreNullValues"></param>
        /// <param name="cancellationToken">Cancel async token</param>
        /// <returns>entities in the database</returns>
        public Task<EntityEntry<TEntity>> UpdateExcludeNowAsync(
            IEnumerable<Expression<Func<TEntity, object>>> propertyPredicates, bool acceptAllChangesOnSuccess,
            bool? ignoreNullValues = null, CancellationToken cancellationToken = default)
        {
            return GetRepository().UpdateExcludeNowAsync(Entity, propertyPredicates, acceptAllChangesOnSuccess,
                ignoreNullValues, cancellationToken);
        }

        /// <summary>
        /// delete a record
        /// </summary>
        /// <returns>Entity in the proxy</returns>
        public EntityEntry<TEntity> Delete()
        {
            return GetRepository().Delete(Entity);
        }

        /// <summary>
        /// delete a record
        /// </summary>
        /// <returns>Entity in the proxy</returns>
        public Task<EntityEntry<TEntity>> DeleteAsync()
        {
            return GetRepository().DeleteAsync(Entity);
        }

        /// <summary>
        /// delete a record并立即提交
        /// </summary>
        /// <returns>Entity in the proxy</returns>
        public EntityEntry<TEntity> DeleteNow()
        {
            return GetRepository().DeleteNow(Entity);
        }

        /// <summary>
        /// delete a record并立即提交
        /// </summary>
        /// <param name="acceptAllChangesOnSuccess">accept all changes</param>
        /// <returns></returns>
        public EntityEntry<TEntity> DeleteNow(bool acceptAllChangesOnSuccess)
        {
            return GetRepository().DeleteNow(Entity, acceptAllChangesOnSuccess);
        }

        /// <summary>
        /// delete a record并立即提交
        /// </summary>
        /// <param name="cancellationToken">Cancel async token</param>
        /// <returns>Entity in the proxy</returns>
        public Task<EntityEntry<TEntity>> DeleteNowAsync(CancellationToken cancellationToken = default)
        {
            return GetRepository().DeleteNowAsync(Entity, cancellationToken);
        }

        /// <summary>
        /// delete a record并立即提交
        /// </summary>
        /// <param name="acceptAllChangesOnSuccess">accept all changes</param>
        /// <param name="cancellationToken">Cancel async token</param>
        /// <returns>Entity in the proxy</returns>
        public Task<EntityEntry<TEntity>> DeleteNowAsync(bool acceptAllChangesOnSuccess,
            CancellationToken cancellationToken = default)
        {
            return GetRepository().DeleteNowAsync(Entity, acceptAllChangesOnSuccess, cancellationToken);
        }

        /// <summary>
        /// Get physical warehousing
        /// </summary>
        /// <returns></returns>
        private IPrivateRepository<TEntity> GetRepository()
        {
            return EngineContext.Current.Resolve(
                    typeof(IRepository<,>).MakeGenericType(typeof(TEntity), DbContextLocator)) as
                IPrivateRepository<TEntity>;
        }
    }
}