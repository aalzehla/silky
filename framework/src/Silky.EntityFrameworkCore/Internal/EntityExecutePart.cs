using System;
using Silky.EntityFrameworkCore.Entities;
using Silky.EntityFrameworkCore.Locators;

namespace Silky.EntityFrameworkCore
{
    /// <summary>
    /// entity execution part
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public sealed partial class EntityExecutePart<TEntity>
        where TEntity : class, IPrivateEntity, new()
    {
        /// <summary>
        /// entity
        /// </summary>
        public TEntity Entity { get; private set; }

        /// <summary>
        /// database context locator
        /// </summary>
        public Type DbContextLocator { get; private set; } = typeof(MasterDbContextLocator);
    }
}