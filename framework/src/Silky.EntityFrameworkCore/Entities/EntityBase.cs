using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Silky.EntityFrameworkCore.Locators;

namespace Silky.EntityFrameworkCore.Entities
{
    /// <summary>
    /// Database entities depend on base classes
    /// </summary>
    public abstract class EntityBase : EntityBase<int, MasterDbContextLocator>
    {
    }

    /// <summary>
    /// Database entities depend on base classes
    /// </summary>
    /// <typeparam name="TKey">Primary key type</typeparam>
    public abstract class EntityBase<TKey> : EntityBase<TKey, MasterDbContextLocator>
    {
    }

    /// <summary>
    /// Database entities depend on base classes
    /// </summary>
    /// <typeparam name="TKey">Primary key type</typeparam>
    /// <typeparam name="TDbContextLocator1">database context locator</typeparam>
    public abstract class EntityBase<TKey, TDbContextLocator1> : PrivateEntityBase<TKey>
        where TDbContextLocator1 : class, IDbContextLocator
    {
    }

    /// <summary>
    /// Database entities depend on base classes
    /// </summary>
    /// <typeparam name="TKey">Primary key type</typeparam>
    /// <typeparam name="TDbContextLocator1">database context locator</typeparam>
    /// <typeparam name="TDbContextLocator2">database context locator</typeparam>
    public abstract class EntityBase<TKey, TDbContextLocator1, TDbContextLocator2> : PrivateEntityBase<TKey>
        where TDbContextLocator1 : class, IDbContextLocator
        where TDbContextLocator2 : class, IDbContextLocator
    {
    }

    /// <summary>
    /// Database entities depend on base classes
    /// </summary>
    /// <typeparam name="TKey">Primary key type</typeparam>
    /// <typeparam name="TDbContextLocator1">database context locator</typeparam>
    /// <typeparam name="TDbContextLocator2">database context locator</typeparam>
    /// <typeparam name="TDbContextLocator3">database context locator</typeparam>
    public abstract class
        EntityBase<TKey, TDbContextLocator1, TDbContextLocator2, TDbContextLocator3> : PrivateEntityBase<TKey>
        where TDbContextLocator1 : class, IDbContextLocator
        where TDbContextLocator2 : class, IDbContextLocator
        where TDbContextLocator3 : class, IDbContextLocator
    {
    }

    /// <summary>
    /// Database entities depend on base classes
    /// </summary>
    /// <typeparam name="TKey">Primary key type</typeparam>
    /// <typeparam name="TDbContextLocator1">database context locator</typeparam>
    /// <typeparam name="TDbContextLocator2">database context locator</typeparam>
    /// <typeparam name="TDbContextLocator3">database context locator</typeparam>
    /// <typeparam name="TDbContextLocator4">database context locator</typeparam>
    public abstract class EntityBase<TKey, TDbContextLocator1, TDbContextLocator2, TDbContextLocator3,
        TDbContextLocator4> : PrivateEntityBase<TKey>
        where TDbContextLocator1 : class, IDbContextLocator
        where TDbContextLocator2 : class, IDbContextLocator
        where TDbContextLocator3 : class, IDbContextLocator
        where TDbContextLocator4 : class, IDbContextLocator
    {
    }

    /// <summary>
    /// Database entities depend on base classes
    /// </summary>
    /// <typeparam name="TKey">Primary key type</typeparam>
    /// <typeparam name="TDbContextLocator1">database context locator</typeparam>
    /// <typeparam name="TDbContextLocator2">database context locator</typeparam>
    /// <typeparam name="TDbContextLocator3">database context locator</typeparam>
    /// <typeparam name="TDbContextLocator4">database context locator</typeparam>
    /// <typeparam name="TDbContextLocator5">database context locator</typeparam>
    public abstract class EntityBase<TKey, TDbContextLocator1, TDbContextLocator2, TDbContextLocator3,
        TDbContextLocator4, TDbContextLocator5> : PrivateEntityBase<TKey>
        where TDbContextLocator1 : class, IDbContextLocator
        where TDbContextLocator2 : class, IDbContextLocator
        where TDbContextLocator3 : class, IDbContextLocator
        where TDbContextLocator4 : class, IDbContextLocator
        where TDbContextLocator5 : class, IDbContextLocator
    {
    }

    /// <summary>
    /// Database entities depend on base classes
    /// </summary>
    /// <typeparam name="TKey">Primary key type</typeparam>
    /// <typeparam name="TDbContextLocator1">database context locator</typeparam>
    /// <typeparam name="TDbContextLocator2">database context locator</typeparam>
    /// <typeparam name="TDbContextLocator3">database context locator</typeparam>
    /// <typeparam name="TDbContextLocator4">database context locator</typeparam>
    /// <typeparam name="TDbContextLocator5">database context locator</typeparam>
    /// <typeparam name="TDbContextLocator6">database context locator</typeparam>
    public abstract class EntityBase<TKey, TDbContextLocator1, TDbContextLocator2, TDbContextLocator3,
        TDbContextLocator4, TDbContextLocator5, TDbContextLocator6> : PrivateEntityBase<TKey>
        where TDbContextLocator1 : class, IDbContextLocator
        where TDbContextLocator2 : class, IDbContextLocator
        where TDbContextLocator3 : class, IDbContextLocator
        where TDbContextLocator4 : class, IDbContextLocator
        where TDbContextLocator5 : class, IDbContextLocator
        where TDbContextLocator6 : class, IDbContextLocator
    {
    }

    /// <summary>
    /// Database entities depend on base classes
    /// </summary>
    /// <typeparam name="TKey">Primary key type</typeparam>
    /// <typeparam name="TDbContextLocator1">database context locator</typeparam>
    /// <typeparam name="TDbContextLocator2">database context locator</typeparam>
    /// <typeparam name="TDbContextLocator3">database context locator</typeparam>
    /// <typeparam name="TDbContextLocator4">database context locator</typeparam>
    /// <typeparam name="TDbContextLocator5">database context locator</typeparam>
    /// <typeparam name="TDbContextLocator6">database context locator</typeparam>
    /// <typeparam name="TDbContextLocator7">database context locator</typeparam>
    public abstract class EntityBase<TKey, TDbContextLocator1, TDbContextLocator2, TDbContextLocator3,
        TDbContextLocator4, TDbContextLocator5, TDbContextLocator6, TDbContextLocator7> : PrivateEntityBase<TKey>
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
    /// Database entities depend on base classes
    /// </summary>
    /// <typeparam name="TKey">Primary key type</typeparam>
    /// <typeparam name="TDbContextLocator1">database context locator</typeparam>
    /// <typeparam name="TDbContextLocator2">database context locator</typeparam>
    /// <typeparam name="TDbContextLocator3">database context locator</typeparam>
    /// <typeparam name="TDbContextLocator4">database context locator</typeparam>
    /// <typeparam name="TDbContextLocator5">database context locator</typeparam>
    /// <typeparam name="TDbContextLocator6">database context locator</typeparam>
    /// <typeparam name="TDbContextLocator7">database context locator</typeparam>
    /// <typeparam name="TDbContextLocator8">database context locator</typeparam>
    public abstract class EntityBase<TKey, TDbContextLocator1, TDbContextLocator2, TDbContextLocator3,
        TDbContextLocator4, TDbContextLocator5, TDbContextLocator6, TDbContextLocator7,
        TDbContextLocator8> : PrivateEntityBase<TKey>
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
    /// Database entities depend on base classes（Forbid external inheritance）
    /// </summary>
    /// <typeparam name="TKey">Primary key type</typeparam>
    public abstract class PrivateEntityBase<TKey> : IPrivateEntity
    {
        /// <summary>
        /// primary keyId
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public virtual TKey Id { get; set; }
    }
}