namespace LittleByte.MessageQueue.Subscribing;

public interface IConsumer
{
    Type MessageType { get; }
    string QueueName { get; }

    Task ProcessMessageAsync(object message);
}

public abstract class Consumer<T> : IConsumer
{
    public abstract string QueueName { get; }
    public Type MessageType { get; } = typeof(T);

    public Task ProcessMessageAsync(object message)
    {
        var castMessage = (T)message;
        return ProcessMessageInternalAsync(castMessage);
    }

    protected abstract Task ProcessMessageInternalAsync(T message);
}
