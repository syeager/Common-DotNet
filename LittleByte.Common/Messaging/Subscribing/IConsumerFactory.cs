namespace LittleByte.Common.Messaging.Subscribing;

public interface IConsumerFactory
{
    IReadOnlyCollection<IConsumer> CreateConsumers();
}
