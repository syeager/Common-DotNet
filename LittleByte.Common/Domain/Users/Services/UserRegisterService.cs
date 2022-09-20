using LittleByte.Common.Domain.Users.Commands;
using LittleByte.Common.Domain.Users.Models;
using LittleByte.Common.Validation;

namespace LittleByte.Common.Domain.Users.Services;

public interface IUserRegisterService
{
    Task<Valid<User>> RegisterAsync(Email email, Name name, Password password);
}

public sealed class UserRegisterService : IUserRegisterService
{
    private readonly IAddUserCommand addUserCommand;
    private readonly IModelValidator<User> userValidator;

    public UserRegisterService(IAddUserCommand addUserCommand, IModelValidator<User> userValidator)
    {
        this.addUserCommand = addUserCommand;
        this.userValidator = userValidator;
    }

    // TODO: Confirm password.
    // TODO: Use password.
    // TODO: Check for existing user with email or name.
    public async Task<Valid<User>> RegisterAsync(Email email, Name name, Password password)
    {
        var id = Guid.NewGuid();
        var user = User.Create(userValidator, id, email, name);

        if(user.IsSuccess)
        {
            await addUserCommand.AddAsync(user, password);
        }

        return user;
    }
}
