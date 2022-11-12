using System;

namespace Silky.Zookeeper
{
    /// <summary>
    /// ZooKeeperClient Options。
    /// </summary>
    public class ZookeeperClientOptions
    {
        /// <summary>
        /// create a newZooKeeperClient Options。
        /// </summary>
        /// <remarks>
        /// <see cref="ConnectionTimeout"/> for10second。
        /// <see cref="SessionTimeout"/> for20second。
        /// <see cref="OperatingTimeout"/> for60second。
        /// <see cref="ReadOnly"/> forfalse。
        /// <see cref="SessionId"/> for0。
        /// <see cref="SessionPasswd"/> fornull。
        /// <see cref="BasePath"/> fornull。
        /// <see cref="EnableEphemeralNodeRestore"/> fortrue。
        /// </remarks>
        protected ZookeeperClientOptions()
        {
            ConnectionTimeout = TimeSpan.FromSeconds(5);
            SessionTimeout = TimeSpan.FromSeconds(20);
            OperatingTimeout = TimeSpan.FromSeconds(60);
            ReadOnly = false;
            SessionId = 0;
            SessionPasswd = null;
            EnableEphemeralNodeRestore = true;
        }

        /// <summary>
        /// create a newZooKeeperClient Options。
        /// </summary>
        /// <param name="connectionString">connection string。</param>
        /// <exception cref="ArgumentNullException"><paramref name="connectionString"/> for空。</exception>
        /// <remarks>
        /// <see cref="ConnectionTimeout"/> for10second。
        /// <see cref="SessionTimeout"/> for20second。
        /// <see cref="OperatingTimeout"/> for60second。
        /// <see cref="ReadOnly"/> forfalse。
        /// <see cref="SessionId"/> for0。
        /// <see cref="SessionPasswd"/> fornull。
        /// <see cref="BasePath"/> fornull。
        /// <see cref="EnableEphemeralNodeRestore"/> fortrue。
        /// </remarks>
        public ZookeeperClientOptions(string connectionString) : this()
        {
            if (string.IsNullOrEmpty(connectionString))
                throw new ArgumentNullException(nameof(connectionString));

            ConnectionString = connectionString;
        }

        /// <summary>
        /// connection string。
        /// </summary>
        public string ConnectionString { get; set; }

        /// <summary>
        /// waitZooKeepertime to connect。
        /// </summary>
        public TimeSpan ConnectionTimeout { get; set; }

        /// <summary>
        /// implementZooKeeper操作的重试wait时间。
        /// </summary>
        public TimeSpan OperatingTimeout { get; set; }

        /// <summary>
        /// zookeepersession timeout。
        /// </summary>
        public TimeSpan SessionTimeout { get; set; }

        /// <summary>
        /// read-only，默认forfalse。
        /// </summary>
        public bool ReadOnly { get; set; }

        /// <summary>
        /// conversationId。
        /// </summary>
        public long SessionId { get; set; }

        /// <summary>
        /// conversation密码。
        /// </summary>
        public byte[] SessionPasswd { get; set; }

        /// <summary>
        /// base path，will be in allzk操作节点路径上加入此base path。
        /// </summary>
        public string BasePath { get; set; }

        /// <summary>
        /// Whether to enable recovery of ephemeral type nodes。
        /// </summary>
        public bool EnableEphemeralNodeRestore { get; set; }
    }
}