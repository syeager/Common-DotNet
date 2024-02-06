using LittleByte.Common;

namespace LittleByte.Domain;

public interface IDomainContext
{
    ValueTask<TDomain?> FindAsync<TDomain>(Id<TDomain> id)
        where TDomain : DomainModel<TDomain>;

    ValueTask<TDomain?> FindForEditAsync<TDomain>(Id<TDomain> id)
        where TDomain : DomainModel<TDomain>;

    ValueTask<TDomain> FindRequiredAsync<TDomain>(Id<TDomain> id)
        where TDomain : DomainModel<TDomain>;

    ValueTask<TDomain> FindRequiredForEditAsync<TDomain>(Id<TDomain> id)
        where TDomain : DomainModel<TDomain>;
}