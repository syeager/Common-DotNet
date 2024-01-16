namespace LittleByte.Common.Logging;

public interface ILoggableProperties
{
    IEnumerable<LogProperty> Properties();
}