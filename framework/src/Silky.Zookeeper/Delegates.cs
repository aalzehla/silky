using System.Collections.Generic;
using System.Threading.Tasks;
using org.apache.zookeeper;

namespace Silky.Zookeeper
{
    /// <summary>
    /// Connection state change event parameters。
    /// </summary>
    public class ConnectionStateChangeArgs
    {
        /// <summary>
        /// Connection Status。
        /// </summary>
        public Watcher.Event.KeeperState State { get; set; }
    }

    /// <summary>
    /// Node change parameters。
    /// </summary>
    public abstract class NodeChangeArgs
    {
        /// <summary>
        /// 创建一个新的Node change parameters。
        /// </summary>
        /// <param name="path">node path。</param>
        /// <param name="type">event type。</param>
        protected NodeChangeArgs(string path, Watcher.Event.EventType type)
        {
            Path = path;
            Type = type;
        }

        /// <summary>
        /// change type。
        /// </summary>
        public Watcher.Event.EventType Type { get; private set; }

        /// <summary>
        /// node path。
        /// </summary>
        public string Path { get; private set; }
    }

    /// <summary>
    /// Node data change parameters。
    /// </summary>
    public sealed class NodeDataChangeArgs : NodeChangeArgs
    {
        /// <summary>
        /// 创建一个新的Node data change parameters。
        /// </summary>
        /// <param name="path">node path。</param>
        /// <param name="type">event type。</param>
        /// <param name="currentData">latest node data。</param>
        public NodeDataChangeArgs(string path, Watcher.Event.EventType type, IEnumerable<byte> currentData) : base(path,
            type)
        {
            CurrentData = currentData;
        }

        /// <summary>
        /// current node data（Newest）
        /// </summary>
        public IEnumerable<byte> CurrentData { get; private set; }
    }

    /// <summary>
    /// 节点子Node change parameters。
    /// </summary>
    public sealed class NodeChildrenChangeArgs : NodeChangeArgs
    {
        /// <summary>
        /// 创建一个新的节点子Node change parameters。
        /// </summary>
        /// <param name="path">node path。</param>
        /// <param name="type">event type。</param>
        /// <param name="currentChildrens">Newest子节点集合。</param>
        public NodeChildrenChangeArgs(string path, Watcher.Event.EventType type, IEnumerable<string> currentChildrens) :
            base(path, type)
        {
            CurrentChildrens = currentChildrens;
        }

        /// <summary>
        /// child node data of the current node（Newest）
        /// </summary>
        public IEnumerable<string> CurrentChildrens { get; private set; }
    }

    /// <summary>
    /// Node data change delegation。
    /// </summary>
    /// <param name="client">ZooKeeperclient。</param>
    /// <param name="args">Node data change parameters。</param>
    public delegate Task NodeDataChangeHandler(IZookeeperClient client, NodeDataChangeArgs args);

    /// <summary>
    /// Node child node change delegate。
    /// </summary>
    /// <param name="client">ZooKeeperclient。</param>
    /// <param name="args">节点子Node change parameters。</param>
    public delegate Task NodeChildrenChangeHandler(IZookeeperClient client, NodeChildrenChangeArgs args);

    /// <summary>
    /// Connection Status变更委托。
    /// </summary>
    /// <param name="client">ZooKeeperclient。</param>
    /// <param name="args">Connection Status变更参数。</param>
    public delegate Task ConnectionStateChangeHandler(IZookeeperClient client, ConnectionStateChangeArgs args);
}