using Microsoft.OpenApi.Models;

namespace Silky.Http.Swagger.Internal
{
    public class SpecificationOpenApiSecurityScheme : OpenApiSecurityScheme
    {
        public SpecificationOpenApiSecurityScheme()
        {
        }

        /// <summary>
        /// onlyId
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Safety requirements
        /// </summary>
        public SpecificationOpenApiSecurityRequirementItem Requirement { get; set; }
    }
}