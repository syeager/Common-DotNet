using LittleByte.Common.Domain.Users.Models;
using LittleByte.Common.Validation;

namespace LittleByte.Common.Domain.Users.Commands;

public interface IAddUserCommand
{
    Task AddAsync(Valid<User> user, Password password);
}
