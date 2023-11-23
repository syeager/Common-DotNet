namespace LittleByte.Common.Database;

public static class IQueryableExtension
{
    public static async Task<IQueryable<TModel>> FilterAsync<TModel, TRequest>(
        this IQueryable<TModel> @this, Filter<TModel, TRequest> filter, TRequest request)
    {
        @this = await filter.FilterAsync(@this, request);
        return @this;
    }

    public static async Task<IQueryable<TModel>> FilterAsync<TModel, TRequest>(
        this Task<IQueryable<TModel>> @this, Filter<TModel, TRequest> filter, TRequest request)
    {
        var query = await @this;
        query = await filter.FilterAsync(query, request);
        return query;
    }

    public static bool IsEmpty<T>(this IQueryable<T> queryable) => !queryable.Any();
}

public abstract class Filter<TModel, TRequest>
{
    protected abstract bool WillFilter(TRequest request);
    protected abstract Task<IQueryable<TModel>> FilterAsyncInternal(IQueryable<TModel> query, TRequest request);

    public async Task<IQueryable<TModel>> FilterAsync(IQueryable<TModel> query, TRequest request)
    {
        var willFilter = WillFilter(request);

        var task = willFilter ? FilterAsyncInternal(query, request) : Task.FromResult(query);
        return await task;
    }
}