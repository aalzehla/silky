using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Silky.Core;
using Silky.Core.DbContext;
using Silky.Core.DependencyInjection;
using Silky.EntityFrameworkCore.ContextPool;
using Silky.EntityFrameworkCore.Locators;

namespace Silky.EntityFrameworkCore.Repositories
{
    /// <summary>
    /// Sql Operational warehouse implementation
    /// </summary>
    public partial class SqlRepository : SqlRepository<MasterDbContextLocator>, ISqlRepository
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="serviceProvider">service provider</param>
        public SqlRepository(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }
    }

    /// <summary>
    /// Sql Operational warehouse implementation
    /// </summary>
    public partial class SqlRepository<TDbContextLocator> : PrivateSqlRepository, ISqlRepository<TDbContextLocator>
        where TDbContextLocator : class, IDbContextLocator
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public SqlRepository(IServiceProvider serviceProvider) : base(typeof(TDbContextLocator))
        {
        }
    }

    /// <summary>
    /// private Sql Warehousing
    /// </summary>
    public partial class PrivateSqlRepository : IPrivateSqlRepository
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="dbContextLocator"></param>
        public PrivateSqlRepository(Type dbContextLocator)
        {
            // Parse the database context
            var dbContextResolve = EngineContext.Current.Resolve<Func<Type, IScopedDependency, DbContext>>();
            var dbContext = dbContextResolve(dbContextLocator, default);
            DynamicContext = Context = dbContext;

            // Initialize database related data
            Database = Context.Database;
        }

        /// <summary>
        /// database context
        /// </summary>
        public virtual DbContext Context { get; }

        /// <summary>
        /// 动态database context
        /// </summary>
        public virtual dynamic DynamicContext { get; }

        /// <summary>
        /// database operation object
        /// </summary>
        public virtual DatabaseFacade Database { get; }

        /// <summary>
        /// 切换Warehousing
        /// </summary>
        /// <typeparam name="TChangeDbContextLocator">database context定位器</typeparam>
        /// <returns>Warehousing</returns>
        public virtual ISqlRepository<TChangeDbContextLocator> Change<TChangeDbContextLocator>()
            where TChangeDbContextLocator : class, IDbContextLocator
        {
            return EngineContext.Current.Resolve<ISqlRepository<TChangeDbContextLocator>>();
        }

        /// <summary>
        /// Parse service
        /// </summary>
        /// <typeparam name="TService"></typeparam>
        /// <returns></returns>
        public virtual TService GetService<TService>()
            where TService : class
        {
            return EngineContext.Current.Resolve<TService>();
        }

        /// <summary>
        /// Parse service
        /// </summary>
        /// <typeparam name="TService"></typeparam>
        /// <returns></returns>
        public virtual TService GetRequiredService<TService>()
            where TService : class
        {
            return EngineContext.Current.Resolve<TService>();
        }

        /// <summary>
        /// 将Warehousing约束为特定Warehousing
        /// </summary>
        /// <typeparam name="TRestrainRepository">特定Warehousing</typeparam>
        /// <returns>TRestrainRepository</returns>
        public virtual TRestrainRepository Constraint<TRestrainRepository>()
            where TRestrainRepository : class, IPrivateRootRepository
        {
            var type = typeof(TRestrainRepository);
            if (!type.IsInterface || typeof(IPrivateRootRepository) == type || type.Name.Equals(nameof(IRepository)) ||
                (type.IsGenericType && type.GetGenericTypeDefinition().Name.Equals(nameof(IRepository))))
            {
                throw new InvalidCastException("Invalid type conversion.");
            }

            return this as TRestrainRepository;
        }

        /// <summary>
        /// ensure unit of work（affairs）available
        /// </summary>
        public virtual void EnsureTransaction()
        {
            // 获取database context
            var dbContextPool = EngineContext.Current.Resolve<ISilkyDbContextPool>() as EfCoreDbContextPool;
            if (dbContextPool == null) return;

            // append context
            dbContextPool.AddToPool(Context);
            // 开启affairs
            dbContextPool.BeginTransaction();
        }
    }
}