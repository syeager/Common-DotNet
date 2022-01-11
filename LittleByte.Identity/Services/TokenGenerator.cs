using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using LittleByte.Core.Common;
using LittleByte.Identity.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace LittleByte.Identity.Services
{
    public interface ITokenGenerator
    {
        JwtSecurityToken GenerateJwt(IEnumerable<Claim> claims);
    }

    public class TokenGenerator : ITokenGenerator
    {
        private readonly JwtOptions jwtOptions;
        private readonly SigningCredentials signingCredentials;

        public TokenGenerator(IOptions<JwtOptions> jwtOptions, ICredentialsGenerator credentialsGenerator)
        {
            this.jwtOptions = jwtOptions.Value;

            signingCredentials = credentialsGenerator.Create(this.jwtOptions.Secret);
        }

        public JwtSecurityToken GenerateJwt(IEnumerable<Claim> claims)
        {
            var validTo = S.Date.UtcNow.AddMinutes(jwtOptions.TtlMinutes);
            var token = new JwtSecurityToken(
                issuer: jwtOptions.Issuer,
                audience: jwtOptions.Audience,
                expires: validTo,
                signingCredentials: signingCredentials,
                claims: claims
            );
            return token;
        }
    }
}
