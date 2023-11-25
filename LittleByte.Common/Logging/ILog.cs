using System.Runtime.CompilerServices;

namespace LittleByte.Common.Logging;

public interface ILog : IDisposable
{
    public ILog Push<TProperty>(object? value);
    public ILog Push<TProperty>(TProperty? value);
    public ILog Push(ILoggable loggable);
    public ILog Push(string name, object? value);
    public ILog DiagnosticPush(string name, object? value);

    public ILog Info(
        string message,
        [CallerMemberName] string memberName = "",
        [CallerLineNumber] int lineNumber = -1
    );
}