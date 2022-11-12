using System;
using System.Collections.Concurrent;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Hosting;
using Silky.Core;

namespace Silky.EntityFrameworkCore.Extensions.DatabaseProvider
{
    /// <summary>
    /// constant、public method configuration class
    /// </summary>
    internal static class Penetrates
    {
        /// <summary>
        /// Database context and locator cache
        /// </summary>
        internal static readonly ConcurrentDictionary<Type, Type> DbContextWithLocatorCached;

        /// <summary>
        /// Database context locator cache
        /// </summary>
        internal static readonly ConcurrentDictionary<string, Type> DbContextLocatorTypeCached;

        /// <summary>
        /// Constructor
        /// </summary>
        static Penetrates()
        {
            DbContextWithLocatorCached = new ConcurrentDictionary<Type, Type>();
            DbContextLocatorTypeCached = new ConcurrentDictionary<string, Type>();
        }

        /// <summary>
        /// configure SqlServer database context
        /// </summary>
        /// <param name="optionBuilder">database context选项构建器</param>
        /// <param name="interceptors">interceptor</param>
        /// <returns></returns>
        internal static Action<IServiceProvider, DbContextOptionsBuilder> ConfigureDbContext(
            Action<DbContextOptionsBuilder> optionBuilder, params IInterceptor[] interceptors)
        {
            return (serviceProvider, options) =>
            {
                // Only the development environment is enabled
                if (EngineContext.Current.HostEnvironment.IsDevelopment())
                {
                    options /*.UseLazyLoadingProxies()*/
                        .EnableDetailedErrors()
                        .EnableSensitiveDataLogging();
                }

                optionBuilder.Invoke(options);

                // 添加interceptor
                AddInterceptors(interceptors, options);

                // .NET 5 version no longer works
                // options.UseInternalServiceProvider(serviceProvider);
            };
        }

        /// <summary>
        /// 数据库数据库interceptor
        /// </summary>
        /// <param name="interceptors">interceptor</param>
        /// <param name="options"></param>
        private static void AddInterceptors(IInterceptor[] interceptors, DbContextOptionsBuilder options)
        {
            // 添加interceptor
            var interceptorList = DbProvider.GetDefaultInterceptors();

            if (interceptors != null || interceptors.Length > 0)
            {
                interceptorList.AddRange(interceptors);
            }

            options.AddInterceptors(interceptorList.ToArray());
        }
    }
}