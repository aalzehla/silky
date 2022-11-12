using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using org.apache.zookeeper;
using org.apache.zookeeper.data;

namespace Silky.Zookeeper.Implementation
{
    internal class NodeEntry
    {
        #region Field

        private readonly IZookeeperClient _client;

        /// <summary>
        /// Data change multicast delegation。
        /// </summary>
        private NodeDataChangeHandler _dataChangeHandler;

        /// <summary>
        /// Child node changes multicast delegation。
        /// </summary>
        private NodeChildrenChangeHandler _childrenChangeHandler;

        /// <summary>
        /// Snapshot of the node。
        /// </summary>
        private NodeSnapshot _localSnapshot = default(NodeSnapshot);

        #endregion Field

        #region Property

        public string Path { get; }

        #endregion Property

        #region Constructor

        public NodeEntry(string path, IZookeeperClient client)
        {
            Path = path;
            _client = client;
        }

        #endregion Constructor

        #region Public Method

        public async Task<IEnumerable<byte>> GetDataAsync(bool watch = false)
        {
            var zookeeper = _client.ZooKeeper;
            var data = await zookeeper.getDataAsync(Path, watch);

            _localSnapshot.SetData(data?.Data);

            return data?.Data;
        }

        public async Task<IEnumerable<string>> GetChildrenAsync(bool watch = false)
        {
            var zookeeper = _client.ZooKeeper;
            var data = await zookeeper.getChildrenAsync(Path, watch);

            _localSnapshot.SetChildrens(data?.Children);

            return data?.Children;
        }

        public async Task<bool> ExistsAsync(bool watch = false)
        {
            var zookeeper = _client.ZooKeeper;
            var data = await zookeeper.existsAsync(Path, watch);

            var exists = data != null;

            _localSnapshot.SetExists(exists);

            return exists;
        }

        public async Task<string> CreateAsync(byte[] data, List<ACL> acls, CreateMode createMode)
        {
            var zooKeeper = _client.ZooKeeper;
            var path = await zooKeeper.createAsync(Path, data, acls, createMode);

            _localSnapshot.Create(createMode, data, acls);

            return path;
        }

        public Task<Stat> SetDataAsync(byte[] data, int version = -1)
        {
            var zooKeeper = _client.ZooKeeper;
            var stat = zooKeeper.setDataAsync(Path, data, version);

            _localSnapshot.Update(data, version);

            return stat;
        }

        public async Task DeleteAsync(int version = -1)
        {
            var zookeeper = _client.ZooKeeper;
            await zookeeper.deleteAsync(Path, version);

            _localSnapshot.Delete();
        }

        #region Listener

        public async Task SubscribeDataChange(NodeDataChangeHandler listener)
        {
            _dataChangeHandler += listener;

            //Monitor data changes
            await WatchDataChange();
        }

        public void UnSubscribeDataChange(NodeDataChangeHandler listener)
        {
            _dataChangeHandler -= listener;
        }

        public async Task<IEnumerable<string>> SubscribeChildrenChange(NodeChildrenChangeHandler listener)
        {
            _childrenChangeHandler += listener;

            //Monitor child node changes
            return await WatchChildrenChange();
        }

        public void UnSubscribeChildrenChange(NodeChildrenChangeHandler listener)
        {
            _childrenChangeHandler -= listener;
        }

        #endregion Listener

        #endregion Public Method

        #region Private Method

        /// <summary>
        /// Notify nodes of changes。
        /// </summary>
        /// <param name="watchedEvent">zookeeper sdklisten event parameter。</param>
        /// <param name="isFirstConnection">whether or notzkConnecting to the server for the first time。</param>
        internal async Task OnChange(WatchedEvent watchedEvent, bool isFirstConnection)
        {
            //get node path（If only the state sends the change then the path isnull）
            var path = watchedEvent.getPath();
            //whether or notzkconnection status change
            var stateChanged = path == null;

            //If it is only a state change; perform state change processing
            if (stateChanged)
            {
                await OnStatusChangeHandle(watchedEvent, isFirstConnection);
            }
            else if (path == Path) //If the changed node belongs to itself
            {
                var eventType = watchedEvent.get_Type();

                //Whether it is a data change
                var dataChanged = new[]
                {
                    Watcher.Event.EventType.NodeCreated,
                    Watcher.Event.EventType.NodeDataChanged,
                    Watcher.Event.EventType.NodeDeleted
                }.Contains(eventType);

                if (dataChanged)
                {
                    //If the child node has just been created and the node has registered child node change listeners，then notifyzkmonitor child nodes（Delay listening）
                    if (eventType == Watcher.Event.EventType.NodeCreated && HasChildrenChangeHandler)
                        await _client.RetryUntilConnected(() => GetChildrenAsync(true));

                    //Perform data change processing
                    await OnDataChangeHandle(watchedEvent);
                }
                else
                {
                    //Perform child node change processing
                    await OnChildrenChangeHandle(watchedEvent);
                }
            }
        }

        /// <summary>
        /// Is there a data change processor。
        /// </summary>
        private bool HasDataChangeHandler => HasHandler(_dataChangeHandler);

        /// <summary>
        /// Whether there is a child node change handler。
        /// </summary>
        private bool HasChildrenChangeHandler => HasHandler(_childrenChangeHandler);

        /// <summary>
        /// state change handling。
        /// </summary>
        /// <param name="watchedEvent"></param>
        /// <param name="isFirstConnection">whether or notzkConnecting to the server for the first time。</param>
        private async Task OnStatusChangeHandle(WatchedEvent watchedEvent, bool isFirstConnection)
        {
            //first connectionzkno notification
            if (isFirstConnection)
                return;

            //try to restore the node
            await RestoreEphemeral();

            if (HasDataChangeHandler)
                await OnDataChangeHandle(watchedEvent);
            if (HasChildrenChangeHandler)
                await OnChildrenChangeHandle(watchedEvent);
        }

        private async Task OnDataChangeHandle(WatchedEvent watchedEvent)
        {
            if (!HasDataChangeHandler)
                return;

            //A delegate to get the latest data of the current node
            var getCurrentData = new Func<Task<IEnumerable<byte>>>(() => _client.RetryUntilConnected(async () =>
            {
                try
                {
                    return await GetDataAsync();
                }
                catch (KeeperException.NoNodeException) //node does not exist returnnull
                {
                    return null;
                }
            }));

            //Build Node Change Event Parameters Based on Event Type
            NodeDataChangeArgs args;
            switch (watchedEvent.get_Type())
            {
                case Watcher.Event.EventType.NodeCreated:
                    args = new NodeDataChangeArgs(Path, Watcher.Event.EventType.NodeCreated, await getCurrentData());
                    break;

                case Watcher.Event.EventType.NodeDeleted:
                    args = new NodeDataChangeArgs(Path, Watcher.Event.EventType.NodeDeleted, null);
                    break;

                case Watcher.Event.EventType.NodeDataChanged:
                case Watcher.Event.EventType.None: //Triggered on reconnect
                    args = new NodeDataChangeArgs(Path, Watcher.Event.EventType.NodeDataChanged,
                        await getCurrentData());
                    break;

                default:
                    throw new NotSupportedException($"Unsupported event type：{watchedEvent.get_Type()}");
            }

            await _dataChangeHandler(_client, args);

            //relisten
            await WatchDataChange();
        }

        private async Task OnChildrenChangeHandle(WatchedEvent watchedEvent)
        {
            if (!HasChildrenChangeHandler)
                return;

            //Get the latest child node information of the current node
            var getCurrentChildrens = new Func<Task<IEnumerable<string>>>(() => _client.RetryUntilConnected(
                async () =>
                {
                    try
                    {
                        return await GetChildrenAsync();
                    }
                    catch (KeeperException.NoNodeException)
                    {
                        return null;
                    }
                }));

            //Build node child nodes based on event type Change event parameters
            NodeChildrenChangeArgs args;
            switch (watchedEvent.get_Type())
            {
                case Watcher.Event.EventType.NodeCreated:
                    args = new NodeChildrenChangeArgs(Path, Watcher.Event.EventType.NodeCreated,
                        await getCurrentChildrens());
                    break;

                case Watcher.Event.EventType.NodeDeleted:
                    args = new NodeChildrenChangeArgs(Path, Watcher.Event.EventType.NodeDeleted, null);
                    break;

                case Watcher.Event.EventType.NodeChildrenChanged:
                case Watcher.Event.EventType.None: //Triggered on reconnect
                    args = new NodeChildrenChangeArgs(Path, Watcher.Event.EventType.NodeChildrenChanged,
                        await getCurrentChildrens());
                    break;

                default:
                    throw new NotSupportedException($"Unsupported event type：{watchedEvent.get_Type()}");
            }

            await _childrenChangeHandler(_client, args);

            //relisten
            await WatchChildrenChange();
        }

        private async Task WatchDataChange()
        {
            await _client.RetryUntilConnected(() => ExistsAsync(true));
        }

        private async Task<IEnumerable<string>> WatchChildrenChange()
        {
            return await _client.RetryUntilConnected(async () =>
            {
                await ExistsAsync(true);
                try
                {
                    return await GetChildrenAsync(true);
                }
                catch (KeeperException.NoNodeException)
                {
                }

                return null;
            });
        }

        private static bool HasHandler(MulticastDelegate multicast)
        {
            return multicast != null && multicast.GetInvocationList().Any();
        }

        private async Task RestoreEphemeral()
        {
            //Recovery is not enabled
            if (!_client.Options.EnableEphemeralNodeRestore)
                return;

            //node does not exist
            if (!_localSnapshot.IsExist)
                return;

            //not ephemeral nodes
            if (_localSnapshot.Mode != CreateMode.EPHEMERAL && _localSnapshot.Mode != CreateMode.EPHEMERAL_SEQUENTIAL)
                return;

            try
            {
                await _client.RetryUntilConnected(async () =>
                {
                    try
                    {
                        return await CreateAsync(_localSnapshot.Data?.ToArray(), _localSnapshot.Acls,
                            _localSnapshot.Mode);
                    }
                    catch (KeeperException.NodeExistsException) //Ignore if node already exists
                    {
                        return Path;
                    }
                });
            }
            catch (Exception exception)
            {
                Console.WriteLine($"Failed to restore node，abnormal：{exception.Message}");
            }
        }

        #endregion Private Method

        #region Help Type

        public struct NodeSnapshot
        {
            public bool IsExist { get; set; }
            public CreateMode Mode { get; set; }
            public IEnumerable<byte> Data { get; set; }
            public int? Version { get; set; }
            public List<ACL> Acls { get; set; }
            public IEnumerable<string> Childrens { get; set; }

            public void Create(CreateMode mode, byte[] data, List<ACL> acls)
            {
                IsExist = true;
                Mode = mode;
                Data = data;
                Version = -1;
                Acls = acls;
                Childrens = null;
            }

            public void Update(IEnumerable<byte> data, int version)
            {
                IsExist = true;
                Data = data;
                Version = version;
            }

            public void Delete()
            {
                IsExist = false;
                Mode = null;
                Data = null;
                Version = null;
                Acls = null;
                Childrens = null;
            }

            public void SetData(IEnumerable<byte> data)
            {
                IsExist = true;
                Data = data;
            }

            public void SetChildrens(IEnumerable<string> childrens)
            {
                IsExist = true;
                Childrens = childrens;
            }

            public void SetExists(bool exists)
            {
                if (!exists)
                {
                    Delete();
                    return;
                }

                IsExist = true;
            }
        }

        #endregion Help Type
    }
}