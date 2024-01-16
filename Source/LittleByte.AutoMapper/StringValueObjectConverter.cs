using AutoMapper;
using JetBrains.Annotations;
using LittleByte.Common;

namespace LittleByte.AutoMapper;

[UsedImplicitly]
public sealed class StringValueObjectConverter : ITypeConverter<StringValueObject, string>
{
    public string Convert(StringValueObject source, string destination, ResolutionContext context) => source.Value;
}