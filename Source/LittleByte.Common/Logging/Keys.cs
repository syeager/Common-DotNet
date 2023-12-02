namespace LittleByte.Common.Logging;

public static class Keys
{
    public static string New(params string[] keys) => string.Join('.', keys);
}