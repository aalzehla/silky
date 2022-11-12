using Microsoft.OpenApi.Models;

namespace Silky.Http.Swagger.Internal
{
    public sealed class SpecificationOpenApiSecurityRequirementItem
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public SpecificationOpenApiSecurityRequirementItem()
        {
            Accesses = System.Array.Empty<string>();
        }

        /// <summary>
        /// SafetySchema
        /// </summary>
        public OpenApiSecurityScheme Scheme { get; set; }

        /// <summary>
        /// permission
        /// </summary>
        public string[] Accesses { get; set; }
    }
}