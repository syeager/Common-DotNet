using AutoMapper;

namespace LittleByte.Common.Domain;

public abstract record StringValueObject(string Value)
{
    public override string ToString() => Value;

    public static implicit operator string(StringValueObject @this) => @this.Value;
}

public sealed class StringValueObjectConverter : ITypeConverter<StringValueObject, string>
{
    public string Convert(StringValueObject source, string destination, ResolutionContext context) => source.Value;
}