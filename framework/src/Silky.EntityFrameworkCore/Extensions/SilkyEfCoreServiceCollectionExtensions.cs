using System;
using Microsoft.EntityFrameworkCore;
using Silky.Core.DbContext;
using Silky.Core.DependencyInjection;
using Silky.EntityFrameworkCore;
using Silky.EntityFrameworkCore.ContextPool;
using Silky.EntityFrameworkCore.Contexts.Dynamic;
using Silky.EntityFrameworkCore.Contexts.Enums;
using Silky.EntityFrameworkCore.Extensions.DatabaseProvider;
using Silky.EntityFrameworkCore.Repositories;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class SilkyEfCoreServiceCollectionExtensions
    {
        public static IServiceCollection AddDatabaseAccessor(
            this IServiceCollection services,
            Action<IServiceCollection> configure = null, string migrationAssemblyName = default)
        {
            // Set the migration class library name
            if (!string.IsNullOrWhiteSpace(migrationAssemblyName)) Db.MigrationAssemblyName = migrationAssemblyName;

            configure?.Invoke(services);

            // Register database context pool
            services.AddScoped<ISilkyDbContextPool, EfCoreDbContextPool>();

            // register Sql Warehousing
            services.AddScoped(typeof(ISqlRepository<>), typeof(SqlRepository<>));

            // register Sql 非泛型Warehousing
            services.AddScoped<ISqlRepository, SqlRepository>();

            // register多数据库上下文Warehousing
            services.AddScoped(typeof(IRepository<,>), typeof(EFCoreRepository<,>));

            // register泛型Warehousing
            services.AddScoped(typeof(IRepository<>), typeof(EFCoreRepository<>));

            // register主从库Warehousing
            services.AddScoped(typeof(IMSRepository), typeof(MSRepository));
            services.AddScoped(typeof(IMSRepository<>), typeof(MSRepository<>));
            services.AddScoped(typeof(IMSRepository<,>), typeof(MSRepository<,>));
            services.AddScoped(typeof(IMSRepository<,,>), typeof(MSRepository<,,>));
            services.AddScoped(typeof(IMSRepository<,,,>), typeof(MSRepository<,,,>));
            services.AddScoped(typeof(IMSRepository<,,,,>), typeof(MSRepository<,,,,>));
            services.AddScoped(typeof(IMSRepository<,,,,,>), typeof(MSRepository<,,,,,>));
            services.AddScoped(typeof(IMSRepository<,,,,,,>), typeof(MSRepository<,,,,,,>));
            services.AddScoped(typeof(IMSRepository<,,,,,,,>), typeof(MSRepository<,,,,,,,>));

            // register非泛型Warehousing
            services.AddScoped<IRepository, EFCoreRepository>();

            // register多数据库Warehousing
            services.AddScoped(typeof(IDbRepository<>), typeof(DbRepository<>));

            // Parse the database context
            services.AddTransient(provider =>
            {
                DbContext dbContextResolve(Type locator, ITransientDependency transient)
                {
                    return ResolveDbContext(provider, locator);
                }

                return (Func<Type, ITransientDependency, DbContext>)dbContextResolve;
            });

            services.AddScoped(provider =>
            {
                DbContext dbContextResolve(Type locator, IScopedDependency transient)
                {
                    return ResolveDbContext(provider, locator);
                }

                return (Func<Type, IScopedDependency, DbContext>)dbContextResolve;
            });
            return services;
        }

        /// <summary>
        /// Parse context by locator
        /// </summary>
        /// <param name="provider"></param>
        /// <param name="locator"></param>
        /// <returns></returns>
        private static DbContext ResolveDbContext(IServiceProvider provider, Type locator)
        {
            // Determine whether the locator is bound to the database context
            var isRegistered = Penetrates.DbContextWithLocatorCached.TryGetValue(locator, out var dbContextType);
            if (!isRegistered)
                throw new InvalidOperationException(
                    $"The DbContext for locator `{locator.FullName}` binding was not found.");

            // 动态Parse the database context
            var dbContext = provider.GetService(dbContextType) as DbContext;

            // Implement dynamic database context function，refresh OnModelCreating
            var dbContextAttribute = DbProvider.GetAppDbContextAttribute(dbContextType);
            if (dbContextAttribute?.Mode == DbContextMode.Dynamic)
            {
                DynamicModelCacheKeyFactory.RebuildModels();
            }

            // Add database context to pool
            var dbContextPool = provider.GetService<ISilkyDbContextPool>() as EfCoreDbContextPool;
            dbContextPool?.AddToPool(dbContext);

            return dbContext;
        }

        /// <summary>
        /// Start custom tenant type
        /// </summary>
        /// <param name="services"></param>
        /// <param name="onTableTenantId">table based multi-tenancyIdname</param>
        /// <returns></returns>
        public static IServiceCollection CustomizeMultiTenants(this IServiceCollection services,
            string onTableTenantId = default)
        {
            Db.CustomizeMultiTenants = true;
            if (!string.IsNullOrWhiteSpace(onTableTenantId)) Db.OnTableTenantId = onTableTenantId;

            return services;
        }
    }
}