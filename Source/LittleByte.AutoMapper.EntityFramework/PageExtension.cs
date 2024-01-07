using AutoMapper;
using LittleByte.Data;

namespace LittleByte.AutoMapper.EntityFramework;

public static class PageExtension
{
    public static Page<TTo> CastResults<TFrom, TTo>(this Page<TFrom> @this, IMapper mapper)
    {
        return new Page<TTo>(
            @this.PageSize,
            @this.PageIndex,
            @this.TotalPages,
            @this.TotalResults,
            @this.Results.Select(o => mapper.Map<TTo>(o)).ToList());
    }
}