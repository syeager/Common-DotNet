using Microsoft.Extensions.Configuration;

namespace LittleByte.Configuration
{
    public static class IConfigurationExtension
    {
        public static T GetSection<T>(this IConfiguration configuration, string? key = null)
        {
            key ??= typeof(T).Name;
            return configuration.GetSection(key).Get<T>();
        }

        public static T? GetSectionOrNull<T>(this IConfiguration configuration, string? key = null)
        {
            key ??= typeof(T).Name;
            var section = configuration.GetSection(key).Get<T>() ?? default;
            return section;
        }
    }
}
