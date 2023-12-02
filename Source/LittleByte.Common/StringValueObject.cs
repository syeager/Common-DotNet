namespace LittleByte.Common;

public abstract record StringValueObject(string Value)
{
    public override string ToString() => Value;

    public static implicit operator string(StringValueObject @this) => @this.Value;
}
