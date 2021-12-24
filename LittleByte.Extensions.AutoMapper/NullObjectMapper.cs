using System.Linq.Expressions;
using AutoMapper;

namespace LittleByte.Extensions.AutoMapper;

public class NullObjectMapper : IMapper
{
    public TDestination Map<TDestination>(object source)
    {
        return default!;
    }

    public TDestination Map<TSource, TDestination>(TSource source)
    {
        return default!;
    }

    public TDestination Map<TSource, TDestination>(TSource source, TDestination destination)
    {
        return default!;
    }

    public object Map(object source, Type sourceType, Type destinationType)
    {
        return default!;
    }

    public object Map(object source, object destination, Type sourceType, Type destinationType)
    {
        return default!;
    }

    public TDestination Map<TDestination>(object source, Action<IMappingOperationOptions<object, TDestination>> opts)
    {
        return default!;
    }

    public TDestination Map<TSource, TDestination>(TSource source,
        Action<IMappingOperationOptions<TSource, TDestination>> opts)
    {
        return default!;
    }

    public TDestination Map<TSource, TDestination>(TSource source, TDestination destination,
        Action<IMappingOperationOptions<TSource, TDestination>> opts)
    {
        return default!;
    }

    public object Map(object source, Type sourceType, Type destinationType,
        Action<IMappingOperationOptions<object, object>> opts)
    {
        return default!;
    }

    public object Map(object source, object destination, Type sourceType, Type destinationType,
        Action<IMappingOperationOptions<object, object>> opts)
    {
        return default!;
    }

    public IQueryable<TDestination> ProjectTo<TDestination>(IQueryable source, object? parameters = null,
        params Expression<Func<TDestination, object>>[] membersToExpand)
    {
        throw new NotImplementedException();
    }

    public IQueryable<TDestination> ProjectTo<TDestination>(IQueryable source, IDictionary<string, object> parameters,
        params string[] membersToExpand)
    {
        throw new NotImplementedException();
    }

    public IQueryable ProjectTo(IQueryable source, Type destinationType, IDictionary<string, object>? parameters = null,
        params string[] membersToExpand)
    {
        throw new NotImplementedException();
    }

    public IConfigurationProvider ConfigurationProvider => null!;
    public Func<Type, object> ServiceCtor => null!;
}