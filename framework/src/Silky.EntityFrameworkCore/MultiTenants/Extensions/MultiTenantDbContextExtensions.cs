using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Silky.Core;
using Silky.Core.Runtime.Session;
using Silky.Rpc.Runtime.Server;

namespace Silky.EntityFrameworkCore.MultiTenants.Extensions
{
    /// <summary>
    /// Multi-tenant database context extension
    /// </summary>
    public static class MultiTenantDbContextExtensions
    {
        /// <summary>
        /// Flush the multi-tenant cache
        /// </summary>
        /// <param name="dbContext"></param>
        public static void RefreshTenantCache(this DbContext dbContext)
        {
            var silkySession = NullSession.Instance;

            // cached Key
            var tenantCachedKey = $"MULTI_TENANT:{silkySession.TenantId}";

            // Remove multi-tenancy information from memcache
            var distributedCache = EngineContext.Current.Resolve<IDistributedCache>();
            distributedCache.Remove(tenantCachedKey);
        }
    }
}