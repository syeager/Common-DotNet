using LittleByte.Common.Old._TO_REVIEW;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace LittleByte.Common.Old._TO_REVIEW;

public static class IServiceCollectionExtension
{
    public static IServiceCollection AddScoped<TService1, TService2, TImplementation>(this IServiceCollection @this)
        where TService1 : class
        where TService2 : class
        where TImplementation : class, TService1, TService2
    {
        return @this
            .AddScoped<TImplementation>()
            .AddScoped<TService1, TImplementation>(s => s.GetRequiredService<TImplementation>())
            .AddScoped<TService2, TImplementation>(s => s.GetRequiredService<TImplementation>());
    }

    public static IServiceCollection AddScoped<TService1, TService2, TService3, TImplementation>(
        this IServiceCollection @this)
        where TService1 : class
        where TService2 : class
        where TService3 : class
        where TImplementation : class, TService1, TService2, TService3
    {
        return @this
            .AddScoped<TImplementation>()
            .AddScoped<TService1, TImplementation>(s => s.GetRequiredService<TImplementation>())
            .AddScoped<TService2, TImplementation>(s => s.GetRequiredService<TImplementation>())
            .AddScoped<TService3, TImplementation>(s => s.GetRequiredService<TImplementation>());
    }

    public static IServiceCollection AddSingleton<TService1, TService2, TImplementation>(this IServiceCollection @this)
        where TService1 : class
        where TService2 : class
        where TImplementation : class, TService1, TService2
    {
        return @this
            .AddSingleton<TImplementation>()
            .AddSingleton<TService1, TImplementation>(s => s.GetRequiredService<TImplementation>())
            .AddSingleton<TService2, TImplementation>(s => s.GetRequiredService<TImplementation>());
    }

    public static IServiceCollection AddHostedService<TService, TImplementation>(this IServiceCollection @this)
        where TService : class, IHostedService
        where TImplementation : class, TService
    {
        return @this
            .AddSingleton<TService, TImplementation>()
            .AddHostedService(s => s.GetRequiredService<TService>());
    }
}
