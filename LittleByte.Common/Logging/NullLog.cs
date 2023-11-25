namespace LittleByte.Common.Logging;

public sealed class NullLog : ILog
{
    public static NullLog Instance { get; } = new();

    private NullLog() {}
    public static ILog Create(Type forType) => Instance;
    public void Dispose() {}
    public ILog Push<TProperty>(object? value) => this;
    public ILog Push<TProperty>(TProperty? value) => this;
    public ILog Push(ILoggable loggable) => this;
    public ILog Push(string name, object? value) => this;
    public ILog DiagnosticPush(string name, object? value) => this;
    public ILog Info(string message, string memberName = "", int lineNumber = -1) => this;
}