using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace LittleByte.Extensions.AspNet
{
    public static class OpenApiConfiguration
    {
        public static IServiceCollection AddOpenApi(this IServiceCollection services, params OpenApiDocument[] documents)
        {
            const string scheme = JwtBearerDefaults.AuthenticationScheme;

            return services.AddSwaggerGen(swagger =>
            {
                foreach(var document in documents)
                {
                    swagger.SwaggerDoc(document.Name, new OpenApiInfo
                    {
                        Version = $"v{document.Version}",
                        Title = document.Title,
                        Description = document.Description,
                    });
                }
                swagger.AddSecurityDefinition(scheme, new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = scheme,
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = $"Enter {scheme} [space] and then your valid token in the text input below.\r\n\r\nExample: \"{scheme} eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9\"",
                });
                swagger.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = scheme
                            }
                        },
                        new string[] { }
                    }
                });
            });
        }

        public static IApplicationBuilder UseOpenApi(this IApplicationBuilder app, string title)
        {
            return app
                .UseSwagger()
                .UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", title));
        }
    }
}