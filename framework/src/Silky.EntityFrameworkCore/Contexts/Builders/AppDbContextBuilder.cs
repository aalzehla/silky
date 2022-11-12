using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Silky.Core;
using Silky.Core.Extensions;
using Silky.EntityFrameworkCore.Contexts.Attributes;
using Silky.EntityFrameworkCore.Contexts.Builders.Models;
using Silky.EntityFrameworkCore.Entities;
using Silky.EntityFrameworkCore.Entities.Attributes;
using Silky.EntityFrameworkCore.Entities.Configures;
using Silky.EntityFrameworkCore.Extensions.DatabaseProvider;
using Silky.EntityFrameworkCore.Locators;
using Silky.EntityFrameworkCore.MultiTenants.Dependencies;
using Silky.EntityFrameworkCore.MultiTenants.Entities;
using Silky.EntityFrameworkCore.MultiTenants.Locators;

namespace Silky.EntityFrameworkCore.Contexts.Builders
{
    /// <summary>
    /// Database context builder
    /// </summary>
    internal static class AppDbContextBuilder
    {
        /// <summary>
        /// Database entity related types
        /// </summary>
        private static readonly IEnumerable<Type> EntityCorrelationTypes;

        /// <summary>
        /// Collection of database function methods
        /// </summary>
        private static readonly IEnumerable<MethodInfo> DbFunctionMethods;

        /// <summary>
        /// Create database entity method
        /// </summary>
        private static readonly MethodInfo ModelBuildEntityMethod;

        /// <summary>
        /// Constructor
        /// </summary>
        static AppDbContextBuilder()
        {
            // scan assembly，获取Database entity related types
            EntityCorrelationTypes = EngineContext.Current.TypeFinder.GetAssemblies().SelectMany(a => a.GetTypes())
                .Where(t => (typeof(IPrivateEntity).IsAssignableFrom(t) ||
                             typeof(IPrivateModelBuilder).IsAssignableFrom(t))
                            && t.IsClass && !t.IsAbstract && !t.IsGenericType && !t.IsInterface &&
                            !t.IsDefined(typeof(ManualAttribute))
                );

            if (EntityCorrelationTypes.Any())
            {
                DbContextLocatorCorrelationTypes = new ConcurrentDictionary<Type, DbContextCorrelationType>();

                // Get ModelBuilder Entity<T> method
                ModelBuildEntityMethod = typeof(ModelBuilder).GetMethods(BindingFlags.Public | BindingFlags.Instance)
                    .FirstOrDefault(u => u.Name == nameof(ModelBuilder.Entity) && u.GetParameters().Length == 0);
            }

            // Find all database functions，必须是公开静态method，且所在父类也必须是公开静态method
            DbFunctionMethods = EngineContext.Current.TypeFinder.GetAssemblies()
                .SelectMany(p => p.ExportedTypes)
                .Where(t => t.IsAbstract && t.IsSealed && t.IsClass && !t.IsDefined(typeof(ManualAttribute), true))
                .SelectMany(t =>
                    t.GetMethods(BindingFlags.Public | BindingFlags.Static)
                        .Where(m => m.IsDefined(typeof(QueryableFunctionAttribute), true)));
        }

        /// <summary>
        /// Configure database context entities
        /// </summary>
        /// <param name="modelBuilder">model builder</param>
        /// <param name="dbContext">database context</param>
        /// <param name="dbContextLocator">database context定位器</param>
        internal static void ConfigureDbContextEntity(ModelBuilder modelBuilder, DbContext dbContext,
            Type dbContextLocator)
        {
            // 获取当前database context关联的type
            var dbContextCorrelationType = GetDbContextCorrelationType(dbContext, dbContextLocator);

            // if no data，skip
            if (!dbContextCorrelationType.EntityTypes.Any()) goto EntityFunctions;

            // 获取当前database context的 [DbContextAttributes] characteristic
            var dbContextType = dbContext.GetType();
            var appDbContextAttribute = DbProvider.GetAppDbContextAttribute(dbContextType);

            // initialize all types
            foreach (var entityType in dbContextCorrelationType.EntityTypes)
            {
                // Create entity type
                var entityBuilder = CreateEntityTypeBuilder(entityType, modelBuilder, dbContext, dbContextType,
                    dbContextLocator, dbContextCorrelationType, appDbContextAttribute);
                if (entityBuilder == null) continue;

                // Configure keyless entity builder
                ConfigureEntityNoKeyType(entityType, entityBuilder, dbContextCorrelationType.EntityNoKeyTypes);

                // Entity build successfully injected interception
                LoadModelBuilderOnCreating(modelBuilder, entityBuilder, dbContext, dbContextLocator,
                    dbContextCorrelationType.ModelBuilderFilterInstances);

                // Config Database Entity Type Builder
                ConfigureEntityTypeBuilder(entityType, entityBuilder, dbContext, dbContextLocator,
                    dbContextCorrelationType);

                // Config database entity seed data
                ConfigureEntitySeedData(entityType, entityBuilder, dbContext, dbContextLocator,
                    dbContextCorrelationType);

                // Entity completes configuration injection interception
                LoadModelBuilderOnCreated(modelBuilder, entityBuilder, dbContext, dbContextLocator,
                    dbContextCorrelationType.ModelBuilderFilterInstances);
            }

            // Configure database functions
            EntityFunctions:
            ConfigureDbFunctions(modelBuilder, dbContextLocator);
        }

        /// <summary>
        /// Create entity type构建器
        /// </summary>
        /// <param name="type">database association type</param>
        /// <param name="modelBuilder">model builder</param>
        /// <param name="dbContext">database context</param>
        /// <param name="dbContextType">database contexttype</param>
        /// <param name="dbContextLocator">database context定位器</param>
        /// <param name="dbContextCorrelationType"></param>
        /// <param name="appDbContextAttribute">database contextcharacteristic</param>
        /// <returns>EntityTypeBuilder</returns>
        private static EntityTypeBuilder CreateEntityTypeBuilder(Type type, ModelBuilder modelBuilder,
            DbContext dbContext, Type dbContextType, Type dbContextLocator,
            DbContextCorrelationType dbContextCorrelationType, AppDbContextAttribute appDbContextAttribute = null)
        {
            // 反射Create entity type构建器
            var entityTypeBuilder =
                ModelBuildEntityMethod.MakeGenericMethod(type).Invoke(modelBuilder, null) as EntityTypeBuilder;

            // Configure dynamic table names
            if (!ConfigureEntityMutableTableName(type, entityTypeBuilder, dbContext, dbContextLocator,
                    dbContextCorrelationType))
            {
                // Configure entity table name
                ConfigureEntityTableName(type, appDbContextAttribute, entityTypeBuilder, dbContext, dbContextType);
            }

            // If multi-tenancy support is not enabled or the tenant is set toOnDatabase or OnSchema Program，the multitenant field is ignored，另外还需要排除多租户database context定位器
            if (dbContextLocator != typeof(MultiTenantDbContextLocator)
                && (!typeof(IPrivateMultiTenant).IsAssignableFrom(dbContextType) ||
                    typeof(IMultiTenantOnDatabase).IsAssignableFrom(dbContextType) ||
                    typeof(IMultiTenantOnSchema).IsAssignableFrom(dbContextType))
                && type.GetProperty(Db.OnTableTenantId) != null)
            {
                entityTypeBuilder.Ignore(Db.OnTableTenantId);
            }

            return entityTypeBuilder;
        }

        /// <summary>
        /// Configure entity table name
        /// </summary>
        /// <param name="type">entity type</param>
        /// <param name="appDbContextAttribute">database contextcharacteristic</param>
        /// <param name="entityTypeBuilder">entity type构建器</param>
        /// <param name="dbContext">database context</param>
        /// <param name="dbContextType">database contexttype</param>
        private static void ConfigureEntityTableName(Type type, AppDbContextAttribute appDbContextAttribute,
            EntityTypeBuilder entityTypeBuilder, DbContext dbContext, Type dbContextType)
        {
            // Get whether the table is defined [Table] characteristic
            var tableAttribute = type.IsDefined(typeof(TableAttribute), true)
                ? type.GetCustomAttribute<TableAttribute>(true)
                : default;

            // 排除无键实体or已经贴了 [Table] characteristic的type
            if (typeof(IPrivateEntityNotKey).IsAssignableFrom(type) ||
                !string.IsNullOrWhiteSpace(tableAttribute?.Schema)) return;

            // Get real table name
            var tableName = tableAttribute?.Name ?? type.Name;

            // Determine if multi-tenant mode is enabled，in the case of，then get Schema
            var dynamicSchema = !typeof(IMultiTenantOnSchema).IsAssignableFrom(dbContextType)
                ? default
                : dbContextType.GetMethod(nameof(IMultiTenantOnSchema.GetSchemaName)).Invoke(dbContext, null)
                    ?.ToString();

            // get type prefix [TablePrefix] characteristic
            var tablePrefixAttribute = !type.IsDefined(typeof(TablePrefixAttribute), true)
                ? default
                : type.GetCustomAttribute<TablePrefixAttribute>(true);

            // 判断是否启用全局表前后缀支持or个别表自定义了前缀
            if (tablePrefixAttribute != null || appDbContextAttribute == null)
            {
                entityTypeBuilder.ToTable($"{tablePrefixAttribute?.Prefix}{tableName}", dynamicSchema);
            }
            else
            {
                // Add table uniform prefix and suffix，Exclude view
                if (!string.IsNullOrWhiteSpace(appDbContextAttribute.TablePrefix) ||
                    !string.IsNullOrWhiteSpace(appDbContextAttribute.TableSuffix))
                {
                    var tablePrefix = appDbContextAttribute.TablePrefix;
                    var tableSuffix = appDbContextAttribute.TableSuffix;

                    if (!string.IsNullOrWhiteSpace(tablePrefix))
                    {
                        // if found in prefix . character
                        if (tablePrefix.IndexOf(".") > 0)
                        {
                            var schema = tablePrefix.EndsWith(".") ? tablePrefix[0..^1] : tablePrefix;
                            entityTypeBuilder.ToTable($"{tableName}{tableSuffix}", schema: schema);
                        }
                        else entityTypeBuilder.ToTable($"{tablePrefix}{tableName}{tableSuffix}", dynamicSchema);
                    }
                    else entityTypeBuilder.ToTable($"{tableName}{tableSuffix}", dynamicSchema);

                    return;
                }
            }
        }

        /// <summary>
        /// Configure entity dynamic table name
        /// </summary>
        /// <param name="entityType">entity type</param>
        /// <param name="entityBuilder">entity type构建器</param>
        /// <param name="dbContext">database context</param>
        /// <param name="dbContextLocator">database context定位器</param>
        /// <param name="dbContextCorrelationType">Database entity association type</param>
        private static bool ConfigureEntityMutableTableName(Type entityType, EntityTypeBuilder entityBuilder,
            DbContext dbContext, Type dbContextLocator, DbContextCorrelationType dbContextCorrelationType)
        {
            var isSet = false;

            // Get entity dynamic configuration table configuration
            var entityMutableTableTypes = dbContextCorrelationType.EntityMutableTableTypes
                .Where(u => u.GetInterfaces()
                    .Any(i => i.HasImplementedRawGeneric(typeof(IPrivateEntityMutableTable<>)) &&
                              i.GenericTypeArguments.Contains(entityType)));

            if (!entityMutableTableTypes.Any()) return isSet;

            // Applies only to the last configuration of the scan
            var lastEntityMutableTableType = entityMutableTableTypes.Last();

            var instance = Activator.CreateInstance(lastEntityMutableTableType);
            var getTableNameMethod = lastEntityMutableTableType.GetMethod("GetTableName");
            var tableName = getTableNameMethod.Invoke(instance, new object[] { dbContext, dbContextLocator });
            if (tableName != null)
            {
                // Set dynamic table name
                entityBuilder.ToTable(tableName.ToString());
                isSet = true;
            }

            return isSet;
        }

        /// <summary>
        /// 配置无键entity type
        /// </summary>
        /// <param name="entityType">entity type</param>
        /// <param name="entityBuilder">entity type构建器</param>
        /// <param name="EntityNoKeyTypes">List of keyless entities</param>
        private static void ConfigureEntityNoKeyType(Type entityType, EntityTypeBuilder entityBuilder,
            List<Type> EntityNoKeyTypes)
        {
            if (!EntityNoKeyTypes.Contains(entityType)) return;

            // configuration view、stored procedure、function keyless entity
            entityBuilder.HasNoKey();
            entityBuilder.ToView((Activator.CreateInstance(entityType) as IPrivateEntityNotKey).GetName());
        }

        /// <summary>
        /// Load model build filter intercept before creation
        /// </summary>
        /// <param name="modelBuilder">model builder</param>
        /// <param name="entityBuilder">entity type构建器</param>
        /// <param name="dbContext">database context</param>
        /// <param name="dbContextLocator">database context定位器</param>
        /// <param name="modelBuilderFilterInstances">model builder筛选器实例</param>
        private static void LoadModelBuilderOnCreating(ModelBuilder modelBuilder, EntityTypeBuilder entityBuilder,
            DbContext dbContext, Type dbContextLocator, List<IPrivateModelBuilderFilter> modelBuilderFilterInstances)
        {
            if (modelBuilderFilterInstances.Count == 0) return;

            // Create filter
            foreach (var filterInstance in modelBuilderFilterInstances)
            {
                // After executing the build
                filterInstance.OnCreating(modelBuilder, entityBuilder, dbContext, dbContextLocator);
            }
        }

        /// <summary>
        /// Load model build filter intercept after creation
        /// </summary>
        /// <param name="modelBuilder">model builder</param>
        /// <param name="entityBuilder">entity type构建器</param>
        /// <param name="dbContext">database context</param>
        /// <param name="dbContextLocator">database context定位器</param>
        /// <param name="modelBuilderFilterInstances">model builder筛选器实例</param>
        private static void LoadModelBuilderOnCreated(ModelBuilder modelBuilder, EntityTypeBuilder entityBuilder,
            DbContext dbContext, Type dbContextLocator, List<IPrivateModelBuilderFilter> modelBuilderFilterInstances)
        {
            if (modelBuilderFilterInstances.Count == 0) return;

            // Create filter
            foreach (var filterInstance in modelBuilderFilterInstances)
            {
                // After executing the build
                filterInstance.OnCreated(modelBuilder, entityBuilder, dbContext, dbContextLocator);
            }
        }

        /// <summary>
        /// Config Database Entity Type Builder
        /// </summary>
        /// <param name="entityType">entity type</param>
        /// <param name="entityBuilder">entity type构建器</param>
        /// <param name="dbContext">database context</param>
        /// <param name="dbContextLocator">database context定位器</param>
        /// <param name="dbContextCorrelationType">Database entity association type</param>
        private static void ConfigureEntityTypeBuilder(Type entityType, EntityTypeBuilder entityBuilder,
            DbContext dbContext, Type dbContextLocator, DbContextCorrelationType dbContextCorrelationType)
        {
            // 获取该entity type的配置type
            var entityTypeBuilderTypes = dbContextCorrelationType.EntityTypeBuilderTypes
                .Where(u => u.GetInterfaces()
                    .Any(i => i.HasImplementedRawGeneric(typeof(IPrivateEntityTypeBuilder<>)) &&
                              i.GenericTypeArguments.Contains(entityType)));

            if (!entityTypeBuilderTypes.Any()) return;

            // Invoke database entity custom configuration
            foreach (var entityTypeBuilderType in entityTypeBuilderTypes)
            {
                var instance = Activator.CreateInstance(entityTypeBuilderType);
                var configureMethod = entityTypeBuilderType.GetMethods(BindingFlags.Public | BindingFlags.Instance)
                    .Where(u => u.Name == "Configure"
                                && u.GetParameters().Length > 0
                                && u.GetParameters().First().ParameterType ==
                                typeof(EntityTypeBuilder<>).MakeGenericType(entityType))
                    .FirstOrDefault();

                configureMethod.Invoke(instance, new object[] { entityBuilder, dbContext, dbContextLocator });
            }
        }

        /// <summary>
        /// Config database entity seed data
        /// </summary>
        /// <param name="entityType">entity type</param>
        /// <param name="entityBuilder">entity type构建器</param>
        /// <param name="dbContext">database context</param>
        /// <param name="dbContextLocator">database context定位器</param>
        /// <param name="dbContextCorrelationType">Database entity association type</param>
        private static void ConfigureEntitySeedData(Type entityType, EntityTypeBuilder entityBuilder,
            DbContext dbContext, Type dbContextLocator, DbContextCorrelationType dbContextCorrelationType)
        {
            // 获取该entity type的种子配置
            var entitySeedDataTypes = dbContextCorrelationType.EntitySeedDataTypes
                .Where(u => u.GetInterfaces()
                    .Any(i => i.HasImplementedRawGeneric(typeof(IPrivateEntitySeedData<>)) &&
                              i.GenericTypeArguments.Contains(entityType)));

            if (!entitySeedDataTypes.Any()) return;

            var data = new List<object>();

            // Load seed configuration data
            foreach (var entitySeedDataType in entitySeedDataTypes)
            {
                var instance = Activator.CreateInstance(entitySeedDataType);
                var hasDataMethod = entitySeedDataType.GetMethod("HasData");

                var seedData = ((IList)hasDataMethod?.Invoke(instance, new object[] { dbContext, dbContextLocator }))
                    ?.Cast<object>();
                if (seedData == null) continue;

                data.AddRange(seedData);
            }

            entityBuilder.HasData(data.ToArray());
        }

        /// <summary>
        /// Configure database functions
        /// </summary>
        /// <param name="modelBuilder">model build</param>
        /// <param name="dbContextLocator">database context定位器</param>
        private static void ConfigureDbFunctions(ModelBuilder modelBuilder, Type dbContextLocator)
        {
            var dbContextFunctionMethods = DbFunctionMethods.Where(u => IsInThisDbContext(dbContextLocator, u));
            if (!dbContextFunctionMethods.Any()) return;

            foreach (var method in dbContextFunctionMethods)
            {
                modelBuilder.HasDbFunction(method);
            }
        }

        /// <summary>
        /// 判断当前type是否在database context中
        /// </summary>
        /// <param name="dbContextLocator">database context定位器</param>
        /// <param name="entityCorrelationType">Database entity association type</param>
        /// <returns>bool</returns>
        private static bool IsInThisDbContext(Type dbContextLocator, Type entityCorrelationType)
        {
            // Handling custom multi-tenancy cases
            if (dbContextLocator == typeof(MultiTenantDbContextLocator) && Db.CustomizeMultiTenants &&
                typeof(ITenant).IsAssignableFrom(entityCorrelationType)) return false;

            // Get all ancestor types
            var ancestorTypes = entityCorrelationType.GetAncestorTypes();
            // get all interfaces
            var interfaces = entityCorrelationType.GetInterfaces().Where(u =>
                typeof(IPrivateEntity).IsAssignableFrom(u) || typeof(IPrivateModelBuilder).IsAssignableFrom(u));

            // 祖先是泛型且泛型参数包含database context定位器
            if (ancestorTypes.Any(u => u.IsGenericType && u.GenericTypeArguments.Contains(dbContextLocator)))
                return true;

            // interface是泛型且泛型参数包含database context定位器
            if (interfaces.Any(u => u.IsGenericType && u.GenericTypeArguments.Contains(dbContextLocator))) return true;

            return false;
        }

        /// <summary>
        /// 判断当前函数是否在database context中
        /// </summary>
        /// <param name="dbContextLocator">database context定位器</param>
        /// <param name="method">A function identified as a database</param>
        /// <returns>bool</returns>
        private static bool IsInThisDbContext(Type dbContextLocator, MethodInfo method)
        {
            var queryableFunctionAttribute = method.GetCustomAttribute<QueryableFunctionAttribute>(true);

            // 如果database context定位器为默认定位器且该函数没有定义database context定位器，then return true
            if (dbContextLocator == typeof(MasterDbContextLocator) &&
                queryableFunctionAttribute.DbContextLocators.Length == 0) return true;

            // 判断是否包含当前database context
            if (queryableFunctionAttribute.DbContextLocators.Contains(dbContextLocator)) return true;

            return false;
        }

        /// <summary>
        /// database context定位器关联type集合
        /// </summary>
        internal static ConcurrentDictionary<Type, DbContextCorrelationType> DbContextLocatorCorrelationTypes;

        /// <summary>
        /// 获取当前database context关联type
        /// </summary>
        /// <param name="dbContext">database context</param>
        /// <param name="dbContextLocator">database context定位器</param>
        /// <returns>DbContextCorrelationType</returns>
        private static DbContextCorrelationType GetDbContextCorrelationType(DbContext dbContext, Type dbContextLocator)
        {
            // read cache
            return DbContextLocatorCorrelationTypes.GetOrAdd(dbContextLocator, Function(dbContext, dbContextLocator));

            // 本地静态method
            static DbContextCorrelationType Function(DbContext dbContext, Type dbContextLocator)
            {
                var result = new DbContextCorrelationType { DbContextLocator = dbContextLocator };

                // 获取当前database context关联type
                var dbContextEntityCorrelationTypes =
                    EntityCorrelationTypes.Where(u => IsInThisDbContext(dbContextLocator, u));

                // assembly object
                foreach (var entityCorrelationType in dbContextEntityCorrelationTypes)
                {
                    // just inherit IEntityDependency interface，all entities
                    if (typeof(IPrivateEntity).IsAssignableFrom(entityCorrelationType))
                    {
                        // add entity
                        result.EntityTypes.Add(entityCorrelationType);

                        // Add keyless entity
                        if (typeof(IPrivateEntityNotKey).IsAssignableFrom(entityCorrelationType))
                        {
                            result.EntityNoKeyTypes.Add(entityCorrelationType);
                        }
                    }

                    if (typeof(IPrivateModelBuilder).IsAssignableFrom(entityCorrelationType))
                    {
                        // 添加model builder
                        if (entityCorrelationType.HasImplementedRawGeneric(typeof(IPrivateEntityTypeBuilder<>)))
                        {
                            result.EntityTypeBuilderTypes.Add(entityCorrelationType);
                        }

                        // Add global filter
                        if (entityCorrelationType.HasImplementedRawGeneric(typeof(IPrivateModelBuilderFilter)))
                        {
                            result.ModelBuilderFilterTypes.Add(entityCorrelationType);

                            // determine whether DbContext type，
                            if (typeof(DbContext).IsAssignableFrom(entityCorrelationType))
                            {
                                // Determine whether the context has been registered and is equal to the current context
                                if (Penetrates.DbContextWithLocatorCached.Values.Contains(entityCorrelationType) &&
                                    entityCorrelationType == dbContext.GetType())
                                {
                                    result.ModelBuilderFilterInstances.Add(dbContext as IPrivateModelBuilderFilter);
                                }
                            }
                            else
                                result.ModelBuilderFilterInstances.Add(
                                    Activator.CreateInstance(entityCorrelationType) as IPrivateModelBuilderFilter);
                        }

                        // Add seed data
                        if (entityCorrelationType.HasImplementedRawGeneric(typeof(IPrivateEntitySeedData<>)))
                        {
                            result.EntitySeedDataTypes.Add(entityCorrelationType);
                        }

                        // 添加动态表type
                        if (entityCorrelationType.HasImplementedRawGeneric(typeof(IPrivateEntityMutableTable<>)))
                        {
                            result.EntityMutableTableTypes.Add(entityCorrelationType);
                        }

                        // add entity数据改变监听
                        if (entityCorrelationType.HasImplementedRawGeneric(typeof(IPrivateEntityChangedListener<>)))
                        {
                            result.EntityChangedTypes.Add(entityCorrelationType);
                        }
                    }
                }

                return result;
            }
        }
    }
}