using System;
using Silky.EntityFrameworkCore.Contexts.Enums;

namespace Silky.EntityFrameworkCore.Contexts.Attributes
{
    public class AppDbContextAttribute : Attribute
    {
        /// <summary>
        /// default constructor
        /// </summary>
        /// <param name="slaveDbContextLocators"></param>
        public AppDbContextAttribute(params Type[] slaveDbContextLocators)
        {
            SlaveDbContextLocators = slaveDbContextLocators;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="connectionString"></param>
        /// <param name="slaveDbContextLocators"></param>
        public AppDbContextAttribute(string connectionString, params Type[] slaveDbContextLocators)
        {
            ConnectionString = connectionString;
            SlaveDbContextLocators = slaveDbContextLocators;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="connectionString"></param>
        /// <param name="providerName"></param>
        /// <param name="slaveDbContextLocators"></param>
        public AppDbContextAttribute(string connectionString, string providerName, params Type[] slaveDbContextLocators)
        {
            ConnectionString = connectionString;
            ProviderName = providerName;
            SlaveDbContextLocators = slaveDbContextLocators;
        }

        /// <summary>
        /// database connection string
        /// </summary>
        public string ConnectionString { get; set; }

        /// <summary>
        /// database provider name
        /// </summary>
        public string ProviderName { get; set; }

        /// <summary>
        /// database context mode
        /// </summary>
        public DbContextMode Mode { get; set; } = DbContextMode.Cached;

        /// <summary>
        /// table uniform prefix
        /// </summary>
        public string TablePrefix { get; set; }

        /// <summary>
        /// table uniform suffix
        /// </summary>
        public string TableSuffix { get; set; }

        /// <summary>
        /// Specify the slave locator
        /// </summary>
        public Type[] SlaveDbContextLocators { get; set; }
    }
}