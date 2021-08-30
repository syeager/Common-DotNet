using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using LittleByte.Core.Common;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace LittleByte.Asp.Identity
{
    public interface ITokenGenerator
    {
        JwtSecurityToken GenerateJwt(IReadOnlyCollection<Claim> claims);
    }

    public class TokenGenerator : ITokenGenerator
    {
        private readonly JwtOptions jwtOptions;
        private readonly SigningCredentials signingCredentials;

        public TokenGenerator(IOptions<JwtOptions> jwtOptions)
        {
            this.jwtOptions = jwtOptions.Value;

            signingCredentials = TokenHelper.CreateCredentials(this.jwtOptions.Secret);
        }

        public JwtSecurityToken GenerateJwt(IReadOnlyCollection<Claim> claims)
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