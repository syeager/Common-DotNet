namespace LittleByte.Common.Logging;

public interface ILoggable
{
    string LogKey { get; }
    string LogValue { get; }
}