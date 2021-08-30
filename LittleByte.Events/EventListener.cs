using System.Threading;
using System.Threading.Tasks;

namespace LittleByte.Events
{
    public interface IEventListener
    {
        Task OnEventPublishedAsync(IEvent @event, CancellationToken cancellationToken);
    }

    public abstract class EventListener<T> : IEventListener
    {
        public abstract Task OnEventPublishedAsync(IEvent<T> @event, CancellationToken cancellationToken);

        Task IEventListener.OnEventPublishedAsync(IEvent @event, CancellationToken cancellationToken)
        {
            return OnEventPublishedAsync((IEvent<T>)@event, cancellationToken);
        }
    }
}