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
        return dbContext.FindAsync(id);
    }

    public ValueTask<TDomain?> FindForEditAsync(Id<TDomain> id)
    {
        return dbContext.FindForEditAsync(id);
    }

    public ValueTask<TDomain> FindRequiredAsync(Id<TDomain> id)
    {
        return dbContext.FindRequiredAsync(id);
    }

    public ValueTask<TDomain> FindRequiredForEditAsync(Id<TDomain> id)
    {
        return dbContext.FindRequiredForEditAsync(id);
    }
}