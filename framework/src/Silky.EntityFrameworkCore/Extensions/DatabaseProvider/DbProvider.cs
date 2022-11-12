using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Silky.Core;
using Silky.Core.Extensions;
using Silky.EntityFrameworkCore.Contexts.Attributes;
using Silky.EntityFrameworkCore.Interceptors;
using Silky.EntityFrameworkCore.Values;

namespace Microsoft.EntityFrameworkCore
{
    public static class DbProvider
    {
        /// <summary>
        /// SqlServer provider assembly
        /// </summary>
        public const string SqlServer = "Microsoft.EntityFrameworkCore.SqlServer";

        /// <summary>
        /// Sqlite provider assembly
        /// </summary>
        public const string Sqlite = "Microsoft.EntityFrameworkCore.Sqlite";

        /// <summary>
        /// Cosmos provider assembly
        /// </summary>
        public const string Cosmos = "Microsoft.EntityFrameworkCore.Cosmos";

        /// <summary>
        /// Memory Database provider assembly
        /// </summary>
        public const string InMemoryDatabase = "Microsoft.EntityFrameworkCore.InMemory";

        /// <summary>
        /// MySql provider assembly
        /// </summary>
        public const string MySql = "Pomelo.EntityFrameworkCore.MySql";

        /// <summary>
        /// MySql official package（Update is not timely，only support 8.0.23+ Version， So make a separate category）
        /// </summary>
        public const string MySqlOfficial = "MySql.EntityFrameworkCore";

        /// <summary>
        /// PostgreSQL provider assembly
        /// </summary>
        public const string Npgsql = "Npgsql.EntityFrameworkCore.PostgreSQL";

        /// <summary>
        /// Oracle provider assembly
        /// </summary>
        public const string Oracle = "Oracle.EntityFrameworkCore";

        /// <summary>
        /// Firebird provider assembly
        /// </summary>
        public const string Firebird = "FirebirdSql.EntityFrameworkCore.Firebird";

        /// <summary>
        /// Dm provider assembly
        /// </summary>
        public const string Dm = "Microsoft.EntityFrameworkCore.Dm";

        /// <summary>
        /// Databases that do not support stored procedures
        /// </summary>
        internal static readonly string[] NotSupportStoredProcedureDatabases;

        /// <summary>
        /// Databases that do not support functions
        /// </summary>
        internal static readonly string[] NotSupportFunctionDatabases;

        /// <summary>
        /// Databases that do not support table-valued functions
        /// </summary>
        internal static readonly string[] NotSupportTableFunctionDatabases;

        /// <summary>
        /// Constructor
        /// </summary>
        static DbProvider()
        {
            NotSupportStoredProcedureDatabases = new[]
            {
                Sqlite,
                InMemoryDatabase
            };

            NotSupportFunctionDatabases = new[]
            {
                Sqlite,
                InMemoryDatabase,
                Firebird,
                Dm,
            };

            NotSupportTableFunctionDatabases = new[]
            {
                Sqlite,
                InMemoryDatabase,
                MySql,
                MySqlOfficial,
                Firebird,
                Dm
            };

            DbContextAppDbContextAttributes = new ConcurrentDictionary<Type, AppDbContextAttribute>();
        }

        /// <summary>
        /// Get database context connection string
        /// </summary>
        /// <typeparam name="TDbContext"></typeparam>
        /// <param name="connectionString"></param>
        /// <returns></returns>
        public static string GetConnectionString<TDbContext>(string connectionString = default)
            where TDbContext : DbContext
        {
            // Support read configuration rendering
            var realConnectionString = connectionString.Render();

            if (!string.IsNullOrWhiteSpace(realConnectionString)) return realConnectionString;

            // If no database connection string is configured，then look for features
            var dbContextAttribute = GetAppDbContextAttribute(typeof(TDbContext));
            if (dbContextAttribute == null) return default;

            // Get attribute connection string（render configuration template）
            var connStr = dbContextAttribute.ConnectionString.Render();

            if (string.IsNullOrWhiteSpace(connStr)) return default;
            // if it contains = symbol，then think it's a connection string
            if (connStr.Contains("=")) return connStr;
            else
            {
                var configuration = EngineContext.Current.Configuration;

                // if it contains : symbol，then consider a Key path
                if (connStr.Contains(":")) return configuration[connStr];
                else
                {
                    // Find first DbConnectionString key，if not found，be regarded as Key to find
                    var connStrValue = configuration.GetConnectionString(connStr);
                    return !string.IsNullOrWhiteSpace(connStrValue) ? connStrValue : configuration[connStr];
                }
            }
        }

        /// <summary>
        /// database context [AppDbContext] feature cache
        /// </summary>
        private static readonly ConcurrentDictionary<Type, AppDbContextAttribute> DbContextAppDbContextAttributes;

        internal static AppDbContextAttribute GetAppDbContextAttribute(Type dbContexType)
        {
            return DbContextAppDbContextAttributes.GetOrAdd(dbContexType, Function);

            // local static function
            static AppDbContextAttribute Function(Type dbContextType)
            {
                if (!dbContextType.IsDefined(typeof(AppDbContextAttribute), true)) return default;

                var appDbContextAttribute = dbContextType.GetCustomAttribute<AppDbContextAttribute>(true);

                return appDbContextAttribute;
            }
        }

        internal static bool IsDatabaseFor(string providerName, string dbAssemblyName)
        {
            return providerName.Equals(dbAssemblyName, StringComparison.Ordinal);
        }

        public static List<IInterceptor> GetDefaultInterceptors()
        {
            return new()
            {
                new SqlConnectionProfilerInterceptor(),
                new SqlCommandProfilerInterceptor(),
                new DbContextSaveChangesInterceptor()
            };
        }

        /// <summary>
        /// Operation type not supported
        /// </summary>
        private const string NotSupportException = "The database provider does not support {0} operations.";


        /// <summary>
        /// Check if a function is supported
        /// </summary>
        /// <param name="providerName">database provider name</param>
        /// <param name="dbFunctionType">database function type</param>
        internal static void CheckFunctionSupported(string providerName, DbFunctionType dbFunctionType)
        {
            if (NotSupportFunctionDatabases.Contains(providerName))
            {
                throw new NotSupportedException(string.Format(NotSupportException, "function"));
            }

            if (dbFunctionType == DbFunctionType.Table && NotSupportTableFunctionDatabases.Contains(providerName))
            {
                throw new NotSupportedException(string.Format(NotSupportException, "table function"));
            }
        }
    }
}