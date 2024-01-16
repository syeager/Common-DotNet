using LittleByte.Common;
using LittleByte.Common.Logging;

namespace LittleByte.Domain;

public abstract class DomainModel<T>(Id<T> id) : ILoggableKeyValue
{
    public Id<T> Id { get; } = id;
    public string LogKey => $"{GetType().Name}.{nameof(Id)}";
    public string LogValue => Id.ToString();

    public static implicit operator Id<T>(DomainModel<T> model) => model.Id;
    public static implicit operator Guid(DomainModel<T> model) => model.Id;
}