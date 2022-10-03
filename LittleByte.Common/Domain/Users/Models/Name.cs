namespace LittleByte.Common.Domain.Users.Models;

public record Name(string Value)
{
    public override string ToString() => Value;
}
