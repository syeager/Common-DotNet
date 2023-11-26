using LittleByte.Common.Logging;

namespace LittleByte.PubSub;

public readonly record struct SubscriberNotifiedResult(Exception? Exception, long DurationMs, TaskStatus Status) : ILoggableProperties
{
    public Type ListenerType { get; }

    public SubscriberNotifiedResult(IEventSubscriber subscriber, Exception? exception, long durationMs, TaskStatus status)
        : this(exception, durationMs, status)
    {
        ListenerType = subscriber.GetType();
        Exception = exception;
        DurationMs = durationMs;
        Status = status;
    }

    public IEnumerable<LogProperty> Properties()
    {
        yield return new("Notify.Status", Status);
        yield return new("Notify.DurationMs", DurationMs);
    }
}