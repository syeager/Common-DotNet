namespace LittleByte.Common;

public static class IListExtension
{
    public static IList<T> Init<T>(this IList<T> list, int count, Func<int, T> onInit)
    {
        for(var i = 0; i < count; i++)
        {
            var newEntry = onInit(i);
            list.Add(newEntry);
        }

        return list;
    }

    public static void ForEach<T>(this IList<T> @this, Action<int, T> action)
    {
        for(var i = 0; i < @this.Count; ++i)
        {
            action(i, @this[i]);
        }
    }

    public static async ValueTask ForEachAsync<T>(this IList<T> @this, Func<int, T, ValueTask> action)
    {
        for(var i = 0; i < @this.Count; ++i)
        {
            await action(i, @this[i]);
        }
    }
}