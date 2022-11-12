using System;
using Microsoft.Extensions.DependencyInjection;
using Silky.Core;
using Silky.EntityFrameworkCore.Entities;
using Silky.EntityFrameworkCore.Locators;

namespace Silky.EntityFrameworkCore.Repositories
{
    /// <summary>
    /// Multi-database warehousing
    /// </summary>
    /// <typeparam name="TDbContextLocator"></typeparam>
    public partial class DbRepository<TDbContextLocator> : IDbRepository<TDbContextLocator>
        where TDbContextLocator : class, IDbContextLocator
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public DbRepository()
        {
        }

        /// <summary>
        /// switch entity
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <returns></returns>
        public virtual IRepository<TEntity, TDbContextLocator> Change<TEntity>()
            where TEntity : class, IPrivateEntity, new()
        {
            return EngineContext.Current.Resolve<IRepository<TEntity, TDbContextLocator>>();
        }

        /// <summary>
        /// Obtain Sql Operating warehouse
        /// </summary>
        /// <returns></returns>
        public virtual ISqlRepository<TDbContextLocator> Sql()
        {
            return EngineContext.Current.Resolve<ISqlRepository<TDbContextLocator>>();
        }
    }
}