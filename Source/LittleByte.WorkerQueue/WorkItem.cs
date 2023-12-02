using LittleByte.Common.Logging;

namespace LittleByte.WorkerQueue;

public abstract class WorkItem : ILoggableProperties
{
    public Guid Id { get; }
    public Type RequesterType { get; }

    protected WorkItem(object requester)
        : this(Guid.NewGuid(), requester.GetType())
    {
    }

    protected WorkItem(Guid id, Type requesterType)
    {
        Id = id;
        RequesterType = requesterType;
    }

    public abstract Task<WorkResult> DoWorkAsync(CancellationToken token);
    public abstract void Cancel();
    public abstract WorkItem Clone();

    public IEnumerable<LogProperty> Properties()
    {
        yield return new("WorkItem.Id", Id);
        yield return new("WorkItem.Type",  RequesterType.Name);
    }
}