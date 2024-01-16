using LittleByte.Common;

namespace LittleByte.Data;

// TODO: Move to LittleByte.Domain.
public interface IFindByIdQuery<TDomain>
{
    public ValueTask<TDomain?> FindAsync(Guid id);
    public ValueTask<TDomain?> FindForEditAsync(Guid id);
    public ValueTask<TDomain> FindRequiredAsync(Guid id);
    public ValueTask<TDomain> FindRequiredForEditAsync(Guid id);
}

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