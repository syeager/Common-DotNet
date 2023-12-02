using System.Collections.Immutable;
using System.Diagnostics;
using LittleByte.Common.Logging;

namespace LittleByte.PubSub;

public interface IEventPublisher
{
    ValueTask<EventPublishResult> PublishAsync<TData>(object initiator, TData eventData, DateTimeOffset now, CancellationToken cancellationToken);
}

public sealed class EventPublisher : IEventPublisher
{
    private readonly IEventListeners eventListeners;
    private readonly ILog log;

    public EventPublisher(IEventListeners eventListeners)
    {
        this.eventListeners = eventListeners;
        log = this.NewLogger();
    }

    public async ValueTask<EventPublishResult> PublishAsync<TData>(object initiator, TData eventData, DateTimeOffset now, CancellationToken cancellationToken)
    {
        var @event = Event<TData>.Create(eventData, now);
        log
            .Push(@event)
            .Push("Event.Publisher", initiator.GetType().Name)
            .Debug("Event publish started");

        var listeners = eventListeners.GetListeners<TData>();
        if (listeners.Count == 0)
        {
            log.Warn("No subscribers for event publish");
            return new EventPublishResult(initiator, @event.FiredTime, 0, 0);
        }

        var stopwatch = Stopwatch.StartNew();
        var (successful, total) = await WaitForListeners(listeners, @event, cancellationToken);
        stopwatch.Stop();

        var publishResult = new EventPublishResult(initiator, @event.FiredTime, successful, total);

        log
            .Push(publishResult)
            .Push("Publish.DurationMs", stopwatch.ElapsedMilliseconds)
            .Info("Event publish completed");

        return publishResult;
    }

    private async Task<(int successful, int total)> WaitForListeners<TData>(
        IReadOnlyCollection<EventSubscriber<TData>> listeners, IEvent<TData> @event,
        CancellationToken cancellationToken)
    {
        var tasks = listeners!
            .Select(l => Task.Run(() => NotifyListenerAsync(l, @event, cancellationToken), cancellationToken))
            .ToImmutableArray();
        await Task.WhenAll(tasks);

        var successfulListeners = tasks.Count(t => t.IsCompletedSuccessfully);
        return (successfulListeners, tasks.Length);
    }

    private async Task NotifyListenerAsync<TData>(EventSubscriber<TData> subscriber, IEvent<TData> @event,
        CancellationToken cancellationToken)
    {
        Exception? exception = null;
        var stopwatch = Stopwatch.StartNew();
        Task? task = null;

        log.Push("Event.SubscriberType", subscriber.GetType().Name);
        try
        {
            log.Debug("Event subscriber notify start");
            task = subscriber.OnEventPublishedAsync(@event, cancellationToken);
            await task;
        }
        catch (Exception e)
        {
            exception = e;
            task ??= Task.FromException(exception);
        }
        finally
        {
            stopwatch.Stop();
            var result = new SubscriberNotifiedResult(subscriber, exception, stopwatch.ElapsedMilliseconds, task?.Status ?? TaskStatus.Faulted);

            var logLevel = result.Exception == null ? LogLevel.Debug : LogLevel.Warn;
            log
                .Push(result)
                .Write(logLevel, "Event subscriber notify completed", result.Exception);
        }
    }
}