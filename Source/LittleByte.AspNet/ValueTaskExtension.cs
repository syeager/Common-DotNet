using System.Runtime.CompilerServices;

namespace LittleByte.AspNet;

public static class ValueTaskExtension
{
    public static ConfiguredValueTaskAwaitable<T> NoAwait<T>(this ValueTask<T> @this)
        => @this.ConfigureAwait(false);
}