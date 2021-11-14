using System.Collections.Generic;
using System.Threading;
using LittleByte.Core.Tasks;
using Microsoft.Extensions.Hosting;

namespace LittleByte.Messaging;

public abstract class MessagePublisher : BackgroundService
{
    private readonly IMessageSerializer messageSerializer;
    private readonly HashSet<string> registeredQueues = new();

    protected MessagePublisher(IMessageSerializer messageSerializer)
    {
        this.messageSerializer = messageSerializer;
    }

    protected abstract Task InitializeAsync();
    protected abstract void AddQueueInternal(string queueName);
    protected abstract void PublishInternal(string queueName, ReadOnlyMemory<byte> messageData);

    private void AddQueue(string queueName)
    {
        AddQueueInternal(queueName);
        registeredQueues.Add(queueName);
    }

    public void Publish(Message message)
    {
        if (!registeredQueues.Contains(message.QueueName)) AddQueue(message.QueueName);

        var messageData = messageSerializer.Serialize(message.Body);
        PublishInternal(message.QueueName, messageData);
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await InitializeAsync();

        await stoppingToken;
    }
}