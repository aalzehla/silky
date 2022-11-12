using System;
using Microsoft.EntityFrameworkCore;
using Silky.Core;
using Silky.Core.DependencyInjection;
using Silky.EntityFrameworkCore.Entities;
using Silky.EntityFrameworkCore.Extensions.DatabaseProvider;
using Silky.EntityFrameworkCore.Locators;

namespace Silky.EntityFrameworkCore
{
    public static class Db
    {
        internal static string MigrationAssemblyName { get; set; }
        internal static bool CustomizeMultiTenants { get; set; }

        /// <summary>
        /// Table-based multi-tenant foreign key names
        /// </summary>
        internal static string OnTableTenantId = "TenantId";

        public static DbContext GetDbContext(Type dbContextLocator)
        {
            // Determine whether the database context is registered
            if (!Penetrates.DbContextWithLocatorCached.ContainsKey(dbContextLocator)) return default;

            var dbContextResolve = EngineContext.Current.Resolve<Func<Type, IScopedDependency, DbContext>>();
            return dbContextResolve(dbContextLocator, default);
        }

        public static DbContext GetMasterDbContext()
        {
            return GetDbContext(typeof(MasterDbContextLocator));
        }

        public static DbContext GetDbContext<TDbContextLocator>()
            where TDbContextLocator : class, IDbContextLocator
        {
            return GetDbContext(typeof(TDbContextLocator));
        }
    }
}