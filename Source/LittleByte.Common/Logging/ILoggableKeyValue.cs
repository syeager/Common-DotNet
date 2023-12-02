namespace LittleByte.Common.Logging;

public interface ILoggableKeyValue
{
    string LogKey { get; }
    string LogValue { get; }
}