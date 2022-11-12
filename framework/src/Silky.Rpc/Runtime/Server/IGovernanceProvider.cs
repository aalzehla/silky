using Silky.Rpc.Endpoint.Selector;

namespace Silky.Rpc.Runtime.Server
{
    public interface IGovernanceProvider
    {
        /// <summary>
        /// load shedding strategy
        /// </summary>
        ShuntStrategy ShuntStrategy { get; set; }

        /// <summary>
        /// execution timeout
        /// </summary>
        int TimeoutMillSeconds { get; set; }

        /// <summary>
        /// Whether to enable cache blocking
        /// </summary>
        bool EnableCachingInterceptor { get; set; }


        /// <summary>
        /// Whether to open the fuse protection
        /// </summary>
        bool EnableCircuitBreaker { get; set; }

        /// <summary>
        /// Fusing time
        /// </summary>
        int BreakerSeconds { get; set; }

        /// <summary>
        /// Exceptions allowed before fusing
        /// </summary>

        int ExceptionsAllowedBeforeBreaking { get; set; }

        /// <summary>
        /// Failover times
        /// </summary>
        int RetryTimes { get; set; }

        int RetryIntervalMillSeconds { get; set; }
    }
}