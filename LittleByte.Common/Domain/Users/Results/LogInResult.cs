using System.Diagnostics.CodeAnalysis;
using System.IdentityModel.Tokens.Jwt;
using LittleByte.Common.Domain.Users.Models;

namespace LittleByte.Common.Domain.Users.Results;

public class LogInResult
{
    public bool Succeeded { get; }

    [MemberNotNullWhen(true, nameof(Succeeded))]
    public JwtSecurityToken? AccessToken { get; }

    [MemberNotNullWhen(true, nameof(Succeeded))]
    public User? User { get; }

    [MemberNotNullWhen(false, nameof(Succeeded))]
    public IReadOnlyList<string>? Errors { get; }

    private LogInResult(bool succeeded, JwtSecurityToken? accessToken, User? user, IEnumerable<string>? errors)
    {
        Succeeded = succeeded;
        AccessToken = accessToken;
        User = user;
        Errors = errors?.ToArray();
    }

    public static LogInResult Success(JwtSecurityToken authToken, User user) => new(true, authToken, user, null);

    public static LogInResult Fail(IEnumerable<string> error) => new(false, null, null, error);
}
