using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace LittleByte.Configuration
{
    public static class IServiceCollectionExtension
    {
        public static void BindOptions<T>(this IServiceCollection @this, IConfiguration configuration, string? key = null) where T : class
        {
            key ??= typeof(T).Name;
            var section = configuration.GetSection(key);
            @this.Configure<T>(section);
        }
        
        public static T BindAndGetOptions<T>(this IServiceCollection @this, IConfiguration configuration, string? key = null) where T : class
        {
            @this.BindOptions<T>(configuration, key);
            var serviceProvider = @this.BuildServiceProvider();
            return serviceProvider.GetService<IOptions<T>>()!.Value;
        }
    }
}