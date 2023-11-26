using System.Diagnostics;

namespace LittleByte.Common.Logging;

public sealed class NullLog : ILog
{
    public static NullLog Instance { get; } = new();

    private NullLog() {}
    [DebuggerHidden]
    public static ILog Create(Type forType) => Instance;
    [DebuggerHidden]
    public void Dispose() {}
    [DebuggerHidden]
    public ILog Push(ILoggableKeyValue loggable) => this;
    [DebuggerHidden]
    public ILog Push(ILoggableKeyValue loggable, string keyPrefix) => this;
    [DebuggerHidden]
    public ILog Push(ILoggableProperties loggable) => this;
    [DebuggerHidden]
    public ILog Push(ILoggableProperties loggable, string keyPrefix) => this;
    [DebuggerHidden]
    public ILog Push(string name, object? value) => this;
    [DebuggerHidden]
    public ILog ContextPush(ILoggableKeyValue loggable) => this;
    [DebuggerHidden]
    public ILog ContextPush(ILoggableKeyValue loggable, string keyPrefix) => this;
    [DebuggerHidden]
    public ILog ContextPush(ILoggableProperties loggable) => this;
    [DebuggerHidden]
    public ILog ContextPush(ILoggableProperties loggable, string keyPrefix) => this;
    [DebuggerHidden]
    public ILog ContextPush(string name, object? value) => this;
    [DebuggerHidden]
    public ILog Write(LogLevel level, string message, Exception? exception = null, string memberName = "", int lineNumber = -1) => this;
    [DebuggerHidden]
    public ILog Debug(string message, string memberName = "", int lineNumber = -1) => this;
    [DebuggerHidden]
    public ILog Info(string message, string memberName = "", int lineNumber = -1) => this;
    [DebuggerHidden]
    public ILog Warn(string message, string memberName = "", int lineNumber = -1) => this;
    [DebuggerHidden]
    public ILog Error(string message, Exception? exception = null, string memberName = "", int lineNumber = -1) => this;
}