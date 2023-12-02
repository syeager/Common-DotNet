namespace LittleByte.Common;

public static class IEnumerableExtension
{
    public static IEnumerable<TSource> WhereNotNull<TSource>(this IEnumerable<TSource?> @this)
        => @this.Where(x => x is not null)!;

    public static IEnumerable<TResult> SelectNotNull<TSource, TResult>(this IEnumerable<TSource?> @this, Func<TSource, TResult> selector)
        => @this.WhereNotNull().Select(selector);

    public static void ForEach<TSource>(this IEnumerable<TSource?> @this, Action<TSource?, int> action)
    {
        var count = 0;
        foreach (var entry in @this)
        {
            action(entry, count);
            ++count;
        }
    }

    public static void ForEach<TSource>(this IEnumerable<TSource?> @this, Action<TSource?> action)
    {
        foreach (var entry in @this)
        {
            action(entry);
        }
    }
}