using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Silky.EntityFrameworkCore.Locators;

namespace Silky.EntityFrameworkCore.Entities.Configures
{
    /// <summary>
    /// Database model building filter dependency interface
    /// </summary>
    public interface IModelBuilderFilter : IModelBuilderFilter<MasterDbContextLocator>
    {
    }

    /// <summary>
    /// Database model building filter dependency interface
    /// </summary>
    /// <typeparam name="TDbContextLocator1">database context locator</typeparam>
    public interface IModelBuilderFilter<TDbContextLocator1> : IPrivateModelBuilderFilter
        where TDbContextLocator1 : class, IDbContextLocator
    {
    }

    /// <summary>
    /// Database model building filter dependency interface
    /// </summary>
    /// <typeparam name="TDbContextLocator1">database context locator</typeparam>
    /// <typeparam name="TDbContextLocator2">database context locator</typeparam>
    public interface IModelBuilderFilter<TDbContextLocator1, TDbContextLocator2> : IPrivateModelBuilderFilter
        where TDbContextLocator1 : class, IDbContextLocator
        where TDbContextLocator2 : class, IDbContextLocator
    {
    }

    /// <summary>
    /// Database model building filter dependency interface
    /// </summary>
    /// <typeparam name="TDbContextLocator1">database context locator</typeparam>
    /// <typeparam name="TDbContextLocator2">database context locator</typeparam>
    /// <typeparam name="TDbContextLocator3">database context locator</typeparam>
    public interface
        IModelBuilderFilter<TDbContextLocator1, TDbContextLocator2, TDbContextLocator3> : IPrivateModelBuilderFilter
        where TDbContextLocator1 : class, IDbContextLocator
        where TDbContextLocator2 : class, IDbContextLocator
        where TDbContextLocator3 : class, IDbContextLocator
    {
    }

    /// <summary>
    /// Database model building filter dependency interface
    /// </summary>
    /// <typeparam name="TDbContextLocator1">database context locator</typeparam>
    /// <typeparam name="TDbContextLocator2">database context locator</typeparam>
    /// <typeparam name="TDbContextLocator3">database context locator</typeparam>
    /// <typeparam name="TDbContextLocator4">database context locator</typeparam>
    public interface
        IModelBuilderFilter<TDbContextLocator1, TDbContextLocator2, TDbContextLocator3,
            TDbContextLocator4> : IPrivateModelBuilderFilter
        where TDbContextLocator1 : class, IDbContextLocator
        where TDbContextLocator2 : class, IDbContextLocator
        where TDbContextLocator3 : class, IDbContextLocator
        where TDbContextLocator4 : class, IDbContextLocator
    {
    }

    /// <summary>
    /// Database model building filter dependency interface
    /// </summary>
    /// <typeparam name="TDbContextLocator1">database context locator</typeparam>
    /// <typeparam name="TDbContextLocator2">database context locator</typeparam>
    /// <typeparam name="TDbContextLocator3">database context locator</typeparam>
    /// <typeparam name="TDbContextLocator4">database context locator</typeparam>
    /// <typeparam name="TDbContextLocator5">database context locator</typeparam>
    public interface IModelBuilderFilter<TDbContextLocator1, TDbContextLocator2, TDbContextLocator3, TDbContextLocator4,
        TDbContextLocator5> : IPrivateModelBuilderFilter
        where TDbContextLocator1 : class, IDbContextLocator
        where TDbContextLocator2 : class, IDbContextLocator
        where TDbContextLocator3 : class, IDbContextLocator
        where TDbContextLocator4 : class, IDbContextLocator
        where TDbContextLocator5 : class, IDbContextLocator
    {
    }

    /// <summary>
    /// Database model building filter dependency interface
    /// </summary>
    /// <typeparam name="TDbContextLocator1">database context locator</typeparam>
    /// <typeparam name="TDbContextLocator2">database context locator</typeparam>
    /// <typeparam name="TDbContextLocator3">database context locator</typeparam>
    /// <typeparam name="TDbContextLocator4">database context locator</typeparam>
    /// <typeparam name="TDbContextLocator5">database context locator</typeparam>
    /// <typeparam name="TDbContextLocator6">database context locator</typeparam>
    public interface IModelBuilderFilter<TDbContextLocator1, TDbContextLocator2, TDbContextLocator3, TDbContextLocator4,
        TDbContextLocator5, TDbContextLocator6> : IPrivateModelBuilderFilter
        where TDbContextLocator1 : class, IDbContextLocator
        where TDbContextLocator2 : class, IDbContextLocator
        where TDbContextLocator3 : class, IDbContextLocator
        where TDbContextLocator4 : class, IDbContextLocator
        where TDbContextLocator5 : class, IDbContextLocator
        where TDbContextLocator6 : class, IDbContextLocator
    {
    }

    /// <summary>
    /// Database model building filter dependency interface
    /// </summary>
    /// <typeparam name="TDbContextLocator1">database context locator</typeparam>
    /// <typeparam name="TDbContextLocator2">database context locator</typeparam>
    /// <typeparam name="TDbContextLocator3">database context locator</typeparam>
    /// <typeparam name="TDbContextLocator4">database context locator</typeparam>
    /// <typeparam name="TDbContextLocator5">database context locator</typeparam>
    /// <typeparam name="TDbContextLocator6">database context locator</typeparam>
    /// <typeparam name="TDbContextLocator7">database context locator</typeparam>
    public interface IModelBuilderFilter<TDbContextLocator1, TDbContextLocator2, TDbContextLocator3, TDbContextLocator4,
        TDbContextLocator5, TDbContextLocator6, TDbContextLocator7> : IPrivateModelBuilderFilter
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
    /// Database model building filter dependency interface
    /// </summary>
    /// <typeparam name="TDbContextLocator1">database context locator</typeparam>
    /// <typeparam name="TDbContextLocator2">database context locator</typeparam>
    /// <typeparam name="TDbContextLocator3">database context locator</typeparam>
    /// <typeparam name="TDbContextLocator4">database context locator</typeparam>
    /// <typeparam name="TDbContextLocator5">database context locator</typeparam>
    /// <typeparam name="TDbContextLocator6">database context locator</typeparam>
    /// <typeparam name="TDbContextLocator7">database context locator</typeparam>
    /// <typeparam name="TDbContextLocator8">database context locator</typeparam>
    public interface IModelBuilderFilter<TDbContextLocator1, TDbContextLocator2, TDbContextLocator3, TDbContextLocator4,
        TDbContextLocator5, TDbContextLocator6, TDbContextLocator7, TDbContextLocator8> : IPrivateModelBuilderFilter
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
    /// Database model building filter dependency interface（Forbid external inheritance）
    /// </summary>
    public interface IPrivateModelBuilderFilter : IPrivateModelBuilder
    {
        /// <summary>
        /// Before model building
        /// </summary>
        /// <param name="modelBuilder">model builder</param>
        /// <param name="entityBuilder">entity builder</param>
        /// <param name="dbContext">database context</param>
        /// <param name="dbContextLocator">database context locator</param>
        void OnCreating(ModelBuilder modelBuilder, EntityTypeBuilder entityBuilder, DbContext dbContext,
            Type dbContextLocator);

        /// <summary>
        /// After the model is built
        /// </summary>
        /// <param name="modelBuilder">model builder</param>
        /// <param name="entityBuilder">entity builder</param>
        /// <param name="dbContext">database context</param>
        /// <param name="dbContextLocator">database context locator</param>
        void OnCreated(ModelBuilder modelBuilder, EntityTypeBuilder entityBuilder, DbContext dbContext,
            Type dbContextLocator)
        {
        }
    }
}