namespace Silky.EntityFrameworkCore.MultiTenants.Dependencies
{
    /// <summary>
    /// Multi-tenant mode based on multiple databases
    /// </summary>
    public interface IMultiTenantOnDatabase : IPrivateMultiTenant
    {
        /// <summary>
        /// Get database connection string
        /// </summary>
        /// <returns></returns>
        string GetDatabaseConnectionString();
    }
}