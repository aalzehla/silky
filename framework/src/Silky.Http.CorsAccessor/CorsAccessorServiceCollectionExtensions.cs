using System;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.Extensions.Configuration;
using Silky.Core;
using Silky.Http.CorsAccessor.Configuration;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class CorsAccessorServiceCollectionExtensions
    {
        /// <summary>
        /// Configure cross domain
        /// </summary>
        /// <param name="services">service collection</param>
        /// <returns>service collection</returns>
        public static IServiceCollection AddCorsAccessor(this IServiceCollection services,
            Action<CorsOptions> corsOptionsHandler = default,
            Action<CorsPolicyBuilder> corsPolicyBuilderHandler = default)
        {
            services.AddOptions<CorsAccessorOptions>()
                .Bind(EngineContext.Current.Configuration.GetSection(CorsAccessorOptions.CorsAccessor));

            // get options
            var corsAccessorSettings =
                EngineContext.Current.Configuration.GetSection(CorsAccessorOptions.CorsAccessor)
                    .Get<CorsAccessorOptions>() ??
                new CorsAccessorOptions();

            // Add cross domain service
            services.AddCors(options =>
            {
                // Add policy cross domain
                options.AddPolicy(name: corsAccessorSettings.PolicyName, builder =>
                {
                    // Determine whether the source is set，because AllowAnyOrigin cannot and AllowCredentialsshare together
                    var isNotSetOrigins = corsAccessorSettings.WithOrigins == null ||
                                          corsAccessorSettings.WithOrigins.Length == 0;

                    // If no source is configured，all sources are allowed
                    if (isNotSetOrigins) builder.AllowAnyOrigin();
                    else
                        builder.WithOrigins(corsAccessorSettings.WithOrigins)
                            .SetIsOriginAllowedToAllowWildcardSubdomains();

                    // If no request headers are configured，all headers are allowed
                    if (corsAccessorSettings.WithHeaders == null || corsAccessorSettings.WithHeaders.Length == 0)
                        builder.AllowAnyHeader();
                    else builder.WithHeaders(corsAccessorSettings.WithHeaders);

                    // If no request verb is configured，then all request verbs are allowed
                    if (corsAccessorSettings.WithMethods == null || corsAccessorSettings.WithMethods.Length == 0)
                        builder.AllowAnyMethod();
                    else builder.WithMethods(corsAccessorSettings.WithMethods);

                    // Configure cross domain凭据
                    if (corsAccessorSettings.AllowCredentials == true && !isNotSetOrigins) builder.AllowCredentials();

                    // Configure response headers
                    if (corsAccessorSettings.WithExposedHeaders != null &&
                        corsAccessorSettings.WithExposedHeaders.Length > 0)
                        builder.WithExposedHeaders(corsAccessorSettings.WithExposedHeaders);

                    // Set preflight expiration time
                    if (corsAccessorSettings.SetPreflightMaxAge.HasValue)
                        builder.SetPreflightMaxAge(TimeSpan.FromSeconds(corsAccessorSettings.SetPreflightMaxAge.Value));

                    // Add custom configuration
                    corsPolicyBuilderHandler?.Invoke(builder);
                });

                // Add custom configuration
                corsOptionsHandler?.Invoke(options);
            });

            return services;
        }
    }
}