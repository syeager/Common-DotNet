using System.Diagnostics;
using LittleByte.Common.Events;

namespace LittleByte.Common.WorkerQueue
{
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

            public EventWorkItem(object requester, TData eventData, IEventPublisher eventPublisher, object initiator)
                : base(requester)
            {
                this.requester = requester;
                this.eventData = eventData;
                this.eventPublisher = eventPublisher;
                this.initiator = initiator;
            }

            public override async Task<WorkResult> DoWorkAsync(CancellationToken token)
            {
                var stopwatch = Stopwatch.StartNew();
                var result = await eventPublisher.PublishAsync(initiator, eventData, S.Date.UtcNow, token);
                stopwatch.Stop();
                return WorkResult.Successful(stopwatch.ElapsedMilliseconds);
            }

            public override void Cancel()
            {
                // TODO
                //throw new NotImplementedException();
            }

            public override WorkItem Clone()
            {
                return new EventWorkItem<TData>(requester, eventData, eventPublisher, initiator);
            }
        }

        private readonly IEventPublisher eventPublisher;
        private readonly IWorkItemPusher workItemPusher;

        public EventQueue(IEventPublisher eventPublisher, IWorkItemPusher workItemPusher)
        {
            this.eventPublisher = eventPublisher;
            this.workItemPusher = workItemPusher;
        }

        public void Push<TData>(object initiator, TData eventData)
        {
            var eventWorkItem = new EventWorkItem<TData>(this, eventData, eventPublisher, initiator);
            var task = workItemPusher.PushAsync(eventWorkItem, CancellationToken.None).AsTask();
            task.GetAwaiter().GetResult();
        }
    }
}