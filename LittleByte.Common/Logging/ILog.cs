using System.Runtime.CompilerServices;

namespace LittleByte.Common.Logging;

public interface ILog : IDisposable
{
    static abstract ILog Create(Type forType);

    ILog Push(ILoggableKeyValue loggable, string? keyPrefix = null);
    ILog Push(ILoggableProperties loggable, string? keyPrefix = null);
    ILog Push(string name, object? value);
    ILog ContextPush(ILoggableKeyValue loggable, string? keyPrefix = null);
    ILog ContextPush(ILoggableProperties loggable, string? keyPrefix = null);
    ILog ContextPush(string name, object? value);

    ILog Write(
        LogLevel level,
        string message,
        Exception? exception = null,
        [CallerMemberName] string memberName = "",
        [CallerLineNumber] int lineNumber = -1
    );

    ILog Debug(
        string message,
        [CallerMemberName] string memberName = "",
        [CallerLineNumber] int lineNumber = -1
    );

    ILog Info(
        string message,
        [CallerMemberName] string memberName = "",
        [CallerLineNumber] int lineNumber = -1
    );

    ILog Warn(
        string message,
        [CallerMemberName] string memberName = "",
        [CallerLineNumber] int lineNumber = -1
    );

    ILog Error(
        string message,
        Exception? exception = null,
        [CallerMemberName] string memberName = "",
        [CallerLineNumber] int lineNumber = -1
    );
}