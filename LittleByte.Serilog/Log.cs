#pragma warning disable Serilog004

using System.Runtime.CompilerServices;
using LittleByte.Common.Logging;
using Serilog;
using Serilog.Context;
using Serilog.Extensions.Hosting;

namespace LittleByte.Serilog;

internal sealed class Log : ILog
{
    private readonly string className;
    private readonly ILogger logger;
    private IDisposable? rootLogContext;

    public static IDiagnosticContext DiagnosticContext { get; set; } = NullDiagnosticContext.Instance;
    public static ILog Create(Type forType) => new Log(forType);

    public Log(Type contextType)
    {
        logger = global::Serilog.Log.ForContext(contextType);
        className = contextType.Name;
    }

    public ILog Push<TProperty>(TProperty? value)
    {
        return Push(typeof(TProperty).Name, value);
    }

    public ILog Push<TProperty>(object? value)
    {
        return Push(typeof(TProperty).Name, value);
    }

    public ILog Push(ILoggable loggable)
    {
        return Push(loggable.LogKey, loggable.LogValue);
    }

    public ILog Push(string name, object? value)
    {
        var context = LogContext.PushProperty(name, value);
        rootLogContext ??= context;
        return this;
    }

    public ILog DiagnosticPush(string name, object? value)
    {
        DiagnosticContext.Set(name, value);
        return Push(name, value);
    }

    public void Dispose()
    {
        rootLogContext?.Dispose();
    }

    public ILog Info(
        string message,
        [CallerMemberName] string memberName = "",
        [CallerLineNumber] int lineNumber = -1
    )
    {
        using var props = LogContext.PushProperty("ClassName", className);
        LogContext.PushProperty("MemberName", memberName);
        LogContext.PushProperty("LineNumber", lineNumber);
        logger.Information(message);

        return this;
    }
}