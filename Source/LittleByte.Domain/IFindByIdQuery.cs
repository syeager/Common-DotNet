namespace LittleByte.Domain;

public interface IFindByIdQuery<TDomain>
{
    public ValueTask<TDomain?> FindAsync(Guid id);
    public ValueTask<TDomain?> FindForEditAsync(Guid id);
    public ValueTask<TDomain> FindRequiredAsync(Guid id);
    public ValueTask<TDomain> FindRequiredForEditAsync(Guid id);
}