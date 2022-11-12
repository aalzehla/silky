using System.ComponentModel;

namespace Silky.EntityFrameworkCore.Contexts.Enums
{
    /// <summary>
    /// database context mode
    /// </summary>
    public enum DbContextMode
    {
        /// <summary>
        /// cache model database context
        /// <para>
        /// OnModelCreating will only be initialized once
        /// </para>
        /// </summary>
        [Description("cache model database context")] Cached,

        /// <summary>
        /// Dynamic Model Database Context
        /// <para>
        /// OnModelCreating will be called every time
        /// </para>
        /// </summary>
        [Description("Dynamic Model Database Context")] Dynamic
    }
}