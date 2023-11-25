using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace LittleByte.AspNet;

public interface ITokenGenerator
{
    JwtSecurityToken GenerateJwt(IEnumerable<Claim> claims);
}

public sealed class TokenGenerator : ITokenGenerator
{
    private TimeProvider timeProvider;
    private readonly JwtOptions jwtOptions;
    private readonly SigningCredentials signingCredentials;

    public TokenGenerator(TimeProvider timeProvider, IOptions<JwtOptions> jwtOptions, ICredentialsGenerator credentialsGenerator)
    {
        this.timeProvider = timeProvider;
        this.jwtOptions = jwtOptions.Value;
        signingCredentials = credentialsGenerator.Create(this.jwtOptions.Secret);
    }

    public JwtSecurityToken GenerateJwt(IEnumerable<Claim> claims)
    {
        var validTo = timeProvider.GetUtcNow().DateTime.AddMinutes(jwtOptions.TtlMinutes);
        var token = new JwtSecurityToken(
            jwtOptions.Issuer,
            jwtOptions.Audience,
            expires: validTo,
            signingCredentials: signingCredentials,
            claims: claims
        );
        return token;
    }
}