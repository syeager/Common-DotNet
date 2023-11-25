using System.Runtime.CompilerServices;

namespace LittleByte.Common.Logging;

public interface ILog : IDisposable
{
    static abstract ILog Create(Type forType);

    ILog Push<TProperty>(object? value);
    ILog Push<TProperty>(TProperty? value);
    ILog Push(ILoggable loggable);
    ILog Push(string name, object? value);
    ILog DiagnosticPush(string name, object? value);

    ILog Info(
        string message,
        [CallerMemberName] string memberName = "",
        [CallerLineNumber] int lineNumber = -1
    );
}