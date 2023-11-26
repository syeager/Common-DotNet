#pragma warning disable Serilog004

using System.Diagnostics;
using System.Runtime.CompilerServices;
using LittleByte.Common;
using LittleByte.Common.Logging;
using Serilog;
using Serilog.Context;
using Serilog.Events;

namespace LittleByte.Serilog;

internal sealed class Log : ILog
{
    private readonly string className;
    private readonly ILogger logger;
    private IDisposable? rootLogContext;

    [DebuggerHidden]
    public static IDiagnosticContext DiagnosticContext { get; set; } = NullDiagnosticContext.Instance;

    [DebuggerHidden]
    private Log(Type contextType)
    {
        logger = global::Serilog.Log.ForContext(contextType);
        className = contextType.Name;
    }

    [DebuggerHidden]
    public static ILog Create(Type forType)
    {
        return new Log(forType);
    }

    [DebuggerHidden]
    public void Dispose() => rootLogContext?.Dispose();

    [DebuggerHidden]
    public ILog Push(ILoggableKeyValue loggable)
    {
        return Push(loggable.LogKey, loggable.LogValue);
    }

    [DebuggerHidden]
    public ILog Push(ILoggableProperties loggable)
    {
        loggable.Properties().ForEach((p, _) => Push(p.Key, p.Value));
        return this;
    }

    [DebuggerHidden]
    public ILog Push(string name, object? value)
    {
        var context = LogContext.PushProperty(name, value);
        rootLogContext ??= context;
        return this;
    }

    [DebuggerHidden]
    public ILog ContextPush(ILoggableKeyValue loggable)
    {
        return ContextPush(loggable.LogKey, loggable.LogValue);
    }

    [DebuggerHidden]
    public ILog ContextPush(ILoggableProperties loggable)
    {
        foreach (var (key, value) in loggable.Properties())
        {
            ContextPush(key, value);
        }

        return this;
    }

    [DebuggerHidden]
    public ILog ContextPush(string name, object? value)
    {
        DiagnosticContext.Set(name, value);
        return this;
    }

    [DebuggerHidden]
    public ILog Write(LogLevel level, string message, Exception? exception = null, string memberName = "", int lineNumber = -1)
    {
        using var props = LogContext.PushProperty("ClassName", className);
        LogContext.PushProperty("MemberName", memberName);
        LogContext.PushProperty("LineNumber", lineNumber);
        logger.Write(ToSerilog(level), exception, message);
        return this;
    }

    [DebuggerHidden]
    public ILog Debug(string message, [CallerMemberName] string memberName = "", [CallerLineNumber] int lineNumber = -1) => Write(LogLevel.Debug, message, null, memberName, lineNumber);

    [DebuggerHidden]
    public ILog Info(string message, [CallerMemberName] string memberName = "", [CallerLineNumber] int lineNumber = -1) => Write(LogLevel.Info, message, null, memberName, lineNumber);

    [DebuggerHidden]
    public ILog Warn(string message, [CallerMemberName] string memberName = "", [CallerLineNumber] int lineNumber = -1) => Write(LogLevel.Warn, message, null, memberName, lineNumber);

    [DebuggerHidden]
    public ILog Error(string message, Exception? exception = null, [CallerMemberName] string memberName = "", [CallerLineNumber] int lineNumber = -1) => Write(LogLevel.Error, message, exception, memberName, lineNumber);


    [DebuggerHidden]
    private static LogEventLevel ToSerilog(LogLevel level)
    {
        return level switch
        {
            LogLevel.Debug => LogEventLevel.Debug,
            LogLevel.Info => LogEventLevel.Information,
            LogLevel.Warn => LogEventLevel.Warning,
            LogLevel.Error => LogEventLevel.Error,
            _ => throw new NotSupportedException("Log level is not supported")
        };
    }
}