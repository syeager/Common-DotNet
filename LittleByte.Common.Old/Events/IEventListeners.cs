namespace LittleByte.Common.Events
{
    public interface IEventListeners
    {
        public IReadOnlyCollection<EventListener<TData>> GetListeners<TData>();
    }
}