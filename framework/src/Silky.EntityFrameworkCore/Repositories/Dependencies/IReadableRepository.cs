using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Query;
using Silky.EntityFrameworkCore.Entities;
using Silky.EntityFrameworkCore.Locators;

namespace Silky.EntityFrameworkCore.Repositories
{
    /// <summary>
    /// Readable repository interface
    /// </summary>
    /// <typeparam name="TEntity">entity type</typeparam>
    public interface IReadableRepository<TEntity> : IReadableRepository<TEntity, MasterDbContextLocator>
        where TEntity : class, IPrivateEntity, new()
    {
    }

    /// <summary>
    /// Readable repository interface
    /// </summary>
    /// <typeparam name="TEntity">entity type</typeparam>
    /// <typeparam name="TDbContextLocator">database context locator</typeparam>
    public interface IReadableRepository<TEntity, TDbContextLocator> : IPrivateReadableRepository<TEntity>
        where TEntity : class, IPrivateEntity, new()
        where TDbContextLocator : class, IDbContextLocator
    {
    }

    /// <summary>
    /// Readable repository interface
    /// </summary>
    /// <typeparam name="TEntity">entity type</typeparam>
    public interface IPrivateReadableRepository<TEntity> : IPrivateRootRepository
        where TEntity : class, IPrivateEntity, new()
    {
        /// <summary>
        /// Query a record by key
        /// </summary>
        /// <param name="key">key</param>
        /// <returns>entities in the database</returns>
        TEntity Find(object key);

        /// <summary>
        /// 根据多个keyquery a record
        /// </summary>
        /// <param name="keyValues">多个key</param>
        /// <returns>entities in the database</returns>
        TEntity Find(params object[] keyValues);

        /// <summary>
        /// Query a record by key
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="cancellationToken">Cancel async token</param>
        /// <returns>database entity</returns>
        Task<TEntity> FindAsync(object key, CancellationToken cancellationToken = default);

        /// <summary>
        /// 根据多个keyquery a record
        /// </summary>
        /// <param name="keyValues">多个key</param>
        /// <returns>entities in the database</returns>
        Task<TEntity> FindAsync(params object[] keyValues);

        /// <summary>
        /// 根据多个keyquery a record
        /// </summary>
        /// <param name="keyValues">多个key</param>
        /// <param name="cancellationToken">Cancel async token</param>
        /// <returns>entities in the database</returns>
        Task<TEntity> FindAsync(object[] keyValues, CancellationToken cancellationToken = default);

        /// <summary>
        /// Query a record by key
        /// </summary>
        /// <param name="key">key</param>
        /// <returns>entities in the database</returns>
        TEntity FindOrDefault(object key);

        /// <summary>
        /// 根据多个keyquery a record
        /// </summary>
        /// <param name="keyValues">多个key</param>
        /// <returns>entities in the database</returns>
        TEntity FindOrDefault(params object[] keyValues);

        /// <summary>
        /// Query a record by key
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="cancellationToken">Cancel async token</param>
        /// <returns>entities in the database</returns>
        Task<TEntity> FindOrDefaultAsync(object key, CancellationToken cancellationToken = default);

        /// <summary>
        /// 根据多个keyquery a record
        /// </summary>
        /// <param name="keyValues">多个key</param>
        /// <returns>entities in the database</returns>
        Task<TEntity> FindOrDefaultAsync(params object[] keyValues);

        /// <summary>
        /// 根据多个keyquery a record
        /// </summary>
        /// <param name="keyValues">多个key</param>
        /// <param name="cancellationToken">Cancel async token</param>
        /// <returns>entities in the database</returns>
        Task<TEntity> FindOrDefaultAsync(object[] keyValues, CancellationToken cancellationToken = default);

        /// <summary>
        /// query a record
        /// </summary>
        /// <param name="tracking">Whether to track entities</param>
        /// <returns>entities in the database</returns>
        TEntity Single(bool? tracking = null);

        /// <summary>
        /// 根据expressionquery a record
        /// </summary>
        /// <param name="predicate">expression</param>
        /// <param name="tracking">Whether to track entities</param>
        /// <returns>entities in the database</returns>
        TEntity Single(Expression<Func<TEntity, bool>> predicate, bool? tracking = null);

        /// <summary>
        /// query a record
        /// </summary>
        /// <param name="tracking">Whether to track entities</param>
        /// <returns>entities in the database</returns>
        TEntity SingleOrDefault(bool? tracking = null);

        /// <summary>
        /// 根据expressionquery a record
        /// </summary>
        /// <param name="predicate">expression</param>
        /// <param name="tracking">Whether to track entities</param>
        /// <returns>entities in the database</returns>
        TEntity SingleOrDefault(Expression<Func<TEntity, bool>> predicate, bool? tracking = null);

        /// <summary>
        /// query a record
        /// </summary>
        /// <param name="tracking">Whether to track entities</param>
        /// <param name="cancellationToken">Cancel async token</param>
        /// <returns>entities in the database</returns>
        Task<TEntity> SingleAsync(bool? tracking = null, CancellationToken cancellationToken = default);

        /// <summary>
        /// 根据expressionquery a record
        /// </summary>
        /// <param name="predicate">expression</param>
        /// <param name="tracking">Whether to track entities</param>
        /// <param name="cancellationToken">Cancel async token</param>
        /// <returns>entities in the database</returns>
        Task<TEntity> SingleAsync(Expression<Func<TEntity, bool>> predicate, bool? tracking = null,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// query a record
        /// </summary>
        /// <param name="tracking">Whether to track entities</param>
        /// <param name="cancellationToken">Asynchronous cancellation token</param>
        /// <returns>entities in the database</returns>
        Task<TEntity> SingleOrDefaultAsync(bool? tracking = null, CancellationToken cancellationToken = default);

        /// <summary>
        /// 根据expressionquery a record
        /// </summary>
        /// <param name="predicate">expression</param>
        /// <param name="tracking">Whether to track entities</param>
        /// <param name="cancellationToken">Asynchronous cancellation token</param>
        /// <returns>entities in the database</returns>
        Task<TEntity> SingleOrDefaultAsync(Expression<Func<TEntity, bool>> predicate, bool? tracking = null,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// query a record
        /// </summary>
        /// <param name="tracking">Whether to track entities</param>
        /// <returns>entities in the database</returns>
        TEntity First(bool? tracking = null);

        /// <summary>
        /// 根据expressionquery a record
        /// </summary>
        /// <param name="predicate">expression</param>
        /// <param name="tracking">Whether to track entities</param>
        /// <returns>entities in the database</returns>
        TEntity First(Expression<Func<TEntity, bool>> predicate, bool? tracking = null);

        /// <summary>
        /// query a record
        /// </summary>
        /// <param name="tracking">Whether to track entities</param>
        /// <returns>entities in the database</returns>
        TEntity FirstOrDefault(bool? tracking = null);

        /// <summary>
        /// 根据expressionquery a record
        /// </summary>
        /// <param name="predicate">expression</param>
        /// <param name="tracking">Whether to track entities</param>
        /// <returns>entities in the database</returns>
        TEntity FirstOrDefault(Expression<Func<TEntity, bool>> predicate, bool? tracking = null);

        /// <summary>
        /// query a record
        /// </summary>
        /// <param name="tracking">Whether to track entities</param>
        /// <param name="cancellationToken">Cancel async token</param>
        /// <returns>entities in the database</returns>
        Task<TEntity> FirstAsync(bool? tracking = null, CancellationToken cancellationToken = default);

        /// <summary>
        /// 根据expressionquery a record
        /// </summary>
        /// <param name="predicate">expression</param>
        /// <param name="tracking">Whether to track entities</param>
        /// <param name="cancellationToken">Cancel async token</param>
        /// <returns>entities in the database</returns>
        Task<TEntity> FirstAsync(Expression<Func<TEntity, bool>> predicate, bool? tracking = null,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// query a record
        /// </summary>
        /// <param name="tracking">Whether to track entities</param>
        /// <param name="cancellationToken">Asynchronous cancellation token</param>
        /// <returns>entities in the database</returns>
        Task<TEntity> FirstOrDefaultAsync(bool? tracking = null, CancellationToken cancellationToken = default);

        /// <summary>
        /// 根据expressionquery a record
        /// </summary>
        /// <param name="predicate">expression</param>
        /// <param name="tracking">Whether to track entities</param>
        /// <param name="cancellationToken">Asynchronous cancellation token</param>
        /// <returns>entities in the database</returns>
        Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate, bool? tracking = null,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// query a record
        /// </summary>
        /// <param name="tracking">Whether to track entities</param>
        /// <returns>entities in the database</returns>
        TEntity Last(bool? tracking = null);

        /// <summary>
        /// 根据expressionquery a record
        /// </summary>
        /// <param name="predicate">expression</param>
        /// <param name="tracking">Whether to track entities</param>
        /// <returns>entities in the database</returns>
        TEntity Last(Expression<Func<TEntity, bool>> predicate, bool? tracking = null);

        /// <summary>
        /// query a record
        /// </summary>
        /// <param name="tracking">Whether to track entities</param>
        /// <returns>entities in the database</returns>
        TEntity LastOrDefault(bool? tracking = null);

        /// <summary>
        /// 根据expressionquery a record
        /// </summary>
        /// <param name="predicate">expression</param>
        /// <param name="tracking">Whether to track entities</param>
        /// <returns>entities in the database</returns>
        TEntity LastOrDefault(Expression<Func<TEntity, bool>> predicate, bool? tracking = null);

        /// <summary>
        /// query a record
        /// </summary>
        /// <param name="tracking">Whether to track entities</param>
        /// <param name="cancellationToken">Cancel async token</param>
        /// <returns>entities in the database</returns>
        Task<TEntity> LastAsync(bool? tracking = null, CancellationToken cancellationToken = default);

        /// <summary>
        /// 根据expressionquery a record
        /// </summary>
        /// <param name="predicate">expression</param>
        /// <param name="tracking">Whether to track entities</param>
        /// <param name="cancellationToken">Cancel async token</param>
        /// <returns>entities in the database</returns>
        Task<TEntity> LastAsync(Expression<Func<TEntity, bool>> predicate, bool? tracking = null,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// query a record
        /// </summary>
        /// <param name="tracking">Whether to track entities</param>
        /// <param name="cancellationToken">Asynchronous cancellation token</param>
        /// <returns>entities in the database</returns>
        Task<TEntity> LastOrDefaultAsync(bool? tracking = null, CancellationToken cancellationToken = default);

        /// <summary>
        /// 根据expressionquery a record
        /// </summary>
        /// <param name="predicate">expression</param>
        /// <param name="tracking">Whether to track entities</param>
        /// <param name="cancellationToken">Asynchronous cancellation token</param>
        /// <returns>entities in the database</returns>
        Task<TEntity> LastOrDefaultAsync(Expression<Func<TEntity, bool>> predicate, bool? tracking = null,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// 根据expression查询多条记录
        /// </summary>
        /// <param name="predicate">expression</param>
        /// <param name="tracking">Whether to track entities</param>
        /// <param name="ignoreQueryFilters">Whether to ignore query filters</param>
        /// <returns>Multiple entities in database</returns>
        IQueryable<TEntity> Where(Expression<Func<TEntity, bool>> predicate, bool? tracking = null,
            bool ignoreQueryFilters = false);

        /// <summary>
        /// 根据expression查询多条记录
        /// </summary>
        /// <param name="predicate">expression</param>
        /// <param name="tracking">Whether to track entities</param>
        /// <param name="ignoreQueryFilters">Whether to ignore query filters</param>
        /// <returns>Multiple entities in database</returns>
        IQueryable<TEntity> Where(Expression<Func<TEntity, int, bool>> predicate, bool? tracking = null,
            bool ignoreQueryFilters = false);

        /// <summary>
        /// 根据conditionimplementexpression查询多条记录
        /// </summary>
        /// <param name="condition">condition</param>
        /// <param name="predicate">expression</param>
        /// <param name="tracking">Whether to track entities</param>
        /// <param name="ignoreQueryFilters">Whether to ignore query filters</param>
        /// <returns>Multiple entities in database</returns>
        IQueryable<TEntity> Where(bool condition, Expression<Func<TEntity, bool>> predicate, bool? tracking = null,
            bool ignoreQueryFilters = false);

        /// <summary>
        /// 根据conditionimplementexpression查询多条记录
        /// </summary>
        /// <param name="condition">condition</param>
        /// <param name="predicate">expression</param>
        /// <param name="tracking">Whether to track entities</param>
        /// <param name="ignoreQueryFilters">Whether to ignore query filters</param>
        /// <returns>Multiple entities in database</returns>
        IQueryable<TEntity> Where(bool condition, Expression<Func<TEntity, int, bool>> predicate, bool? tracking = null,
            bool ignoreQueryFilters = false);

        /// <summary>
        /// 根据conditionimplementexpression查询多条记录
        /// </summary>
        /// <param name="predicates">expression集合</param>
        /// <returns>Multiple entities in database</returns>
        IQueryable<TEntity> Where(params Expression<Func<TEntity, bool>>[] predicates);

        /// <summary>
        /// 根据conditionimplementexpression查询多条记录
        /// </summary>
        /// <param name="predicates">expression集合</param>
        /// <param name="tracking">Whether to track entities</param>
        /// <param name="ignoreQueryFilters">Whether to ignore query filters</param>
        /// <returns>Multiple entities in database</returns>
        IQueryable<TEntity> Where(Expression<Func<TEntity, bool>>[] predicates, bool? tracking = null,
            bool ignoreQueryFilters = false);

        /// <summary>
        /// 根据conditionimplementexpression查询多条记录
        /// </summary>
        /// <param name="predicates">expression集合</param>
        /// <returns>Multiple entities in database</returns>
        IQueryable<TEntity> Where(params Expression<Func<TEntity, int, bool>>[] predicates);

        /// <summary>
        /// 根据conditionimplementexpression查询多条记录
        /// </summary>
        /// <param name="predicates">expression集合</param>
        /// <param name="tracking">Whether to track entities</param>
        /// <param name="ignoreQueryFilters">Whether to ignore query filters</param>
        /// <returns>Multiple entities in database</returns>
        IQueryable<TEntity> Where(Expression<Func<TEntity, int, bool>>[] predicates, bool? tracking = null,
            bool ignoreQueryFilters = false);

        /// <summary>
        /// 根据conditionimplementexpression查询多条记录
        /// </summary>
        /// <param name="conditionPredicates">conditionexpression集合</param>
        /// <returns>Multiple entities in database</returns>
        IQueryable<TEntity> Where(
            params (bool condition, Expression<Func<TEntity, bool>> expression)[] conditionPredicates);

        /// <summary>
        /// 根据conditionimplementexpression查询多条记录
        /// </summary>
        /// <param name="conditionPredicates">conditionexpression集合</param>
        /// <param name="tracking">Whether to track entities</param>
        /// <param name="ignoreQueryFilters">Whether to ignore query filters</param>
        /// <returns>Multiple entities in database</returns>
        IQueryable<TEntity> Where((bool condition, Expression<Func<TEntity, bool>> expression)[] conditionPredicates,
            bool? tracking = null, bool ignoreQueryFilters = false);

        /// <summary>
        /// 根据conditionimplementexpression查询多条记录
        /// </summary>
        /// <param name="conditionPredicates">conditionexpression集合</param>
        /// <returns>Multiple entities in database</returns>
        IQueryable<TEntity> Where(
            params (bool condition, Expression<Func<TEntity, int, bool>> expression)[] conditionPredicates);

        /// <summary>
        /// 根据conditionimplementexpression查询多条记录
        /// </summary>
        /// <param name="conditionPredicates">conditionexpression集合</param>
        /// <param name="tracking">Whether to track entities</param>
        /// <param name="ignoreQueryFilters">Whether to ignore query filters</param>
        /// <returns>Multiple entities in database</returns>
        IQueryable<TEntity> Where(
            (bool condition, Expression<Func<TEntity, int, bool>> expression)[] conditionPredicates,
            bool? tracking = null, bool ignoreQueryFilters = false);

        /// <summary>
        /// Load Linked Data
        /// </summary>
        /// <param name="predicate">expression</param>
        /// <param name="tracking">Whether to track entities</param>
        /// <param name="ignoreQueryFilters">Whether to ignore query filters</param>
        /// <returns>Multiple entities in database</returns>
        IIncludableQueryable<TEntity, TProperty> Include<TProperty>(Expression<Func<TEntity, TProperty>> predicate,
            bool? tracking = null, bool ignoreQueryFilters = false);

        /// <summary>
        /// 根据conditionLoad Linked Data
        /// </summary>
        /// <param name="condition">condition</param>
        /// <param name="predicate">expression</param>
        /// <param name="tracking">Whether to track entities</param>
        /// <param name="ignoreQueryFilters">Whether to ignore query filters</param>
        /// <returns>Multiple entities in database</returns>
        IIncludableQueryable<TEntity, TProperty> Include<TProperty>(bool condition,
            Expression<Func<TEntity, TProperty>> predicate, bool? tracking = null, bool ignoreQueryFilters = false);

        /// <summary>
        /// Determine if the record exists
        /// </summary>
        /// <param name="tracking">Whether to track entities</param>
        /// <param name="ignoreQueryFilters">Whether to ignore query filters</param>
        /// <returns>bool</returns>
        bool Any(bool? tracking = null, bool ignoreQueryFilters = false);

        /// <summary>
        /// 根据expressionDetermine if the record exists
        /// </summary>
        /// <param name="predicate">expression</param>
        /// <param name="tracking">Whether to track entities</param>
        /// <param name="ignoreQueryFilters">Whether to ignore query filters</param>
        /// <returns>bool</returns>
        bool Any(Expression<Func<TEntity, bool>> predicate, bool? tracking = null, bool ignoreQueryFilters = false);

        /// <summary>
        /// Determine if the record exists
        /// </summary>
        /// <param name="tracking">Whether to track entities</param>
        /// <param name="ignoreQueryFilters">Whether to ignore query filters</param>
        /// <param name="cancellationToken">Cancel async token</param>
        /// <returns>bool</returns>
        Task<bool> AnyAsync(bool? tracking = null, bool ignoreQueryFilters = false,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// 根据expressionDetermine if the record exists
        /// </summary>
        /// <param name="predicate">expression</param>
        /// <param name="tracking">Whether to track entities</param>
        /// <param name="ignoreQueryFilters">Whether to ignore query filters</param>
        /// <param name="cancellationToken">Cancel async token</param>
        /// <returns>bool</returns>
        Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate, bool? tracking = null,
            bool ignoreQueryFilters = false, CancellationToken cancellationToken = default);

        /// <summary>
        /// 根据expression判断记录是否全部满足condition
        /// </summary>
        /// <param name="predicate">expression</param>
        /// <param name="tracking">Whether to track entities</param>
        /// <param name="ignoreQueryFilters">Whether to ignore query filters</param>
        /// <returns>bool</returns>
        bool All(Expression<Func<TEntity, bool>> predicate, bool? tracking = null, bool ignoreQueryFilters = false);

        /// <summary>
        /// 根据expression判断记录是否全部满足condition
        /// </summary>
        /// <param name="predicate">expression</param>
        /// <param name="tracking">Whether to track entities</param>
        /// <param name="ignoreQueryFilters">Whether to ignore query filters</param>
        /// <param name="cancellationToken">Cancel async token</param>
        /// <returns>bool</returns>
        Task<bool> AllAsync(Expression<Func<TEntity, bool>> predicate, bool? tracking = null,
            bool ignoreQueryFilters = false, CancellationToken cancellationToken = default);

        /// <summary>
        /// View the number of records
        /// </summary>
        /// <param name="tracking">Whether to track entities</param>
        /// <param name="ignoreQueryFilters">Whether to ignore query filters</param>
        /// <returns>int</returns>
        int Count(bool? tracking = null, bool ignoreQueryFilters = false);

        /// <summary>
        /// 根据expression查询记录条数
        /// </summary>
        /// <param name="predicate">expression</param>
        /// <param name="tracking">Whether to track entities</param>
        /// <param name="ignoreQueryFilters">Whether to ignore query filters</param>
        /// <returns>int</returns>
        int Count(Expression<Func<TEntity, bool>> predicate, bool? tracking = null, bool ignoreQueryFilters = false);

        /// <summary>
        /// View the number of records
        /// </summary>
        /// <param name="tracking">Whether to track entities</param>
        /// <param name="ignoreQueryFilters">Whether to ignore query filters</param>
        /// <param name="cancellationToken">Cancel async token</param>
        /// <returns>int</returns>
        Task<int> CountAsync(bool? tracking = null, bool ignoreQueryFilters = false,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// 根据expression查询记录条数
        /// </summary>
        /// <param name="predicate">expression</param>
        /// <param name="tracking">Whether to track entities</param>
        /// <param name="ignoreQueryFilters">Whether to ignore query filters</param>
        /// <param name="cancellationToken">Cancel async token</param>
        /// <returns>int</returns>
        Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate, bool? tracking = null,
            bool ignoreQueryFilters = false, CancellationToken cancellationToken = default);

        /// <summary>
        /// View minimum records
        /// </summary>
        /// <param name="tracking">Whether to track entities</param>
        /// <param name="ignoreQueryFilters">Whether to ignore query filters</param>
        /// <returns>entity</returns>
        TEntity Min(bool? tracking = null, bool ignoreQueryFilters = false);

        /// <summary>
        /// 根据expression查看最小值
        /// </summary>
        /// <typeparam name="TResult">Minimum type</typeparam>
        /// <param name="predicate">expression</param>
        /// <param name="tracking">Whether to track entities</param>
        /// <param name="ignoreQueryFilters">Whether to ignore query filters</param>
        /// <returns>TResult</returns>
        TResult Min<TResult>(Expression<Func<TEntity, TResult>> predicate, bool? tracking = null,
            bool ignoreQueryFilters = false);

        /// <summary>
        /// View minimum records
        /// </summary>
        /// <param name="tracking">Whether to track entities</param>
        /// <param name="ignoreQueryFilters">Whether to ignore query filters</param>
        /// <param name="cancellationToken">Cancel async token</param>
        /// <returns>entity</returns>
        Task<TEntity> MinAsync(bool? tracking = null, bool ignoreQueryFilters = false,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// 根据expression查看最小值
        /// </summary>
        /// <typeparam name="TResult">Minimum type</typeparam>
        /// <param name="predicate">expression</param>
        /// <param name="tracking">Whether to track entities</param>
        /// <param name="ignoreQueryFilters">Whether to ignore query filters</param>
        /// <param name="cancellationToken">Cancel async token</param>
        /// <returns>TResult</returns>
        Task<TResult> MinAsync<TResult>(Expression<Func<TEntity, TResult>> predicate, bool? tracking = null,
            bool ignoreQueryFilters = false, CancellationToken cancellationToken = default);

        /// <summary>
        /// View max records
        /// </summary>
        /// <param name="tracking">Whether to track entities</param>
        /// <param name="ignoreQueryFilters">Whether to ignore query filters</param>
        /// <returns>entity</returns>
        TEntity Max(bool? tracking = null, bool ignoreQueryFilters = false);

        /// <summary>
        /// 根据expression查看最大值
        /// </summary>
        /// <typeparam name="TResult">Maximum type</typeparam>
        /// <param name="predicate">expression</param>
        /// <param name="tracking">Whether to track entities</param>
        /// <param name="ignoreQueryFilters">Whether to ignore query filters</param>
        /// <returns>TResult</returns>
        TResult Max<TResult>(Expression<Func<TEntity, TResult>> predicate, bool? tracking = null,
            bool ignoreQueryFilters = false);

        /// <summary>
        /// View max records
        /// </summary>
        /// <param name="tracking">Whether to track entities</param>
        /// <param name="ignoreQueryFilters">Whether to ignore query filters</param>
        /// <param name="cancellationToken">Cancel async token</param>
        /// <returns>entity</returns>
        Task<TEntity> MaxAsync(bool? tracking = null, bool ignoreQueryFilters = false,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// 根据expression查看最大值
        /// </summary>
        /// <typeparam name="TResult">Maximum type</typeparam>
        /// <param name="predicate">expression</param>
        /// <param name="tracking">Whether to track entities</param>
        /// <param name="ignoreQueryFilters">Whether to ignore query filters</param>
        /// <param name="cancellationToken">Cancel async token</param>
        /// <returns>TResult</returns>
        Task<TResult> MaxAsync<TResult>(Expression<Func<TEntity, TResult>> predicate, bool? tracking = null,
            bool ignoreQueryFilters = false, CancellationToken cancellationToken = default);

        /// <summary>
        /// Build a query analyzer
        /// </summary>
        /// <param name="tracking">Whether to track entities</param>
        /// <returns>IQueryable{TEntity}</returns>
        IQueryable<TEntity> AsQueryable(bool? tracking = null);

        /// <summary>
        /// Build a query analyzer
        /// </summary>
        /// <param name="predicate">expression</param>
        /// <param name="tracking">Whether to track entities</param>
        /// <param name="ignoreQueryFilters">Whether to ignore query filters</param>
        /// <returns>IQueryable{TEntity}</returns>
        IQueryable<TEntity> AsQueryable(Expression<Func<TEntity, bool>> predicate, bool? tracking = null,
            bool ignoreQueryFilters = false);

        /// <summary>
        /// Build a query analyzer
        /// </summary>
        /// <param name="predicate">expression</param>
        /// <param name="tracking">Whether to track entities</param>
        /// <param name="ignoreQueryFilters">Whether to ignore query filters</param>
        /// <returns>IQueryable{TEntity}</returns>
        IQueryable<TEntity> AsQueryable(Expression<Func<TEntity, int, bool>> predicate, bool? tracking = null,
            bool ignoreQueryFilters = false);

        /// <summary>
        /// Return database results directly
        /// </summary>
        /// <param name="tracking">Whether to track entities</param>
        /// <returns>IEnumerable{TEntity}</returns>
        IEnumerable<TEntity> AsEnumerable(bool? tracking = null);

        /// <summary>
        /// Return database results directly
        /// </summary>
        /// <param name="predicate">expression</param>
        /// <param name="tracking">Whether to track entities</param>
        /// <param name="ignoreQueryFilters">Whether to ignore query filters</param>
        /// <returns>List{TEntity}</returns>
        IEnumerable<TEntity> AsEnumerable(Expression<Func<TEntity, bool>> predicate, bool? tracking = null,
            bool ignoreQueryFilters = false);

        /// <summary>
        /// Return database results directly
        /// </summary>
        /// <param name="predicate">expression</param>
        /// <param name="tracking">Whether to track entities</param>
        /// <param name="ignoreQueryFilters">Whether to ignore query filters</param>
        /// <returns>IEnumerable{TEntity}</returns>
        IEnumerable<TEntity> AsEnumerable(Expression<Func<TEntity, int, bool>> predicate, bool? tracking = null,
            bool ignoreQueryFilters = false);

        /// <summary>
        /// Return database results directly
        /// </summary>
        /// <param name="tracking">Whether to track entities</param>
        /// <returns>List{TEntity}</returns>
        IAsyncEnumerable<TEntity> AsAsyncEnumerable(bool? tracking = null);

        /// <summary>
        /// Return database results directly
        /// </summary>
        /// <param name="predicate">expression</param>
        /// <param name="tracking">Whether to track entities</param>
        /// <param name="ignoreQueryFilters">Whether to ignore query filters</param>
        /// <returns>IAsyncEnumerable{TEntity}</returns>
        IAsyncEnumerable<TEntity> AsAsyncEnumerable(Expression<Func<TEntity, bool>> predicate, bool? tracking = null,
            bool ignoreQueryFilters = false);

        /// <summary>
        /// Return database results directly
        /// </summary>
        /// <param name="predicate">expression</param>
        /// <param name="tracking">Whether to track entities</param>
        /// <param name="ignoreQueryFilters">Whether to ignore query filters</param>
        /// <returns>IAsyncEnumerable{TEntity}</returns>
        IAsyncEnumerable<TEntity> AsAsyncEnumerable(Expression<Func<TEntity, int, bool>> predicate,
            bool? tracking = null, bool ignoreQueryFilters = false);

        /// <summary>
        /// implement Sql return IQueryable
        /// </summary>
        /// <param name="sql">sql statement</param>
        /// <param name="parameters">Command parameters</param>
        /// <returns>IQueryable{TEntity}</returns>
        IQueryable<TEntity> FromSqlRaw(string sql, params object[] parameters);

        /// <summary>
        /// implement Sql return IQueryable
        /// </summary>
        /// <remarks>
        /// Supports string interpolation syntax
        /// </remarks>
        /// <param name="sql">sql statement</param>
        /// <returns>IQueryable{TEntity}</returns>
        IQueryable<TEntity> FromSqlInterpolated(FormattableString sql);
    }
}