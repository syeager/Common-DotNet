using System.Collections.Generic;

namespace LittleByte.Events
{
    public interface IEventListeners
    {
        public IReadOnlyCollection<EventListener<TData>> GetListeners<TData>();
    }
}