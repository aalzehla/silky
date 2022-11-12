namespace Silky.EntityFrameworkCore.Entities.Configures
{
    /// <summary>
    /// Database Model Builder dependencies（Direct inheritance is prohibited）
    /// </summary>
    /// <remarks>
    /// correspond <see cref="Microsoft.EntityFrameworkCore.DbContext.OnModelCreating(Microsoft.EntityFrameworkCore.ModelBuilder)"/>
    /// </remarks>
    public interface IPrivateModelBuilder
    {
    }
}