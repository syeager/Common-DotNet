using LittleByte.Common.Messaging.Implementations.RabbitMq;
using LittleByte.Common.Messaging.Publishing;
using LittleByte.Common.Messaging.Serialization;
using LittleByte.Common.Messaging.Serialization.JsonText;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LittleByte.Common.Messaging.Configuration;

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
