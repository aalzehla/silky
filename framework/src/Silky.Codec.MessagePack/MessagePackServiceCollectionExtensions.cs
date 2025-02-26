using Microsoft.Extensions.DependencyInjection.Extensions;
using Silky.Codec;
using Silky.Core.DependencyInjection;
using Silky.Rpc.Transport.Codec;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class MessagePackServiceCollectionExtensions
    {
        public static IServiceCollection AddMessagePackCodec(this IServiceCollection services)
        {
            if (!services.IsAdded(typeof(ITransportMessageDecoder)))
            {
                services.AddSingleton<ITransportMessageDecoder, MessagePackTransportMessageDecoder>();
            }
            else
            {
                services.Replace(
                    ServiceDescriptor.Singleton<ITransportMessageDecoder, MessagePackTransportMessageDecoder>());
            }

            if (!services.IsAdded(typeof(ITransportMessageEncoder)))
            {
                services.AddSingleton<ITransportMessageEncoder, MessagePackTransportMessageEncoder>();
            }
            else
            {
                services.Replace(
                    ServiceDescriptor.Singleton<ITransportMessageEncoder, MessagePackTransportMessageEncoder>());
            }

            return services;
        }
    }
}