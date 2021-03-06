﻿using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using NSwag;
using NSwag.AspNetCore;
using NSwag.SwaggerGeneration.Processors.Security;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.AspNetCore.Builder
{
    public static class NswagExtensions
    {
        public static void AddOpenApiDocument(this IServiceCollection services)
        {
            services.AddOpenApiDocument(document =>
            {
                document.Title = "Spatem API";
                document.AddSecurity("bearer", Enumerable.Empty<string>(), new SwaggerSecurityScheme
                {
                    Type = SwaggerSecuritySchemeType.OAuth2,
                    Flows = new OpenApiOAuthFlows()
                    {
                        Implicit = new OpenApiOAuthFlow()
                        {
                            Scopes = new Dictionary<string, string>
                            {
                                { "spatem.api", "Spatem API" }
                            },
                            AuthorizationUrl = "http://localhost:5001/connect/authorize"
                        }
                    }
                });
                document.PostProcess = swaggerDocument =>
                {
                    //Remove api level security requirenment
                    swaggerDocument.Security = null;
                };
                //Add method level security requirenment
                document.OperationProcessors.Add(new AspNetCoreOperationSecurityScopeProcessor("bearer"));
            });
        }

        public static void UseOpenApiDoc(this IApplicationBuilder app)
        {
            app.UseOpenApi(settigns => { });
        }

        public static void UseSwaggerUi(this IApplicationBuilder app)
        {
            app.UseSwaggerUi3(settings =>
            {
                settings.OAuth2Client = new OAuth2ClientSettings
                {
                    ClientId = "spatem.swagger",
                    AppName = "Swagger"
                };
            });
        }
    }
}