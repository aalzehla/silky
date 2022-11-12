namespace Silky.EntityFrameworkCore.MultiTenants.Dependencies
{
    /// <summary>
    /// Multi-tenant schema based on database tables
    /// </summary>
    public interface IMultiTenantOnTable : IPrivateMultiTenant
    {
        /// <summary>
        /// get tenantId
        /// </summary>
        /// <returns></returns>
        object GetTenantId();
    }
}