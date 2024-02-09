using System.Diagnostics.CodeAnalysis;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Text;
using LittleByte.Common;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace LittleByte.AspNet;

public static class JwtConfiguration
{
    // TODO: Need to enforce that this comes after .AddIdentity().
    [SuppressMessage("ReSharper", "UnusedMethodReturnValue.Global")]
    public static IServiceCollection AddJwtAuthentication(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        var options = services.BindAndGetOptions<JwtOptions>(configuration);

        services
            .AddTransient<ITokenGenerator, TokenGenerator>()
            .AddTransient<ICredentialsGenerator, CredentialsGenerator>()
            .AddSingleton<SecurityTokenHandler, JwtSecurityTokenHandler>()
            .AddJwtAuthentication(options);

        return services;
    }

    [SuppressMessage("ReSharper", "UnusedMethodReturnValue.Global")]
    public static AuthenticationBuilder AddJwtAuthentication(this IServiceCollection services, JwtOptions jwtOptions)
    {
        JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

        return services
            .AddAuthentication(options =>
            {
                const string scheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultAuthenticateScheme = scheme;
                options.DefaultChallengeScheme = scheme;
                options.DefaultScheme = scheme;
            })
            .AddJwtBearer(options =>
            {
                options.SaveToken = true;
                // TODO: Enable for non-dev.
                options.RequireHttpsMetadata = false;
                var secretBytes = Encoding.UTF8.GetBytes(jwtOptions.Secret);
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    IssuerSigningKey = new SymmetricSecurityKey(secretBytes),
                    ValidAudience = jwtOptions.Audience,
                    ValidIssuer = jwtOptions.Issuer,
                    ValidateAudience = true,
                    ValidateIssuer = true,
                    ValidateIssuerSigningKey = true,
                    ValidateLifetime = true,
                };
                options.Events = new JwtBearerEvents
                {
                    OnChallenge = async context =>
                    {
                        context.HandleResponse();
                        if(context.AuthenticateFailure != null)
                        {
                            var result = new ApiResponse(HttpStatusCode.Unauthorized, context.AuthenticateFailure.Message);
                            await context.Response.WriteJsonAsync(result, (int) HttpStatusCode.Unauthorized);
                        }
                    },
                };
            });
    }
}