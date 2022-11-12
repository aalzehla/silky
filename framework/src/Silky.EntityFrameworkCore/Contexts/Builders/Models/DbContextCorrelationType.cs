using System;
using System.Collections.Generic;
using System.Reflection;
using Silky.EntityFrameworkCore.Entities.Configures;

namespace Silky.EntityFrameworkCore.Contexts.Builders.Models
{
    /// <summary>
    /// Database context association type
    /// </summary>
    internal sealed class DbContextCorrelationType
    {
        /// <summary>
        /// Constructor
        /// </summary>
        internal DbContextCorrelationType()
        {
            EntityTypes = new List<Type>();
            EntityNoKeyTypes = new List<Type>();
            EntityTypeBuilderTypes = new List<Type>();
            EntitySeedDataTypes = new List<Type>();
            EntityChangedTypes = new List<Type>();
            ModelBuilderFilterTypes = new List<Type>();
            EntityMutableTableTypes = new List<Type>();
            ModelBuilderFilterInstances = new List<IPrivateModelBuilderFilter>();
            DbFunctionMethods = new List<MethodInfo>();
        }

        /// <summary>
        /// associated database context
        /// </summary>
        internal Type DbContextLocator { get; set; }

        /// <summary>
        /// All association types
        /// </summary>
        internal List<Type> Types { get; set; }

        /// <summary>
        /// collection of entity types
        /// </summary>
        internal List<Type> EntityTypes { get; set; }

        /// <summary>
        /// 无键collection of entity types
        /// </summary>
        internal List<Type> EntityNoKeyTypes { get; set; }

        /// <summary>
        /// Entity Builder Type Collection
        /// </summary>
        internal List<Type> EntityTypeBuilderTypes { get; set; }

        /// <summary>
        /// Seed data type collection
        /// </summary>
        internal List<Type> EntitySeedDataTypes { get; set; }

        /// <summary>
        /// Entity data change type
        /// </summary>
        internal List<Type> EntityChangedTypes { get; set; }

        /// <summary>
        /// Model building filter type collection
        /// </summary>
        internal List<Type> ModelBuilderFilterTypes { get; set; }

        /// <summary>
        /// 可变表collection of entity types
        /// </summary>
        internal List<Type> EntityMutableTableTypes { get; set; }

        /// <summary>
        /// Collection of database function methods
        /// </summary>
        internal List<MethodInfo> DbFunctionMethods { get; set; }

        /// <summary>
        /// ModelBuilder filter instance
        /// </summary>
        internal List<IPrivateModelBuilderFilter> ModelBuilderFilterInstances { get; set; }
    }
}