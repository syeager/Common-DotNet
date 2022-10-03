using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using LittleByte.Common.Domain.Users.Models;
using LittleByte.Common.Domain.Users.Queries;
using LittleByte.Common.Domain.Users.Results;
using LittleByte.Common.Identity.Services;

namespace LittleByte.Common.Domain.Users.Services;

public interface ILogInService
{
    public Task<LogInResult> LogInAsync(Email email, Password password);
}

public sealed class LogInService : ILogInService
{
    private readonly IFindUserByEmailAndPasswordQuery findUserByEmailAndPasswordQuery;
    private readonly ITokenGenerator tokenGenerator;

    public LogInService(
        ITokenGenerator tokenGenerator,
        IFindUserByEmailAndPasswordQuery findUserByEmailAndPasswordQuery)
    {
        this.tokenGenerator = tokenGenerator;
        this.findUserByEmailAndPasswordQuery = findUserByEmailAndPasswordQuery;
    }

    public async Task<LogInResult> LogInAsync(Email email, Password password)
    {
        LogInResult result;

        var validUser = await findUserByEmailAndPasswordQuery.TryFindAsync(email, password);
        if(validUser is not null)
        {
            var user = validUser.Value.GetModelOrThrow();
            var claims = GetUserClaims(user);
            var token = tokenGenerator.GenerateJwt(claims);
            result = LogInResult.Success(token, user);
        }
        else
        {
            result = LogInResult.Fail(new[] {"todo"}); // TODO
        }

        return result;
    }

    private static IEnumerable<Claim> GetUserClaims(User user)
    {
        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Email, user.Email.ToString()),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(JwtRegisteredClaimNames.NameId, user.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.UniqueName, user.Name.ToString())
        };
        return claims;
    }
}
