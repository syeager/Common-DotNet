using LittleByte.Configuration;
using LittleByte.Core.Extensions;
using LittleByte.Messaging.Implementations.RabbitMq;
using LittleByte.Messaging.Publishing;
using LittleByte.Messaging.Serialization;
using LittleByte.Messaging.Serialization.JsonText;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LittleByte.Messaging.Configuration;

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
