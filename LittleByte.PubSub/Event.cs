using LittleByte.Common.Logging;

namespace LittleByte.PubSub;

public interface IEvent : ILoggableProperties
{
    public Guid Id { get; }
    public DateTime FiredTime { get; }
    public object Data { get; }
}

public interface IEvent<out T> : IEvent
{
    new T Data { get; }
}

public sealed record Event<T>(Guid Id, DateTime FiredTime, T Data) : IEvent<T>
{
    object IEvent.Data => Data!;

    public static Event<T> Create(T data, DateTime firedTime)
        => new(Guid.NewGuid(), firedTime, data);

    public IEnumerable<LogProperty> Properties()
    {
        yield return new("Event.Id", Id);
        yield return new("Event.Type", typeof(T).Name);
    }
}