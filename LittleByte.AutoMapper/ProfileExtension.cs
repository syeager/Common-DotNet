using AutoMapper;

namespace LittleByte.AutoMapper;

[Obsolete]
public static class ProfileExtension
{
    public static void MapBoth<T1, T2>(this Profile @this)
    {
        @this.CreateMap<T1, T2>();
        @this.CreateMap<T2, T1>();
    }

    public static void MapBothConvertBoth<T1, T2, TMap>(this Profile @this)
        where TMap : ITypeConverter<T1, T2>, ITypeConverter<T2, T1>
    {
        @this.CreateMap<T1, T2>().ConvertUsing<TMap>();
        @this.CreateMap<T2, T1>().ConvertUsing<TMap>();
    }

    public static void MapBothConvertOne<T1, T2, TMap>(this Profile @this)
        where TMap : ITypeConverter<T1, T2>
    {
        @this.CreateMap<T1, T2>().ConvertUsing<TMap>();
        @this.CreateMap<T2, T1>();
    }
}