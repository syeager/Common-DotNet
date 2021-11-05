using System;
using JetBrains.Annotations;
using LittleByte.Core.Dates;

namespace LittleByte.Core.Dates
{
    public interface IDateService
    {
        DateTime UtcNow { get; }
    }

    public class DateService : IDateService
    {
        public DateTime UtcNow => DateTime.UtcNow;
    }
}

namespace LittleByte.Core.Common
{
    public static partial class S
    {
        public static IDateService Date { get; [UsedImplicitly] set; } = new DateService();
    }
}