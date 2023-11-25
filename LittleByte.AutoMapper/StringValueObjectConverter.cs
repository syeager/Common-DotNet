using AutoMapper;
using LittleByte.Common;

namespace LittleByte.AutoMapper;

public sealed class StringValueObjectConverter : ITypeConverter<StringValueObject, string>
{
    public string Convert(StringValueObject source, string destination, ResolutionContext context) => source.Value;
}