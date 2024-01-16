using LittleByte.MessageQueue.Serialization;
using LittleByte.MessageQueue.Subscribing;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace LittleByte.MessageQueue.RabbitMq;

public sealed class RabbitMqBroker : MessageBrokerService
{
    private readonly RabbitMqOptions options;
    private IModel channel = null!;
    private IConnection connection = null!;

    public RabbitMqBroker(
        IOptions<RabbitMqOptions> options,
        IMessageDeserializer messageDeserializer,
        IConsumerFactory consumerFactory)
        : base(consumerFactory, messageDeserializer)
    {
        this.options = options.Value;
    }

    public override void Dispose()
    {
        base.Dispose();

        connection.Dispose();
        channel.Dispose();
    }

    protected override async Task InitializeAsync()
    {
        var connected = false;
        do
        {
            try
            {
                Connect();
                connected = true;
            }
            catch (Exception exception)
            {
                Console.WriteLine($"Failed to connect to RabbitMQ: {exception.Message}");
            }

            if (!connected)
            {
                await Task.Delay(TimeSpan.FromSeconds(10));
            }
        } while (!connected);

        Console.WriteLine("Connected to RabbitMQ");
    }

    private void Connect()
    {
        var factory = new ConnectionFactory
        {
            HostName = options.HostName
        };

        connection = factory.CreateConnection();
        channel = connection.CreateModel();
    }

    protected override void AddConsumer(IConsumer consumer)
    {
        channel.QueueDeclare(
            consumer.QueueName,
            true,
            false,
            false);

        var basicConsumer = new EventingBasicConsumer(channel);
        basicConsumer.Received += OnMessageReceived;
        channel.BasicConsume(consumer.QueueName, true, basicConsumer);
    }

    private void OnMessageReceived(object? model, BasicDeliverEventArgs args)
    {
        NotifyConsumer(args.RoutingKey, args.Body);
    }
}
