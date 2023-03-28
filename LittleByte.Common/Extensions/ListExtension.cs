namespace LittleByte.Common.Extensions
{
    public static class ListExtension
    {
        public static List<T> Init<T>(this List<T> list, int count, Func<int, T> onInit)
        {
            for(var i = 0; i < count; i++)
            {
                var newEntry = onInit(i);
                list.Add(newEntry);
            }

            return list;
        }

        public static void ForEach<T>(this IReadOnlyList<T> @this, Action<int, T> action)
        {
            for(var i = 0; i < @this.Count; ++i)
            {
                action(i, @this[i]);
            }
        }

        public static async ValueTask ForEachAsync<T>(this IReadOnlyList<T> @this, Func<int, T, ValueTask> action)
        {
            for(var i = 0; i < @this.Count; ++i)
            {
                await action(i, @this[i]);
            }
        }
    }
}
