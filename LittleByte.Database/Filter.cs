using System.Linq;
using System.Threading.Tasks;

namespace LittleByte.Database
{
    public static class IQueryableExtension
    {
        public static async Task<IQueryable<TModel>> FilterAsync<TModel, TRequest>(this IQueryable<TModel> query, Filter<TModel, TRequest> filter, TRequest request)
        {
            query = await filter.FilterAsync(query, request);
            return query;
        }

        public static async Task<IQueryable<TModel>> FilterAsync<TModel, TRequest>(this Task<IQueryable<TModel>> queryTask, Filter<TModel, TRequest> filter, TRequest request)
        {
            var query = await queryTask;
            query = await filter.FilterAsync(query, request);
            return query;
        }
    }

    public abstract class Filter<TModel, TRequest>
    {
        protected abstract bool WillFilter(TRequest request);
        protected abstract Task<IQueryable<TModel>> FilterAsyncInternal(IQueryable<TModel> query, TRequest request);

        public async Task<IQueryable<TModel>> FilterAsync(IQueryable<TModel> query, TRequest request)
        {
            var willFilter = WillFilter(request);

            Task<IQueryable<TModel>> task = willFilter ? FilterAsyncInternal(query, request) : Task.FromResult(query);
            return await task;
        }
    }
}