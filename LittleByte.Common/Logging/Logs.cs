using Serilog;

namespace LittleByte.Common.Logging;

public static class Logs
{
    public const string DefaultTemplate =
        "{Timestamp:HH:mm:ss}|{Level:u3}|{ClassName}.{MemberName}:{LineNumber}|{Message:lj}|{Properties:j}|{Exception}{NewLine}";

    public static IDiagnosticContext DiagnosticContext { get; set; } = new NullDiagnosticContext();

    public static ILog NewLogger(this object @this)
    {
        var log = new Log(@this.GetType());

        if(@this is ILoggable loggable)
        {
            log.Push(loggable);
        }

        return log;
    }
}