using System;
using Microsoft.EntityFrameworkCore;

namespace Silky.EntityFrameworkCore.Entities.Attributes
{
    /// <summary>
    /// Entity Function Configuration Properties
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public class QueryableFunctionAttribute : DbFunctionAttribute
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="name">Function name</param>
        /// <param name="schema">schema name</param>
        public QueryableFunctionAttribute(string name, string schema = null) : base(name, schema)
        {
            DbContextLocators = Array.Empty<Type>();
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="name">Function name</param>
        /// <param name="schema">schema name</param>
        /// <param name="dbContextLocators">database context locator</param>
        public QueryableFunctionAttribute(string name, string schema = null, params Type[] dbContextLocators) : base(
            name, schema)
        {
            DbContextLocators = dbContextLocators ?? Array.Empty<Type>();
        }

        /// <summary>
        /// database context locator
        /// </summary>
        public Type[] DbContextLocators { get; set; }
    }
}