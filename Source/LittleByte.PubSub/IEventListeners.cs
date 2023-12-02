namespace LittleByte.PubSub;

public interface IEventListeners
{
    public IReadOnlyCollection<EventSubscriber<TData>> GetListeners<TData>();
}