using LittleByte.Common;
using LittleByte.Domain;

namespace LittleByte.Data;

public class FindByIdQuery<TDomain, TContext>(TContext dbContext) : IFindByIdQuery<TDomain>
    where TDomain : DomainModel<TDomain>
    where TContext : IDomainContext
{
    private readonly TContext dbContext = dbContext;

    public ValueTask<TDomain?> FindAsync(Id<TDomain> id)
    {
        return dbContext.FindAsync<TDomain>(id);
    }

    public ValueTask<TDomain?> FindForEditAsync(Id<TDomain> id)
    {
        return dbContext.FindForEditAsync<TDomain>(id);
    }

    public ValueTask<TDomain> FindRequiredAsync(Id<TDomain> id)
    {
        return dbContext.FindRequiredAsync<TDomain>(id);
    }

    public ValueTask<TDomain> FindRequiredForEditAsync(Id<TDomain> id)
    {
        return dbContext.FindRequiredForEditAsync<TDomain>(id);
    }
}