using AutoMapper;

namespace LittleByte.AutoMapper;

public static class IMapperExtension
{
    public static T[] MapRange<T>(this IMapper @this, IEnumerable<object> source) => source.Select(@this.Map<T>).ToArray();
}
