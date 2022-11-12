using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Silky.EntityFrameworkCore.Entities;
using Silky.EntityFrameworkCore.Locators;

namespace Silky.EntityFrameworkCore.Repositories
{
    /// <summary>
    /// Updatable repository interface
    /// </summary>
    /// <typeparam name="TEntity">entity type</typeparam>
    public partial interface IUpdateableRepository<TEntity> : IUpdateableRepository<TEntity, MasterDbContextLocator>
        where TEntity : class, IPrivateEntity, new()
    {
    }

    /// <summary>
    /// Updatable repository interface
    /// </summary>
    /// <typeparam name="TEntity">entity type</typeparam>
    /// <typeparam name="TDbContextLocator">database context locator</typeparam>
    public partial interface IUpdateableRepository<TEntity, TDbContextLocator> : IPrivateUpdateableRepository<TEntity>
        where TEntity : class, IPrivateEntity, new()
        where TDbContextLocator : class, IDbContextLocator
    {
    }

    /// <summary>
    /// Updatable repository interface
    /// </summary>
    /// <typeparam name="TEntity">entity type</typeparam>
    public partial interface IPrivateUpdateableRepository<TEntity> : IPrivateRootRepository
        where TEntity : class, IPrivateEntity, new()
    {
        /// <summary>
        /// update a record
        /// </summary>
        /// <param name="entity">entity</param>
        /// <param name="ignoreNullValues"></param>
        /// <returns>代理中的entity</returns>
        EntityEntry<TEntity> Update(TEntity entity, bool? ignoreNullValues = null);

        /// <summary>
        /// Update multiple records
        /// </summary>
        /// <param name="entities">多个entity</param>
        void Update(params TEntity[] entities);

        /// <summary>
        /// Update multiple records
        /// </summary>
        /// <param name="entities">多个entity</param>
        void Update(IEnumerable<TEntity> entities);

        /// <summary>
        /// update a record
        /// </summary>
        /// <param name="entity">entity</param>
        /// <param name="ignoreNullValues"></param>
        /// <returns>代理中的entity</returns>
        Task<EntityEntry<TEntity>> UpdateAsync(TEntity entity, bool? ignoreNullValues = null);

        /// <summary>
        /// Update multiple records
        /// </summary>
        /// <param name="entities">多个entity</param>
        /// <returns>Task</returns>
        Task UpdateAsync(params TEntity[] entities);

        /// <summary>
        /// Update multiple records
        /// </summary>
        /// <param name="entities">多个entity</param>
        /// <returns>Task</returns>
        Task UpdateAsync(IEnumerable<TEntity> entities);

        /// <summary>
        /// update a record并立即提交
        /// </summary>
        /// <param name="entity">entity</param>
        /// <param name="ignoreNullValues"></param>
        /// <returns>数据库中的entity</returns>
        EntityEntry<TEntity> UpdateNow(TEntity entity, bool? ignoreNullValues = null);

        /// <summary>
        /// update a record并立即提交
        /// </summary>
        /// <param name="entity">entity</param>
        /// <param name="acceptAllChangesOnSuccess">accept all changes</param>
        /// <param name="ignoreNullValues"></param>
        /// <returns>数据库中的entity</returns>
        EntityEntry<TEntity> UpdateNow(TEntity entity, bool acceptAllChangesOnSuccess, bool? ignoreNullValues = null);

        /// <summary>
        /// Update multiple records并立即提交
        /// </summary>
        /// <param name="entities">多个entity</param>
        void UpdateNow(params TEntity[] entities);

        /// <summary>
        /// Update multiple records并立即提交
        /// </summary>
        /// <param name="entities">多个entity</param>
        /// <param name="acceptAllChangesOnSuccess">accept all changes</param>
        void UpdateNow(TEntity[] entities, bool acceptAllChangesOnSuccess);

        /// <summary>
        /// Update multiple records并立即提交
        /// </summary>
        /// <param name="entities">多个entity</param>
        void UpdateNow(IEnumerable<TEntity> entities);

        /// <summary>
        /// Update multiple records并立即提交
        /// </summary>
        /// <param name="entities">多个entity</param>
        /// <param name="acceptAllChangesOnSuccess">accept all changes</param>
        void UpdateNow(IEnumerable<TEntity> entities, bool acceptAllChangesOnSuccess);

        /// <summary>
        /// update a record并立即提交
        /// </summary>
        /// <param name="entity">entity</param>
        /// <param name="cancellationToken">Cancel async token</param>
        /// <param name="ignoreNullValues"></param>
        /// <returns>数据库中的entity</returns>
        Task<EntityEntry<TEntity>> UpdateNowAsync(TEntity entity, bool? ignoreNullValues = null,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// update a record并立即提交
        /// </summary>
        /// <param name="entity">entity</param>
        /// <param name="acceptAllChangesOnSuccess">accept all changes</param>
        /// <param name="ignoreNullValues"></param>
        /// <param name="cancellationToken">Cancel async token</param>
        /// <returns>数据库中的entity</returns>
        Task<EntityEntry<TEntity>> UpdateNowAsync(TEntity entity, bool acceptAllChangesOnSuccess,
            bool? ignoreNullValues = null, CancellationToken cancellationToken = default);

        /// <summary>
        /// Update multiple records并立即提交
        /// </summary>
        /// <param name="entities">多个entity</param>
        /// <returns>Task</returns>
        Task UpdateNowAsync(params TEntity[] entities);

        /// <summary>
        /// Update multiple records并立即提交
        /// </summary>
        /// <param name="entities">多个entity</param>
        /// <param name="cancellationToken">Cancel async token</param>
        /// <returns></returns>
        Task UpdateNowAsync(TEntity[] entities, CancellationToken cancellationToken = default);

        /// <summary>
        /// Update multiple records并立即提交
        /// </summary>
        /// <param name="entities">多个entity</param>
        /// <param name="acceptAllChangesOnSuccess">accept all changes</param>
        /// <param name="cancellationToken">Cancel async token</param>
        /// <returns>Task</returns>
        Task UpdateNowAsync(TEntity[] entities, bool acceptAllChangesOnSuccess,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Update multiple records并立即提交
        /// </summary>
        /// <param name="entities">多个entity</param>
        /// <param name="cancellationToken">Cancel async token</param>
        /// <returns>Task</returns>
        Task UpdateNowAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default);

        /// <summary>
        /// Update multiple records并立即提交
        /// </summary>
        /// <param name="entities">多个entity</param>
        /// <param name="acceptAllChangesOnSuccess">accept all changes</param>
        /// <param name="cancellationToken">Cancel async token</param>
        /// <returns>Task</returns>
        Task UpdateNowAsync(IEnumerable<TEntity> entities, bool acceptAllChangesOnSuccess,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// update a record中特定属性
        /// </summary>
        /// <param name="entity">entity</param>
        /// <param name="propertyNames">property name</param>
        /// <param name="ignoreNullValues"></param>
        /// <returns>代理中的entity</returns>
        EntityEntry<TEntity> UpdateInclude(TEntity entity, string[] propertyNames, bool? ignoreNullValues = null);

        /// <summary>
        /// update a record中特定属性
        /// </summary>
        /// <param name="entity">entity</param>
        /// <param name="propertyPredicates">property expression</param>
        /// <param name="ignoreNullValues"></param>
        /// <returns>代理中的entity</returns>
        EntityEntry<TEntity> UpdateInclude(TEntity entity, Expression<Func<TEntity, object>>[] propertyPredicates,
            bool? ignoreNullValues = null);

        /// <summary>
        /// update a record中特定属性
        /// </summary>
        /// <param name="entity">entity</param>
        /// <param name="propertyNames">property name</param>
        /// <param name="ignoreNullValues"></param>
        /// <returns>代理中的entity</returns>
        EntityEntry<TEntity> UpdateInclude(TEntity entity, IEnumerable<string> propertyNames,
            bool? ignoreNullValues = null);

        /// <summary>
        /// update a record中特定属性
        /// </summary>
        /// <param name="entity">entity</param>
        /// <param name="propertyPredicates">property expression</param>
        /// <param name="ignoreNullValues"></param>
        /// <returns>代理中的entity</returns>
        EntityEntry<TEntity> UpdateInclude(TEntity entity,
            IEnumerable<Expression<Func<TEntity, object>>> propertyPredicates, bool? ignoreNullValues = null);

        /// <summary>
        /// update a record中的特定属性
        /// </summary>
        /// <param name="entity">entity</param>
        /// <param name="propertyNames">property name</param>
        /// <param name="ignoreNullValues"></param>
        /// <returns>代理中的entity</returns>
        Task<EntityEntry<TEntity>> UpdateIncludeAsync(TEntity entity, string[] propertyNames,
            bool? ignoreNullValues = null);

        /// <summary>
        /// update a record中的特定属性
        /// </summary>
        /// <param name="entity">entity</param>
        /// <param name="propertyPredicates">property expression</param>
        /// <param name="ignoreNullValues"></param>
        /// <returns>代理中的entity</returns>
        Task<EntityEntry<TEntity>> UpdateIncludeAsync(TEntity entity,
            Expression<Func<TEntity, object>>[] propertyPredicates, bool? ignoreNullValues = null);

        /// <summary>
        /// update a record中的特定属性
        /// </summary>
        /// <param name="entity">entity</param>
        /// <param name="propertyNames">property expression</param>
        /// <param name="ignoreNullValues"></param>
        /// <returns>代理中的entity</returns>
        Task<EntityEntry<TEntity>> UpdateIncludeAsync(TEntity entity, IEnumerable<string> propertyNames,
            bool? ignoreNullValues = null);

        /// <summary>
        /// update a record中的特定属性
        /// </summary>
        /// <param name="entity">entity</param>
        /// <param name="propertyPredicates">property expression</param>
        /// <param name="ignoreNullValues"></param>
        /// <returns>代理中的entity</returns>
        Task<EntityEntry<TEntity>> UpdateIncludeAsync(TEntity entity,
            IEnumerable<Expression<Func<TEntity, object>>> propertyPredicates, bool? ignoreNullValues = null);

        /// <summary>
        /// update a record中的特定属性并立即提交
        /// </summary>
        /// <param name="entity">entity</param>
        /// <param name="propertyNames">property name</param>
        /// <param name="ignoreNullValues"></param>
        /// <returns>数据库中的entity</returns>
        EntityEntry<TEntity> UpdateIncludeNow(TEntity entity, string[] propertyNames, bool? ignoreNullValues = null);

        /// <summary>
        /// update a record中的特定属性并立即提交
        /// </summary>
        /// <param name="entity">entity</param>
        /// <param name="propertyNames">property name</param>
        /// <param name="acceptAllChangesOnSuccess">accept all changes</param>
        /// <param name="ignoreNullValues"></param>
        /// <returns>数据库中的entity</returns>
        EntityEntry<TEntity> UpdateIncludeNow(TEntity entity, string[] propertyNames, bool acceptAllChangesOnSuccess,
            bool? ignoreNullValues = null);

        /// <summary>
        /// update a record中的特定属性并立即提交
        /// </summary>
        /// <param name="entity">entity</param>
        /// <param name="propertyPredicates">property expression</param>
        /// <param name="ignoreNullValues"></param>
        /// <returns>数据库中entity</returns>
        EntityEntry<TEntity> UpdateIncludeNow(TEntity entity, Expression<Func<TEntity, object>>[] propertyPredicates,
            bool? ignoreNullValues = null);

        /// <summary>
        /// update a record中的特定属性并立即提交
        /// </summary>
        /// <param name="entity">entity</param>
        /// <param name="propertyPredicates">property expression</param>
        /// <param name="acceptAllChangesOnSuccess">accept all changes</param>
        /// <param name="ignoreNullValues"></param>
        /// <returns>数据库中的entity</returns>
        EntityEntry<TEntity> UpdateIncludeNow(TEntity entity, Expression<Func<TEntity, object>>[] propertyPredicates,
            bool acceptAllChangesOnSuccess, bool? ignoreNullValues = null);

        /// <summary>
        /// update a record中的特定属性并立即提交
        /// </summary>
        /// <param name="entity">entity</param>
        /// <param name="propertyNames">property name</param>
        /// <param name="ignoreNullValues"></param>
        /// <returns>数据库中的entity</returns>
        EntityEntry<TEntity> UpdateIncludeNow(TEntity entity, IEnumerable<string> propertyNames,
            bool? ignoreNullValues = null);

        /// <summary>
        /// update a record中的特定属性并立即提交
        /// </summary>
        /// <param name="entity">entity</param>
        /// <param name="propertyNames">property name</param>
        /// <param name="acceptAllChangesOnSuccess">accept all changes</param>
        /// <param name="ignoreNullValues"></param>
        /// <returns>数据库中的entity</returns>
        EntityEntry<TEntity> UpdateIncludeNow(TEntity entity, IEnumerable<string> propertyNames,
            bool acceptAllChangesOnSuccess, bool? ignoreNullValues = null);

        /// <summary>
        /// update a record中的特定属性并立即提交
        /// </summary>
        /// <param name="entity">entity</param>
        /// <param name="propertyPredicates">property expression</param>
        /// <param name="ignoreNullValues"></param>
        /// <returns>数据库中的entity</returns>
        EntityEntry<TEntity> UpdateIncludeNow(TEntity entity,
            IEnumerable<Expression<Func<TEntity, object>>> propertyPredicates, bool? ignoreNullValues = null);

        /// <summary>
        /// update a record中的特定属性并立即提交
        /// </summary>
        /// <param name="entity">entity</param>
        /// <param name="propertyPredicates">property expression</param>
        /// <param name="acceptAllChangesOnSuccess">accept all changes</param>
        /// <param name="ignoreNullValues"></param>
        /// <returns>数据库中的entity</returns>
        EntityEntry<TEntity> UpdateIncludeNow(TEntity entity,
            IEnumerable<Expression<Func<TEntity, object>>> propertyPredicates, bool acceptAllChangesOnSuccess,
            bool? ignoreNullValues = null);

        /// <summary>
        /// update a record中的特定属性并立即提交
        /// </summary>
        /// <param name="entity">entity</param>
        /// <param name="propertyNames">property name</param>
        /// <param name="ignoreNullValues"></param>
        /// <param name="cancellationToken">Cancel async token</param>
        /// <returns>数据库中的entity</returns>
        Task<EntityEntry<TEntity>> UpdateIncludeNowAsync(TEntity entity, string[] propertyNames,
            bool? ignoreNullValues = null, CancellationToken cancellationToken = default);

        /// <summary>
        /// update a record中的特定属性并立即提交
        /// </summary>
        /// <param name="entity">entity</param>
        /// <param name="propertyNames">property name</param>
        /// <param name="acceptAllChangesOnSuccess">accept all changes</param>
        /// <param name="ignoreNullValues"></param>
        /// <param name="cancellationToken">Cancel async token</param>
        /// <returns>数据库中的entity</returns>
        Task<EntityEntry<TEntity>> UpdateIncludeNowAsync(TEntity entity, string[] propertyNames,
            bool acceptAllChangesOnSuccess, bool? ignoreNullValues = null,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// update a record中的特定属性并立即提交
        /// </summary>
        /// <param name="entity">entity</param>
        /// <param name="propertyPredicates">property expression</param>
        /// <param name="ignoreNullValues"></param>
        /// <param name="cancellationToken">Cancel async token</param>
        /// <returns>数据库中的entity</returns>
        Task<EntityEntry<TEntity>> UpdateIncludeNowAsync(TEntity entity,
            Expression<Func<TEntity, object>>[] propertyPredicates, bool? ignoreNullValues = null,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// update a record中的特定属性并立即提交
        /// </summary>
        /// <param name="entity">entity</param>
        /// <param name="propertyPredicates">property expression</param>
        /// <param name="acceptAllChangesOnSuccess">accept all changes</param>
        /// <param name="ignoreNullValues"></param>
        /// <param name="cancellationToken">Cancel async token</param>
        /// <returns>数据库中的entity</returns>
        Task<EntityEntry<TEntity>> UpdateIncludeNowAsync(TEntity entity,
            Expression<Func<TEntity, object>>[] propertyPredicates, bool acceptAllChangesOnSuccess,
            bool? ignoreNullValues = null, CancellationToken cancellationToken = default);

        /// <summary>
        /// update a record中的特定属性并立即提交
        /// </summary>
        /// <param name="entity">entity</param>
        /// <param name="propertyNames">property name</param>
        /// <param name="ignoreNullValues"></param>
        /// <param name="cancellationToken">Cancel async token</param>
        /// <returns>数据库中的entity</returns>
        Task<EntityEntry<TEntity>> UpdateIncludeNowAsync(TEntity entity, IEnumerable<string> propertyNames,
            bool? ignoreNullValues = null, CancellationToken cancellationToken = default);

        /// <summary>
        /// update a record中的特定属性并立即提交
        /// </summary>
        /// <param name="entity">entity</param>
        /// <param name="propertyNames">property name</param>
        /// <param name="acceptAllChangesOnSuccess">accept all changes</param>
        /// <param name="ignoreNullValues"></param>
        /// <param name="cancellationToken">Cancel async token</param>
        /// <returns>数据库中的entity</returns>
        Task<EntityEntry<TEntity>> UpdateIncludeNowAsync(TEntity entity, IEnumerable<string> propertyNames,
            bool acceptAllChangesOnSuccess, bool? ignoreNullValues = null,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// update a record中的特定属性并立即提交
        /// </summary>
        /// <param name="entity">entity</param>
        /// <param name="propertyPredicates">property expression</param>
        /// <param name="ignoreNullValues"></param>
        /// <param name="cancellationToken">Cancel async token</param>
        /// <returns>数据库中的entity</returns>
        Task<EntityEntry<TEntity>> UpdateIncludeNowAsync(TEntity entity,
            IEnumerable<Expression<Func<TEntity, object>>> propertyPredicates, bool? ignoreNullValues = null,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// update a record中的特定属性并立即提交
        /// </summary>
        /// <param name="entity">entity</param>
        /// <param name="propertyPredicates">property expression</param>
        /// <param name="acceptAllChangesOnSuccess">accept all changes</param>
        /// <param name="ignoreNullValues"></param>
        /// <param name="cancellationToken">Cancel async token</param>
        /// <returns>数据库中的entity</returns>
        Task<EntityEntry<TEntity>> UpdateIncludeNowAsync(TEntity entity,
            IEnumerable<Expression<Func<TEntity, object>>> propertyPredicates, bool acceptAllChangesOnSuccess,
            bool? ignoreNullValues = null, CancellationToken cancellationToken = default);

        /// <summary>
        /// update a record并排除属性
        /// </summary>
        /// <param name="entity">entity</param>
        /// <param name="propertyNames">property name</param>
        /// <param name="ignoreNullValues"></param>
        /// <returns>代理中的entity</returns>
        EntityEntry<TEntity> UpdateExclude(TEntity entity, string[] propertyNames, bool? ignoreNullValues = null);

        /// <summary>
        /// update a record并排除属性
        /// </summary>
        /// <param name="entity">entity</param>
        /// <param name="propertyPredicates">property expression</param>
        /// <param name="ignoreNullValues"></param>
        /// <returns>代理中的entity</returns>
        EntityEntry<TEntity> UpdateExclude(TEntity entity, Expression<Func<TEntity, object>>[] propertyPredicates,
            bool? ignoreNullValues = null);

        /// <summary>
        /// update a record并排除属性
        /// </summary>
        /// <param name="entity">entity</param>
        /// <param name="propertyNames">property name</param>
        /// <param name="ignoreNullValues"></param>
        /// <returns>代理中的entity</returns>
        EntityEntry<TEntity> UpdateExclude(TEntity entity, IEnumerable<string> propertyNames,
            bool? ignoreNullValues = null);

        /// <summary>
        /// update a record并排除属性
        /// </summary>
        /// <param name="entity">entity</param>
        /// <param name="propertyPredicates">property expression</param>
        /// <param name="ignoreNullValues"></param>
        /// <returns>代理中的entity</returns>
        EntityEntry<TEntity> UpdateExclude(TEntity entity,
            IEnumerable<Expression<Func<TEntity, object>>> propertyPredicates, bool? ignoreNullValues = null);

        /// <summary>
        /// update a record并排除属性
        /// </summary>
        /// <param name="entity">entity</param>
        /// <param name="propertyNames">property name</param>
        /// <param name="ignoreNullValues"></param>
        /// <returns>代理中的entity</returns>
        Task<EntityEntry<TEntity>> UpdateExcludeAsync(TEntity entity, string[] propertyNames,
            bool? ignoreNullValues = null);

        /// <summary>
        /// update a record并排除属性
        /// </summary>
        /// <param name="entity">entity</param>
        /// <param name="propertyPredicates">property expression</param>
        /// <param name="ignoreNullValues"></param>
        /// <returns>代理中的entity</returns>
        Task<EntityEntry<TEntity>> UpdateExcludeAsync(TEntity entity,
            Expression<Func<TEntity, object>>[] propertyPredicates, bool? ignoreNullValues = null);

        /// <summary>
        /// update a record并排除属性
        /// </summary>
        /// <param name="entity">entity</param>
        /// <param name="propertyNames">property expression</param>
        /// <param name="ignoreNullValues"></param>
        /// <returns>代理中的entity</returns>
        Task<EntityEntry<TEntity>> UpdateExcludeAsync(TEntity entity, IEnumerable<string> propertyNames,
            bool? ignoreNullValues = null);

        /// <summary>
        /// update a record并排除属性
        /// </summary>
        /// <param name="entity">entity</param>
        /// <param name="propertyPredicates">property expression</param>
        /// <param name="ignoreNullValues"></param>
        /// <returns>代理中的entity</returns>
        Task<EntityEntry<TEntity>> UpdateExcludeAsync(TEntity entity,
            IEnumerable<Expression<Func<TEntity, object>>> propertyPredicates, bool? ignoreNullValues = null);

        /// <summary>
        /// update a record并排除属性并立即提交
        /// </summary>
        /// <param name="entity">entity</param>
        /// <param name="propertyNames">property name</param>
        /// <param name="ignoreNullValues"></param>
        /// <returns>数据库中的entity</returns>
        EntityEntry<TEntity> UpdateExcludeNow(TEntity entity, string[] propertyNames, bool? ignoreNullValues = null);

        /// <summary>
        /// update a record并排除属性并立即提交
        /// </summary>
        /// <param name="entity">entity</param>
        /// <param name="propertyNames">property name</param>
        /// <param name="acceptAllChangesOnSuccess">accept all changes</param>
        /// <param name="ignoreNullValues"></param>
        /// <returns>数据库中的entity</returns>
        EntityEntry<TEntity> UpdateExcludeNow(TEntity entity, string[] propertyNames, bool acceptAllChangesOnSuccess,
            bool? ignoreNullValues = null);

        /// <summary>
        /// update a record并排除属性并立即提交
        /// </summary>
        /// <param name="entity">entity</param>
        /// <param name="propertyPredicates">property expression</param>
        /// <param name="ignoreNullValues"></param>
        /// <returns>数据库中entity</returns>
        EntityEntry<TEntity> UpdateExcludeNow(TEntity entity, Expression<Func<TEntity, object>>[] propertyPredicates,
            bool? ignoreNullValues = null);

        /// <summary>
        /// update a record并排除属性并立即提交
        /// </summary>
        /// <param name="entity">entity</param>
        /// <param name="propertyPredicates">property expression</param>
        /// <param name="acceptAllChangesOnSuccess">accept all changes</param>
        /// <param name="ignoreNullValues"></param>
        /// <returns>数据库中的entity</returns>
        EntityEntry<TEntity> UpdateExcludeNow(TEntity entity, Expression<Func<TEntity, object>>[] propertyPredicates,
            bool acceptAllChangesOnSuccess, bool? ignoreNullValues = null);

        /// <summary>
        /// update a record并排除属性并立即提交
        /// </summary>
        /// <param name="entity">entity</param>
        /// <param name="propertyNames">property name</param>
        /// <param name="ignoreNullValues"></param>
        /// <returns>数据库中的entity</returns>
        EntityEntry<TEntity> UpdateExcludeNow(TEntity entity, IEnumerable<string> propertyNames,
            bool? ignoreNullValues = null);

        /// <summary>
        /// update a record并排除属性并立即提交
        /// </summary>
        /// <param name="entity">entity</param>
        /// <param name="propertyNames">property name</param>
        /// <param name="acceptAllChangesOnSuccess">accept all changes</param>
        /// <param name="ignoreNullValues"></param>
        /// <returns>数据库中的entity</returns>
        EntityEntry<TEntity> UpdateExcludeNow(TEntity entity, IEnumerable<string> propertyNames,
            bool acceptAllChangesOnSuccess, bool? ignoreNullValues = null);

        /// <summary>
        /// update a record并排除属性并立即提交
        /// </summary>
        /// <param name="entity">entity</param>
        /// <param name="propertyPredicates">property expression</param>
        /// <param name="ignoreNullValues"></param>
        /// <returns>数据库中的entity</returns>
        EntityEntry<TEntity> UpdateExcludeNow(TEntity entity,
            IEnumerable<Expression<Func<TEntity, object>>> propertyPredicates, bool? ignoreNullValues = null);

        /// <summary>
        /// update a record并排除属性并立即提交
        /// </summary>
        /// <param name="entity">entity</param>
        /// <param name="propertyPredicates">property expression</param>
        /// <param name="acceptAllChangesOnSuccess">accept all changes</param>
        /// <param name="ignoreNullValues"></param>
        /// <returns>数据库中的entity</returns>
        EntityEntry<TEntity> UpdateExcludeNow(TEntity entity,
            IEnumerable<Expression<Func<TEntity, object>>> propertyPredicates, bool acceptAllChangesOnSuccess,
            bool? ignoreNullValues = null);

        /// <summary>
        /// update a record并排除属性并立即提交
        /// </summary>
        /// <param name="entity">entity</param>
        /// <param name="propertyNames">property name</param>
        /// <param name="ignoreNullValues"></param>
        /// <param name="cancellationToken">Cancel async token</param>
        /// <returns>数据库中的entity</returns>
        Task<EntityEntry<TEntity>> UpdateExcludeNowAsync(TEntity entity, string[] propertyNames,
            bool? ignoreNullValues = null, CancellationToken cancellationToken = default);

        /// <summary>
        /// update a record并排除属性并立即提交
        /// </summary>
        /// <param name="entity">entity</param>
        /// <param name="propertyNames">property name</param>
        /// <param name="acceptAllChangesOnSuccess">accept all changes</param>
        /// <param name="ignoreNullValues"></param>
        /// <param name="cancellationToken">Cancel async token</param>
        /// <returns>数据库中的entity</returns>
        Task<EntityEntry<TEntity>> UpdateExcludeNowAsync(TEntity entity, string[] propertyNames,
            bool acceptAllChangesOnSuccess, bool? ignoreNullValues = null,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// update a record并排除属性并立即提交
        /// </summary>
        /// <param name="entity">entity</param>
        /// <param name="propertyPredicates">property expression</param>
        /// <param name="ignoreNullValues"></param>
        /// <param name="cancellationToken">Cancel async token</param>
        /// <returns>数据库中的entity</returns>
        Task<EntityEntry<TEntity>> UpdateExcludeNowAsync(TEntity entity,
            Expression<Func<TEntity, object>>[] propertyPredicates, bool? ignoreNullValues = null,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// update a record并排除属性并立即提交
        /// </summary>
        /// <param name="entity">entity</param>
        /// <param name="propertyPredicates">property expression</param>
        /// <param name="acceptAllChangesOnSuccess">accept all changes</param>
        /// <param name="ignoreNullValues"></param>
        /// <param name="cancellationToken">Cancel async token</param>
        /// <returns>数据库中的entity</returns>
        Task<EntityEntry<TEntity>> UpdateExcludeNowAsync(TEntity entity,
            Expression<Func<TEntity, object>>[] propertyPredicates, bool acceptAllChangesOnSuccess,
            bool? ignoreNullValues = null, CancellationToken cancellationToken = default);

        /// <summary>
        /// update a record并排除属性并立即提交
        /// </summary>
        /// <param name="entity">entity</param>
        /// <param name="propertyNames">property name</param>
        /// <param name="ignoreNullValues"></param>
        /// <param name="cancellationToken">Cancel async token</param>
        /// <returns>数据库中的entity</returns>
        Task<EntityEntry<TEntity>> UpdateExcludeNowAsync(TEntity entity, IEnumerable<string> propertyNames,
            bool? ignoreNullValues = null, CancellationToken cancellationToken = default);

        /// <summary>
        /// update a record并排除属性并立即提交
        /// </summary>
        /// <param name="entity">entity</param>
        /// <param name="propertyNames">property name</param>
        /// <param name="acceptAllChangesOnSuccess">accept all changes</param>
        /// <param name="ignoreNullValues"></param>
        /// <param name="cancellationToken">Cancel async token</param>
        /// <returns>数据库中的entity</returns>
        Task<EntityEntry<TEntity>> UpdateExcludeNowAsync(TEntity entity, IEnumerable<string> propertyNames,
            bool acceptAllChangesOnSuccess, bool? ignoreNullValues = null,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// update a record并排除属性并立即提交
        /// </summary>
        /// <param name="entity">entity</param>
        /// <param name="propertyPredicates">property expression</param>
        /// <param name="ignoreNullValues"></param>
        /// <param name="cancellationToken">Cancel async token</param>
        /// <returns>数据库中的entity</returns>
        Task<EntityEntry<TEntity>> UpdateExcludeNowAsync(TEntity entity,
            IEnumerable<Expression<Func<TEntity, object>>> propertyPredicates, bool? ignoreNullValues = null,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// update a record并排除属性并立即提交
        /// </summary>
        /// <param name="entity">entity</param>
        /// <param name="propertyPredicates">property expression</param>
        /// <param name="acceptAllChangesOnSuccess">accept all changes</param>
        /// <param name="ignoreNullValues"></param>
        /// <param name="cancellationToken">Cancel async token</param>
        /// <returns>数据库中的entity</returns>
        Task<EntityEntry<TEntity>> UpdateExcludeNowAsync(TEntity entity,
            IEnumerable<Expression<Func<TEntity, object>>> propertyPredicates, bool acceptAllChangesOnSuccess,
            bool? ignoreNullValues = null, CancellationToken cancellationToken = default);
    }
}