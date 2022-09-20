namespace LittleByte.Common.Extensions
{
    public static class IEnumerableExtension
    {
        public static IEnumerable<TSource> WhereNotNull<TSource>(this IEnumerable<TSource?> enumerable)
        {
            return enumerable.Where(x => x != null)!;
        }

        public static IEnumerable<TResult> SelectNotNull<TSource, TResult>(
            this IEnumerable<TSource?> enumerable,
            Func<TSource, TResult> selector)
        {
            return enumerable.WhereNotNull().Select(selector);
        }
    }
}
