using System;
using System.Net;
using Silky.Core.Runtime.Rpc;
using Silky.Rpc.Endpoint.Descriptor;

namespace Silky.Rpc.Endpoint
{
    public interface ISilkyEndpoint
    {
        /// <summary>
        /// address(Ipor domain name)
        /// </summary>
        string Host { get; }

        /// <summary>
        /// specified port number
        /// </summary>
        int Port { get; }

        /// <summary>
        /// address类型
        /// </summary>
        ServiceProtocol ServiceProtocol { get; }

        /// <summary>
        /// ipendpoint
        /// </summary>
        IPEndPoint IPEndPoint { get; }

        /// <summary>
        ///  该address是否可用
        /// </summary>
        bool Enabled { get; }

        /// <summary>
        /// Last unavailable time
        /// </summary>
        DateTime? LastDisableTime { get; }

        /// <summary>
        /// 让address熔断
        /// </summary>
        void MakeFusing(int fuseSleepDuration);

        void InitFuseTimes();

        int FuseTimes { get; }

        /// <summary>
        /// address描述符
        /// </summary>
        SilkyEndpointDescriptor Descriptor { get; }
    }
}