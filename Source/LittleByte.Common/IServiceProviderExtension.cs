using Microsoft.Extensions.DependencyInjection;

namespace LittleByte.Common;

public static class IServiceProviderExtension
{
    public static T Get<T>(this IServiceProvider @this)
        where T : notnull => @this.GetRequiredService<T>();
}