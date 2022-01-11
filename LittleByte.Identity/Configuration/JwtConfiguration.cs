using System.IdentityModel.Tokens.Jwt;
using System.Text;
using LittleByte.Configuration;
using LittleByte.Identity.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace LittleByte.Identity.Configuration;

public static class JwtConfiguration
{
    public static IServiceCollection AddJwtAuthentication(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var options = services.BindAndGetOptions<JwtOptions>(configuration);
        services
            .AddTransient<SecurityTokenHandler, JwtSecurityTokenHandler>()
            .AddTransient<ITokenGenerator, TokenGenerator>()
            .AddTransient<ICredentialsGenerator, CredentialsGenerator>()
            .AddJwtAuthentication(options);

        return services;
    }

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
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidAudience = jwtOptions.Audience,
                    ValidIssuer = jwtOptions.Issuer,
                    IssuerSigningKey = new SymmetricSecurityKey(secretBytes)
                };
            });
    }
}
