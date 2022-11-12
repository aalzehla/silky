using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Silky.Core.Extensions;
using Silky.EntityFrameworkCore.Contexts.Builders;
using Silky.EntityFrameworkCore.Entities.Attributes;
using Silky.EntityFrameworkCore.Entities.Configures;
using Silky.EntityFrameworkCore.Locators;

namespace Silky.EntityFrameworkCore.Contexts
{
    /// <summary>
    /// Default application database context
    /// </summary>
    /// <typeparam name="TDbContext">database context</typeparam>
    public abstract class SilkyDbContext<TDbContext> : SilkyDbContext<TDbContext, MasterDbContextLocator>
        where TDbContext : DbContext
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="options"></param>
        protected SilkyDbContext(DbContextOptions<TDbContext> options) : base(options)
        {
        }
    }

    public abstract class SilkyDbContext<TDbContext, TDbContextLocator> : DbContext
        where TDbContext : DbContext
        where TDbContextLocator : class, IDbContextLocator
    {
        protected SilkyDbContext(DbContextOptions<TDbContext> options)
            : base(options)
        {
            ChangeTrackerEntities ??= new Dictionary<EntityEntry, PropertyValues>();
        }

        /// <summary>
        /// database context提交更改之前执行事件
        /// </summary>
        /// <param name="eventData"></param>
        /// <param name="result"></param>
        protected virtual void SavingChangesEvent(DbContextEventData eventData, InterceptionResult<int> result)
        {
        }

        /// <summary>
        /// database context提交更改成功执行事件
        /// </summary>
        /// <param name="eventData"></param>
        /// <param name="result"></param>
        protected virtual void SavedChangesEvent(SaveChangesCompletedEventData eventData, int result)
        {
        }

        /// <summary>
        /// database context提交更改失败执行事件
        /// </summary>
        /// <param name="eventData"></param>
        protected virtual void SaveChangesFailedEvent(DbContextErrorEventData eventData)
        {
        }

        /// <summary>
        /// database context初始化调用方法
        /// </summary>
        /// <param name="optionsBuilder"></param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        /// <summary>
        /// database context配置模型调用方法
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // 配置database context实体
            AppDbContextBuilder.ConfigureDbContextEntity(modelBuilder, this, typeof(TDbContextLocator));
        }

        /// <summary>
        /// Add or update ignore null values（Defaults）
        /// </summary>
        public virtual bool InsertOrUpdateIgnoreNullValues { get; protected set; } = false;

        /// <summary>
        /// Enable entity tracking（Defaults）
        /// </summary>
        public virtual bool EnabledEntityStateTracked { get; protected set; } = true;

        /// <summary>
        /// Enable entity data change listening
        /// </summary>
        public virtual bool EnabledEntityChangedListener { get; protected set; } = false;

        /// <summary>
        /// Automatic rollback if save fails
        /// </summary>
        public virtual bool FailedAutoRollback { get; protected set; } = true;
        
        /// <summary>
        /// Data being changed and tracked
        /// </summary>
        private Dictionary<EntityEntry, PropertyValues> ChangeTrackerEntities { get; set; }

        /// <summary>
        /// 内部database context提交更改之前执行事件
        /// </summary>
        /// <param name="eventData"></param>
        /// <param name="result"></param>
        internal void SavingChangesEventInner(DbContextEventData eventData, InterceptionResult<int> result)
        {
            // Additional Entity Change Notification
            if (EnabledEntityChangedListener)
            {
                var dbContext = eventData.Context;

                // get get database operation context，skip the post [NotChangedListener] Properties of Properties
                ChangeTrackerEntities = (dbContext).ChangeTracker.Entries()
                    .Where(u => !u.Entity.GetType().IsDefined(typeof(SuppressChangedListenerAttribute), true) &&
                                (u.State == EntityState.Added || u.State == EntityState.Modified ||
                                 u.State == EntityState.Deleted)).ToDictionary(u => u, u => u.GetDatabaseValues());

                AttachEntityChangedListener(eventData.Context, "OnChanging", ChangeTrackerEntities);
            }

            SavingChangesEvent(eventData, result);
        }

        /// <summary>
        /// 内部database context提交更改成功执行事件
        /// </summary>
        /// <param name="eventData"></param>
        /// <param name="result"></param>
        internal void SavedChangesEventInner(SaveChangesCompletedEventData eventData, int result)
        {
            // Additional Entity Change Notification
            if (EnabledEntityChangedListener)
                AttachEntityChangedListener(eventData.Context, "OnChanged", ChangeTrackerEntities);

            SavedChangesEvent(eventData, result);
        }

        /// <summary>
        /// 内部database context提交更改失败执行事件
        /// </summary>
        /// <param name="eventData"></param>
        internal void SaveChangesFailedEventInner(DbContextErrorEventData eventData)
        {
            // Additional Entity Change Notification
            if (EnabledEntityChangedListener)
                AttachEntityChangedListener(eventData.Context, "OnChangeFailed", ChangeTrackerEntities);

            SaveChangesFailedEvent(eventData);
        }

        /// <summary>
        /// Additional entity change listener
        /// </summary>
        /// <param name="dbContext"></param>
        /// <param name="triggerMethodName"></param>
        /// <param name="changeTrackerEntities"></param>
        private static void AttachEntityChangedListener(DbContext dbContext, string triggerMethodName,
            Dictionary<EntityEntry, PropertyValues> changeTrackerEntities = null)
        {
            // Get all changed types
            var entityChangedTypes = AppDbContextBuilder.DbContextLocatorCorrelationTypes[typeof(TDbContextLocator)]
                .EntityChangedTypes;
            if (!entityChangedTypes.Any()) return;

            // iterate over all changed entities
            foreach (var trackerEntities in changeTrackerEntities)
            {
                var entryEntity = trackerEntities.Key;
                var entity = entryEntity.Entity;

                // Get the seed configuration for this entity type
                var entitiesTypeByChanged = entityChangedTypes
                    .Where(u => u.GetInterfaces()
                        .Any(i => i.HasImplementedRawGeneric(typeof(IPrivateEntityChangedListener<>)) &&
                                  i.GenericTypeArguments.Contains(entity.GetType())));

                if (!entitiesTypeByChanged.Any()) continue;

                // Notify all listener types
                foreach (var entityChangedType in entitiesTypeByChanged)
                {
                    var OnChangeMethod = entityChangedType.GetMethods(BindingFlags.Public | BindingFlags.Instance)
                        .Where(u => u.Name == triggerMethodName
                                    && u.GetParameters().Length > 0
                                    && u.GetParameters().First().ParameterType == entity.GetType())
                        .FirstOrDefault();
                    if (OnChangeMethod == null) continue;

                    var instance = Activator.CreateInstance(entityChangedType);

                    // right OnChanged special treatment
                    if (triggerMethodName.Equals("OnChanged"))
                    {
                        // get entity old value
                        var oldEntity = trackerEntities.Value?.ToObject();

                        OnChangeMethod.Invoke(instance,
                            new object[]
                                { entity, oldEntity, dbContext, typeof(TDbContextLocator), entryEntity.State });
                    }
                    else
                    {
                        OnChangeMethod.Invoke(instance,
                            new object[] { entity, dbContext, typeof(TDbContextLocator), entryEntity.State });
                    }
                }
            }
        }
    }
}