using LittleByte.Common;
using LittleByte.MessageQueue.Publishing;
using LittleByte.MessageQueue.Serialization;
using LittleByte.MessageQueue.Serialization.JsonText;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LittleByte.MessageQueue.RabbitMq;

public static class MessagingConfiguration
{
    public static IServiceCollection AddMessaging(this IServiceCollection @this, IConfiguration configuration)
    {
        @this.BindOptions<RabbitMqOptions>(configuration);

        return @this
            .AddSingleton<IMessageSerializer, JsonTextSerializer>()
            .AddHostedService<MessagePublisher, RabbitMqPublisher>();
    }
}
