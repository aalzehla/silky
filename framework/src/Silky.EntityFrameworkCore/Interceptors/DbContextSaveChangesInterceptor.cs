using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Silky.EntityFrameworkCore.Interceptors
{
    /// <summary>
    /// Database context commit interceptor
    /// </summary>
    public class DbContextSaveChangesInterceptor : SaveChangesInterceptor
    {
        /// <summary>
        /// Intercept before saving the database
        /// </summary>
        /// <param name="eventData"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        public override InterceptionResult<int> SavingChanges(DbContextEventData eventData,
            InterceptionResult<int> result)
        {
            dynamic dbContext = eventData.Context;
            dbContext.SavingChangesEventInner(eventData, result);

            return base.SavingChanges(eventData, result);
        }

        /// <summary>
        /// Intercept before saving the database
        /// </summary>
        /// <param name="eventData"></param>
        /// <param name="result"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData,
            InterceptionResult<int> result, CancellationToken cancellationToken = default)
        {
            dynamic dbContext = eventData.Context;
            dbContext.SavingChangesEventInner(eventData, result);

            return base.SavingChangesAsync(eventData, result, cancellationToken);
        }

        /// <summary>
        /// Intercept save database successfully
        /// </summary>
        /// <param name="eventData"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        public override int SavedChanges(SaveChangesCompletedEventData eventData, int result)
        {
            dynamic dbContext = eventData.Context;
            dbContext.SavedChangesEventInner(eventData, result);

            return base.SavedChanges(eventData, result);
        }

        /// <summary>
        /// Intercept save database successfully
        /// </summary>
        /// <param name="eventData"></param>
        /// <param name="result"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public override ValueTask<int> SavedChangesAsync(SaveChangesCompletedEventData eventData, int result,
            CancellationToken cancellationToken = default)
        {
            dynamic dbContext = eventData.Context;
            dbContext.SavedChangesEventInner(eventData, result);

            return base.SavedChangesAsync(eventData, result, cancellationToken);
        }

        /// <summary>
        /// Failed to intercept save database
        /// </summary>
        /// <param name="eventData"></param>
        public override void SaveChangesFailed(DbContextErrorEventData eventData)
        {
            dynamic dbContext = eventData.Context;
            dbContext.SaveChangesFailedEventInner(eventData);

            base.SaveChangesFailed(eventData);
        }

        /// <summary>
        /// Failed to intercept save database
        /// </summary>
        /// <param name="eventData"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public override Task SaveChangesFailedAsync(DbContextErrorEventData eventData,
            CancellationToken cancellationToken = default)
        {
            dynamic dbContext = eventData.Context;
            dbContext.SaveChangesFailedEventInner(eventData);

            return base.SaveChangesFailedAsync(eventData, cancellationToken);
        }
    }
}