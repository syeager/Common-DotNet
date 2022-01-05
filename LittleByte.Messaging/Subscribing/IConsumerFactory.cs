using System.Collections.Generic;

namespace LittleByte.Messaging.RabbitMq;

public interface IConsumerFactory
{
    IReadOnlyCollection<IConsumer> CreateConsumers();
}
