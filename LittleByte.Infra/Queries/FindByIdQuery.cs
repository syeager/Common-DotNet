using LittleByte.Core.Objects;
using LittleByte.Infra.Contexts;
using LittleByte.Validation;

namespace LittleByte.Infra.Queries;

public interface IFindByIdQuery<TDomain>
{
    public ValueTask<Valid<TDomain>?> FindAsync(Guid id);
    public ValueTask<Valid<TDomain>?> FindForEditAsync(Guid id);
    public ValueTask<Valid<TDomain>> FindRequiredAsync(Guid id);
    public ValueTask<Valid<TDomain>> FindRequiredForEditAsync(Guid id);
}

public class FindByIdQuery<TDomain, TEntity, TContext> : IFindByIdQuery<TDomain>
    where TEntity : class, IIdObject
    where TContext : IDomainContext
    where TDomain : IIdObject
{
    private readonly TContext dbContext;

    public FindByIdQuery(TContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public ValueTask<Valid<TDomain>?> FindAsync(Guid id) => dbContext.FindAsync<TDomain, TEntity>(id);

    public ValueTask<Valid<TDomain>?> FindForEditAsync(Guid id) => dbContext.FindForEditAsync<TDomain, TEntity>(id);

    public ValueTask<Valid<TDomain>> FindRequiredAsync(Guid id) => dbContext.FindRequiredAsync<TDomain, TEntity>(id);

    public ValueTask<Valid<TDomain>> FindRequiredForEditAsync(Guid id) =>
        dbContext.FindRequiredForEditAsync<TDomain, TEntity>(id);
}
