namespace LittleByte.Common;

public readonly record struct DateRange(DateTime Start, DateTime End)
{
    public TimeSpan Length => End - Start;
}