using System.Diagnostics.CodeAnalysis;
using LittleByte.Common.Domain.Users.Models;
using LittleByte.Common.Validation;

namespace LittleByte.Common.Domain.Users.Results;

public class RegisterResult
{
    public bool Succeeded { get; }

    [MemberNotNullWhen(true, nameof(Succeeded))]
    public Valid<User>? User { get; }

    [MemberNotNullWhen(false, nameof(Succeeded))]
    public IReadOnlyList<string>? Errors { get; }

    private RegisterResult(bool succeeded, Valid<User>? user, IEnumerable<string>? errors)
    {
        Succeeded = succeeded;
        User = user;
        Errors = errors?.ToArray();
    }

    public static RegisterResult Success(Valid<User> user) => new(true, user, null);

    public static RegisterResult Fail(IEnumerable<string> errors) => new(false, null, errors);
}
