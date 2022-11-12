using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Silky.EntityFrameworkCore.Locators;

namespace Silky.EntityFrameworkCore.Entities.Configures
{
    /// <summary>
    /// Database entity type configuration dependency interface
    /// </summary>
    /// <typeparam name="TEntity">entity type</typeparam>
    public interface IEntityTypeBuilder<TEntity> : IEntityTypeBuilder<TEntity, MasterDbContextLocator>
        where TEntity : class, IPrivateEntity, new()
    {
    }

    /// <summary>
    /// Database entity type configuration dependency interface
    /// </summary>
    /// <typeparam name="TEntity">entity type</typeparam>
    /// <typeparam name="TDbContextLocator1">database context locator</typeparam>
    public interface IEntityTypeBuilder<TEntity, TDbContextLocator1> : IPrivateEntityTypeBuilder<TEntity>
        where TEntity : class, IPrivateEntity, new()
        where TDbContextLocator1 : class, IDbContextLocator
    {
    }

    /// <summary>
    /// Database entity type configuration dependency interface
    /// </summary>
    /// <typeparam name="TEntity">entity type</typeparam>
    /// <typeparam name="TDbContextLocator1">database context locator</typeparam>
    /// <typeparam name="TDbContextLocator2">database context locator</typeparam>
    public interface
        IEntityTypeBuilder<TEntity, TDbContextLocator1, TDbContextLocator2> : IPrivateEntityTypeBuilder<TEntity>
        where TEntity : class, IPrivateEntity, new()
        where TDbContextLocator1 : class, IDbContextLocator
        where TDbContextLocator2 : class, IDbContextLocator
    {
    }

    /// <summary>
    /// Database entity type configuration dependency interface
    /// </summary>
    /// <typeparam name="TEntity">entity type</typeparam>
    /// <typeparam name="TDbContextLocator1">database context locator</typeparam>
    /// <typeparam name="TDbContextLocator2">database context locator</typeparam>
    /// <typeparam name="TDbContextLocator3">database context locator</typeparam>
    public interface
        IEntityTypeBuilder<TEntity, TDbContextLocator1, TDbContextLocator2,
            TDbContextLocator3> : IPrivateEntityTypeBuilder<TEntity>
        where TEntity : class, IPrivateEntity, new()
        where TDbContextLocator1 : class, IDbContextLocator
        where TDbContextLocator2 : class, IDbContextLocator
        where TDbContextLocator3 : class, IDbContextLocator
    {
    }

    /// <summary>
    /// Database entity type configuration dependency interface
    /// </summary>
    /// <typeparam name="TEntity">entity type</typeparam>
    /// <typeparam name="TDbContextLocator1">database context locator</typeparam>
    /// <typeparam name="TDbContextLocator2">database context locator</typeparam>
    /// <typeparam name="TDbContextLocator3">database context locator</typeparam>
    /// <typeparam name="TDbContextLocator4">database context locator</typeparam>
    public interface IEntityTypeBuilder<TEntity, TDbContextLocator1, TDbContextLocator2, TDbContextLocator3,
        TDbContextLocator4> : IPrivateEntityTypeBuilder<TEntity>
        where TEntity : class, IPrivateEntity, new()
        where TDbContextLocator1 : class, IDbContextLocator
        where TDbContextLocator2 : class, IDbContextLocator
        where TDbContextLocator3 : class, IDbContextLocator
        where TDbContextLocator4 : class, IDbContextLocator
    {
    }

    /// <summary>
    /// Database entity type configuration dependency interface
    /// </summary>
    /// <typeparam name="TEntity">entity type</typeparam>
    /// <typeparam name="TDbContextLocator1">database context locator</typeparam>
    /// <typeparam name="TDbContextLocator2">database context locator</typeparam>
    /// <typeparam name="TDbContextLocator3">database context locator</typeparam>
    /// <typeparam name="TDbContextLocator4">database context locator</typeparam>
    /// <typeparam name="TDbContextLocator5">database context locator</typeparam>
    public interface IEntityTypeBuilder<TEntity, TDbContextLocator1, TDbContextLocator2, TDbContextLocator3,
        TDbContextLocator4, TDbContextLocator5> : IPrivateEntityTypeBuilder<TEntity>
        where TEntity : class, IPrivateEntity, new()
        where TDbContextLocator1 : class, IDbContextLocator
        where TDbContextLocator2 : class, IDbContextLocator
        where TDbContextLocator3 : class, IDbContextLocator
        where TDbContextLocator4 : class, IDbContextLocator
        where TDbContextLocator5 : class, IDbContextLocator
    {
    }

    /// <summary>
    /// Database entity type configuration dependency interface
    /// </summary>
    /// <typeparam name="TEntity">entity type</typeparam>
    /// <typeparam name="TDbContextLocator1">database context locator</typeparam>
    /// <typeparam name="TDbContextLocator2">database context locator</typeparam>
    /// <typeparam name="TDbContextLocator3">database context locator</typeparam>
    /// <typeparam name="TDbContextLocator4">database context locator</typeparam>
    /// <typeparam name="TDbContextLocator5">database context locator</typeparam>
    /// <typeparam name="TDbContextLocator6">database context locator</typeparam>
    public interface IEntityTypeBuilder<TEntity, TDbContextLocator1, TDbContextLocator2, TDbContextLocator3,
        TDbContextLocator4, TDbContextLocator5, TDbContextLocator6> : IPrivateEntityTypeBuilder<TEntity>
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
    /// Database entity type configuration dependency interface
    /// </summary>
    /// <typeparam name="TEntity">entity type</typeparam>
    /// <typeparam name="TDbContextLocator1">database context locator</typeparam>
    /// <typeparam name="TDbContextLocator2">database context locator</typeparam>
    /// <typeparam name="TDbContextLocator3">database context locator</typeparam>
    /// <typeparam name="TDbContextLocator4">database context locator</typeparam>
    /// <typeparam name="TDbContextLocator5">database context locator</typeparam>
    /// <typeparam name="TDbContextLocator6">database context locator</typeparam>
    /// <typeparam name="TDbContextLocator7">database context locator</typeparam>
    public interface IEntityTypeBuilder<TEntity, TDbContextLocator1, TDbContextLocator2, TDbContextLocator3,
        TDbContextLocator4, TDbContextLocator5, TDbContextLocator6,
        TDbContextLocator7> : IPrivateEntityTypeBuilder<TEntity>
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
    /// Database entity type configuration dependency interface
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
    public interface IEntityTypeBuilder<TEntity, TDbContextLocator1, TDbContextLocator2, TDbContextLocator3,
        TDbContextLocator4, TDbContextLocator5, TDbContextLocator6, TDbContextLocator7,
        TDbContextLocator8> : IPrivateEntityTypeBuilder<TEntity>
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
    /// Database entity type configuration dependency interface（Forbid external inheritance）
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public interface IPrivateEntityTypeBuilder<TEntity> : IPrivateModelBuilder
        where TEntity : class, IPrivateEntity, new()
    {
        /// <summary>
        /// entity type配置
        /// </summary>
        /// <param name="entityBuilder">entity type构建器</param>
        /// <param name="dbContext">database context</param>
        /// <param name="dbContextLocator">database context locator</param>
        void Configure(EntityTypeBuilder<TEntity> entityBuilder, DbContext dbContext, Type dbContextLocator);
    }
}