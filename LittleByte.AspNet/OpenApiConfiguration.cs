using Microsoft.AspNetCore.Authentication.JwtBearer;
using NSwag;
using NSwag.Generation.Processors.Security;

namespace LittleByte.AspNet;

public static class OpenApiConfiguration
{
    public static IServiceCollection AddOpenApi(this IServiceCollection services, string title)
    {
        return services
            .AddOpenApiDocument(options =>
            {
                const string scheme = JwtBearerDefaults.AuthenticationScheme;

                options.Title = title;
                options.DocumentName = title;
                options.OperationProcessors.Add(new OperationSecurityScopeProcessor(scheme));
                options.DocumentProcessors.Add(new SecurityDefinitionAppender(
                    scheme,
                    new OpenApiSecurityScheme
                    {
                        Type = OpenApiSecuritySchemeType.ApiKey,
                        Name = "Authorization",
                        In = OpenApiSecurityApiKeyLocation.Header,
                        BearerFormat = "jwt",
                        Scheme = scheme,
                        Description =
                            $"Enter {scheme} [space] and then your valid token in the text input below.\r\n\r\nExample: \"{scheme} eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9\"",
                    }));
            });
    }

    public static IApplicationBuilder UseOpenApi(this IApplicationBuilder app)
    {
        return app
            .UseOpenApi(null)
            .UseSwaggerUi3();
    }
}