using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Silky.Core;
using Silky.Core.Extensions.Collections.Generic;
using Silky.Core.Threading;
using Silky.Http.Swagger.Configuration;
using Silky.Rpc.Runtime.Server;
using Silky.Swagger.Abstraction;
using Silky.Swagger.Abstraction.SwaggerGen.DependencyInjection;
using Silky.Swagger.Abstraction.SwaggerGen.Filters;
using Silky.Swagger.Abstraction.SwaggerUI;
using Silky.Swagger.Gen.Provider;

namespace Silky.Http.Swagger.Builders
{
    internal static class SwaggerDocumentBuilder
    {
        private static readonly IEnumerable<string> DocumentGroups;
        private static readonly IEnumerable<Assembly> ApplicationInterfaceAssemblies;
        private static readonly IEnumerable<Assembly> ProjectAssemblies;

        private static readonly string RouteTemplate = "/swagger/{documentName}/swagger.json";
        private static readonly string SilkyAppServicePrefix = "Silky.Http.Dashboard";
     
        private static readonly string RpcAppService = "Silky.Rpc";


        static SwaggerDocumentBuilder()
        {
            ProjectAssemblies = EngineContext.Current.TypeFinder.GetAssemblies();
            ApplicationInterfaceAssemblies = ServiceHelper.ReadInterfaceAssemblies();
            DocumentGroups = ReadGroups(ApplicationInterfaceAssemblies);
        }


        public static void BuildGen(SwaggerGenOptions swaggerGenOptions, IConfiguration configuration)
        {
            var swaggerDocumentOptions = configuration
                .GetSection(SwaggerDocumentOptions.SwaggerDocument)
                .Get<SwaggerDocumentOptions>() ?? new SwaggerDocumentOptions();

            AddGroupSwaggerGen(swaggerGenOptions, swaggerDocumentOptions);

            swaggerGenOptions.DocInclusionPredicate(CheckServiceEntryInCurrentGroup);

            swaggerGenOptions.SchemaFilter<EnumSchemaFilter>();

            ConfigureSecurities(swaggerGenOptions, swaggerDocumentOptions);

            if (swaggerDocumentOptions.EnableMultipleServiceKey)
            {
                swaggerGenOptions.MultipleServiceKey();
            }

            LoadXmlComments(swaggerGenOptions, swaggerDocumentOptions);
        }

        private static void LoadXmlComments(SwaggerGenOptions swaggerGenOptions,
            SwaggerDocumentOptions swaggerDocumentOptions)
        {
            foreach (var projectAssembly in ProjectAssemblies)
            {
                var xmlFile = $"{projectAssembly.GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                if (File.Exists(xmlPath))
                {
                    swaggerGenOptions.IncludeXmlComments(xmlPath);
                }
            }

            if (!swaggerDocumentOptions.XmlComments.IsNullOrEmpty())
            {
                foreach (var xmlPath in swaggerDocumentOptions.XmlComments)
                {
                    if (File.Exists(xmlPath))
                    {
                        swaggerGenOptions.IncludeXmlComments(xmlPath);
                    }
                }
            }
        }

        public static void Build(SwaggerOptions swaggerOptions, SwaggerDocumentOptions swaggerDocumentOptions)
        {
            swaggerOptions.SerializeAsV2 = swaggerDocumentOptions.FormatAsV2;
            // Configure route templates
            swaggerOptions.RouteTemplate = RouteTemplate;
        }

        public static void BuildUI(SwaggerUIOptions swaggerUIOptions, SwaggerDocumentOptions swaggerDocumentOptions)
        {
            // Configure packet endpoint routing
            CreateEndpoint(swaggerUIOptions, swaggerDocumentOptions);

            InjectMiniProfilerPlugin(swaggerUIOptions);

            // Configure multilingual and automatic logintoken
            AddDefaultInterceptor(swaggerUIOptions);

            // Configure document title
            swaggerUIOptions.DocumentTitle = swaggerDocumentOptions.Title;


            // configureUIaddress
            swaggerUIOptions.RoutePrefix = swaggerDocumentOptions.RoutePrefix;

            // Document expansion settings
            swaggerUIOptions.DocExpansion(swaggerDocumentOptions.DocExpansionState);
        }

        private static void AddDefaultInterceptor(SwaggerUIOptions swaggerUIOptions)
        {
            // Configure multilingual and automatic logintoken
            swaggerUIOptions.UseRequestInterceptor("(request) => { return defaultRequestInterceptor(request); }");
            swaggerUIOptions.UseResponseInterceptor("(response) => { return defaultResponseInterceptor(response); }");
        }

        private static IEnumerable<string> ReadGroups(IEnumerable<Assembly> applicationInterfaceAssemblies)
        {
            var swaggerDocumentOptions = EngineContext.Current.Configuration
                .GetSection(SwaggerDocumentOptions.SwaggerDocument)
                .Get<SwaggerDocumentOptions>() ?? new SwaggerDocumentOptions();

            var groups = new List<string>();
            switch (swaggerDocumentOptions.ShowMode)
            {
                case ShowMode.All:
                    groups.AddRange(GetLocalGroups(applicationInterfaceAssemblies, swaggerDocumentOptions));
                    groups.AddRange(GetRegisterCenterGroupGroups());
                    break;
                case ShowMode.Interface:
                    groups.AddRange(GetLocalGroups(applicationInterfaceAssemblies, swaggerDocumentOptions));

                    break;
                case ShowMode.RegisterCenter:
                    groups.AddRange(GetRegisterCenterGroupGroups());
                    break;
            }

            return groups.Distinct();
        }

        private static ICollection<string> GetLocalGroups(IEnumerable<Assembly> applicationInterfaceAssemblies,
            SwaggerDocumentOptions swaggerDocumentOptions)
        {
            var groups = new List<string>();
            if (!swaggerDocumentOptions.ShowDashboardService)
            {
                applicationInterfaceAssemblies =
                    applicationInterfaceAssemblies.Where(p => !p.FullName.Contains(SilkyAppServicePrefix));
            }

            switch (swaggerDocumentOptions.OrganizationMode)
            {
                case OrganizationMode.Group:
                    groups.AddRange(applicationInterfaceAssemblies
                        .Select(p => p.GetName().Name));
                    break;
                case OrganizationMode.NoGroup:
                    groups.Add(swaggerDocumentOptions.Title);
                    break;
                case OrganizationMode.AllAndGroup:
                    groups.Add(swaggerDocumentOptions.Title);
                    groups.AddRange(applicationInterfaceAssemblies
                        .Select(p => p.GetName().Name));
                    break;
            }

            return groups;
        }

        private static ICollection<string> GetRegisterCenterGroupGroups()
        {
            var swaggerInfoProvider = EngineContext.Current.Resolve<ISwaggerInfoProvider>();
            var registerCenterGroups = AsyncHelper.RunSync(()=> swaggerInfoProvider.GetGroups());
            return registerCenterGroups;
        }

        private static void ConfigureSecurities(SwaggerGenOptions swaggerGenOptions,
            SwaggerDocumentOptions swaggerDocumentOptions)
        {
            // Determine if authorization is enabled
            if (swaggerDocumentOptions.EnableAuthorized != true ||
                swaggerDocumentOptions.SecurityDefinitions.Length == 0) return;

            var openApiSecurityRequirement = new OpenApiSecurityRequirement();

            // Generate security definitions
            foreach (var securityDefinition in swaggerDocumentOptions.SecurityDefinitions)
            {
                // Id must be defined
                if (string.IsNullOrWhiteSpace(securityDefinition.Id)) continue;

                // Add security definition
                var openApiSecurityScheme = securityDefinition as OpenApiSecurityScheme;
                swaggerGenOptions.AddSecurityDefinition(securityDefinition.Id, openApiSecurityScheme);

                // Add security requirements
                var securityRequirement = securityDefinition.Requirement;

                // C# 9.0 New syntax for pattern matching
                if (securityRequirement is { Scheme: { Reference: not null } })
                {
                    securityRequirement.Scheme.Reference.Id ??= securityDefinition.Id;
                    openApiSecurityRequirement.Add(securityRequirement.Scheme, securityRequirement.Accesses);
                }
            }

            // Add security requirements
            if (openApiSecurityRequirement.Count > 0)
            {
                swaggerGenOptions.AddSecurityRequirement(openApiSecurityRequirement);
            }
        }

        private static bool CheckServiceEntryInCurrentGroup(string currentGroup, ServiceEntry serviceEntry)
        {
            var swaggerDocumentOptions = EngineContext.Current.Configuration
                .GetSection(SwaggerDocumentOptions.SwaggerDocument)
                .Get<SwaggerDocumentOptions>() ?? new SwaggerDocumentOptions();

            if (serviceEntry.Id.StartsWith(SilkyAppServicePrefix) && !swaggerDocumentOptions.ShowDashboardService)
            {
                return false;
            }

            if (serviceEntry.Id.StartsWith(RpcAppService))
            {
                return false;
            }

            if (currentGroup.Equals(swaggerDocumentOptions.Title))
            {
                return true;
            }

            return serviceEntry.ServiceEntryDescriptor.Id.Contains(currentGroup);
        }


        private static void AddGroupSwaggerGen(SwaggerGenOptions options, SwaggerDocumentOptions swaggerDocumentOptions)
        {
            foreach (var documentGroup in DocumentGroups)
            {
                var groupDescription =
                    swaggerDocumentOptions.Groups.FirstOrDefault(p => p.ApplicationInterface == documentGroup);
                if (groupDescription == null)
                {
                    groupDescription = new GroupDescription(documentGroup, swaggerDocumentOptions);
                }

                options.SwaggerDoc(documentGroup,
                    new OpenApiInfo()
                    {
                        Title = groupDescription.Title ?? documentGroup,
                        Description = groupDescription.Description,
                        Version = groupDescription.Version,
                        TermsOfService = groupDescription.TermsOfService,
                        Contact = groupDescription.Contact
                    });
            }
        }

        private static void InjectMiniProfilerPlugin(SwaggerUIOptions swaggerUIOptions)
        {
            // enable MiniProfiler components
            var thisType = typeof(SwaggerUIOptions);
            var thisAssembly = thisType.Assembly;

            // customize Swagger front page
            var customIndex = $"Silky.Swagger.Abstraction.SwaggerUI.index-mini-profiler.html";
            swaggerUIOptions.IndexStream = () => thisAssembly.GetManifestResourceStream(customIndex);
        }

        private static void CreateEndpoint(SwaggerUIOptions options, SwaggerDocumentOptions swaggerDocumentOptions)
        {
            foreach (var documentGroup in DocumentGroups)
            {
                var routeTemplate =
                    RouteTemplate.Replace("{documentName}", Uri.EscapeDataString(documentGroup));

                options.SwaggerEndpoint(routeTemplate, documentGroup);
            }
        }
    }
}