namespace LittleByte.MessageQueue.Subscribing;

public interface IConsumerFactory
{
    IReadOnlyCollection<IConsumer> CreateConsumers();
}
