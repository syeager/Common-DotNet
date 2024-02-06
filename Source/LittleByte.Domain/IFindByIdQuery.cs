using LittleByte.Common;

namespace LittleByte.Domain;

public interface IFindByIdQuery<TDomain>
{
    public ValueTask<TDomain?> FindAsync(Id<TDomain> id);
    public ValueTask<TDomain?> FindForEditAsync(Id<TDomain> id);
    public ValueTask<TDomain> FindRequiredAsync(Id<TDomain> id);
    public ValueTask<TDomain> FindRequiredForEditAsync(Id<TDomain> id);
}