using System.Threading.Tasks;
using Silky.Core;
using Silky.Core.Exceptions;
using Silky.Core.Extensions;
using Silky.Core.Serialization;
using Silky.Rpc.Configuration;
using Silky.Rpc.Runtime;
using Silky.Rpc.Runtime.Server;
using Silky.Rpc.Runtime.Server.Parameter;
using Silky.Rpc.Transport;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Silky.Core.Rpc;
using Silky.Http.Core.Configuration;
using Silky.Rpc;
using Silky.Rpc.MiniProfiler;

namespace Silky.Http.Core.Handlers
{
    internal abstract class MessageReceivedHandlerBase : IMessageReceivedHandler
    {
        protected readonly IParameterParser _parameterParser;
        protected readonly ISerializer _serializer;
        protected readonly IServiceExecutor _serviceExecutor;
        
        protected GatewayOptions _gatewayOptions;
        protected RpcOptions _rpcOptions;

        protected MessageReceivedHandlerBase(
            IParameterParser parameterParser,
            ISerializer serializer,
            IOptionsMonitor<RpcOptions> rpcOptions,
            IOptionsMonitor<GatewayOptions> gatewayOptions,
            IServiceExecutor serviceExecutor)
        {
            _parameterParser = parameterParser;
            _serializer = serializer;
            _serviceExecutor = serviceExecutor;
            _rpcOptions = rpcOptions.CurrentValue;
            _gatewayOptions = gatewayOptions.CurrentValue;
            rpcOptions.OnChange((options, s) => _rpcOptions = options);
            gatewayOptions.OnChange((options, s) => _gatewayOptions = options);
        }

        public virtual async Task Handle(HttpContext context, ServiceEntry serviceEntry)
        {
            Check.NotNull(context, nameof(context));
            Check.NotNull(serviceEntry, nameof(serviceEntry));
            var requestParameters = await _parameterParser.Parser(context.Request, serviceEntry);
            RpcContext.Context
                .SetAttachment(AttachmentKeys.RequestHeader, requestParameters[ParameterFrom.Header]);
            RpcContext.Context
                .SetAttachment(AttachmentKeys.IsGatewayHost, true);
            var rpcParameters = serviceEntry.ResolveParameters(requestParameters);
            string serviceKey = null;

            if (context.Request.Headers.ContainsKey("serviceKey"))
            {
                serviceKey = context.Request.Headers["serviceKey"].ToString();
                RpcContext.Context.SetAttachment(AttachmentKeys.ServiceKey, serviceKey);
                MiniProfilerPrinter.Print(MiniProfileConstant.Route.Name,
                    MiniProfileConstant.Route.State.FindServiceKey,
                    $"serviceKey => {serviceKey}");
            }
            else
            {
                MiniProfilerPrinter.Print(MiniProfileConstant.Route.Name,
                    MiniProfileConstant.Route.State.FindServiceKey,
                    "No serviceKey is set");
            }

            RpcContext.Context.SetAttachment(AttachmentKeys.RpcToken, _rpcOptions.Token);
            var excuteResult = await _serviceExecutor.Execute(serviceEntry, rpcParameters, serviceKey);
            context.Response.ContentType = "application/json;charset=utf-8";
            context.Response.StatusCode = ResponseStatusCode.Success;
            context.Response.SetResultCode(StatusCode.Success);
            if (_gatewayOptions.WrapResult)
            {
                var responseResult = new ResponseResultDto()
                {
                    Data = excuteResult,
                    Status = StatusCode.Success,
                };
                var responseData = _serializer.Serialize(responseResult);
                context.Response.ContentLength = responseData.GetBytes().Length;
                await context.Response.WriteAsync(responseData);
            }
            else
            {
                if (excuteResult != null)
                {
                    var responseData = _serializer.Serialize(excuteResult);
                    context.Response.ContentLength = responseData.GetBytes().Length;
                    await context.Response.WriteAsync(responseData);
                }
                else
                {
                    context.Response.ContentLength = 0;
                    await context.Response.WriteAsync(string.Empty);
                }
            }
        }
    }
}