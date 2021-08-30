using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace LittleByte.Configuration
{
    public static class ServiceCollectionExtension
    {
        public static T BindOptions<T>(this IServiceCollection services, IConfiguration configuration, string? key = null) where T : class
        {
            key ??= typeof(T).Name;
            var section = configuration.GetSection(key);
            services.Configure<T>(section);
            var serviceProvider = services.BuildServiceProvider();
            return serviceProvider.GetService<IOptions<T>>()!.Value;
        }
    }
}