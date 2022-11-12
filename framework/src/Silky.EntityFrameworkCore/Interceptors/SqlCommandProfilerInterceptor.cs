using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Silky.EntityFrameworkCore.Interceptors
{
    /// <summary>
    /// Database execution command interception
    /// </summary>
    internal sealed class SqlCommandProfilerInterceptor : DbCommandInterceptor
    {
    }
}