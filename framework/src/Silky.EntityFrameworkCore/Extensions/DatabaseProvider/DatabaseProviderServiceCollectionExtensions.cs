using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Silky.EntityFrameworkCore;
using Silky.EntityFrameworkCore.Contexts.Dynamic;
using Silky.EntityFrameworkCore.Contexts.Enums;
using Silky.EntityFrameworkCore.Extensions.DatabaseProvider;
using IDbContextLocator = Silky.EntityFrameworkCore.Locators.IDbContextLocator;
using MasterDbContextLocator = Silky.EntityFrameworkCore.Locators.MasterDbContextLocator;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class DatabaseProviderServiceCollectionExtensions
    {
        /// <summary>
        /// Add default database context
        /// </summary>
        /// <typeparam name="TDbContext">database context</typeparam>
        /// <param name="services">Serve</param>
        /// <param name="providerName">database provider</param>
        /// <param name="optionBuilder"></param>
        /// <param name="connectionString">connection string</param>
        /// <param name="poolSize">pool size</param>
        /// <param name="interceptors">interceptor</param>
        /// <returns>Serve集合</returns>
        public static IServiceCollection AddDbPool<TDbContext>(this IServiceCollection services,
            string providerName = default, Action<DbContextOptionsBuilder> optionBuilder = null,
            string connectionString = default, int poolSize = 100, params IInterceptor[] interceptors)
            where TDbContext : DbContext
        {
            // 避免重复注册默认database context
            if (Penetrates.DbContextWithLocatorCached.ContainsKey(typeof(MasterDbContextLocator)))
                throw new InvalidOperationException("Prevent duplicate registration of default DbContext.");

            // 注册database context
            return services.AddDbPool<TDbContext, MasterDbContextLocator>(providerName, optionBuilder, connectionString,
                poolSize, interceptors);
        }

        /// <summary>
        /// Add default database context
        /// </summary>
        /// <typeparam name="TDbContext">database context</typeparam>
        /// <param name="services">Serve</param>
        /// <param name="optionBuilder">custom configuration</param>
        /// <param name="poolSize">pool size</param>
        /// <param name="interceptors">interceptor</param>
        /// <returns>Serve集合</returns>
        public static IServiceCollection AddDbPool<TDbContext>(this IServiceCollection services,
            Action<DbContextOptionsBuilder> optionBuilder, int poolSize = 100, params IInterceptor[] interceptors)
            where TDbContext : DbContext
        {
            // 避免重复注册默认database context
            if (Penetrates.DbContextWithLocatorCached.ContainsKey(typeof(MasterDbContextLocator)))
                throw new InvalidOperationException("Prevent duplicate registration of default DbContext.");

            // 注册database context
            return services.AddDbPool<TDbContext, MasterDbContextLocator>(optionBuilder, poolSize, interceptors);
        }

        /// <summary>
        /// 添加其他database context
        /// </summary>
        /// <typeparam name="TDbContext">database context</typeparam>
        /// <typeparam name="TDbContextLocator">database context定位器</typeparam>
        /// <param name="services">Serve</param>
        /// <param name="providerName">database provider</param>
        /// <param name="optionBuilder"></param>
        /// <param name="connectionString">connection string</param>
        /// <param name="poolSize">pool size</param>
        /// <param name="interceptors">interceptor</param>
        /// <returns>Serve集合</returns>
        public static IServiceCollection AddDbPool<TDbContext, TDbContextLocator>(this IServiceCollection services,
            string providerName = default, Action<DbContextOptionsBuilder> optionBuilder = null,
            string connectionString = default, int poolSize = 100, params IInterceptor[] interceptors)
            where TDbContext : DbContext
            where TDbContextLocator : class, IDbContextLocator
        {
            // 注册database context
            services.RegisterDbContext<TDbContext, TDbContextLocator>();

            // configuredatabase context
            var connStr = DbProvider.GetConnectionString<TDbContext>(connectionString);
            services.AddDbContextPool<TDbContext>(Penetrates.ConfigureDbContext(options =>
            {
                var _options = ConfigureDatabase<TDbContext>(providerName, connStr, options);
                optionBuilder?.Invoke(_options);
            }, interceptors), poolSize: poolSize);

            return services;
        }

        /// <summary>
        ///  Add default database context
        /// </summary>
        /// <typeparam name="TDbContext">database context</typeparam>
        /// <param name="services">Serve</param>
        /// <param name="providerName">database provider</param>
        /// <param name="optionBuilder"></param>
        /// <param name="connectionString">connection string</param>
        /// <param name="interceptors">interceptor</param>
        /// <returns>Serve集合</returns>
        public static IServiceCollection AddDb<TDbContext>(this IServiceCollection services,
            string providerName = default, Action<DbContextOptionsBuilder> optionBuilder = null,
            string connectionString = default, params IInterceptor[] interceptors)
            where TDbContext : DbContext
        {
            // 避免重复注册默认database context
            if (Penetrates.DbContextWithLocatorCached.ContainsKey(typeof(MasterDbContextLocator)))
                throw new InvalidOperationException("Prevent duplicate registration of default DbContext.");

            // 注册database context
            return services.AddDb<TDbContext, MasterDbContextLocator>(providerName, optionBuilder, connectionString,
                interceptors);
        }

        /// <summary>
        /// 添加database context
        /// </summary>
        /// <typeparam name="TDbContext">database context</typeparam>
        /// <typeparam name="TDbContextLocator">database context定位器</typeparam>
        /// <param name="services">Serve</param>
        /// <param name="providerName">database provider</param>
        /// <param name="optionBuilder"></param>
        /// <param name="connectionString">connection string</param>
        /// <param name="interceptors">interceptor</param>
        /// <returns>Serve集合</returns>
        public static IServiceCollection AddDb<TDbContext, TDbContextLocator>(this IServiceCollection services,
            string providerName = default, Action<DbContextOptionsBuilder> optionBuilder = null,
            string connectionString = default, params IInterceptor[] interceptors)
            where TDbContext : DbContext
            where TDbContextLocator : class, IDbContextLocator
        {
            // 注册database context
            services.RegisterDbContext<TDbContext, TDbContextLocator>();

            // configuredatabase context
            var connStr = DbProvider.GetConnectionString<TDbContext>(connectionString);
            services.AddDbContext<TDbContext>(Penetrates.ConfigureDbContext(options =>
            {
                var _options = ConfigureDatabase<TDbContext>(providerName, connStr, options);
                optionBuilder?.Invoke(_options);
            }, interceptors));

            return services;
        }

        /// <summary>
        /// 添加其他database context
        /// </summary>
        /// <typeparam name="TDbContext">database context</typeparam>
        /// <typeparam name="TDbContextLocator">database context定位器</typeparam>
        /// <param name="services">Serve</param>
        /// <param name="optionBuilder">custom configuration</param>
        /// <param name="poolSize">pool size</param>
        /// <param name="interceptors">interceptor</param>
        /// <returns>Serve集合</returns>
        public static IServiceCollection AddDbPool<TDbContext, TDbContextLocator>(this IServiceCollection services,
            Action<DbContextOptionsBuilder> optionBuilder, int poolSize = 100, params IInterceptor[] interceptors)
            where TDbContext : DbContext
            where TDbContextLocator : class, IDbContextLocator
        {
            // 注册database context
            services.RegisterDbContext<TDbContext, TDbContextLocator>();

            // configuredatabase context
            services.AddDbContextPool<TDbContext>(Penetrates.ConfigureDbContext(optionBuilder, interceptors),
                poolSize: poolSize);

            return services;
        }

        /// <summary>
        /// database provider UseXXX method cache collection
        /// </summary>
        private static readonly ConcurrentDictionary<string, (MethodInfo, object)> DatabaseProviderUseMethodCollection;

        /// <summary>
        /// configureCode First assembly Actionentrust
        /// </summary>
        private static readonly Action<IRelationalDbContextOptionsBuilderInfrastructure> MigrationsAssemblyAction;

        /// <summary>
        /// static constructor
        /// </summary>
        static DatabaseProviderServiceCollectionExtensions()
        {
            DatabaseProviderUseMethodCollection = new ConcurrentDictionary<string, (MethodInfo, object)>();
            MigrationsAssemblyAction = options => options.GetType()
                .GetMethod("MigrationsAssembly")
                .Invoke(options, new[] { Db.MigrationAssemblyName });
        }

        /// <summary>
        /// configure数据库
        /// </summary>
        /// <typeparam name="TDbContext"></typeparam>
        /// <param name="providerName">database provider</param>
        /// <param name="connectionString">数据库connection string</param>
        /// <param name="options">database context选项构建器</param>
        private static DbContextOptionsBuilder ConfigureDatabase<TDbContext>(string providerName,
            string connectionString, DbContextOptionsBuilder options)
            where TDbContext : DbContext
        {
            var dbContextOptionsBuilder = options;

            // Obtaindatabase context特性
            var dbContextAttribute = DbProvider.GetAppDbContextAttribute(typeof(TDbContext));
            if (!string.IsNullOrWhiteSpace(connectionString))
            {
                providerName ??= dbContextAttribute?.ProviderName;

                // Parsedatabase provider信息
                (var name, var version) = ReadProviderInfo(providerName);
                providerName = name;

                // 调用对应数据库assembly
                var (UseMethod, MySqlVersion) = GetDatabaseProviderUseMethod(providerName, version);

                // Deal with the latest third parties MySql package compatibility issues
                // https://github.com/PomeloFoundation/Pomelo.EntityFrameworkCore.MySql/commit/83c699f5b747253dc1b6fa9c470f469467d77686
                if (DbProvider.IsDatabaseFor(providerName, DbProvider.MySql))
                {
                    dbContextOptionsBuilder = UseMethod
                            .Invoke(null,
                                new object[] { options, connectionString, MySqlVersion, MigrationsAssemblyAction })
                        as DbContextOptionsBuilder;
                }
                // deal with SqlServer 2005-2008 Compatibility problems
                else if (DbProvider.IsDatabaseFor(providerName, DbProvider.SqlServer) &&
                         (version == "2008" || version == "2005"))
                {
                    // replacement factory
                    dbContextOptionsBuilder
                        .ReplaceService<IQueryTranslationPostprocessorFactory,
                            SqlServer2008QueryTranslationPostprocessorFactory>();

                    dbContextOptionsBuilder = UseMethod
                            .Invoke(null, new object[] { options, connectionString, MigrationsAssemblyAction }) as
                        DbContextOptionsBuilder;
                }
                // deal with Oracle 11 Compatibility problems
                else if (DbProvider.IsDatabaseFor(providerName, DbProvider.Oracle) &&
                         !string.IsNullOrWhiteSpace(version))
                {
                    Action<IRelationalDbContextOptionsBuilderInfrastructure> oracleOptionsAction = options =>
                    {
                        var optionsType = options.GetType();

                        // deal with版本号
                        optionsType.GetMethod("UseOracleSQLCompatibility")
                            .Invoke(options, new[] { version });

                        // deal with迁移assembly
                        optionsType.GetMethod("MigrationsAssembly")
                            .Invoke(options, new[] { Db.MigrationAssemblyName });
                    };

                    dbContextOptionsBuilder = UseMethod
                            .Invoke(null, new object[] { options, connectionString, oracleOptionsAction }) as
                        DbContextOptionsBuilder;
                }
                else
                {
                    dbContextOptionsBuilder = UseMethod
                            .Invoke(null, new object[] { options, connectionString, MigrationsAssemblyAction }) as
                        DbContextOptionsBuilder;
                }
            }

            // Solve the sub-table and sub-library
            if (dbContextAttribute?.Mode == DbContextMode.Dynamic)
                dbContextOptionsBuilder
                    .ReplaceService<IModelCacheKeyFactory, DynamicModelCacheKeyFactory>();

            return dbContextOptionsBuilder;
        }

        /// <summary>
        /// Obtaindatabase provider对应的 useXXX method
        /// </summary>
        /// <param name="providerName">database provider</param>
        /// <param name="version"></param>
        /// <returns></returns>
        private static (MethodInfo UseMethod, object MySqlVersion) GetDatabaseProviderUseMethod(string providerName,
            string version)
        {
            return DatabaseProviderUseMethodCollection.GetOrAdd(providerName, Function(providerName, version));

            // 本地静态method
            static (MethodInfo, object) Function(string providerName, string version)
            {
                // deal with最新 MySql package compatibility issues
                // https://github.com/PomeloFoundation/Pomelo.EntityFrameworkCore.MySql/commit/83c699f5b747253dc1b6fa9c470f469467d77686
                object mySqlVersionInstance = default;

                // 加载对应的database providerassembly
                var databaseProviderAssembly = Assembly.Load(providerName);

                // database providerServe拓展类型名
                var databaseProviderServiceExtensionTypeName = providerName switch
                {
                    DbProvider.SqlServer => "SqlServerDbContextOptionsExtensions",
                    DbProvider.Sqlite => "SqliteDbContextOptionsBuilderExtensions",
                    DbProvider.Cosmos => "CosmosDbContextOptionsExtensions",
                    DbProvider.InMemoryDatabase => "InMemoryDbContextOptionsExtensions",
                    DbProvider.MySql => "MySqlDbContextOptionsBuilderExtensions",
                    DbProvider.MySqlOfficial => "MySQLDbContextOptionsExtensions",
                    DbProvider.Npgsql => "NpgsqlDbContextOptionsBuilderExtensions",
                    DbProvider.Oracle => "OracleDbContextOptionsExtensions",
                    DbProvider.Firebird => "FbDbContextOptionsBuilderExtensions",
                    DbProvider.Dm => "DmDbContextOptionsExtensions",
                    _ => null
                };

                // Load extension type
                var databaseProviderServiceExtensionType =
                    databaseProviderAssembly.GetType(
                        $"Microsoft.EntityFrameworkCore.{databaseProviderServiceExtensionTypeName}");

                // useXXXmethod名
                var useMethodName = providerName switch
                {
                    DbProvider.SqlServer => $"Use{nameof(DbProvider.SqlServer)}",
                    DbProvider.Sqlite => $"Use{nameof(DbProvider.Sqlite)}",
                    DbProvider.Cosmos => $"Use{nameof(DbProvider.Cosmos)}",
                    DbProvider.InMemoryDatabase => $"Use{nameof(DbProvider.InMemoryDatabase)}",
                    DbProvider.MySql => $"Use{nameof(DbProvider.MySql)}",
                    DbProvider.MySqlOfficial => $"UseMySQL",
                    DbProvider.Npgsql => $"Use{nameof(DbProvider.Npgsql)}",
                    DbProvider.Oracle => $"Use{nameof(DbProvider.Oracle)}",
                    DbProvider.Firebird => $"Use{nameof(DbProvider.Firebird)}",
                    DbProvider.Dm => $"Use{nameof(DbProvider.Dm)}",
                    _ => null
                };

                // ObtainUseXXXmethod
                MethodInfo useMethod;

                // deal with最新 MySql 第三方package compatibility issues
                // https://github.com/PomeloFoundation/Pomelo.EntityFrameworkCore.MySql/commit/83c699f5b747253dc1b6fa9c470f469467d77686
                if (DbProvider.IsDatabaseFor(providerName, DbProvider.MySql))
                {
                    useMethod = databaseProviderServiceExtensionType
                        .GetMethods(BindingFlags.Public | BindingFlags.Static)
                        .FirstOrDefault(u =>
                            u.Name == useMethodName && !u.IsGenericMethod && u.GetParameters().Length == 4 &&
                            u.GetParameters()[1].ParameterType == typeof(string));

                    // ParsemysqlVersion type
                    var mysqlVersionType =
                        databaseProviderAssembly.GetType("Microsoft.EntityFrameworkCore.MySqlServerVersion");
                    mySqlVersionInstance = Activator.CreateInstance(mysqlVersionType,
                        new object[] { new Version(version ?? "8.0.22") });
                }
                else
                {
                    useMethod = databaseProviderServiceExtensionType
                        .GetMethods(BindingFlags.Public | BindingFlags.Static)
                        .FirstOrDefault(u =>
                            u.Name == useMethodName && !u.IsGenericMethod && u.GetParameters().Length == 3 &&
                            u.GetParameters()[1].ParameterType == typeof(string));
                }

                return (useMethod, mySqlVersionInstance);
            }
        }

        private static (string name, string version) ReadProviderInfo(string providerName)
        {
            // Parse真实的database provider
            var providerNameAndVersion = providerName.Split('@', StringSplitOptions.RemoveEmptyEntries);
            providerName = providerNameAndVersion.First();

            var providerVersion = providerNameAndVersion.Length > 1 ? providerNameAndVersion[1] : default;
            return (providerName, providerVersion);
        }

        /// <summary>
        /// 注册database context
        /// </summary>
        /// <typeparam name="TDbContext">database context</typeparam>
        /// <typeparam name="TDbContextLocator">database context定位器</typeparam>
        /// <param name="services">Serve提供器</param>
        public static IServiceCollection RegisterDbContext<TDbContext, TDbContextLocator>(
            this IServiceCollection services)
            where TDbContext : DbContext
            where TDbContextLocator : class, IDbContextLocator
        {
            var dbContextLocatorType = (typeof(TDbContextLocator));

            // 将database context和定位器一一保存起来
            var isSuccess = Penetrates.DbContextWithLocatorCached.TryAdd(dbContextLocatorType, typeof(TDbContext));
            Penetrates.DbContextLocatorTypeCached.TryAdd(dbContextLocatorType.FullName, dbContextLocatorType);

            if (!isSuccess)
                throw new InvalidOperationException(
                    $"The locator `{dbContextLocatorType.FullName}` is bound to another DbContext.");

            // 注册database context
            services.TryAddScoped<TDbContext>();

            return services;
        }
    }
}