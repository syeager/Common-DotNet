using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace LittleByte.Asp.Identity;

public interface ICredentialsGenerator
{
    SigningCredentials Create(string secret);
}

public class CredentialsGenerator : ICredentialsGenerator
{
    public SigningCredentials Create(string secret)
    {
        var secretBytes = Encoding.UTF8.GetBytes(secret);
        var signingKey = new SymmetricSecurityKey(secretBytes);
        var signingCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);
        return signingCredentials;
    }
}
