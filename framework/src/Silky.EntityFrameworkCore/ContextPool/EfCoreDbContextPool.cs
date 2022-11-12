using System;
using System.Collections.Concurrent;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using Silky.Core.DbContext;
using Silky.EntityFrameworkCore.Extensions.DatabaseProvider;

namespace Silky.EntityFrameworkCore.ContextPool
{
    public class EfCoreDbContextPool : ISilkyDbContextPool
    {
        /// <summary>
        /// A collection of thread-safe database contexts
        /// </summary>
        private readonly ConcurrentDictionary<Guid, DbContext> dbContexts;

        /// <summary>
        /// Registering the wrong database context
        /// </summary>
        private readonly ConcurrentDictionary<Guid, DbContext> failedDbContexts;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="serviceProvider"></param>
        public EfCoreDbContextPool()
        {
            dbContexts = new ConcurrentDictionary<Guid, DbContext>();
            failedDbContexts = new ConcurrentDictionary<Guid, DbContext>();
        }

        /// <summary>
        /// database context transaction
        /// </summary>
        public IDbContextTransaction DbContextTransaction { get; private set; }

        /// <summary>
        /// Get all database contexts
        /// </summary>
        /// <returns></returns>
        public ConcurrentDictionary<Guid, DbContext> GetDbContexts()
        {
            return dbContexts;
        }

        /// <summary>
        /// save database context
        /// </summary>
        /// <param name="dbContext"></param>
        public void AddToPool(DbContext dbContext)
        {
            var instanceId = dbContext.ContextId.InstanceId;

            var canAdd = dbContexts.TryAdd(instanceId, dbContext);
            if (canAdd)
            {
                // Subscribe to database context operation failure events
                dbContext.SaveChangesFailed += (s, e) =>
                {
                    // Exclude existing database contexts
                    var canAdd = failedDbContexts.TryAdd(instanceId, dbContext);
                    if (canAdd)
                    {
                        dynamic context = s as DbContext;

                        // current affairs
                        var database = context.Database as DatabaseFacade;
                        var currentTransaction = database?.CurrentTransaction;
                        if (currentTransaction != null && context.FailedAutoRollback == true)
                        {
                            // Get database connection information
                            var connection = database.GetDbConnection();

                            // rollback transaction
                            currentTransaction.Rollback();
                        }
                    }
                };
            }
        }

        /// <summary>
        /// save database context（asynchronous）
        /// </summary>
        /// <param name="dbContext"></param>
        public Task AddToPoolAsync(DbContext dbContext)
        {
            AddToPool(dbContext);
            return Task.CompletedTask;
        }

        public void EnsureDbContextAddToPools()
        {
            if (dbContexts.Any()) return;
            var locators = Penetrates.DbContextWithLocatorCached;
            foreach (var locator in locators)
            {
                var dbContext = Db.GetDbContext(locator.Key);
                if (!dbContexts.Values.Contains(dbContext))
                {
                    AddToPool(dbContext);
                }
            }
        }

        /// <summary>
        /// save database context池中所有已更改的数据库上下文
        /// </summary>
        /// <returns></returns>
        public int SavePoolNow()
        {
            // Find all changed database contexts and save changes
            return dbContexts
                .Where(u => u.Value != null && u.Value.ChangeTracker.HasChanges() && !failedDbContexts.Contains(u))
                .Select(u => u.Value.SaveChanges()).Count();
        }

        /// <summary>
        /// save database context池中所有已更改的数据库上下文
        /// </summary>
        /// <param name="acceptAllChangesOnSuccess"></param>
        /// <returns></returns>
        public int SavePoolNow(bool acceptAllChangesOnSuccess)
        {
            // Find all changed database contexts and save changes
            return dbContexts
                .Where(u => u.Value != null && u.Value.ChangeTracker.HasChanges() && !failedDbContexts.Contains(u))
                .Select(u => u.Value.SaveChanges(acceptAllChangesOnSuccess)).Count();
        }

        /// <summary>
        /// save database context池中所有已更改的数据库上下文
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<int> SavePoolNowAsync(CancellationToken cancellationToken = default)
        {
            // Find all changed database contexts and save changes
            var tasks = dbContexts
                .Where(u => u.Value != null && u.Value.ChangeTracker.HasChanges() && !failedDbContexts.Contains(u))
                .Select(u => u.Value.SaveChangesAsync(cancellationToken));

            // 等待所有asynchronous完成
            var results = await Task.WhenAll(tasks);
            return results.Length;
        }

        /// <summary>
        /// save database context池中所有已更改的数据库上下文（asynchronous）
        /// </summary>
        /// <param name="acceptAllChangesOnSuccess"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<int> SavePoolNowAsync(bool acceptAllChangesOnSuccess,
            CancellationToken cancellationToken = default)
        {
            // Find all changed database contexts and save changes
            var tasks = dbContexts
                .Where(u => u.Value != null && u.Value.ChangeTracker.HasChanges() && !failedDbContexts.Contains(u))
                .Select(u => u.Value.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken));

            // 等待所有asynchronous完成
            var results = await Task.WhenAll(tasks);
            return results.Length;
        }

        /// <summary>
        /// open transaction
        /// </summary>
        /// <param name="ensureTransaction"></param>
        /// <returns></returns>
        public void BeginTransaction(bool ensureTransaction = false)
        {
            // judge dbContextPool does it containDbContext，in the case of，then use the first database context to start the transaction，and applied to other database contexts
            EnsureTransaction:
            if (dbContexts.Any())
            {
                // if the shared transaction is not null，share directly
                if (DbContextTransaction != null) goto ShareTransaction;

                // 先judge是否已经有上下文开启了事务
                var transactionDbContext = dbContexts.FirstOrDefault(u => u.Value.Database.CurrentTransaction != null);
                if (transactionDbContext.Value != null)
                {
                    DbContextTransaction = transactionDbContext.Value.Database.CurrentTransaction;
                }
                else
                {
                    // If there is no context there is a transaction，will be the first to open the transaction
                    DbContextTransaction = dbContexts.First().Value.Database.BeginTransaction();
                }

                // shared transaction
                ShareTransaction:
                ShareTransaction(DbContextTransaction.GetDbTransaction());
            }
            else
            {
                // judge是否确保事务强制可用（Here is helpless）
                if (ensureTransaction)
                {
                    var defaultDbContextLocator = Penetrates.DbContextWithLocatorCached.LastOrDefault();
                    if (defaultDbContextLocator.Key == null) return;

                    // create a new context
                    var newDbContext = Db.GetDbContext(defaultDbContextLocator.Key);
                    if (!dbContexts.Any())
                    {
                        AddToPool(newDbContext);
                    }

                    goto EnsureTransaction;
                }
            }
        }

        /// <summary>
        /// commit transaction
        /// </summary>
        /// <param name="isManualSaveChanges"></param>
        /// <param name="exception"></param>
        /// <param name="withCloseAll">Whether to automatically close all connections</param>
        public void CommitTransaction(bool isManualSaveChanges = true, Exception exception = default,
            bool withCloseAll = false)
        {
            // judge是否异常
            if (exception == null)
            {
                try
                {
                    // Modify all database contexts SaveChanges();，这里另外judge是否需要手动提交
                    var hasChangesCount = !isManualSaveChanges ? SavePoolNow() : 0;

                    // if transaction is empty，Then close the connection after execution
                    if (DbContextTransaction == null) goto CloseAll;

                    // 提交shared transaction
                    DbContextTransaction?.Commit();
                }
                catch
                {
                    // rollback transaction
                    if (DbContextTransaction?.GetDbTransaction()?.Connection != null) DbContextTransaction?.Rollback();

                    throw;
                }
                finally
                {
                    if (DbContextTransaction?.GetDbTransaction() != null)
                    {
                        DbContextTransaction?.Dispose();
                        DbContextTransaction = null;
                    }
                }
            }
            else
            {
                // rollback transaction
                if (DbContextTransaction?.GetDbTransaction() != null) DbContextTransaction?.Rollback();
                DbContextTransaction?.Dispose();
                DbContextTransaction = null;
            }

            // close all connections
            CloseAll:
            if (withCloseAll) CloseAll();
        }

        /// <summary>
        /// release all database contexts
        /// </summary>
        public void CloseAll()
        {
            if (!dbContexts.Any()) return;

            foreach (var item in dbContexts)
            {
                var conn = item.Value.Database.GetDbConnection();
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
        }

        /// <summary>
        /// 设置数据库上下文shared transaction
        /// </summary>
        /// <param name="transaction"></param>
        /// <returns></returns>
        private void ShareTransaction(DbTransaction transaction)
        {
            // 跳过第一个数据库上下文并设置shared transaction
            _ = dbContexts
                .Where(u => u.Value != null && u.Value.Database.CurrentTransaction == null)
                .Select(u => u.Value.Database.UseTransaction(transaction));
        }
    }
}