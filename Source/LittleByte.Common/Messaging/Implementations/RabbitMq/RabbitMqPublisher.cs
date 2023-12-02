using LittleByte.Common.Messaging.Publishing;
using LittleByte.Common.Messaging.Serialization;
using Microsoft.Extensions.Options;

namespace LittleByte.Common.Messaging.Implementations.RabbitMq;

public sealed class RabbitMqPublisher : MessagePublisher
{
    private readonly RabbitMqOptions options;
    private IModel channel = null!;
    private IConnection connection = null!;

    public RabbitMqPublisher(IMessageSerializer messageSerializer, IOptions<RabbitMqOptions> options)
        : base(messageSerializer)
    {
        this.options = options.Value;
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

    // TODO: Create retry functionality in core.
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
            catch(Exception exception)
            {
                Console.WriteLine($"Failed to connect to RabbitMQ: {exception.Message}");
            }

            if(!connected)
            {
                await Task.Delay(TimeSpan.FromSeconds(10));
            }
        } while(!connected);

        Console.WriteLine("Connected to RabbitMQ");
    }

    protected override void AddQueueInternal(string queueName)
    {
        channel.QueueDeclare(queueName, true, false, false);
    }

    protected override void PublishInternal(string queueName, ReadOnlyMemory<byte> messageData)
    {
        channel.BasicPublish("", queueName, body: messageData);
    }

    public override void Dispose()
    {
        base.Dispose();

        connection.Dispose();
        channel.Dispose();
    }
}
