using System.Collections.Immutable;
using System.Diagnostics;
using Serilog;
using Serilog.Context;
using Serilog.Events;

namespace LittleByte.Common.Events
{
    public interface IEventPublisher
    {
        Task<EventPublishResult> PublishAsync<TData>(object initiator, TData eventData, DateTime now, CancellationToken cancellationToken);
    }

    public class EventPublisher : IEventPublisher
    {
        private readonly IEventListeners eventListeners;
        private readonly ILogger logger = Log.ForContext<EventPublisher>();

        public EventPublisher(IEventListeners eventListeners)
        {
            this.eventListeners = eventListeners;
        }

        public readonly struct EventProperty
        {
            public Guid Id { get; }
            public string Type { get; }
            public string Initiator { get; }

            public EventProperty(Guid id, Type type, object initiator)
            {
                Id = id;
                Type = type.Name;
                Initiator = initiator.GetType().Name;
            }
        }

        public async Task<EventPublishResult> PublishAsync<TData>(object initiator, TData eventData, DateTime now, CancellationToken cancellationToken)
        {
            var @event = Event<TData>.Create(eventData, now);
            using var _ = LogContext.PushProperty("Event", new EventProperty(@event.Id, typeof(TData), initiator), true);
            logger.Information("Event publish started");

            var listeners = eventListeners.GetListeners<TData>();
            if(listeners.Count == 0)
            {
                logger.Warning("No subscribers for event publish");
                return new EventPublishResult(initiator, @event.FiredTime, 0, 0);
            }

            var stopwatch = Stopwatch.StartNew();
            var (successful, total) = await WaitForListeners(listeners!, @event, cancellationToken);
            stopwatch.Stop();

            logger
                .Information("Event publish completed with {EventSubscribersSuccessful}/{EventSubscribersTotal} subscribers successful after {EventDurationMs}ms"
                    , successful, total, stopwatch.ElapsedMilliseconds);
            return new EventPublishResult(initiator, @event.FiredTime, successful, total);
        }

        private async Task<(int successful, int total)> WaitForListeners<TData>(IReadOnlyCollection<EventListener<TData>> listeners, IEvent<TData> @event, CancellationToken cancellationToken)
        {
            var tasks = listeners!
                .Select(l => Task.Run(() => NotifyListenerAsync(l, @event, cancellationToken), cancellationToken))
                .ToImmutableArray();
            await Task.WhenAll(tasks);

            var successfulListeners = tasks.Count(t => t.IsCompletedSuccessfully);
            return (successfulListeners, tasks.Length);
        }

        private async Task NotifyListenerAsync<TData>(EventListener<TData> listener, IEvent<TData> @event, CancellationToken cancellationToken)
        {
            Exception? exception = null;
            var stopwatch = Stopwatch.StartNew();
            Task? task = null;

            using var _ = LogContext.PushProperty("EventSubscriberType", listener.GetType().Name);
            try
            {
                logger.Information("Event subscriber notify start");
                task = listener.OnEventPublishedAsync(@event, cancellationToken);
                await task;
            }
            catch(Exception e)
            {
                exception = e;
                task ??= Task.FromException(exception);
            }
            finally
            {
                stopwatch.Stop();
                var result = new ListenerNotifiedResult(listener, exception, stopwatch.ElapsedMilliseconds, task!.Status);
                var logLevel = result.Exception == null ? LogEventLevel.Information : LogEventLevel.Warning;
                logger
                    .Write(logLevel,
                        result.Exception,
                        "Event subscriber notify completed with {EventSubscriberResultStatus} in {EventSubscriberResultDurationMs}ms",
                        result.Status,
                        result.DurationMs);
            }
        }
    }
}