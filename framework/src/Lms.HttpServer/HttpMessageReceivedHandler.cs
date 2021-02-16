using System.Threading.Tasks;
using Lms.Core.DependencyInjection;
using Lms.Core.Extensions;
using Lms.Core.Serialization;
using Lms.Rpc.Runtime;
using Lms.Rpc.Runtime.Server;
using Lms.Rpc.Runtime.Server.Parameter;
using Lms.Rpc.Transport;
using Microsoft.AspNetCore.Http;

namespace Lms.HttpServer
{
    internal class HttpMessageReceivedHandler : IScopedDependency
    {
        private readonly IParameterParser _parameterParser;
        private readonly IServiceExecutor _serverExecutor;
        private readonly ISerializer _serializer;

        public HttpMessageReceivedHandler(
            IParameterParser parameterParser,
            IServiceExecutor serverExecutor,
            ISerializer serializer)
        {
            _parameterParser = parameterParser;
            _serverExecutor = serverExecutor;
            _serializer = serializer;
        }

        internal async Task Handle(HttpContext context, ServiceEntry serviceEntry)
        {
            var parameters = await _parameterParser.Parser(context.Request, serviceEntry);
            RpcContext.GetContext().SetAttachment("requestHeader", parameters[ParameterFrom.Header]);
            var excuteResult = await _serverExecutor.Execute(serviceEntry, parameters);
            context.Response.ContentType = "application/json;charset=utf-8";
            context.Response.StatusCode = 200;
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