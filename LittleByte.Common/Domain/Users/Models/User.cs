using LittleByte.Common.Validation;

namespace LittleByte.Common.Domain.Users.Models;

public class User : DomainModel<User>
{
    public Email Email { get; }
    public Name Name { get; }

    protected User(Id<User> id, Email email, Name name)
        : base(id)
    {
        Email = email;
        Name = name;
    }

    public static Valid<User> Create(IModelValidator<User> validator, Id<User> id, Email email, Name name)
    {
        var user = new User(id, email, name);
        var result = validator.Sign(user);
        return result;
    }
}
