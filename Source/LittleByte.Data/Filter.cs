namespace LittleByte.Data;

public static class IQueryableExtension
{
    public static async ValueTask<IQueryable<TModel>> FilterAsync<TModel, TRequest>(
        this IQueryable<TModel> @this,
        Filter<TModel, TRequest> filter,
        TRequest request
    )
    {
        @this = await filter.FilterAsync(@this, request);
        return @this;
    }

    public static async ValueTask<IQueryable<TModel>> FilterAsync<TModel, TRequest>(
        this ValueTask<IQueryable<TModel>> @this,
        Filter<TModel, TRequest> filter,
        TRequest request
    )
    {
        var query = await @this;
        query = await filter.FilterAsync(query, request);
        return query;
    }

    public static bool IsEmpty<T>(this IQueryable<T> queryable)
    {
        return !queryable.Any();
    }
}

public abstract class Filter<TModel, TRequest>
{
    protected abstract bool WillFilter(TRequest parameters);
    
    
    protected virtual ValueTask<IQueryable<TModel>> FilterAsyncInternal(IQueryable<TModel> query, TRequest parameters)
    {
        return ValueTask.FromResult(FilterInternal(query, parameters));
    }

    protected virtual IQueryable<TModel> FilterInternal(IQueryable<TModel> query, TRequest parameters)
    {
        throw new NotSupportedException();
    }

    public async ValueTask<IQueryable<TModel>> FilterAsync(IQueryable<TModel> query, TRequest request)
    {
        var willFilter = WillFilter(request);

        var task = willFilter ? FilterAsyncInternal(query, request) : ValueTask.FromResult(query);
        return await task;
    }
}