using System.Threading;
using System.Threading.Tasks;
using Silky.EntityFrameworkCore.Entities;

namespace Silky.EntityFrameworkCore.Repositories
{
    /// <summary>
    /// Writable repository subclass
    /// </summary>
    public partial class PrivateRepository<TEntity>
        where TEntity : class, IPrivateEntity, new()
    {
        /// <summary>
        /// accept all changes
        /// </summary>
        public virtual void AcceptAllChanges()
        {
            ChangeTracker.AcceptAllChanges();
        }

        /// <summary>
        /// Save all changed database contexts in the database context pool
        /// </summary>
        /// <returns></returns>
        public int SavePoolNow()
        {
            return _silkyDbContextPool.SavePoolNow();
        }

        /// <summary>
        /// Save all changed database contexts in the database context pool
        /// </summary>
        /// <param name="acceptAllChangesOnSuccess"></param>
        /// <returns></returns>
        public int SavePoolNow(bool acceptAllChangesOnSuccess)
        {
            return _silkyDbContextPool.SavePoolNow(acceptAllChangesOnSuccess);
        }

        /// <summary>
        /// Save all changed database contexts in the database context pool
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task<int> SavePoolNowAsync(CancellationToken cancellationToken = default)
        {
            return _silkyDbContextPool.SavePoolNowAsync(cancellationToken);
        }

        /// <summary>
        /// Save all changed database contexts in the database context pool
        /// </summary>
        /// <param name="acceptAllChangesOnSuccess"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task<int> SavePoolNowAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            return _silkyDbContextPool.SavePoolNowAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        /// <summary>
        /// Commit changes
        /// </summary>
        /// <returns></returns>
        public virtual int SaveNow()
        {
            return Context.SaveChanges();
        }

        /// <summary>
        /// Commit changes
        /// </summary>
        /// <param name="acceptAllChangesOnSuccess"></param>
        /// <returns></returns>
        public virtual int SaveNow(bool acceptAllChangesOnSuccess)
        {
            return Context.SaveChanges(acceptAllChangesOnSuccess);
        }

        /// <summary>
        /// Commit changes（asynchronous）
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual Task<int> SaveNowAsync(CancellationToken cancellationToken = default)
        {
            return Context.SaveChangesAsync(cancellationToken);
        }

        /// <summary>
        /// Commit changes（asynchronous）
        /// </summary>
        /// <param name="acceptAllChangesOnSuccess"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual Task<int> SaveNowAsync(bool acceptAllChangesOnSuccess,
            CancellationToken cancellationToken = default)
        {
            return Context.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }
    }
}