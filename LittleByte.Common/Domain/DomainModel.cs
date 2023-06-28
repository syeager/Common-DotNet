using LittleByte.Common.Logging;

namespace LittleByte.Common.Domain;

public abstract class DomainModel<T> : ILoggable
{
    public Id<T> Id { get; }
    public string LogKey => $"{GetType().Name}.{nameof(Id)}";
    public string LogValue => Id.ToString();

    protected DomainModel(Id<T> id)
    {
        Id = id;
    }

    public static implicit operator Id<T>(DomainModel<T> model) => model.Id;
    public static implicit operator Guid(DomainModel<T> model) => model.Id;
}