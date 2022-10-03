using JetBrains.Annotations;

namespace LittleByte.Common;

public interface IDateService
{
    DateTime UtcNow { get; }
}

public class DateService : IDateService
{
    public DateTime UtcNow => DateTime.UtcNow;
}

public static partial class S
{
    public static IDateService Date
    {
        get;
        [UsedImplicitly]
        set;
    } = new DateService();
}