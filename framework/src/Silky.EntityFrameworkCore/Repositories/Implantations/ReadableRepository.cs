using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Silky.Core.Extensions;
using Silky.EntityFrameworkCore.Entities;
using Silky.EntityFrameworkCore.Helpers;

namespace Silky.EntityFrameworkCore.Repositories
{
    /// <summary>
    /// Writable repository subclass
    /// </summary>
    public partial class PrivateRepository<TEntity>
        where TEntity : class, IPrivateEntity, new()
    {
        /// <summary>
        /// Query a record by key
        /// </summary>
        /// <param name="key">key</param>
        /// <returns>entities in the database</returns>
        public virtual TEntity Find(object key)
        {
            var entity = FindOrDefault(key) ?? throw DbHelpers.DataNotFoundException();
            return entity;
        }

        /// <summary>
        /// 根据多个keyquery a record
        /// </summary>
        /// <param name="keyValues">多个key</param>
        /// <returns>entities in the database</returns>
        public virtual TEntity Find(params object[] keyValues)
        {
            var entity = FindOrDefault(keyValues) ?? throw DbHelpers.DataNotFoundException();
            return entity;
        }

        /// <summary>
        /// Query a record by key
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="cancellationToken">Cancel async token</param>
        /// <returns>database entity</returns>
        public virtual async Task<TEntity> FindAsync(object key, CancellationToken cancellationToken = default)
        {
            var entity = await FindOrDefaultAsync(key, cancellationToken);
            return entity ?? throw DbHelpers.DataNotFoundException();
        }

        /// <summary>
        /// 根据多个keyquery a record
        /// </summary>
        /// <param name="keyValues">多个key</param>
        /// <returns>entities in the database</returns>
        public virtual async Task<TEntity> FindAsync(params object[] keyValues)
        {
            var entity = await FindOrDefaultAsync(keyValues);
            return entity ?? throw DbHelpers.DataNotFoundException();
        }

        /// <summary>
        /// 根据多个keyquery a record
        /// </summary>
        /// <param name="keyValues">多个key</param>
        /// <param name="cancellationToken">Cancel async token</param>
        /// <returns>entities in the database</returns>
        public virtual async Task<TEntity> FindAsync(object[] keyValues, CancellationToken cancellationToken = default)
        {
            var entity = await FindOrDefaultAsync(keyValues, cancellationToken);
            return entity ?? throw DbHelpers.DataNotFoundException();
        }

        /// <summary>
        /// Query a record by key
        /// </summary>
        /// <param name="key">key</param>
        /// <returns>entities in the database</returns>
        public virtual TEntity FindOrDefault(object key)
        {
            return Entities.Find(key);
        }

        /// <summary>
        /// 根据多个keyquery a record
        /// </summary>
        /// <param name="keyValues">多个key</param>
        /// <returns>entities in the database</returns>
        public virtual TEntity FindOrDefault(params object[] keyValues)
        {
            return Entities.Find(keyValues);
        }

        /// <summary>
        /// Query a record by key
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="cancellationToken">Cancel async token</param>
        /// <returns>entities in the database</returns>
        public virtual async Task<TEntity> FindOrDefaultAsync(object key, CancellationToken cancellationToken = default)
        {
            var entity = await Entities.FindAsync(new object[] { key }, cancellationToken);
            return entity;
        }

        /// <summary>
        /// 根据多个keyquery a record
        /// </summary>
        /// <param name="keyValues">多个key</param>
        /// <returns>entities in the database</returns>
        public virtual async Task<TEntity> FindOrDefaultAsync(params object[] keyValues)
        {
            var entity = await Entities.FindAsync(keyValues);
            return entity;
        }

        /// <summary>
        /// 根据多个keyquery a record
        /// </summary>
        /// <param name="keyValues">多个key</param>
        /// <param name="cancellationToken">Cancel async token</param>
        /// <returns>entities in the database</returns>
        public virtual async Task<TEntity> FindOrDefaultAsync(object[] keyValues,
            CancellationToken cancellationToken = default)
        {
            var entity = await Entities.FindAsync(keyValues, cancellationToken);
            return entity;
        }

        /// <summary>
        /// query a record
        /// </summary>
        /// <param name="tracking">Whether to track entities</param>
        /// <returns>entities in the database</returns>
        public virtual TEntity Single(bool? tracking = null)
        {
            return AsQueryable(tracking).Single();
        }

        /// <summary>
        /// 根据expressionquery a record
        /// </summary>
        /// <param name="predicate">expression</param>
        /// <param name="tracking">Whether to track entities</param>
        /// <returns>entities in the database</returns>
        public virtual TEntity Single(Expression<Func<TEntity, bool>> predicate, bool? tracking = null)
        {
            return AsQueryable(tracking).Single(predicate);
        }

        /// <summary>
        /// query a record
        /// </summary>
        /// <param name="tracking">Whether to track entities</param>
        /// <returns>entities in the database</returns>
        public virtual TEntity SingleOrDefault(bool? tracking = null)
        {
            return AsQueryable(tracking).SingleOrDefault();
        }

        /// <summary>
        /// 根据expressionquery a record
        /// </summary>
        /// <param name="predicate">expression</param>
        /// <param name="tracking">Whether to track entities</param>
        /// <returns>entities in the database</returns>
        public virtual TEntity SingleOrDefault(Expression<Func<TEntity, bool>> predicate, bool? tracking = null)
        {
            return AsQueryable(tracking).SingleOrDefault(predicate);
        }

        /// <summary>
        /// query a record
        /// </summary>
        /// <param name="tracking">Whether to track entities</param>
        /// <param name="cancellationToken">Cancel async token</param>
        /// <returns>entities in the database</returns>
        public virtual Task<TEntity> SingleAsync(bool? tracking = null, CancellationToken cancellationToken = default)
        {
            return AsQueryable(tracking).SingleAsync(cancellationToken);
        }

        /// <summary>
        /// 根据expressionquery a record
        /// </summary>
        /// <param name="predicate">expression</param>
        /// <param name="tracking">Whether to track entities</param>
        /// <param name="cancellationToken">Cancel async token</param>
        /// <returns>entities in the database</returns>
        public virtual Task<TEntity> SingleAsync(Expression<Func<TEntity, bool>> predicate, bool? tracking = null,
            CancellationToken cancellationToken = default)
        {
            return AsQueryable(tracking).SingleAsync(predicate, cancellationToken);
        }

        /// <summary>
        /// query a record
        /// </summary>
        /// <param name="tracking">Whether to track entities</param>
        /// <returns>entities in the database</returns>
        /// <param name="cancellationToken">Asynchronous cancellation token</param>
        public virtual Task<TEntity> SingleOrDefaultAsync(bool? tracking = null,
            CancellationToken cancellationToken = default)
        {
            return AsQueryable(tracking).SingleOrDefaultAsync(cancellationToken);
        }

        /// <summary>
        /// 根据expressionquery a record
        /// </summary>
        /// <param name="predicate">expression</param>
        /// <param name="tracking">Whether to track entities</param>
        /// <param name="cancellationToken">Asynchronous cancellation token</param>
        /// <returns>entities in the database</returns>
        public virtual Task<TEntity> SingleOrDefaultAsync(Expression<Func<TEntity, bool>> predicate,
            bool? tracking = null, CancellationToken cancellationToken = default)
        {
            return AsQueryable(tracking).SingleOrDefaultAsync(predicate, cancellationToken);
        }

        /// <summary>
        /// query a record
        /// </summary>
        /// <param name="tracking">Whether to track entities</param>
        /// <returns>entities in the database</returns>
        public virtual TEntity First(bool? tracking = null)
        {
            return AsQueryable(tracking).First();
        }

        /// <summary>
        /// 根据expressionquery a record
        /// </summary>
        /// <param name="predicate">expression</param>
        /// <param name="tracking">Whether to track entities</param>
        /// <returns>entities in the database</returns>
        public virtual TEntity First(Expression<Func<TEntity, bool>> predicate, bool? tracking = null)
        {
            return AsQueryable(tracking).First(predicate);
        }

        /// <summary>
        /// query a record
        /// </summary>
        /// <param name="tracking">Whether to track entities</param>
        /// <returns>entities in the database</returns>
        public virtual TEntity FirstOrDefault(bool? tracking = null)
        {
            return AsQueryable(tracking).FirstOrDefault();
        }

        /// <summary>
        /// 根据expressionquery a record
        /// </summary>
        /// <param name="predicate">expression</param>
        /// <param name="tracking">Whether to track entities</param>
        /// <returns>entities in the database</returns>
        public virtual TEntity FirstOrDefault(Expression<Func<TEntity, bool>> predicate, bool? tracking = null)
        {
            return AsQueryable(tracking).FirstOrDefault(predicate);
        }

        /// <summary>
        /// query a record
        /// </summary>
        /// <param name="tracking">Whether to track entities</param>
        /// <param name="cancellationToken">Cancel async token</param>
        /// <returns>entities in the database</returns>
        public virtual Task<TEntity> FirstAsync(bool? tracking = null, CancellationToken cancellationToken = default)
        {
            return AsQueryable(tracking).FirstAsync(cancellationToken);
        }

        /// <summary>
        /// 根据expressionquery a record
        /// </summary>
        /// <param name="predicate">expression</param>
        /// <param name="tracking">Whether to track entities</param>
        /// <param name="cancellationToken">Cancel async token</param>
        /// <returns>entities in the database</returns>
        public virtual Task<TEntity> FirstAsync(Expression<Func<TEntity, bool>> predicate, bool? tracking = null,
            CancellationToken cancellationToken = default)
        {
            return AsQueryable(tracking).FirstAsync(predicate, cancellationToken);
        }

        /// <summary>
        /// query a record
        /// </summary>
        /// <param name="tracking">Whether to track entities</param>
        /// <param name="cancellationToken">Asynchronous cancellation token</param>
        /// <returns>entities in the database</returns>
        public virtual Task<TEntity> FirstOrDefaultAsync(bool? tracking = null,
            CancellationToken cancellationToken = default)
        {
            return AsQueryable(tracking).FirstOrDefaultAsync(cancellationToken);
        }

        /// <summary>
        /// 根据expressionquery a record
        /// </summary>
        /// <param name="predicate">expression</param>
        /// <param name="tracking">Whether to track entities</param>
        /// <param name="cancellationToken">Asynchronous cancellation token</param>
        /// <returns>entities in the database</returns>
        public virtual Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate,
            bool? tracking = null, CancellationToken cancellationToken = default)
        {
            return AsQueryable(tracking).FirstOrDefaultAsync(predicate, cancellationToken);
        }

        /// <summary>
        /// query a record
        /// </summary>
        /// <param name="tracking">Whether to track entities</param>
        /// <returns>entities in the database</returns>
        public virtual TEntity Last(bool? tracking = null)
        {
            return AsQueryable(tracking).Last();
        }

        /// <summary>
        /// 根据expressionquery a record
        /// </summary>
        /// <param name="predicate">expression</param>
        /// <param name="tracking">Whether to track entities</param>
        /// <returns>entities in the database</returns>
        public virtual TEntity Last(Expression<Func<TEntity, bool>> predicate, bool? tracking = null)
        {
            return AsQueryable(tracking).Last(predicate);
        }

        /// <summary>
        /// query a record
        /// </summary>
        /// <param name="tracking">Whether to track entities</param>
        /// <returns>entities in the database</returns>
        public virtual TEntity LastOrDefault(bool? tracking = null)
        {
            return AsQueryable(tracking).LastOrDefault();
        }

        /// <summary>
        /// 根据expressionquery a record
        /// </summary>
        /// <param name="predicate">expression</param>
        /// <param name="tracking">Whether to track entities</param>
        /// <returns>entities in the database</returns>
        public virtual TEntity LastOrDefault(Expression<Func<TEntity, bool>> predicate, bool? tracking = null)
        {
            return AsQueryable(tracking).LastOrDefault(predicate);
        }

        /// <summary>
        /// query a record
        /// </summary>
        /// <param name="tracking">Whether to track entities</param>
        /// <param name="cancellationToken">Cancel async token</param>
        /// <returns>entities in the database</returns>
        public virtual Task<TEntity> LastAsync(bool? tracking = null, CancellationToken cancellationToken = default)
        {
            return AsQueryable(tracking).LastAsync(cancellationToken);
        }

        /// <summary>
        /// 根据expressionquery a record
        /// </summary>
        /// <param name="predicate">expression</param>
        /// <param name="tracking">Whether to track entities</param>
        /// <param name="cancellationToken">Cancel async token</param>
        /// <returns>entities in the database</returns>
        public virtual Task<TEntity> LastAsync(Expression<Func<TEntity, bool>> predicate, bool? tracking = null,
            CancellationToken cancellationToken = default)
        {
            return AsQueryable(tracking).LastAsync(predicate, cancellationToken);
        }

        /// <summary>
        /// query a record
        /// </summary>
        /// <param name="tracking">Whether to track entities</param>
        /// <param name="cancellationToken">Asynchronous cancellation token</param>
        /// <returns>entities in the database</returns>
        public virtual Task<TEntity> LastOrDefaultAsync(bool? tracking = null,
            CancellationToken cancellationToken = default)
        {
            return AsQueryable(tracking).LastOrDefaultAsync(cancellationToken);
        }

        /// <summary>
        /// 根据expressionquery a record
        /// </summary>
        /// <param name="predicate">expression</param>
        /// <param name="tracking">Whether to track entities</param>
        /// <param name="cancellationToken">Asynchronous cancellation token</param>
        /// <returns>entities in the database</returns>
        public virtual Task<TEntity> LastOrDefaultAsync(Expression<Func<TEntity, bool>> predicate,
            bool? tracking = null, CancellationToken cancellationToken = default)
        {
            return AsQueryable(tracking).LastOrDefaultAsync(predicate, cancellationToken);
        }

        /// <summary>
        /// 根据expression查询多条记录
        /// </summary>
        /// <param name="predicate">expression</param>
        /// <param name="tracking">Whether to track entities</param>
        /// <param name="ignoreQueryFilters">Whether to ignore query filters</param>
        /// <returns>Multiple entities in database</returns>
        public virtual IQueryable<TEntity> Where(Expression<Func<TEntity, bool>> predicate, bool? tracking = null,
            bool ignoreQueryFilters = false)
        {
            return AsQueryable(predicate, tracking, ignoreQueryFilters);
        }

        /// <summary>
        /// 根据expression查询多条记录
        /// </summary>
        /// <param name="predicate">expression</param>
        /// <param name="tracking">Whether to track entities</param>
        /// <param name="ignoreQueryFilters">Whether to ignore query filters</param>
        /// <returns>Multiple entities in database</returns>
        public virtual IQueryable<TEntity> Where(Expression<Func<TEntity, int, bool>> predicate, bool? tracking = null,
            bool ignoreQueryFilters = false)
        {
            return AsQueryable(predicate, tracking, ignoreQueryFilters);
        }

        /// <summary>
        /// 根据conditionimplementexpression查询多条记录
        /// </summary>
        /// <param name="condition">condition</param>
        /// <param name="predicate">expression</param>
        /// <param name="tracking">Whether to track entities</param>
        /// <param name="ignoreQueryFilters">Whether to ignore query filters</param>
        /// <returns>Multiple entities in database</returns>
        public virtual IQueryable<TEntity> Where(bool condition, Expression<Func<TEntity, bool>> predicate,
            bool? tracking = null, bool ignoreQueryFilters = false)
        {
            return AsQueryable(default(Expression<Func<TEntity, bool>>), tracking, ignoreQueryFilters)
                .Where(condition, predicate);
        }

        /// <summary>
        /// 根据conditionimplementexpression查询多条记录
        /// </summary>
        /// <param name="condition">condition</param>
        /// <param name="predicate">expression</param>
        /// <param name="tracking">Whether to track entities</param>
        /// <param name="ignoreQueryFilters">Whether to ignore query filters</param>
        /// <returns>Multiple entities in database</returns>
        public virtual IQueryable<TEntity> Where(bool condition, Expression<Func<TEntity, int, bool>> predicate,
            bool? tracking = null, bool ignoreQueryFilters = false)
        {
            return AsQueryable(default(Expression<Func<TEntity, int, bool>>), tracking, ignoreQueryFilters)
                .Where(condition, predicate);
        }

        /// <summary>
        /// 根据conditionimplementexpression查询多条记录
        /// </summary>
        /// <param name="predicates">expression集合</param>
        /// <returns>Multiple entities in database</returns>
        public virtual IQueryable<TEntity> Where(params Expression<Func<TEntity, bool>>[] predicates)
        {
            return AsQueryable(default(Expression<Func<TEntity, bool>>)).Where(predicates);
        }

        /// <summary>
        /// 根据conditionimplementexpression查询多条记录
        /// </summary>
        /// <param name="predicates">expression集合</param>
        /// <param name="tracking">Whether to track entities</param>
        /// <param name="ignoreQueryFilters">Whether to ignore query filters</param>
        /// <returns>Multiple entities in database</returns>
        public virtual IQueryable<TEntity> Where(Expression<Func<TEntity, bool>>[] predicates, bool? tracking = null,
            bool ignoreQueryFilters = false)
        {
            return AsQueryable(default(Expression<Func<TEntity, bool>>), tracking, ignoreQueryFilters)
                .Where(predicates);
        }

        /// <summary>
        /// 根据conditionimplementexpression查询多条记录
        /// </summary>
        /// <param name="predicates">expression集合</param>
        /// <returns>Multiple entities in database</returns>
        public virtual IQueryable<TEntity> Where(params Expression<Func<TEntity, int, bool>>[] predicates)
        {
            return AsQueryable(default(Expression<Func<TEntity, int, bool>>)).Where(predicates);
        }

        /// <summary>
        /// 根据conditionimplementexpression查询多条记录
        /// </summary>
        /// <param name="predicates">expression集合</param>
        /// <param name="tracking">Whether to track entities</param>
        /// <param name="ignoreQueryFilters">Whether to ignore query filters</param>
        /// <returns>Multiple entities in database</returns>
        public virtual IQueryable<TEntity> Where(Expression<Func<TEntity, int, bool>>[] predicates,
            bool? tracking = null, bool ignoreQueryFilters = false)
        {
            return AsQueryable(default(Expression<Func<TEntity, int, bool>>), tracking, ignoreQueryFilters)
                .Where(predicates);
        }

        /// <summary>
        /// 根据conditionimplementexpression查询多条记录
        /// </summary>
        /// <param name="conditionPredicates">conditionexpression集合</param>
        /// <returns>Multiple entities in database</returns>
        public virtual IQueryable<TEntity> Where(
            params (bool condition, Expression<Func<TEntity, bool>> expression)[] conditionPredicates)
        {
            return AsQueryable(default(Expression<Func<TEntity, bool>>)).Where(conditionPredicates);
        }

        /// <summary>
        /// 根据conditionimplementexpression查询多条记录
        /// </summary>
        /// <param name="conditionPredicates">conditionexpression集合</param>
        /// <param name="tracking">Whether to track entities</param>
        /// <param name="ignoreQueryFilters">Whether to ignore query filters</param>
        /// <returns>Multiple entities in database</returns>
        public virtual IQueryable<TEntity> Where(
            (bool condition, Expression<Func<TEntity, bool>> expression)[] conditionPredicates, bool? tracking = null,
            bool ignoreQueryFilters = false)
        {
            return AsQueryable(default(Expression<Func<TEntity, bool>>), tracking, ignoreQueryFilters)
                .Where(conditionPredicates);
        }

        /// <summary>
        /// 根据conditionimplementexpression查询多条记录
        /// </summary>
        /// <param name="conditionPredicates">conditionexpression集合</param>
        /// <returns>Multiple entities in database</returns>
        public virtual IQueryable<TEntity> Where(
            params (bool condition, Expression<Func<TEntity, int, bool>> expression)[] conditionPredicates)
        {
            return AsQueryable(default(Expression<Func<TEntity, int, bool>>)).Where(conditionPredicates);
        }

        /// <summary>
        /// 根据conditionimplementexpression查询多条记录
        /// </summary>
        /// <param name="conditionPredicates">conditionexpression集合</param>
        /// <param name="tracking">Whether to track entities</param>
        /// <param name="ignoreQueryFilters">Whether to ignore query filters</param>
        /// <returns>Multiple entities in database</returns>
        public virtual IQueryable<TEntity> Where(
            (bool condition, Expression<Func<TEntity, int, bool>> expression)[] conditionPredicates,
            bool? tracking = null, bool ignoreQueryFilters = false)
        {
            return AsQueryable(default(Expression<Func<TEntity, int, bool>>), tracking, ignoreQueryFilters)
                .Where(conditionPredicates);
        }

        /// <summary>
        /// Load Linked Data
        /// </summary>
        /// <param name="predicate">expression</param>
        /// <param name="tracking">Whether to track entities</param>
        /// <param name="ignoreQueryFilters">Whether to ignore query filters</param>
        /// <returns>Multiple entities in database</returns>
        public virtual IIncludableQueryable<TEntity, TProperty> Include<TProperty>(
            Expression<Func<TEntity, TProperty>> predicate, bool? tracking = null, bool ignoreQueryFilters = false)
        {
            return AsQueryable(default(Expression<Func<TEntity, bool>>), tracking, ignoreQueryFilters)
                .Include(predicate);
        }

        /// <summary>
        /// 根据conditionLoad Linked Data
        /// </summary>
        /// <param name="condition">condition</param>
        /// <param name="predicate">expression</param>
        /// <param name="tracking">Whether to track entities</param>
        /// <param name="ignoreQueryFilters">Whether to ignore query filters</param>
        /// <returns>Multiple entities in database</returns>
        public virtual IIncludableQueryable<TEntity, TProperty> Include<TProperty>(bool condition,
            Expression<Func<TEntity, TProperty>> predicate, bool? tracking = null, bool ignoreQueryFilters = false)
        {
            return AsQueryable(default(Expression<Func<TEntity, bool>>), tracking, ignoreQueryFilters)
                .Include(condition, predicate);
        }

        /// <summary>
        /// Determine if the record exists
        /// </summary>
        /// <param name="tracking">Whether to track entities</param>
        /// <param name="ignoreQueryFilters">Whether to ignore query filters</param>
        /// <returns>bool</returns>
        public virtual bool Any(bool? tracking = null, bool ignoreQueryFilters = false)
        {
            return AsQueryable(default(Expression<Func<TEntity, bool>>), tracking, ignoreQueryFilters).Any();
        }

        /// <summary>
        /// 根据expressionDetermine if the record exists
        /// </summary>
        /// <param name="predicate">expression</param>
        /// <param name="tracking">Whether to track entities</param>
        /// <param name="ignoreQueryFilters">Whether to ignore query filters</param>
        /// <returns>bool</returns>
        public virtual bool Any(Expression<Func<TEntity, bool>> predicate, bool? tracking = null,
            bool ignoreQueryFilters = false)
        {
            return AsQueryable(default(Expression<Func<TEntity, bool>>), tracking, ignoreQueryFilters).Any(predicate);
        }

        /// <summary>
        /// Determine if the record exists
        /// </summary>
        /// <param name="tracking">Whether to track entities</param>
        /// <param name="ignoreQueryFilters">Whether to ignore query filters</param>
        /// <param name="cancellationToken">Cancel async token</param>
        /// <returns>bool</returns>
        public virtual Task<bool> AnyAsync(bool? tracking = null, bool ignoreQueryFilters = false,
            CancellationToken cancellationToken = default)
        {
            return AsQueryable(default(Expression<Func<TEntity, bool>>), tracking, ignoreQueryFilters)
                .AnyAsync(cancellationToken);
        }

        /// <summary>
        /// 根据expressionDetermine if the record exists
        /// </summary>
        /// <param name="predicate">expression</param>
        /// <param name="tracking">Whether to track entities</param>
        /// <param name="ignoreQueryFilters">Whether to ignore query filters</param>
        /// <param name="cancellationToken">Cancel async token</param>
        /// <returns>bool</returns>
        public virtual Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate, bool? tracking = null,
            bool ignoreQueryFilters = false, CancellationToken cancellationToken = default)
        {
            return AsQueryable(default(Expression<Func<TEntity, bool>>), tracking, ignoreQueryFilters)
                .AnyAsync(predicate, cancellationToken);
        }

        /// <summary>
        /// 根据expression判断记录是否全部满足condition
        /// </summary>
        /// <param name="predicate">expression</param>
        /// <param name="tracking">Whether to track entities</param>
        /// <param name="ignoreQueryFilters">Whether to ignore query filters</param>
        /// <returns>bool</returns>
        public virtual bool All(Expression<Func<TEntity, bool>> predicate, bool? tracking = null,
            bool ignoreQueryFilters = false)
        {
            return AsQueryable(default(Expression<Func<TEntity, bool>>), tracking, ignoreQueryFilters).All(predicate);
        }

        /// <summary>
        /// 根据expression判断记录是否全部满足condition
        /// </summary>
        /// <param name="predicate">expression</param>
        /// <param name="tracking">Whether to track entities</param>
        /// <param name="ignoreQueryFilters">Whether to ignore query filters</param>
        /// <param name="cancellationToken">Cancel async token</param>
        /// <returns>bool</returns>
        public virtual Task<bool> AllAsync(Expression<Func<TEntity, bool>> predicate, bool? tracking = null,
            bool ignoreQueryFilters = false, CancellationToken cancellationToken = default)
        {
            return AsQueryable(default(Expression<Func<TEntity, bool>>), tracking, ignoreQueryFilters)
                .AllAsync(predicate, cancellationToken);
        }

        /// <summary>
        /// View the number of records
        /// </summary>
        /// <param name="tracking">Whether to track entities</param>
        /// <param name="ignoreQueryFilters">Whether to ignore query filters</param>
        /// <returns>int</returns>
        public virtual int Count(bool? tracking = null, bool ignoreQueryFilters = false)
        {
            return AsQueryable(default(Expression<Func<TEntity, bool>>), tracking, ignoreQueryFilters).Count();
        }

        /// <summary>
        /// 根据expression查询记录条数
        /// </summary>
        /// <param name="predicate">expression</param>
        /// <param name="tracking">Whether to track entities</param>
        /// <param name="ignoreQueryFilters">Whether to ignore query filters</param>
        /// <returns>int</returns>
        public virtual int Count(Expression<Func<TEntity, bool>> predicate, bool? tracking = null,
            bool ignoreQueryFilters = false)
        {
            return AsQueryable(default(Expression<Func<TEntity, bool>>), tracking, ignoreQueryFilters).Count(predicate);
        }

        /// <summary>
        /// View the number of records
        /// </summary>
        /// <param name="tracking">Whether to track entities</param>
        /// <param name="ignoreQueryFilters">Whether to ignore query filters</param>
        /// <param name="cancellationToken">Cancel async token</param>
        /// <returns>int</returns>
        public virtual Task<int> CountAsync(bool? tracking = null, bool ignoreQueryFilters = false,
            CancellationToken cancellationToken = default)
        {
            return AsQueryable(default(Expression<Func<TEntity, bool>>), tracking, ignoreQueryFilters)
                .CountAsync(cancellationToken);
        }

        /// <summary>
        /// 根据expression查询记录条数
        /// </summary>
        /// <param name="predicate">expression</param>
        /// <param name="tracking">Whether to track entities</param>
        /// <param name="ignoreQueryFilters">Whether to ignore query filters</param>
        /// <param name="cancellationToken">Cancel async token</param>
        /// <returns>int</returns>
        public virtual Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate, bool? tracking = null,
            bool ignoreQueryFilters = false, CancellationToken cancellationToken = default)
        {
            return AsQueryable(default(Expression<Func<TEntity, bool>>), tracking, ignoreQueryFilters)
                .CountAsync(predicate, cancellationToken);
        }

        /// <summary>
        /// View minimum records
        /// </summary>
        /// <param name="tracking">Whether to track entities</param>
        /// <param name="ignoreQueryFilters">Whether to ignore query filters</param>
        /// <returns>entity</returns>
        public virtual TEntity Min(bool? tracking = null, bool ignoreQueryFilters = false)
        {
            return AsQueryable(default(Expression<Func<TEntity, bool>>), tracking, ignoreQueryFilters).Min();
        }

        /// <summary>
        /// 根据expression查看最小值
        /// </summary>
        /// <typeparam name="TResult">Minimum type</typeparam>
        /// <param name="predicate">expression</param>
        /// <param name="tracking">Whether to track entities</param>
        /// <param name="ignoreQueryFilters">Whether to ignore query filters</param>
        /// <returns>TResult</returns>
        public virtual TResult Min<TResult>(Expression<Func<TEntity, TResult>> predicate, bool? tracking = null,
            bool ignoreQueryFilters = false)
        {
            return AsQueryable(default(Expression<Func<TEntity, bool>>), tracking, ignoreQueryFilters).Min(predicate);
        }

        /// <summary>
        /// View minimum records
        /// </summary>
        /// <param name="tracking">Whether to track entities</param>
        /// <param name="ignoreQueryFilters">Whether to ignore query filters</param>
        /// <param name="cancellationToken">Cancel async token</param>
        /// <returns>entity</returns>
        public virtual Task<TEntity> MinAsync(bool? tracking = null, bool ignoreQueryFilters = false,
            CancellationToken cancellationToken = default)
        {
            return AsQueryable(default(Expression<Func<TEntity, bool>>), tracking, ignoreQueryFilters)
                .MinAsync(cancellationToken);
        }

        /// <summary>
        /// 根据expression查看最小值
        /// </summary>
        /// <typeparam name="TResult">Minimum type</typeparam>
        /// <param name="predicate">expression</param>
        /// <param name="tracking">Whether to track entities</param>
        /// <param name="ignoreQueryFilters">Whether to ignore query filters</param>
        /// <param name="cancellationToken">Cancel async token</param>
        /// <returns>TResult</returns>
        public virtual Task<TResult> MinAsync<TResult>(Expression<Func<TEntity, TResult>> predicate,
            bool? tracking = null, bool ignoreQueryFilters = false, CancellationToken cancellationToken = default)
        {
            return AsQueryable(default(Expression<Func<TEntity, bool>>), tracking, ignoreQueryFilters)
                .MinAsync(predicate, cancellationToken);
        }

        /// <summary>
        /// View max records
        /// </summary>
        /// <param name="tracking">Whether to track entities</param>
        /// <param name="ignoreQueryFilters">Whether to ignore query filters</param>
        /// <returns>entity</returns>
        public virtual TEntity Max(bool? tracking = null, bool ignoreQueryFilters = false)
        {
            return AsQueryable(default(Expression<Func<TEntity, bool>>), tracking, ignoreQueryFilters).Max();
        }

        /// <summary>
        /// 根据expression查看最大值
        /// </summary>
        /// <typeparam name="TResult">Maximum type</typeparam>
        /// <param name="predicate">expression</param>
        /// <param name="tracking">Whether to track entities</param>
        /// <param name="ignoreQueryFilters">Whether to ignore query filters</param>
        /// <returns>TResult</returns>
        public virtual TResult Max<TResult>(Expression<Func<TEntity, TResult>> predicate, bool? tracking = null,
            bool ignoreQueryFilters = false)
        {
            return AsQueryable(default(Expression<Func<TEntity, bool>>), tracking, ignoreQueryFilters).Max(predicate);
        }

        /// <summary>
        /// View max records
        /// </summary>
        /// <param name="tracking">Whether to track entities</param>
        /// <param name="ignoreQueryFilters">Whether to ignore query filters</param>
        /// <param name="cancellationToken">Cancel async token</param>
        /// <returns>entity</returns>
        public virtual Task<TEntity> MaxAsync(bool? tracking = null, bool ignoreQueryFilters = false,
            CancellationToken cancellationToken = default)
        {
            return AsQueryable(default(Expression<Func<TEntity, bool>>), tracking, ignoreQueryFilters)
                .MaxAsync(cancellationToken);
        }

        /// <summary>
        /// 根据expression查看最大值
        /// </summary>
        /// <typeparam name="TResult">Maximum type</typeparam>
        /// <param name="predicate">expression</param>
        /// <param name="tracking">Whether to track entities</param>
        /// <param name="ignoreQueryFilters">Whether to ignore query filters</param>
        /// <param name="cancellationToken">Cancel async token</param>
        /// <returns>TResult</returns>
        public virtual Task<TResult> MaxAsync<TResult>(Expression<Func<TEntity, TResult>> predicate,
            bool? tracking = null, bool ignoreQueryFilters = false, CancellationToken cancellationToken = default)
        {
            return AsQueryable(default(Expression<Func<TEntity, bool>>), tracking, ignoreQueryFilters)
                .MaxAsync(predicate, cancellationToken);
        }

        /// <summary>
        /// Build a query analyzer
        /// </summary>
        /// <param name="tracking">Whether to track entities</param>
        /// <returns>IQueryable{TEntity}</returns>
        public virtual IQueryable<TEntity> AsQueryable(bool? tracking = null)
        {
            // 启用entity跟踪
            var isTracking = tracking ?? DynamicContext.EnabledEntityStateTracked;

            return isTracking ? Entities : DetachedEntities;
        }

        /// <summary>
        /// Build a query analyzer
        /// </summary>
        /// <param name="predicate">expression</param>
        /// <param name="tracking">Whether to track entities</param>
        /// <param name="ignoreQueryFilters">Whether to ignore query filters</param>
        /// <returns>IQueryable{TEntity}</returns>
        public virtual IQueryable<TEntity> AsQueryable(Expression<Func<TEntity, bool>> predicate, bool? tracking = null,
            bool ignoreQueryFilters = false)
        {
            var entities = AsQueryable(tracking);
            if (ignoreQueryFilters) entities = entities.IgnoreQueryFilters();
            if (predicate != null) entities = entities.Where(predicate);

            return entities;
        }

        /// <summary>
        /// Build a query analyzer
        /// </summary>
        /// <param name="predicate">expression</param>
        /// <param name="tracking">Whether to track entities</param>
        /// <param name="ignoreQueryFilters">Whether to ignore query filters</param>
        /// <returns>IQueryable{TEntity}</returns>
        public virtual IQueryable<TEntity> AsQueryable(Expression<Func<TEntity, int, bool>> predicate,
            bool? tracking = null, bool ignoreQueryFilters = false)
        {
            var entities = AsQueryable(tracking);
            if (ignoreQueryFilters) entities = entities.IgnoreQueryFilters();
            if (predicate != null) entities = entities.Where(predicate);

            return entities;
        }

        /// <summary>
        /// Return database results directly
        /// </summary>
        /// <param name="tracking">Whether to track entities</param>
        /// <returns>IEnumerable{TEntity}</returns>
        public virtual IEnumerable<TEntity> AsEnumerable(bool? tracking = null)
        {
            return AsQueryable(tracking).AsEnumerable();
        }

        /// <summary>
        /// Return database results directly
        /// </summary>
        /// <param name="predicate">expression</param>
        /// <param name="tracking">Whether to track entities</param>
        /// <param name="ignoreQueryFilters">Whether to ignore query filters</param>
        /// <returns>List{TEntity}</returns>
        public virtual IEnumerable<TEntity> AsEnumerable(Expression<Func<TEntity, bool>> predicate,
            bool? tracking = null, bool ignoreQueryFilters = false)
        {
            return AsQueryable(predicate, tracking, ignoreQueryFilters).AsEnumerable();
        }

        /// <summary>
        /// Return database results directly
        /// </summary>
        /// <param name="predicate">expression</param>
        /// <param name="tracking">Whether to track entities</param>
        /// <param name="ignoreQueryFilters">Whether to ignore query filters</param>
        /// <returns>IEnumerable{TEntity}</returns>
        public virtual IEnumerable<TEntity> AsEnumerable(Expression<Func<TEntity, int, bool>> predicate,
            bool? tracking = null, bool ignoreQueryFilters = false)
        {
            return AsQueryable(predicate, tracking, ignoreQueryFilters).AsEnumerable();
        }

        /// <summary>
        /// Return database results directly
        /// </summary>
        /// <param name="tracking">Whether to track entities</param>
        /// <returns>List{TEntity}</returns>
        public virtual IAsyncEnumerable<TEntity> AsAsyncEnumerable(bool? tracking = null)
        {
            return AsQueryable(tracking).AsAsyncEnumerable();
        }

        /// <summary>
        /// Return database results directly
        /// </summary>
        /// <param name="predicate">expression</param>
        /// <param name="tracking">Whether to track entities</param>
        /// <param name="ignoreQueryFilters">Whether to ignore query filters</param>
        /// <returns>IAsyncEnumerable{TEntity}</returns>
        public virtual IAsyncEnumerable<TEntity> AsAsyncEnumerable(Expression<Func<TEntity, bool>> predicate,
            bool? tracking = null, bool ignoreQueryFilters = false)
        {
            return AsQueryable(predicate, tracking, ignoreQueryFilters).AsAsyncEnumerable();
        }

        /// <summary>
        /// Return database results directly
        /// </summary>
        /// <param name="predicate">expression</param>
        /// <param name="tracking">Whether to track entities</param>
        /// <param name="ignoreQueryFilters">Whether to ignore query filters</param>
        /// <returns>IAsyncEnumerable{TEntity}</returns>
        public virtual IAsyncEnumerable<TEntity> AsAsyncEnumerable(Expression<Func<TEntity, int, bool>> predicate,
            bool? tracking = null, bool ignoreQueryFilters = false)
        {
            return AsQueryable(predicate, tracking, ignoreQueryFilters).AsAsyncEnumerable();
        }

        /// <summary>
        /// implement Sql return IQueryable
        /// </summary>
        /// <param name="sql">sql statement</param>
        /// <param name="parameters">Command parameters</param>
        /// <returns>IQueryable</returns>
        public virtual IQueryable<TEntity> FromSqlRaw(string sql, params object[] parameters)
        {
            // Support read configuration rendering
            var realSql = sql.Render();

            return Entities.FromSqlRaw(realSql, parameters);
        }

        /// <summary>
        /// implement Sql return IQueryable
        /// </summary>
        /// <remarks>
        /// Supports string interpolation syntax
        /// </remarks>
        /// <param name="sql">sql statement</param>
        /// <returns>IQueryable</returns>
        public virtual IQueryable<TEntity> FromSqlInterpolated(FormattableString sql)
        {
            return Entities.FromSqlInterpolated(sql);
        }
    }
}