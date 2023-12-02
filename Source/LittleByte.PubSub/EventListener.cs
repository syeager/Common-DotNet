namespace LittleByte.PubSub;

public interface IEventSubscriber
{
    Task OnEventPublishedAsync(IEvent @event, CancellationToken cancellationToken);
}

public abstract class EventSubscriber<T> : IEventSubscriber
{
    public abstract Task OnEventPublishedAsync(IEvent<T> @event, CancellationToken cancellationToken);

    Task IEventSubscriber.OnEventPublishedAsync(IEvent @event, CancellationToken cancellationToken)
        => OnEventPublishedAsync((IEvent<T>)@event, cancellationToken);
}