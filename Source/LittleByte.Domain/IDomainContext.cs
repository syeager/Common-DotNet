using LittleByte.Common;

namespace LittleByte.Domain;

public interface IDomainContext
{
    ValueTask<TDomain?> FindAsync<TDomain, TEntity>(Guid id)
        where TEntity : class, IIdObject;

    ValueTask<TDomain?> FindForEditAsync<TDomain, TEntity>(Guid id)
        where TEntity : class, IIdObject;

    ValueTask<TDomain> FindRequiredAsync<TDomain, TEntity>(Guid id)
        where TEntity : class, IIdObject;

    ValueTask<TDomain> FindRequiredForEditAsync<TDomain, TEntity>(Guid id)
        where TEntity : class, IIdObject;
}