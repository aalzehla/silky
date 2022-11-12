using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace Silky.Core.Extensions
{
    public static class StringTemplateExtensions
    {
        /// <summary>
        /// Template regular expressions
        /// </summary>
        private const string commonTemplatePattern = @"\{(?<p>.+?)\}";

        /// <summary>
        /// 读取配置Template regular expressions
        /// </summary>
        private const string configTemplatePattern = @"\#\((?<p>.*?)\)";
        
        
        /// <summary>
        /// Render string template from config
        /// </summary>
        /// <param name="template"></param>
        /// <param name="encode"></param>
        /// <returns></returns>
        public static string Render(this string template, bool encode = false)
        {
            if (template == null) return default;

            // Check if a string contains a template
            if (!Regex.IsMatch(template, configTemplatePattern)) return template;

            // Get all matching templates
            var templateValues = Regex.Matches(template, configTemplatePattern)
                .Select(u => new
                {
                    Template = u.Groups["p"].Value,
                    Value = EngineContext.Current.Configuration[u.Groups["p"].Value]
                });

            // Circular replacement template
            foreach (var item in templateValues)
            {
                template = template.Replace($"#({item.Template})",
                    encode ? Uri.EscapeDataString(item.Value?.ToString() ?? string.Empty) : item.Value?.ToString());
            }

            return template;
        }

        private static object ResolveTemplateValue(string template, object data)
        {
            // according to . split template
            var propertyCrumbs = template.Split('.', StringSplitOptions.RemoveEmptyEntries);
            return GetValue(propertyCrumbs, data);

            // static local function
            static object GetValue(string[] propertyCrumbs, object data)
            {
                if (data == null || propertyCrumbs == null || propertyCrumbs.Length <= 1) return data;
                var dataType = data.GetType();

                // If it is a primitive type; return directly
                if (dataType.IsRichPrimitive()) return data;
                object value = null;

                // Recursively get the next level template value
                for (var i = 1; i < propertyCrumbs.Length; i++)
                {
                    var propery = dataType.GetProperty(propertyCrumbs[i]);
                    if (propery == null) break;

                    value = propery.GetValue(data);
                    if (i + 1 < propertyCrumbs.Length)
                    {
                        value = GetValue(propertyCrumbs.Skip(i).ToArray(), value);
                    }
                    else break;
                }

                return value;
            }
        }
    }
}