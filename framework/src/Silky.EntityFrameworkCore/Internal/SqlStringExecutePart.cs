using System;
using Silky.EntityFrameworkCore.Locators;

namespace Silky.EntityFrameworkCore
{
    /// <summary>
    /// Construct Sql String execution part
    /// </summary>
    public sealed partial class SqlStringExecutePart
    {
        /// <summary>
        /// Sql string
        /// </summary>
        public string SqlString { get; private set; }

        /// <summary>
        /// set timeout
        /// </summary>
        public int Timeout { get; private set; }

        /// <summary>
        /// database context locator
        /// </summary>
        public Type DbContextLocator { get; private set; } = typeof(MasterDbContextLocator);
    }
}