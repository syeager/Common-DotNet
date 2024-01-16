namespace LittleByte.MessageQueue.Publishing;

public abstract class Message
{
    protected Message(object body)
    {
        Body = body;
    }

    public abstract string QueueName { get; }

    public object Body { get; }
}
