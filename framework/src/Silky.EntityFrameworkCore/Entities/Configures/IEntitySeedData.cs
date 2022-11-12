using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Silky.EntityFrameworkCore.Locators;

namespace Silky.EntityFrameworkCore.Entities.Configures
{
    /// <summary>
    /// Database seed data dependency interface
    /// </summary>
    /// <typeparam name="TEntity">entity type</typeparam>
    public interface IEntitySeedData<TEntity> : IEntitySeedData<TEntity, MasterDbContextLocator>
        where TEntity : class, IPrivateEntity, new()
    {
    }

    /// <summary>
    /// Database seed data dependency interface
    /// </summary>
    /// <typeparam name="TEntity">entity type</typeparam>
    /// <typeparam name="TDbContextLocator1">database context locator</typeparam>
    public interface IEntitySeedData<TEntity, TDbContextLocator1> : IPrivateEntitySeedData<TEntity>
        where TEntity : class, IPrivateEntity, new()
        where TDbContextLocator1 : class, IDbContextLocator
    {
    }

    /// <summary>
    /// Database seed data dependency interface
    /// </summary>
    /// <typeparam name="TEntity">entity type</typeparam>
    /// <typeparam name="TDbContextLocator1">database context locator</typeparam>
    /// <typeparam name="TDbContextLocator2">database context locator</typeparam>
    public interface IEntitySeedData<TEntity, TDbContextLocator1, TDbContextLocator2> : IPrivateEntitySeedData<TEntity>
        where TEntity : class, IPrivateEntity, new()
        where TDbContextLocator1 : class, IDbContextLocator
        where TDbContextLocator2 : class, IDbContextLocator
    {
    }

    /// <summary>
    /// Database seed data dependency interface
    /// </summary>
    /// <typeparam name="TEntity">entity type</typeparam>
    /// <typeparam name="TDbContextLocator1">database context locator</typeparam>
    /// <typeparam name="TDbContextLocator2">database context locator</typeparam>
    /// <typeparam name="TDbContextLocator3">database context locator</typeparam>
    public interface
        IEntitySeedData<TEntity, TDbContextLocator1, TDbContextLocator2,
            TDbContextLocator3> : IPrivateEntitySeedData<TEntity>
        where TEntity : class, IPrivateEntity, new()
        where TDbContextLocator1 : class, IDbContextLocator
        where TDbContextLocator2 : class, IDbContextLocator
        where TDbContextLocator3 : class, IDbContextLocator
    {
    }

    /// <summary>
    /// Database seed data dependency interface
    /// </summary>
    /// <typeparam name="TEntity">entity type</typeparam>
    /// <typeparam name="TDbContextLocator1">database context locator</typeparam>
    /// <typeparam name="TDbContextLocator2">database context locator</typeparam>
    /// <typeparam name="TDbContextLocator3">database context locator</typeparam>
    /// <typeparam name="TDbContextLocator4">database context locator</typeparam>
    public interface IEntitySeedData<TEntity, TDbContextLocator1, TDbContextLocator2, TDbContextLocator3,
        TDbContextLocator4> : IPrivateEntitySeedData<TEntity>
        where TEntity : class, IPrivateEntity, new()
        where TDbContextLocator1 : class, IDbContextLocator
        where TDbContextLocator2 : class, IDbContextLocator
        where TDbContextLocator3 : class, IDbContextLocator
        where TDbContextLocator4 : class, IDbContextLocator
    {
    }

    /// <summary>
    /// Database seed data dependency interface
    /// </summary>
    /// <typeparam name="TEntity">entity type</typeparam>
    /// <typeparam name="TDbContextLocator1">database context locator</typeparam>
    /// <typeparam name="TDbContextLocator2">database context locator</typeparam>
    /// <typeparam name="TDbContextLocator3">database context locator</typeparam>
    /// <typeparam name="TDbContextLocator4">database context locator</typeparam>
    /// <typeparam name="TDbContextLocator5">database context locator</typeparam>
    public interface IEntitySeedData<TEntity, TDbContextLocator1, TDbContextLocator2, TDbContextLocator3,
        TDbContextLocator4, TDbContextLocator5> : IPrivateEntitySeedData<TEntity>
        where TEntity : class, IPrivateEntity, new()
        where TDbContextLocator1 : class, IDbContextLocator
        where TDbContextLocator2 : class, IDbContextLocator
        where TDbContextLocator3 : class, IDbContextLocator
        where TDbContextLocator4 : class, IDbContextLocator
        where TDbContextLocator5 : class, IDbContextLocator
    {
    }

    /// <summary>
    /// Database seed data dependency interface
    /// </summary>
    /// <typeparam name="TEntity">entity type</typeparam>
    /// <typeparam name="TDbContextLocator1">database context locator</typeparam>
    /// <typeparam name="TDbContextLocator2">database context locator</typeparam>
    /// <typeparam name="TDbContextLocator3">database context locator</typeparam>
    /// <typeparam name="TDbContextLocator4">database context locator</typeparam>
    /// <typeparam name="TDbContextLocator5">database context locator</typeparam>
    /// <typeparam name="TDbContextLocator6">database context locator</typeparam>
    public interface IEntitySeedData<TEntity, TDbContextLocator1, TDbContextLocator2, TDbContextLocator3,
        TDbContextLocator4, TDbContextLocator5, TDbContextLocator6> : IPrivateEntitySeedData<TEntity>
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
    /// Database seed data dependency interface
    /// </summary>
    /// <typeparam name="TEntity">entity type</typeparam>
    /// <typeparam name="TDbContextLocator1">database context locator</typeparam>
    /// <typeparam name="TDbContextLocator2">database context locator</typeparam>
    /// <typeparam name="TDbContextLocator3">database context locator</typeparam>
    /// <typeparam name="TDbContextLocator4">database context locator</typeparam>
    /// <typeparam name="TDbContextLocator5">database context locator</typeparam>
    /// <typeparam name="TDbContextLocator6">database context locator</typeparam>
    /// <typeparam name="TDbContextLocator7">database context locator</typeparam>
    public interface IEntitySeedData<TEntity, TDbContextLocator1, TDbContextLocator2, TDbContextLocator3,
        TDbContextLocator4, TDbContextLocator5, TDbContextLocator6,
        TDbContextLocator7> : IPrivateEntitySeedData<TEntity>
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
    /// Database seed data dependency interface
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
    public interface IEntitySeedData<TEntity, TDbContextLocator1, TDbContextLocator2, TDbContextLocator3,
        TDbContextLocator4, TDbContextLocator5, TDbContextLocator6, TDbContextLocator7,
        TDbContextLocator8> : IPrivateEntitySeedData<TEntity>
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
    /// Database seed data dependency interface（Forbid external inheritance）
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public interface IPrivateEntitySeedData<TEntity> : IPrivateModelBuilder
        where TEntity : class, IPrivateEntity, new()
    {
        /// <summary>
        /// Configure seed data
        /// </summary>
        /// <param name="dbContext">database context</param>
        /// <param name="dbContextLocator">database context locator</param>
        /// <returns></returns>
        IEnumerable<TEntity> HasData(DbContext dbContext, Type dbContextLocator);
    }
}