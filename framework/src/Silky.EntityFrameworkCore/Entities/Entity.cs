using System;
using Silky.EntityFrameworkCore.Locators;

namespace Silky.EntityFrameworkCore.Entities
{
    /// <summary>
    /// Database entities depend on base classes
    /// </summary>
    public abstract class Entity : Entity<int, MasterDbContextLocator>
    {
    }

    /// <summary>
    /// Database entities depend on base classes
    /// </summary>
    /// <typeparam name="TKey">Primary key type</typeparam>
    public abstract class Entity<TKey> : Entity<TKey, MasterDbContextLocator>
    {
    }

    /// <summary>
    /// Database entities depend on base classes
    /// </summary>
    /// <typeparam name="TKey">Primary key type</typeparam>
    /// <typeparam name="TDbContextLocator1">database context locator</typeparam>
    public abstract class Entity<TKey, TDbContextLocator1> : PrivateEntity<TKey>
        where TDbContextLocator1 : class, IDbContextLocator
    {
    }

    /// <summary>
    /// Database entities depend on base classes
    /// </summary>
    /// <typeparam name="TKey">Primary key type</typeparam>
    /// <typeparam name="TDbContextLocator1">database context locator</typeparam>
    /// <typeparam name="TDbContextLocator2">database context locator</typeparam>
    public abstract class Entity<TKey, TDbContextLocator1, TDbContextLocator2> : PrivateEntity<TKey>
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
    public abstract class Entity<TKey, TDbContextLocator1, TDbContextLocator2, TDbContextLocator3> : PrivateEntity<TKey>
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
    public abstract class
        Entity<TKey, TDbContextLocator1, TDbContextLocator2, TDbContextLocator3,
            TDbContextLocator4> : PrivateEntity<TKey>
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
    public abstract class Entity<TKey, TDbContextLocator1, TDbContextLocator2, TDbContextLocator3, TDbContextLocator4,
        TDbContextLocator5> : PrivateEntity<TKey>
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
    public abstract class Entity<TKey, TDbContextLocator1, TDbContextLocator2, TDbContextLocator3, TDbContextLocator4,
        TDbContextLocator5, TDbContextLocator6> : PrivateEntity<TKey>
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
    public abstract class Entity<TKey, TDbContextLocator1, TDbContextLocator2, TDbContextLocator3, TDbContextLocator4,
        TDbContextLocator5, TDbContextLocator6, TDbContextLocator7> : PrivateEntity<TKey>
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
    public abstract class Entity<TKey, TDbContextLocator1, TDbContextLocator2, TDbContextLocator3, TDbContextLocator4,
        TDbContextLocator5, TDbContextLocator6, TDbContextLocator7, TDbContextLocator8> : PrivateEntity<TKey>
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
    public abstract class PrivateEntity<TKey> : Entities.PrivateEntityBase<TKey>
    {
        /// <summary>
        /// creation time
        /// </summary>
        public virtual DateTimeOffset CreatedTime { get; set; }

        /// <summary>
        /// update time
        /// </summary>
        public virtual DateTimeOffset? UpdatedTime { get; set; }
    }
}