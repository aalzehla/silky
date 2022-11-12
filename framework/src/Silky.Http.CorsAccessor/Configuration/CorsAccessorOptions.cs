using System;

namespace Silky.Http.CorsAccessor.Configuration
{
    public class CorsAccessorOptions
    {
        internal const string CorsAccessor = "CorsAccessor";

        /// <summary>
        /// Policy name
        /// </summary>
        public string PolicyName { get; set; }

        /// <summary>
        /// Allow source domains，Allow all sources without configuration
        /// </summary>
        public string[] WithOrigins { get; set; }

        /// <summary>
        /// request header，All headers are allowed without configuration
        /// </summary>
        public string[] WithHeaders { get; set; }

        /// <summary>
        /// response headers
        /// </summary>
        public string[] WithExposedHeaders { get; set; }

        /// <summary>
        /// Set cross-domain allow request verb，No configuration allows all
        /// </summary>
        public string[] WithMethods { get; set; }

        /// <summary>
        /// Credentials in Cross-Origin Requests
        /// </summary>
        public bool? AllowCredentials { get; set; }

        /// <summary>
        /// Set preflight expiration time
        /// </summary>
        public int? SetPreflightMaxAge { get; set; }

        public CorsAccessorOptions()
        {
            PolicyName ??= "App.CorsAccessor.Policy";
            WithOrigins ??= Array.Empty<string>();
            AllowCredentials ??= true;
        }
    }
}