using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace LittleByte.Asp.Identity
{
    public static class TokenHelper
    {
        public static SigningCredentials CreateCredentials(string secret)
        {
            var secretBytes = Encoding.UTF8.GetBytes(secret);
            var signingKey = new SymmetricSecurityKey(secretBytes);
            var signingCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);
            return signingCredentials;
        }
    }
}