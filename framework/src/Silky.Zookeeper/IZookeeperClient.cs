using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using org.apache.zookeeper;
using org.apache.zookeeper.data;

namespace Silky.Zookeeper
{
    /// <summary>
    /// an abstractZooKeeperclient。
    /// </summary>
    public interface IZookeeperClient : IDisposable
    {
        /// <summary>
        /// specificZooKeeperconnect。
        /// </summary>
        ZooKeeper ZooKeeper { get; }

        /// <summary>
        /// client选项。
        /// </summary>
        ZookeeperClientOptions Options { get; }

        /// <summary>
        /// waitzkconnect到specific某一个状态。
        /// </summary>
        /// <param name="states">desired state。</param>
        /// <param name="timeout">最长wait时间。</param>
        /// <returns>return if successfultrue，otherwise returnfalse。</returns>
        bool WaitForKeeperState(Watcher.Event.KeeperState states, TimeSpan timeout);

        /// <summary>
        /// retry untilzkconnect上。
        /// </summary>
        /// <typeparam name="T">return type。</typeparam>
        /// <param name="callable">implementedzkoperate。</param>
        /// <returns>Results of the。</returns>
        Task<T> RetryUntilConnected<T>(Func<Task<T>> callable);

        /// <summary>
        /// Get the data of the specified node。
        /// </summary>
        /// <param name="path">node path。</param>
        /// <returns>Node data。</returns>
        Task<IEnumerable<byte>> GetDataAsync(string path);

        /// <summary>
        /// Get all child nodes under the specified node。
        /// </summary>
        /// <param name="path">node path。</param>
        /// <returns>child node collection。</returns>
        Task<IEnumerable<string>> GetChildrenAsync(string path);

        /// <summary>
        /// Check if a node exists。
        /// </summary>
        /// <param name="path">node path。</param>
        /// <returns>return if existstrue，otherwise returnfalse。</returns>
        Task<bool> ExistsAsync(string path);

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
        Task<string> CreateAsync(string path, byte[] data, List<ACL> acls, CreateMode createMode);

        /// <summary>
        /// 设置Node data。
        /// </summary>
        /// <param name="path">node path。</param>
        /// <param name="data">Node data。</param>
        /// <param name="version">version number。</param>
        /// <returns>node status。</returns>
        Task<Stat> SetDataAsync(string path, byte[] data, int version = -1);

        /// <summary>
        /// delete node。
        /// </summary>
        /// <param name="path">node path。</param>
        /// <param name="version">version number。</param>
        Task DeleteAsync(string path, int version = -1);

        /// <summary>
        /// 订阅Node data变更。
        /// </summary>
        /// <param name="path">node path。</param>
        /// <param name="listener">listener。</param>
        Task SubscribeDataChange(string path, NodeDataChangeHandler listener);

        /// <summary>
        /// 取消订阅Node data变更。
        /// </summary>
        /// <param name="path">node path。</param>
        /// <param name="listener">listener。</param>
        void UnSubscribeDataChange(string path, NodeDataChangeHandler listener);

        /// <summary>
        /// 订阅connect状态变更。
        /// </summary>
        /// <param name="listener">listener。</param>
        void SubscribeStatusChange(ConnectionStateChangeHandler listener);

        /// <summary>
        /// 取消订阅connect状态变更。
        /// </summary>
        /// <param name="listener">listener。</param>
        void UnSubscribeStatusChange(ConnectionStateChangeHandler listener);

        /// <summary>
        /// Subscribe node child node changes。
        /// </summary>
        /// <param name="path">node path。</param>
        /// <param name="listener">listener。</param>
        Task<IEnumerable<string>> SubscribeChildrenChange(string path, NodeChildrenChangeHandler listener);

        /// <summary>
        /// 取消Subscribe node child node changes。
        /// </summary>
        /// <param name="path">node path。</param>
        /// <param name="listener">listener。</param>
        void UnSubscribeChildrenChange(string path, NodeChildrenChangeHandler listener);

        Task Authorize(AuthScheme authScheme, string auth = "");
    }

    /// <summary>
    /// ZooKeeperclient扩展方法。
    /// </summary>
    public static class ZookeeperClientExtensions
    {
        /// <summary>
        /// Create ephemeral nodes。
        /// </summary>
        /// <param name="client">ZooKeeperclient。</param>
        /// <param name="path">node path。</param>
        /// <param name="data">Node data。</param>
        /// <param name="isSequential">whether to create in order。</param>
        /// <returns>node path。</returns>
        /// <remarks>
        /// 因为使用序列方式create nodezkwill modify the nodename，所以需要返回真正的node path。
        /// </remarks>
        public static Task<string> CreateEphemeralAsync(this IZookeeperClient client, string path, byte[] data,
            bool isSequential = false)
        {
            return client.CreateEphemeralAsync(path, data, ZooDefs.Ids.OPEN_ACL_UNSAFE, isSequential);
        }

        /// <summary>
        /// Create ephemeral nodes。
        /// </summary>
        /// <param name="client">ZooKeeperclient。</param>
        /// <param name="path">node path。</param>
        /// <param name="data">Node data。</param>
        /// <param name="acls">permission。</param>
        /// <param name="isSequential">whether to create in order。</param>
        /// <returns>node path。</returns>
        /// <remarks>
        /// 因为使用序列方式create nodezkwill modify the nodename，所以需要返回真正的node path。
        /// </remarks>
        public static Task<string> CreateEphemeralAsync(this IZookeeperClient client, string path, byte[] data,
            List<ACL> acls, bool isSequential = false)
        {
            return client.CreateAsync(path, data, acls,
                isSequential ? CreateMode.EPHEMERAL_SEQUENTIAL : CreateMode.EPHEMERAL);
        }

        /// <summary>
        /// create node。
        /// </summary>
        /// <param name="client">ZooKeeperclient。</param>
        /// <param name="path">node path。</param>
        /// <param name="data">Node data。</param>
        /// <param name="isSequential">whether to create in order。</param>
        /// <returns>node path。</returns>
        /// <remarks>
        /// 因为使用序列方式create nodezkwill modify the nodename，所以需要返回真正的node path。
        /// </remarks>
        public static Task<string> CreatePersistentAsync(this IZookeeperClient client, string path, byte[] data,
            bool isSequential = false)
        {
            return client.CreatePersistentAsync(path, data, ZooDefs.Ids.OPEN_ACL_UNSAFE, isSequential);
        }

        /// <summary>
        /// create node。
        /// </summary>
        /// <param name="client">ZooKeeperclient。</param>
        /// <param name="path">node path。</param>
        /// <param name="data">Node data。</param>
        /// <param name="acls">permission。</param>
        /// <param name="isSequential">whether to create in order。</param>
        /// <returns>node path。</returns>
        /// <remarks>
        /// 因为使用序列方式create nodezkwill modify the nodename，所以需要返回真正的node path。
        /// </remarks>
        public static Task<string> CreatePersistentAsync(this IZookeeperClient client, string path, byte[] data,
            List<ACL> acls, bool isSequential = false)
        {
            return client.CreateAsync(path, data, acls,
                isSequential ? CreateMode.PERSISTENT_SEQUENTIAL : CreateMode.PERSISTENT);
        }

        /// <summary>
        /// Recursively delete all child nodes under the node and the node itself。
        /// </summary>
        /// <param name="client">ZooKeeperclient。</param>
        /// <param name="path">node path。</param>
        /// <returns>return if successfultrue，false。</returns>
        public static async Task<bool> DeleteRecursiveAsync(this IZookeeperClient client, string path)
        {
            IEnumerable<string> children;
            try
            {
                children = await client.GetChildrenAsync(path);
            }
            catch (KeeperException.NoNodeException)
            {
                return true;
            }

            foreach (var subPath in children)
            {
                if (!await client.DeleteRecursiveAsync(path + "/" + subPath))
                {
                    return false;
                }
            }

            await client.DeleteAsync(path);
            return true;
        }

        /// <summary>
        /// Recursively create all child nodes under the node and the node itself。
        /// </summary>
        /// <param name="client">ZooKeeperclient。</param>
        /// <param name="path">node path。</param>
        /// <param name="data">Node data。</param>
        public static Task CreateRecursiveAsync(this IZookeeperClient client, string path, byte[] data)
        {
            return client.CreateRecursiveAsync(path, data, ZooDefs.Ids.OPEN_ACL_UNSAFE);
        }

        /// <summary>
        /// Recursively create all child nodes under the node and the node itself。
        /// </summary>
        /// <param name="client">ZooKeeperclient。</param>
        /// <param name="path">node path。</param>
        /// <param name="data">Node data。</param>
        /// <param name="acls">permission。</param>
        public static Task CreateRecursiveAsync(this IZookeeperClient client, string path, byte[] data, List<ACL> acls)
        {
            return client.CreateRecursiveAsync(path, p => data, p => acls);
        }

        /// <summary>
        /// Recursively create all child nodes under the node and the node itself。
        /// </summary>
        /// <param name="client">ZooKeeperclient。</param>
        /// <param name="path">node path。</param>
        /// <param name="getNodeData">获取当前被创建Node data的委托。</param>
        /// <param name="getNodeAcls">获取当前被create nodepermission的委托。</param>
        public static async Task CreateRecursiveAsync(this IZookeeperClient client, string path,
            Func<string, byte[]> getNodeData, Func<string, List<ACL>> getNodeAcls)
        {
            var data = getNodeData(path);
            var acls = getNodeAcls(path);
            try
            {
                await client.CreateAsync(path, data, acls, CreateMode.PERSISTENT);
            }
            catch (KeeperException.NodeExistsException)
            {
            }
            catch (KeeperException.NoNodeException)
            {
                var parentDir = path.Substring(0, path.LastIndexOf('/'));
                await CreateRecursiveAsync(client, parentDir, getNodeData, getNodeAcls);
                await client.CreateAsync(path, data, acls, CreateMode.PERSISTENT);
            }
        }

        /// <summary>
        /// wait直到zkconnect成功，The timeout iszk选项中的operate超时时间配置值。
        /// </summary>
        /// <param name="client">zkclient。</param>
        public static void WaitForRetry(this IZookeeperClient client)
        {
            client.WaitUntilConnected(client.Options.ConnectionTimeout);
        }

        /// <summary>
        /// wait直到zkconnect成功。
        /// </summary>
        /// <param name="client">zkclient。</param>
        /// <param name="timeout">最长wait时间。</param>
        /// <returns>return if successfultrue，otherwise returnfalse。</returns>
        public static bool WaitUntilConnected(this IZookeeperClient client, TimeSpan timeout)
        {
            return client.WaitForKeeperState(Watcher.Event.KeeperState.SyncConnected, timeout);
        }
    }
}