using Silky.EntityFrameworkCore.Locators;

namespace Silky.EntityFrameworkCore.Entities
{
    /// <summary>
    /// Database keyless entities depend on base interfaces
    /// </summary>
    public abstract class EntityNotKey : EntityNotKey<MasterDbContextLocator>
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="name">Defined name in database</param>
        public EntityNotKey(string name) : base(name)
        {
        }
    }

    /// <summary>
    /// Database keyless entities depend on base interfaces
    /// </summary>
    /// <typeparam name="TDbContextLocator1">database context locator</typeparam>
    public abstract class EntityNotKey<TDbContextLocator1> : PrivateEntityNotKey
        where TDbContextLocator1 : class, IDbContextLocator
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="name">Defined name in database</param>
        public EntityNotKey(string name) : base(name)
        {
        }
    }

    /// <summary>
    /// Database keyless entities depend on base interfaces
    /// </summary>
    /// <typeparam name="TDbContextLocator1">database context locator</typeparam>
    /// <typeparam name="TDbContextLocator2">database context locator</typeparam>
    public abstract class EntityNotKey<TDbContextLocator1, TDbContextLocator2> : PrivateEntityNotKey
        where TDbContextLocator2 : class, IDbContextLocator
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="name">Defined name in database</param>
        public EntityNotKey(string name) : base(name)
        {
        }
    }

    /// <summary>
    /// Database keyless entities depend on base interfaces
    /// </summary>
    /// <typeparam name="TDbContextLocator1">database context locator</typeparam>
    /// <typeparam name="TDbContextLocator2">database context locator</typeparam>
    /// <typeparam name="TDbContextLocator3">database context locator</typeparam>
    public abstract class EntityNotKey<TDbContextLocator1, TDbContextLocator2, TDbContextLocator3> : PrivateEntityNotKey
        where TDbContextLocator1 : class, IDbContextLocator
        where TDbContextLocator2 : class, IDbContextLocator
        where TDbContextLocator3 : class, IDbContextLocator
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="name">Defined name in database</param>
        public EntityNotKey(string name) : base(name)
        {
        }
    }

    /// <summary>
    /// Database keyless entities depend on base interfaces
    /// </summary>
    /// <typeparam name="TDbContextLocator1">database context locator</typeparam>
    /// <typeparam name="TDbContextLocator2">database context locator</typeparam>
    /// <typeparam name="TDbContextLocator3">database context locator</typeparam>
    /// <typeparam name="TDbContextLocator4">database context locator</typeparam>
    public abstract class
        EntityNotKey<TDbContextLocator1, TDbContextLocator2, TDbContextLocator3,
            TDbContextLocator4> : PrivateEntityNotKey
        where TDbContextLocator1 : class, IDbContextLocator
        where TDbContextLocator2 : class, IDbContextLocator
        where TDbContextLocator3 : class, IDbContextLocator
        where TDbContextLocator4 : class, IDbContextLocator
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="name">Defined name in database</param>
        public EntityNotKey(string name) : base(name)
        {
        }
    }

    /// <summary>
    /// Database keyless entities depend on base interfaces
    /// </summary>
    /// <typeparam name="TDbContextLocator1">database context locator</typeparam>
    /// <typeparam name="TDbContextLocator2">database context locator</typeparam>
    /// <typeparam name="TDbContextLocator3">database context locator</typeparam>
    /// <typeparam name="TDbContextLocator4">database context locator</typeparam>
    /// <typeparam name="TDbContextLocator5">database context locator</typeparam>
    public abstract class EntityNotKey<TDbContextLocator1, TDbContextLocator2, TDbContextLocator3, TDbContextLocator4,
        TDbContextLocator5> : PrivateEntityNotKey
        where TDbContextLocator1 : class, IDbContextLocator
        where TDbContextLocator2 : class, IDbContextLocator
        where TDbContextLocator3 : class, IDbContextLocator
        where TDbContextLocator4 : class, IDbContextLocator
        where TDbContextLocator5 : class, IDbContextLocator
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="name">Defined name in database</param>
        public EntityNotKey(string name) : base(name)
        {
        }
    }

    /// <summary>
    /// Database keyless entities depend on base interfaces
    /// </summary>
    /// <typeparam name="TDbContextLocator1">database context locator</typeparam>
    /// <typeparam name="TDbContextLocator2">database context locator</typeparam>
    /// <typeparam name="TDbContextLocator3">database context locator</typeparam>
    /// <typeparam name="TDbContextLocator4">database context locator</typeparam>
    /// <typeparam name="TDbContextLocator5">database context locator</typeparam>
    /// <typeparam name="TDbContextLocator6">database context locator</typeparam>
    public abstract class EntityNotKey<TDbContextLocator1, TDbContextLocator2, TDbContextLocator3, TDbContextLocator4,
        TDbContextLocator5, TDbContextLocator6> : PrivateEntityNotKey
        where TDbContextLocator1 : class, IDbContextLocator
        where TDbContextLocator2 : class, IDbContextLocator
        where TDbContextLocator3 : class, IDbContextLocator
        where TDbContextLocator4 : class, IDbContextLocator
        where TDbContextLocator5 : class, IDbContextLocator
        where TDbContextLocator6 : class, IDbContextLocator
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="name">Defined name in database</param>
        public EntityNotKey(string name) : base(name)
        {
        }
    }

    /// <summary>
    /// Database keyless entities depend on base interfaces
    /// </summary>
    /// <typeparam name="TDbContextLocator1">database context locator</typeparam>
    /// <typeparam name="TDbContextLocator2">database context locator</typeparam>
    /// <typeparam name="TDbContextLocator3">database context locator</typeparam>
    /// <typeparam name="TDbContextLocator4">database context locator</typeparam>
    /// <typeparam name="TDbContextLocator5">database context locator</typeparam>
    /// <typeparam name="TDbContextLocator6">database context locator</typeparam>
    /// <typeparam name="TDbContextLocator7">database context locator</typeparam>
    public abstract class EntityNotKey<TDbContextLocator1, TDbContextLocator2, TDbContextLocator3, TDbContextLocator4,
        TDbContextLocator5, TDbContextLocator6, TDbContextLocator7> : PrivateEntityNotKey
        where TDbContextLocator1 : class, IDbContextLocator
        where TDbContextLocator2 : class, IDbContextLocator
        where TDbContextLocator3 : class, IDbContextLocator
        where TDbContextLocator4 : class, IDbContextLocator
        where TDbContextLocator5 : class, IDbContextLocator
        where TDbContextLocator6 : class, IDbContextLocator
        where TDbContextLocator7 : class, IDbContextLocator
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="name">Defined name in database</param>
        public EntityNotKey(string name) : base(name)
        {
        }
    }

    /// <summary>
    /// Database keyless entities depend on base interfaces
    /// </summary>
    /// <typeparam name="TDbContextLocator1">database context locator</typeparam>
    /// <typeparam name="TDbContextLocator2">database context locator</typeparam>
    /// <typeparam name="TDbContextLocator3">database context locator</typeparam>
    /// <typeparam name="TDbContextLocator4">database context locator</typeparam>
    /// <typeparam name="TDbContextLocator5">database context locator</typeparam>
    /// <typeparam name="TDbContextLocator6">database context locator</typeparam>
    /// <typeparam name="TDbContextLocator7">database context locator</typeparam>
    /// <typeparam name="TDbContextLocator8">database context locator</typeparam>
    public abstract class EntityNotKey<TDbContextLocator1, TDbContextLocator2, TDbContextLocator3, TDbContextLocator4,
        TDbContextLocator5, TDbContextLocator6, TDbContextLocator7, TDbContextLocator8> : PrivateEntityNotKey
        where TDbContextLocator1 : class, IDbContextLocator
        where TDbContextLocator2 : class, IDbContextLocator
        where TDbContextLocator3 : class, IDbContextLocator
        where TDbContextLocator4 : class, IDbContextLocator
        where TDbContextLocator5 : class, IDbContextLocator
        where TDbContextLocator6 : class, IDbContextLocator
        where TDbContextLocator7 : class, IDbContextLocator
        where TDbContextLocator8 : class, IDbContextLocator
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="name">Defined name in database</param>
        public EntityNotKey(string name) : base(name)
        {
        }
    }

    /// <summary>
    /// Database keyless entity base class（Forbid external inheritance）
    /// </summary>
    public abstract class PrivateEntityNotKey : IPrivateEntityNotKey
    {
        /// <summary>
        /// keyless entity name
        /// </summary>
        private readonly string _name;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="name">Defined name in database</param>
        public PrivateEntityNotKey(string name)
        {
            _name = name;
        }

        /// <summary>
        /// get view name
        /// </summary>
        /// <returns></returns>
        public string GetName()
        {
            return _name;
        }
    }
}