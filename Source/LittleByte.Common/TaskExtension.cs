using System.Runtime.CompilerServices;

namespace LittleByte.Common;

public static class TaskExtension
{
    public static ConfiguredTaskAwaitable<T> NoAwait<T>(this Task<T> @this)
        => @this.ConfigureAwait(false);
}