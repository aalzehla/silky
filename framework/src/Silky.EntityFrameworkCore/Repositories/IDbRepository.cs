using Silky.EntityFrameworkCore.Entities;
using Silky.EntityFrameworkCore.Locators;

namespace Silky.EntityFrameworkCore.Repositories
{
    /// <summary>
    /// Multi-database warehousing
    /// </summary>
    /// <typeparam name="TDbContextLocator"></typeparam>
    public partial interface IDbRepository<TDbContextLocator>
        where TDbContextLocator : class, IDbContextLocator
    {
        /// <summary>
        /// switch storage
        /// </summary>
        /// <typeparam name="TEntity">entity type</typeparam>
        /// <returns>Warehousing</returns>
        IRepository<TEntity, TDbContextLocator> Change<TEntity>()
            where TEntity : class, IPrivateEntity, new();

        /// <summary>
        /// Obtain Sql 操作Warehousing
        /// </summary>
        /// <returns></returns>
        ISqlRepository<TDbContextLocator> Sql();
    }
}