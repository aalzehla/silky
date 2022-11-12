using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Silky.EntityFrameworkCore.Locators;

namespace Silky.EntityFrameworkCore.Repositories
{
    /// <summary>
    /// Sql Operation warehouse interface
    /// </summary>
    public interface ISqlRepository : ISqlRepository<MasterDbContextLocator>
        , ISqlExecutableRepository
        , ISqlReaderRepository
    {
    }

    /// <summary>
    /// Sql Operation warehouse interface
    /// </summary>
    /// <typeparam name="TDbContextLocator">database context locator</typeparam>
    public interface ISqlRepository<TDbContextLocator> : IPrivateSqlRepository
        , ISqlExecutableRepository<TDbContextLocator>
        , ISqlReaderRepository<TDbContextLocator>
        where TDbContextLocator : class, IDbContextLocator
    {
    }

    /// <summary>
    /// private Sql Warehousing
    /// </summary>
    public interface IPrivateSqlRepository : IPrivateSqlExecutableRepository
        , IPrivateSqlReaderRepository
        , IPrivateRootRepository
    {
        /// <summary>
        /// database operation object
        /// </summary>
        DatabaseFacade Database { get; }

        /// <summary>
        /// database context
        /// </summary>
        DbContext Context { get; }

        /// <summary>
        /// 动态database context
        /// </summary>
        dynamic DynamicContext { get; }

        /// <summary>
        /// 切换Warehousing
        /// </summary>
        /// <typeparam name="TChangeDbContextLocator">database context locator</typeparam>
        /// <returns>Warehousing</returns>
        ISqlRepository<TChangeDbContextLocator> Change<TChangeDbContextLocator>()
            where TChangeDbContextLocator : class, IDbContextLocator;

        /// <summary>
        /// Parse service
        /// </summary>
        /// <typeparam name="TService"></typeparam>
        /// <returns></returns>
        TService GetService<TService>()
            where TService : class;

        /// <summary>
        /// Parse service
        /// </summary>
        /// <typeparam name="TService"></typeparam>
        /// <returns></returns>
        TService GetRequiredService<TService>()
            where TService : class;

        /// <summary>
        /// 将Warehousing约束为特定Warehousing
        /// </summary>
        /// <typeparam name="TRestrainRepository">特定Warehousing</typeparam>
        /// <returns>TRestrainRepository</returns>
        TRestrainRepository Constraint<TRestrainRepository>()
            where TRestrainRepository : class, IPrivateRootRepository;

        /// <summary>
        /// ensure unit of work（affairs）available
        /// </summary>
        void EnsureTransaction();
    }
}