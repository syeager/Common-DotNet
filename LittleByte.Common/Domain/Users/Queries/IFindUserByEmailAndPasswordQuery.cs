using LittleByte.Common.Domain.Users.Models;
using LittleByte.Common.Validation;

namespace LittleByte.Common.Domain.Users.Queries;

public interface IFindUserByEmailAndPasswordQuery
{
    ValueTask<Valid<User>?> TryFindAsync(Email email, Password password);
}
