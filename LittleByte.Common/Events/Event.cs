namespace LittleByte.Common.Events
{
    public interface IEvent
    {
        public Guid Id { get; }
        public DateTime FiredTime { get; }
        public object Data { get; }
    }

    public interface IEvent<out T> : IEvent
    {
        new T Data { get; }
    }

    public sealed class Event<T>: IEvent<T>
    {
        public Guid Id { get; }
        public DateTime FiredTime { get; }
        object IEvent.Data => Data!;
        public T Data { get; }

        private Event(Guid id, DateTime firedTime, T data)
        {
            Id = id;
            FiredTime = firedTime;
            Data = data;
        }

        public static Event<T> Create(T data, DateTime firedTime)
        {
            return new(Guid.NewGuid(), firedTime, data);
        }
    }
}