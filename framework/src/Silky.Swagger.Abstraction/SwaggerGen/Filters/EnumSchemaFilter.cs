using System;
using System.ComponentModel;
using System.Reflection;
using System.Text;
using Microsoft.OpenApi.Models;
using Silky.Core;
using Silky.Core.Serialization;
using Silky.Swagger.Abstraction.SwaggerGen.SchemaGenerator;
using Silky.Swagger.Abstraction.SwaggerGen.SwaggerGenerator;

namespace Silky.Swagger.Abstraction.SwaggerGen.Filters
{
    public class EnumSchemaFilter : ISchemaFilter
    {
        /// <summary>
        /// JSON Serialization
        /// </summary>
        private readonly ISerializer _serializer;

        public EnumSchemaFilter(ISerializer serializer)
        {
            _serializer = serializer;
        }

        /// <summary>
        /// Implement the filter method
        /// </summary>
        /// <param name="model"></param>
        /// <param name="context"></param>
        public void Apply(OpenApiSchema model, SchemaFilterContext context)
        {
            var type = context.Type;

            // Exclude enumeration of other assemblies
            if (type.IsEnum && EngineContext.Current.TypeFinder.GetAssemblies().Contains(type.Assembly))
            {
                model.Enum.Clear();
                var stringBuilder = new StringBuilder();
                stringBuilder.Append($"{model.Description}<br />");

                var enumValues = Enum.GetValues(type);
                foreach (var value in enumValues)
                {
                    // Get enum member properties
                    var fieldinfo = type.GetField(Enum.GetName(type, value));
                    var descriptionAttribute = fieldinfo.GetCustomAttribute<DescriptionAttribute>(true);
                    model.Enum.Add(OpenApiAnyFactory.CreateFromJson(_serializer.Serialize(value)));

                    stringBuilder.Append($"&nbsp;{descriptionAttribute?.Description} {value} = {value}<br />");
                }

                model.Description = stringBuilder.ToString();
            }
        }
    }
}