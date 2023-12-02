using System.Collections.Immutable;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace LittleByte.Common.Messaging.Subscribing;

public sealed class ReflectionConsumerFactory : IConsumerFactory
{
    private readonly IServiceProvider services;

    public ReflectionConsumerFactory(IServiceProvider services)
    {
        this.services = services;
    }

    public IReadOnlyCollection<IConsumer> CreateConsumers()
    {
        var consumers = Assembly.GetEntryAssembly()!
            .GetTypes()
            .Where(t => !t.IsAbstract && t.IsAssignableTo(typeof(IConsumer)))
            .Select(t => (IConsumer)services.GetRequiredService(t))
            .ToImmutableArray();
        return consumers;
    }
}
