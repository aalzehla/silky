using System.Threading;
using System.Threading.Tasks;
using Silky.EntityFrameworkCore.Entities;
using Silky.EntityFrameworkCore.Locators;

namespace Silky.EntityFrameworkCore.Repositories
{
    /// <summary>
    /// Writable repository interface
    /// </summary>
    /// <typeparam name="TEntity">entity type</typeparam>
    public partial interface IWritableRepository<TEntity> : IWritableRepository<TEntity, MasterDbContextLocator>
        where TEntity : class, IPrivateEntity, new()
    {
    }

    /// <summary>
    /// Writable repository interface
    /// </summary>
    /// <typeparam name="TEntity">entity type</typeparam>
    /// <typeparam name="TDbContextLocator">database context locator</typeparam>
    public partial interface IWritableRepository<TEntity, TDbContextLocator> : IPrivateWritableRepository<TEntity>
        , IInsertableRepository<TEntity>
        , IUpdateableRepository<TEntity>
        , IDeletableRepository<TEntity>
        where TEntity : class, IPrivateEntity, new()
        where TDbContextLocator : class, IDbContextLocator
    {
    }

    /// <summary>
    /// Writable repository interface
    /// </summary>
    /// <typeparam name="TEntity">entity type</typeparam>
    public partial interface IPrivateWritableRepository<TEntity>
        : IPrivateInsertableRepository<TEntity>
            , IPrivateUpdateableRepository<TEntity>
            , IPrivateDeletableRepository<TEntity>
            , IPrivateRootRepository
        where TEntity : class, IPrivateEntity, new()
    {
        /// <summary>
        /// accept all changes
        /// </summary>
        void AcceptAllChanges();

        /// <summary>
        /// Save all changed database contexts in the database context pool
        /// </summary>
        /// <returns></returns>
        int SavePoolNow();

        /// <summary>
        /// Save all changed database contexts in the database context pool
        /// </summary>
        /// <param name="acceptAllChangesOnSuccess"></param>
        /// <returns></returns>
        int SavePoolNow(bool acceptAllChangesOnSuccess);

        /// <summary>
        /// Save all changed database contexts in the database context pool
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<int> SavePoolNowAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Save all changed database contexts in the database context pool
        /// </summary>
        /// <param name="acceptAllChangesOnSuccess"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<int> SavePoolNowAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default);

        /// <summary>
        /// Commit changes
        /// </summary>
        /// <returns></returns>
        int SaveNow();

        /// <summary>
        /// Commit changes
        /// </summary>
        /// <param name="acceptAllChangesOnSuccess"></param>
        /// <returns></returns>
        int SaveNow(bool acceptAllChangesOnSuccess);

        /// <summary>
        /// Commit changes（asynchronous）
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<int> SaveNowAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Commit changes（asynchronous）
        /// </summary>
        /// <param name="acceptAllChangesOnSuccess"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<int> SaveNowAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default);
    }
}