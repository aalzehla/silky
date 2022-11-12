namespace Silky.EntityFrameworkCore.MultiTenants.Dependencies
{
    /// <summary>
    /// Multi-tenant mode based on database architecture
    /// </summary>
    public interface IMultiTenantOnSchema : IPrivateMultiTenant
    {
        /// <summary>
        /// Get database schema name
        /// </summary>
        /// <returns></returns>
        string GetSchemaName();
    }
}