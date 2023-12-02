using System.Diagnostics;
using LittleByte.PubSub;

namespace LittleByte.WorkerQueue;

public interface IEventQueue
{
    void Push<TData>(object initiator, TData eventData);
}

public class EventQueue : IEventQueue
{
    private class EventWorkItem<TData> : WorkItem
    {
        private readonly object requester; // TODO: Remove need for this.
        private readonly TData eventData;
        private readonly IEventPublisher eventPublisher;
        private readonly object initiator;
        private readonly TimeProvider timeProvider;

        public EventWorkItem(object requester, TData eventData, IEventPublisher eventPublisher, object initiator, TimeProvider timeProvider)
            : base(requester)
        {
            this.requester = requester;
            this.eventData = eventData;
            this.eventPublisher = eventPublisher;
            this.initiator = initiator;
            this.timeProvider = timeProvider;
        }

        public override async Task<WorkResult> DoWorkAsync(CancellationToken token)
        {
            var stopwatch = Stopwatch.StartNew();
            var result = await eventPublisher.PublishAsync(initiator, eventData, timeProvider.GetUtcNow(), token);
            stopwatch.Stop();
            return WorkResult.Successful(stopwatch.ElapsedMilliseconds);
        }

        public override void Cancel()
        {
            // TODO
            //throw new NotImplementedException();
        }

        public override WorkItem Clone() => new EventWorkItem<TData>(requester, eventData, eventPublisher, initiator, timeProvider);
    }

    private readonly IEventPublisher eventPublisher;
    private readonly IWorkItemPusher workItemPusher;
    private readonly TimeProvider timeProvider;

    public EventQueue(IEventPublisher eventPublisher, IWorkItemPusher workItemPusher, TimeProvider timeProvider)
    {
        this.eventPublisher = eventPublisher;
        this.workItemPusher = workItemPusher;
        this.timeProvider = timeProvider;
    }

    public void Push<TData>(object initiator, TData eventData)
    {
        var eventWorkItem = new EventWorkItem<TData>(this, eventData, eventPublisher, initiator, timeProvider);
        var task = workItemPusher.PushAsync(eventWorkItem, CancellationToken.None).AsTask();
        task.GetAwaiter().GetResult();
    }
}