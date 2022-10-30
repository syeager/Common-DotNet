using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace LittleByte.Common.Identity.Services;

public class NullTokenGenerator : ITokenGenerator
{
    public JwtSecurityToken GenerateJwt(IEnumerable<Claim> claims)
    {
        return new JwtSecurityToken();
    }
}