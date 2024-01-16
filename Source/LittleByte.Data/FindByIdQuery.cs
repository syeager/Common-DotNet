using LittleByte.Common;
using LittleByte.Domain;

namespace LittleByte.Data;

public class FindByIdQuery<TDomain, TEntity, TContext>(TContext dbContext) : IFindByIdQuery<TDomain>
    where TEntity : class, IIdObject
    where TContext : IDomainContext
{
    private readonly TContext dbContext = dbContext;

    public ValueTask<TDomain?> FindAsync(Guid id)
    {
        return dbContext.FindAsync<TDomain, TEntity>(id);
    }

    public ValueTask<TDomain?> FindForEditAsync(Guid id)
    {
        return dbContext.FindForEditAsync<TDomain, TEntity>(id);
    }

    public ValueTask<TDomain> FindRequiredAsync(Guid id)
    {
        return dbContext.FindRequiredAsync<TDomain, TEntity>(id);
    }

    public ValueTask<TDomain> FindRequiredForEditAsync(Guid id)
    {
        return dbContext.FindRequiredForEditAsync<TDomain, TEntity>(id);
    }
}