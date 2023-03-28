namespace LittleByte.Common.Extensions;

public static class IntExtension
{
    public static List<T> Execute<T>(this int @this, Func<int, T> operation)
    {
        var results = new List<T>(@this);
        for(var i = 0; i < @this; i++)
        {
            var result = operation(i);
            results.Add(result);
        }

        return results;
    }
}