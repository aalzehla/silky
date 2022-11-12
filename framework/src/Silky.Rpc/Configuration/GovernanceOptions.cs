using Silky.Rpc.Endpoint.Selector;
using Silky.Rpc.Runtime.Server;

namespace Silky.Rpc.Configuration
{
    public class GovernanceOptions : IGovernanceProvider
    {
        internal static string Governance = "Governance";

        public GovernanceOptions()
        {
            ShuntStrategy = ShuntStrategy.Polling;
            TimeoutMillSeconds = 5000;
            EnableCachingInterceptor = true;
            EnableCircuitBreaker = true;
            ExceptionsAllowedBeforeBreaking = 3;
            BreakerSeconds = 60;
            AddressFuseSleepDurationSeconds = 60;
            UnHealthAddressTimesAllowedBeforeRemoving = 3;
            RetryIntervalMillSeconds = 50;
            RetryTimes = 3;
            MaxConcurrentHandlingCount = 50;
            _heartbeatWatchIntervalSeconds = 300;
            EnableHeartbeat = false;
            ApiIsRESTfulStyle = true;
            
        }


        /// <summary>
        /// load shedding strategy
        /// </summary>
        public ShuntStrategy ShuntStrategy { get; set; }


        /// <summary>
        /// RpcCall execution timeout
        /// </summary>
        public int TimeoutMillSeconds { get; set; }

        /// <summary>
        /// Whether to enable cache blocking
        /// </summary>
        public bool EnableCachingInterceptor { get; set; }

        /// <summary>
        /// fuse sleep duration
        /// </summary>
        public int AddressFuseSleepDurationSeconds { get; set; }

        /// <summary>
        /// How many times an address is identified as unhealthy before it is removed
        /// </summary>
        public int UnHealthAddressTimesAllowedBeforeRemoving { get; set; }

        /// <summary>
        /// Whether to open the fuse protection,User-friendly exceptions do not trigger circuit breakers
        /// </summary>
        public bool EnableCircuitBreaker { get; set; }

        /// <summary>
        /// Fusing time
        /// </summary>
        public int BreakerSeconds { get; set; }

        /// <summary>
        /// Exceptions allowed before fusing
        /// </summary>
        public int ExceptionsAllowedBeforeBreaking { get; set; }

        /// <summary>
        /// Failover times
        /// </summary>
        public int RetryTimes { get; set; }

        /// <summary>
        /// Failover Interval
        /// </summary>
        public int RetryIntervalMillSeconds { get; set; }

        public int MaxConcurrentHandlingCount { get; set; }

        public bool EnableHeartbeat { get; set; }

        private int _heartbeatWatchIntervalSeconds;
        public bool ApiIsRESTfulStyle { get; set; }

        public int HeartbeatWatchIntervalSeconds
        {
            get => _heartbeatWatchIntervalSeconds;
            set => _heartbeatWatchIntervalSeconds = value <= 60 ? 60 : value;
        }
    }
}