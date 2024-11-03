namespace LittleByte.Common;

public abstract record StringValueObject(string Value)
{
    public int Length => Value.Length;

    public override string ToString() => Value;
    public override int GetHashCode() => Value.GetHashCode();


    public static implicit operator string(StringValueObject @this) => @this.Value;
}
