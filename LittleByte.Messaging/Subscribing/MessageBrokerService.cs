using LittleByte.Core.Tasks;
using Microsoft.Extensions.Hosting;
using System.Collections.Generic;
using System.Threading;

namespace LittleByte.Messaging.RabbitMq;

public abstract class MessageBrokerService : BackgroundService
{
    private readonly IConsumerFactory consumerFactory;
    private readonly IMessageDeserializer messageDeserializer;
    private readonly Dictionary<string, IConsumer> registeredConsumers = new();

    protected MessageBrokerService(IConsumerFactory consumerFactory, IMessageDeserializer messageDeserializer)
    {
        this.consumerFactory = consumerFactory;
        this.messageDeserializer = messageDeserializer;
    }

    protected abstract Task InitializeAsync();
    protected abstract void AddConsumer(IConsumer consumer);

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await InitializeAsync();
        
        var consumers = consumerFactory.CreateConsumers();
        RegisterConsumers(consumers);

        await stoppingToken;
    }

    private void RegisterConsumers(IReadOnlyCollection<IConsumer> consumers)
    {
        foreach (var consumer in consumers)
        {
            AddConsumer(consumer);
            registeredConsumers.Add(consumer.QueueName, consumer);
        }
    }

    protected void NotifyConsumer(string queueName, ReadOnlyMemory<byte> messageData)
    {
        var consumer = registeredConsumers[queueName];

        var message = messageDeserializer.Deserialize(consumer.MessageType, messageData);
        if(message == null)
        {
            // todo
            return;
        }

        consumer.ProcessMessageAsync(message);
    }
}