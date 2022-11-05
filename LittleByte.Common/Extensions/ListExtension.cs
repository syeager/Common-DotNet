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
    }
}
