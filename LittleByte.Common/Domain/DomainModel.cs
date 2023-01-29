namespace LittleByte.Common.Domain;

public abstract class DomainModel<T>
{
    public Id<T> Id { get; }

    protected DomainModel(Id<T> id)
    {
        Id = id;
    }

    public static implicit operator Id<T>(DomainModel<T> model) => model.Id;
    public static implicit operator Guid(DomainModel<T> model) => model.Id;
}