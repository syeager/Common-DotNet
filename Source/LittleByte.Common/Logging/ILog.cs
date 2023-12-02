using System.Runtime.CompilerServices;

namespace LittleByte.Common.Logging;

public interface ILog : IDisposable
{
    static abstract ILog Create(Type forType);

    ILog Push(ILoggableKeyValue loggable);
    ILog Push(ILoggableKeyValue loggable, string keyPrefix);
    ILog Push(ILoggableProperties loggable);
    ILog Push(ILoggableProperties loggable, string keyPrefix);
    ILog Push(string name, object? value);
    ILog ContextPush(ILoggableKeyValue loggable);
    ILog ContextPush(ILoggableKeyValue loggable, string keyPrefix);
    ILog ContextPush(ILoggableProperties loggable);
    ILog ContextPush(ILoggableProperties loggable, string keyPrefix);
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