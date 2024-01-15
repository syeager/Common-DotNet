using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

namespace LittleByte.Common;

public static class IServiceCollectionExtension
{
    public static IServiceCollection BindOptions<T>(
        this IServiceCollection @this,
        IConfiguration configuration,
        string? key = null
    )
        where T : class
    {
        key ??= typeof(T).Name;
        var section = configuration.GetSection(key);
        @this.Configure<T>(section);

        return @this;
    }

    public static T BindAndGetOptions<T>(
        this IServiceCollection @this,
        IConfiguration configuration,
        string? key = null
    )
        where T : class
    {
        @this.BindOptions<T>(configuration, key);
        var serviceProvider = @this.BuildServiceProvider();
        return serviceProvider.GetService<IOptions<T>>()!.Value;
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