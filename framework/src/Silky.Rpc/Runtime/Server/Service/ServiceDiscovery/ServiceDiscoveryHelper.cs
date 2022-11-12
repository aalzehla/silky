using System;
using System.Linq;
using System.Reflection;
using Silky.Core.Exceptions;
using Silky.Rpc.Routing;

namespace Silky.Rpc.Runtime.Server
{
    public static class ServiceDiscoveryHelper
    {
        public static IRouteTemplateProvider GetServiceBundleProvider(Type serviceType)
        {
            var serviceTypeInterface = serviceType.IsInterface
                ? serviceType
                : serviceType.GetInterfaces()
                    .FirstOrDefault(p => p.GetCustomAttribute<ServiceRouteAttribute>() != null);
            if (serviceTypeInterface == null)
            {
                throw new SilkyException($"{serviceType.FullName}not a service type,Service type must passServiceBundleAttributefeature to identify");
            }

            return serviceTypeInterface.GetCustomAttribute<ServiceRouteAttribute>();
        }
    }
}