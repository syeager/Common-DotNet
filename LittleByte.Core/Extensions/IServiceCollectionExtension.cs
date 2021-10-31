using Microsoft.Extensions.DependencyInjection;

namespace Articlib.Articles.Infra;

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
}
