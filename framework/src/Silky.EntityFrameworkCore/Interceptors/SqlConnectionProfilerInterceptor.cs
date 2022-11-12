using System.Data.Common;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Silky.Core;
using Silky.Core.Configuration;

namespace Silky.EntityFrameworkCore.Interceptors
{
    /// <summary>
    /// Database Connection Interception Analyzer
    /// </summary>
    internal sealed class SqlConnectionProfilerInterceptor : DbConnectionInterceptor
    {
        /// <summary>
        /// MiniProfiler Category name
        /// </summary>
        private const string MiniProfilerCategory = "connection";


        /// <summary>
        /// Intercept database connections
        /// </summary>
        /// <param name="connection">database connection object</param>
        /// <param name="eventData">Database connection event data</param>
        /// <param name="result">Intercept result</param>
        /// <returns></returns>
        public override InterceptionResult ConnectionOpening(DbConnection connection, ConnectionEventData eventData,
            InterceptionResult result)
        {
            return base.ConnectionOpening(connection, eventData, result);
        }

        /// <summary>
        /// Intercept database connections
        /// </summary>
        /// <param name="connection">database connection object</param>
        /// <param name="eventData">Database connection event data</param>
        /// <param name="result">Interceptor result</param>
        /// <param name="cancellationToken">Cancel asyncToken</param>
        /// <returns></returns>
        public override ValueTask<InterceptionResult> ConnectionOpeningAsync(DbConnection connection,
            ConnectionEventData eventData, InterceptionResult result, CancellationToken cancellationToken = default)
        {
            return base.ConnectionOpeningAsync(connection, eventData, result, cancellationToken);
        }
    }
}