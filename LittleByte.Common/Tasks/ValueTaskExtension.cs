using System.Runtime.CompilerServices;

namespace LittleByte.Common.Tasks;

public static class ValueTaskExtension
{
    public static ConfiguredValueTaskAwaitable<T> NoAwait<T>(this ValueTask<T> @this) => @this.ConfigureAwait(false);
}