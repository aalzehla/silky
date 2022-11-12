using Silky.Rpc.Routing;
using Silky.Rpc.Security;
using SilkyApp.Application.Contracts.System.Dtos;

namespace SilkyApp.Application.Contracts.System
{
    /// <summary>
    /// system information service
    /// </summary>
    [ServiceRoute(template:"api/system/{appservice=silkyapp}")]
    public interface ISystemAppService
    {
        /// <summary>
        /// Get current app details
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        GetSystemInfoOutput GetInfo();
    }
}