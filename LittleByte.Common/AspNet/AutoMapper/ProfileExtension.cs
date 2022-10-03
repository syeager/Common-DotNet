using AutoMapper;

namespace LittleByte.Common.AspNet.AutoMapper;

public static class ProfileExtension
{
    public static void CreateBiDirectionMap<T1, T2>(this Profile @this)
    {
        @this.CreateMap<T1, T2>();
        @this.CreateMap<T2, T1>();
    }
}
