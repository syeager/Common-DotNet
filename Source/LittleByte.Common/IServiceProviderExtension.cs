using Microsoft.Extensions.DependencyInjection;

namespace LittleByte.Common;

public static class IServiceProviderExtension
{
    public static T Get<T>(this IServiceProvider @this)
        where T : notnull => @this.GetRequiredService<T>();

    public static T Create<T>(this IServiceProvider @this)
        where T : notnull
    {
        var constructor = typeof(T).GetConstructors().First();
        var args = new object[constructor.GetParameters().Length];
        for(var i = 0; i < args.Length; i++)
        {
            var param = constructor.GetParameters()[i];
            var instance = @this.GetRequiredService(param.ParameterType);
            args[i] = instance;
        }

        var obj = constructor.Invoke(args);
        return (T)obj;
    }
}