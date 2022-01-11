using System.Collections.Generic;

namespace LittleByte.Messaging;

public interface IConsumerFactory
{
    IReadOnlyCollection<IConsumer> CreateConsumers();
}
