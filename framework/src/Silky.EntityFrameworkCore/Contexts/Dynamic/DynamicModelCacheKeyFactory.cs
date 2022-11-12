using System.Threading;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Silky.EntityFrameworkCore.Contexts.Dynamic
{
    public class DynamicModelCacheKeyFactory : IModelCacheKeyFactory
    {
        /// <summary>
        /// Dynamic Model CachingKey
        /// </summary>
        private static int cacheKey;

        /// <summary>
        /// Rewrite the build model
        /// </summary>
        /// <remarks>This method needs to be called after dynamically switching the table</remarks>
        public static void RebuildModels()
        {
            Interlocked.Increment(ref cacheKey);
        }

        /// <summary>
        /// Update model cache
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public object Create(DbContext context)
        {
            return (context.GetType(), cacheKey);
        }

        public object Create(DbContext context, bool designTime)
        {
            return (context.GetType(), cacheKey);
        }
    }
}