using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using org.apache.zookeeper;
using org.apache.zookeeper.data;
#if !NET40
using TaskEx = System.Threading.Tasks.Task;

#endif

namespace Silky.Zookeeper.Implementation
{
    /// <summary>
    /// ZooKeeperclient。
    /// </summary>
    public class ZookeeperClient : Watcher, IZookeeperClient
    {
        #region Field

        private readonly ConcurrentDictionary<string, NodeEntry> _nodeEntries =
            new ConcurrentDictionary<string, NodeEntry>();

        private ConnectionStateChangeHandler _connectionStateChangeHandler;

        private Event.KeeperState _currentState;
        private readonly AutoResetEvent _stateChangedCondition = new(false);

        private readonly object _zkEventLock = new object();

        private bool _isDispose;

        #endregion Field

        #region Constructor

        /// <summary>
        /// create a newZooKeeperclient。
        /// </summary>
        /// <param name="connectionString">connection string。</param>
        /// <exception cref="ArgumentNullException"><paramref name="connectionString"/> Is empty。</exception>
        public ZookeeperClient(string connectionString)
            : this(new ZookeeperClientOptions(connectionString))
        {
        }

        /// <summary>
        /// create a newZooKeeperclient。
        /// </summary>
        /// <param name="options">client选项。</param>
        public ZookeeperClient(ZookeeperClientOptions options)
        {
            Options = options;
            ZooKeeper = CreateZooKeeper();
        }

        #endregion Constructor

        #region Public Method

        /// <summary>
        /// specificZooKeeperconnect。
        /// </summary>
        public ZooKeeper ZooKeeper { get; private set; }

        /// <summary>
        /// client选项。
        /// </summary>
        public ZookeeperClientOptions Options { get; }

        /// <summary>
        /// waitzkconnect到specific某一个状态。
        /// </summary>
        /// <param name="states">desired state。</param>
        /// <param name="timeout">最长wait时间。</param>
        /// <returns>return if successfultrue，otherwise returnfalse。</returns>
        public bool WaitForKeeperState(Event.KeeperState states, TimeSpan timeout)
        {
            var stillWaiting = true;
            while (_currentState != states)
            {
                if (!stillWaiting)
                {
                    return false;
                }

                stillWaiting = _stateChangedCondition.WaitOne(timeout);
            }

            return true;
        }

        /// <summary>
        /// retry untilzkconnect上。
        /// </summary>
        /// <typeparam name="T">return type。</typeparam>
        /// <param name="callable">implementedzkoperate。</param>
        /// <returns>Results of the。</returns>
        public async Task<T> RetryUntilConnected<T>(Func<Task<T>> callable)
        {
            var operationStartTime = DateTime.Now;
            T data;
            var success = false;
            while (true)
            {
                try
                {
                    data = await callable();
                    success = true;
                    break;
                }
                catch (KeeperException.ConnectionLossException)
                {
#if NET40
                    await TaskEx.Yield();
#else
                    await Task.Yield();
#endif
                    this.WaitForRetry();
                }
                catch (KeeperException.SessionExpiredException)
                {
#if NET40
                    await TaskEx.Yield();
#else
                    await Task.Yield();
#endif
                    this.WaitForRetry();
                }

                if (DateTime.Now - operationStartTime > Options.OperatingTimeout)
                {
                    throw new TimeoutException(
                        $"Operation cannot be retried because of retry timeout ({Options.OperatingTimeout.TotalMilliseconds} milli seconds)");
                }
            }

            if (!success)
            {
                throw new Exception("failed to execute callback method for RetryUntilConnected");
            }

            return data;
        }

        /// <summary>
        /// Get the data of the specified node。
        /// </summary>
        /// <param name="path">node path。</param>
        /// <returns>Node data。</returns>
        public async Task<IEnumerable<byte>> GetDataAsync(string path)
        {
            path = GetZooKeeperPath(path);

            var nodeEntry = GetOrAddNodeEntry(path);
            return await RetryUntilConnected(async () => await nodeEntry.GetDataAsync());
        }

        /// <summary>
        /// Get all child nodes under the specified node。
        /// </summary>
        /// <param name="path">node path。</param>
        /// <returns>child node collection。</returns>
        public async Task<IEnumerable<string>> GetChildrenAsync(string path)
        {
            path = GetZooKeeperPath(path);

            var nodeEntry = GetOrAddNodeEntry(path);
            return await RetryUntilConnected(async () => await nodeEntry.GetChildrenAsync());
        }

        /// <summary>
        /// Check if a node exists。
        /// </summary>
        /// <param name="path">node path。</param>
        /// <returns>return if existstrue，otherwise returnfalse。</returns>
        public async Task<bool> ExistsAsync(string path)
        {
            path = GetZooKeeperPath(path);

            var nodeEntry = GetOrAddNodeEntry(path);
            return await RetryUntilConnected(async () => await nodeEntry.ExistsAsync());
        }

        /// <summary>
        /// create node。
        /// </summary>
        /// <param name="path">node path。</param>
        /// <param name="data">Node data。</param>
        /// <param name="acls">permission。</param>
        /// <param name="createMode">Create a schema。</param>
        /// <returns>node path。</returns>
        /// <remarks>
        /// 因为使用序列方式create nodezkwill modify the nodename，所以需要返回真正的node path。
        /// </remarks>
        public async Task<string> CreateAsync(string path, byte[] data, List<ACL> acls, CreateMode createMode)
        {
            path = GetZooKeeperPath(path);

            var nodeEntry = GetOrAddNodeEntry(path);
            return await RetryUntilConnected(async () => await nodeEntry.CreateAsync(data, acls, createMode));
        }

        /// <summary>
        /// 设置Node data。
        /// </summary>
        /// <param name="path">node path。</param>
        /// <param name="data">Node data。</param>
        /// <param name="version">version number。</param>
        /// <returns>node status。</returns>
        public async Task<Stat> SetDataAsync(string path, byte[] data, int version = -1)
        {
            path = GetZooKeeperPath(path);

            var nodeEntry = GetOrAddNodeEntry(path);
            return await RetryUntilConnected(async () => await nodeEntry.SetDataAsync(data, version));
        }

        /// <summary>
        /// delete node。
        /// </summary>
        /// <param name="path">node path。</param>
        /// <param name="version">version number。</param>
        public async Task DeleteAsync(string path, int version = -1)
        {
            path = GetZooKeeperPath(path);

            var nodeEntry = GetOrAddNodeEntry(path);
            await RetryUntilConnected(async () =>
            {
                await nodeEntry.DeleteAsync(version);
                return 0;
            });
        }

        /// <summary>
        /// 订阅Node data变更。
        /// </summary>
        /// <param name="path">node path。</param>
        /// <param name="listener">listener。</param>
        public async Task SubscribeDataChange(string path, NodeDataChangeHandler listener)
        {
            path = GetZooKeeperPath(path);

            var node = GetOrAddNodeEntry(path);
            await node.SubscribeDataChange(listener);
        }

        /// <summary>
        /// 取消订阅Node data变更。
        /// </summary>
        /// <param name="path">node path。</param>
        /// <param name="listener">listener。</param>
        public void UnSubscribeDataChange(string path, NodeDataChangeHandler listener)
        {
            path = GetZooKeeperPath(path);

            var node = GetOrAddNodeEntry(path);
            node.UnSubscribeDataChange(listener);
        }

        /// <summary>
        /// 订阅connect状态变更。
        /// </summary>
        /// <param name="listener">listener。</param>
        public void SubscribeStatusChange(ConnectionStateChangeHandler listener)
        {
            _connectionStateChangeHandler += listener;
        }

        /// <summary>
        /// 取消订阅connect状态变更。
        /// </summary>
        /// <param name="listener">listener。</param>
        public void UnSubscribeStatusChange(ConnectionStateChangeHandler listener)
        {
            _connectionStateChangeHandler -= listener;
        }

        /// <summary>
        /// Subscribe node child node changes。
        /// </summary>
        /// <param name="path">node path。</param>
        /// <param name="listener">listener。</param>
        public async Task<IEnumerable<string>> SubscribeChildrenChange(string path, NodeChildrenChangeHandler listener)
        {
            path = GetZooKeeperPath(path);

            var node = GetOrAddNodeEntry(path);
            return await node.SubscribeChildrenChange(listener);
        }

        /// <summary>
        /// 取消Subscribe node child node changes。
        /// </summary>
        /// <param name="path">node path。</param>
        /// <param name="listener">listener。</param>
        public void UnSubscribeChildrenChange(string path, NodeChildrenChangeHandler listener)
        {
            path = GetZooKeeperPath(path);

            var node = GetOrAddNodeEntry(path);
            node.UnSubscribeChildrenChange(listener);
        }

        public async Task Authorize(AuthScheme authScheme, string auth = "")
        {
            await RetryUntilConnected(async () =>
            {
                ZooKeeper.addAuthInfo(authScheme.ToString().ToLower(), Encoding.UTF8.GetBytes(auth));
                return 0;
            });
        }

        #endregion Public Method

        #region Overrides of Watcher

        /// <summary>Processes the specified event.</summary>
        /// <param name="watchedEvent">The event.</param>
        /// <returns></returns>
        public override async Task process(WatchedEvent watchedEvent)
        {
            if (_isDispose)
                return;

            var path = watchedEvent.getPath();
            if (path == null)
            {
                await OnConnectionStateChange(watchedEvent);
            }
            else
            {
                NodeEntry nodeEntry;
                if (!_nodeEntries.TryGetValue(path, out nodeEntry))
                    return;
                await nodeEntry.OnChange(watchedEvent, false);
            }
        }

        #endregion Overrides of Watcher

        #region Implementation of IDisposable

        /// <summary>Execute application-defined tasks associated with releasing or resetting unmanaged resources。</summary>
        public void Dispose()
        {
            if (_isDispose)
                return;
            _isDispose = true;

            lock (_zkEventLock)
            {
                TaskEx.Run(async () => { await ZooKeeper.closeAsync().ConfigureAwait(false); }).ConfigureAwait(false)
                    .GetAwaiter().GetResult();
            }
        }

        #endregion Implementation of IDisposable

        #region Private Method

        private bool _isFirstConnectioned = true;

        private async Task OnConnectionStateChange(WatchedEvent watchedEvent)
        {
            if (_isDispose)
                return;

            var state = watchedEvent.getState();
            SetCurrentState(state);

            if (state == Event.KeeperState.Expired)
            {
                await ReConnect();
            }
            else if (state == Event.KeeperState.SyncConnected)
            {
                if (_isFirstConnectioned)
                {
                    _isFirstConnectioned = false;
                }
                else
                {
                    foreach (var nodeEntry in _nodeEntries)
                    {
                        await nodeEntry.Value.OnChange(watchedEvent, true);
                    }
                }
            }

            _stateChangedCondition.Set();
            if (_connectionStateChangeHandler == null)
                return;
            await _connectionStateChangeHandler(this, new ConnectionStateChangeArgs
            {
                State = state
            });
        }

        private NodeEntry GetOrAddNodeEntry(string path)
        {
            return _nodeEntries.GetOrAdd(path, k => new NodeEntry(path, this));
        }

        private ZooKeeper CreateZooKeeper()
        {
            return new ZooKeeper(Options.ConnectionString, (int)Options.SessionTimeout.TotalMilliseconds, this,
                Options.SessionId, Options.SessionPasswd, Options.ReadOnly);
        }

        private async Task ReConnect()
        {
            if (!Monitor.TryEnter(_zkEventLock, Options.ConnectionTimeout))
                return;
            try
            {
                if (ZooKeeper != null)
                {
                    try
                    {
                        await ZooKeeper.closeAsync();
                    }
                    catch
                    {
                        // ignored
                    }
                }

                ZooKeeper = CreateZooKeeper();
            }
            finally
            {
                Monitor.Exit(_zkEventLock);
            }
        }

        private void SetCurrentState(Event.KeeperState state)
        {
            lock (this)
            {
                _currentState = state;
            }
        }

        private string GetZooKeeperPath(string path)
        {
            var basePath = Options.BasePath ?? "/";

            if (!basePath.StartsWith("/"))
                basePath = basePath.Insert(0, "/");

            basePath = basePath.TrimEnd('/');

            if (!path.StartsWith("/"))
                path = path.Insert(0, "/");

            path = $"{basePath}{path.TrimEnd('/')}";
            return string.IsNullOrEmpty(path) ? "/" : path;
        }

        #endregion Private Method
    }
}