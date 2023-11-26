using LittleByte.Common.Logging;

namespace LittleByte.PubSub;

public readonly record struct EventPublishResult(object Initiator, DateTime StartTime, int SuccessfulSubscribers,
    int TotalSubscribers) : ILoggableProperties
{
    public IEnumerable<LogProperty> Properties()
    {
        yield return new("Publish.Success", SuccessfulSubscribers);
        yield return new("Publish.Total", TotalSubscribers);
        yield return new("Publish.Start", StartTime);
    }
}