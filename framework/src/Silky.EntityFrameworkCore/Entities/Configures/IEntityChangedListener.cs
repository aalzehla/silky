using System;
using Silky.EntityFrameworkCore.Locators;
using Microsoft.EntityFrameworkCore;

namespace Silky.EntityFrameworkCore.Entities.Configures
{
    /// <summary>
    /// Entity data change monitoring dependency interface
    /// </summary>
    /// <typeparam name="TEntity">entity type</typeparam>
    public interface IEntityChangedListener<TEntity> : IEntityChangedListener<TEntity, MasterDbContextLocator>
        where TEntity : class, IPrivateEntity, new()
    {
    }

    /// <summary>
    /// Entity data change monitoring dependency interface
    /// </summary>
    /// <typeparam name="TEntity">entity type</typeparam>
    /// <typeparam name="TDbContextLocator1">database context locator</typeparam>
    public interface IEntityChangedListener<TEntity, TDbContextLocator1> : IPrivateEntityChangedListener<TEntity>
        where TEntity : class, IPrivateEntity, new()
        where TDbContextLocator1 : class, IDbContextLocator
    {
    }

    /// <summary>
    /// Entity data change monitoring dependency interface
    /// </summary>
    /// <typeparam name="TEntity">entity type</typeparam>
    /// <typeparam name="TDbContextLocator1">database context locator</typeparam>
    /// <typeparam name="TDbContextLocator2">database context locator</typeparam>
    public interface
        IEntityChangedListener<TEntity, TDbContextLocator1, TDbContextLocator2> : IPrivateEntityChangedListener<TEntity>
        where TEntity : class, IPrivateEntity, new()
        where TDbContextLocator1 : class, IDbContextLocator
        where TDbContextLocator2 : class, IDbContextLocator
    {
    }

    /// <summary>
    /// Entity data change monitoring dependency interface
    /// </summary>
    /// <typeparam name="TEntity">entity type</typeparam>
    /// <typeparam name="TDbContextLocator1">database context locator</typeparam>
    /// <typeparam name="TDbContextLocator2">database context locator</typeparam>
    /// <typeparam name="TDbContextLocator3">database context locator</typeparam>
    public interface
        IEntityChangedListener<TEntity, TDbContextLocator1, TDbContextLocator2,
            TDbContextLocator3> : IPrivateEntityChangedListener<TEntity>
        where TEntity : class, IPrivateEntity, new()
        where TDbContextLocator1 : class, IDbContextLocator
        where TDbContextLocator2 : class, IDbContextLocator
        where TDbContextLocator3 : class, IDbContextLocator
    {
    }

    /// <summary>
    /// Entity data change monitoring dependency interface
    /// </summary>
    /// <typeparam name="TEntity">entity type</typeparam>
    /// <typeparam name="TDbContextLocator1">database context locator</typeparam>
    /// <typeparam name="TDbContextLocator2">database context locator</typeparam>
    /// <typeparam name="TDbContextLocator3">database context locator</typeparam>
    /// <typeparam name="TDbContextLocator4">database context locator</typeparam>
    public interface IEntityChangedListener<TEntity, TDbContextLocator1, TDbContextLocator2, TDbContextLocator3,
        TDbContextLocator4> : IPrivateEntityChangedListener<TEntity>
        where TEntity : class, IPrivateEntity, new()
        where TDbContextLocator1 : class, IDbContextLocator
        where TDbContextLocator2 : class, IDbContextLocator
        where TDbContextLocator3 : class, IDbContextLocator
        where TDbContextLocator4 : class, IDbContextLocator
    {
    }

    /// <summary>
    /// Entity data change monitoring dependency interface
    /// </summary>
    /// <typeparam name="TEntity">entity type</typeparam>
    /// <typeparam name="TDbContextLocator1">database context locator</typeparam>
    /// <typeparam name="TDbContextLocator2">database context locator</typeparam>
    /// <typeparam name="TDbContextLocator3">database context locator</typeparam>
    /// <typeparam name="TDbContextLocator4">database context locator</typeparam>
    /// <typeparam name="TDbContextLocator5">database context locator</typeparam>
    public interface IEntityChangedListener<TEntity, TDbContextLocator1, TDbContextLocator2, TDbContextLocator3,
        TDbContextLocator4, TDbContextLocator5> : IPrivateEntityChangedListener<TEntity>
        where TEntity : class, IPrivateEntity, new()
        where TDbContextLocator1 : class, IDbContextLocator
        where TDbContextLocator2 : class, IDbContextLocator
        where TDbContextLocator3 : class, IDbContextLocator
        where TDbContextLocator4 : class, IDbContextLocator
        where TDbContextLocator5 : class, IDbContextLocator
    {
    }

    /// <summary>
    /// Entity data change monitoring dependency interface
    /// </summary>
    /// <typeparam name="TEntity">entity type</typeparam>
    /// <typeparam name="TDbContextLocator1">database context locator</typeparam>
    /// <typeparam name="TDbContextLocator2">database context locator</typeparam>
    /// <typeparam name="TDbContextLocator3">database context locator</typeparam>
    /// <typeparam name="TDbContextLocator4">database context locator</typeparam>
    /// <typeparam name="TDbContextLocator5">database context locator</typeparam>
    /// <typeparam name="TDbContextLocator6">database context locator</typeparam>
    public interface IEntityChangedListener<TEntity, TDbContextLocator1, TDbContextLocator2, TDbContextLocator3,
        TDbContextLocator4, TDbContextLocator5, TDbContextLocator6> : IPrivateEntityChangedListener<TEntity>
        where TEntity : class, IPrivateEntity, new()
        where TDbContextLocator1 : class, IDbContextLocator
        where TDbContextLocator2 : class, IDbContextLocator
        where TDbContextLocator3 : class, IDbContextLocator
        where TDbContextLocator4 : class, IDbContextLocator
        where TDbContextLocator5 : class, IDbContextLocator
        where TDbContextLocator6 : class, IDbContextLocator
    {
    }

    /// <summary>
    /// Entity data change monitoring dependency interface
    /// </summary>
    /// <typeparam name="TEntity">entity type</typeparam>
    /// <typeparam name="TDbContextLocator1">database context locator</typeparam>
    /// <typeparam name="TDbContextLocator2">database context locator</typeparam>
    /// <typeparam name="TDbContextLocator3">database context locator</typeparam>
    /// <typeparam name="TDbContextLocator4">database context locator</typeparam>
    /// <typeparam name="TDbContextLocator5">database context locator</typeparam>
    /// <typeparam name="TDbContextLocator6">database context locator</typeparam>
    /// <typeparam name="TDbContextLocator7">database context locator</typeparam>
    public interface IEntityChangedListener<TEntity, TDbContextLocator1, TDbContextLocator2, TDbContextLocator3,
        TDbContextLocator4, TDbContextLocator5, TDbContextLocator6,
        TDbContextLocator7> : IPrivateEntityChangedListener<TEntity>
        where TEntity : class, IPrivateEntity, new()
        where TDbContextLocator1 : class, IDbContextLocator
        where TDbContextLocator2 : class, IDbContextLocator
        where TDbContextLocator3 : class, IDbContextLocator
        where TDbContextLocator4 : class, IDbContextLocator
        where TDbContextLocator5 : class, IDbContextLocator
        where TDbContextLocator6 : class, IDbContextLocator
        where TDbContextLocator7 : class, IDbContextLocator
    {
    }

    /// <summary>
    /// Entity data change monitoring dependency interface
    /// </summary>
    /// <typeparam name="TEntity">entity type</typeparam>
    /// <typeparam name="TDbContextLocator1">database context locator</typeparam>
    /// <typeparam name="TDbContextLocator2">database context locator</typeparam>
    /// <typeparam name="TDbContextLocator3">database context locator</typeparam>
    /// <typeparam name="TDbContextLocator4">database context locator</typeparam>
    /// <typeparam name="TDbContextLocator5">database context locator</typeparam>
    /// <typeparam name="TDbContextLocator6">database context locator</typeparam>
    /// <typeparam name="TDbContextLocator7">database context locator</typeparam>
    /// <typeparam name="TDbContextLocator8">database context locator</typeparam>
    public interface IEntityChangedListener<TEntity, TDbContextLocator1, TDbContextLocator2, TDbContextLocator3,
        TDbContextLocator4, TDbContextLocator5, TDbContextLocator6, TDbContextLocator7,
        TDbContextLocator8> : IPrivateEntityChangedListener<TEntity>
        where TEntity : class, IPrivateEntity, new()
        where TDbContextLocator1 : class, IDbContextLocator
        where TDbContextLocator2 : class, IDbContextLocator
        where TDbContextLocator3 : class, IDbContextLocator
        where TDbContextLocator4 : class, IDbContextLocator
        where TDbContextLocator5 : class, IDbContextLocator
        where TDbContextLocator6 : class, IDbContextLocator
        where TDbContextLocator7 : class, IDbContextLocator
        where TDbContextLocator8 : class, IDbContextLocator
    {
    }

    /// <summary>
    /// Entity data change monitoring dependency interface（Forbid external inheritance）
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public interface IPrivateEntityChangedListener<TEntity> : IPrivateModelBuilder
        where TEntity : class, IPrivateEntity, new()
    {
        /// <summary>
        /// Before listening for data changes（only supportedEFCoreoperate）
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="dbContext"></param>
        /// <param name="dbContextLocator"></param>
        /// <param name="state"></param>
        void OnChanging(TEntity entity, DbContext dbContext, Type dbContextLocator, EntityState state)
        {
        }

        /// <summary>
        /// After listening for data changes（only supportedEFCoreoperate）
        /// </summary>
        /// <param name="newEntity"></param>
        /// <param name="oldEntity"></param>
        /// <param name="dbContext"></param>
        /// <param name="dbContextLocator"></param>
        /// <param name="state"></param>
        void OnChanged(TEntity newEntity, TEntity oldEntity, DbContext dbContext, Type dbContextLocator,
            EntityState state);

        /// <summary>
        /// Failed to monitor data change（only supportedEFCoreoperate）
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="dbContext"></param>
        /// <param name="dbContextLocator"></param>
        /// <param name="state"></param>
        void OnChangeFailed(TEntity entity, DbContext dbContext, Type dbContextLocator, EntityState state)
        {
        }
    }
}