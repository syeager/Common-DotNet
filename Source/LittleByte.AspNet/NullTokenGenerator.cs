using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace LittleByte.AspNet;

public sealed class NullTokenGenerator : ITokenGenerator
{
    public JwtSecurityToken GenerateJwt(IEnumerable<Claim> claims)
    {
        return new JwtSecurityToken();
    }
}