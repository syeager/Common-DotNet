using Serilog;

namespace LittleByte.Common.Logging;

public static class Logs
{
    public const string DefaultTemplate =
        "{Timestamp:HH:mm:ss}|{Level:u3}|{ClassName}.{MemberName}:{LineNumber}|{Message:lj}|{Properties:j}|{Exception}";

    public static IDiagnosticContext DiagnosticContext { get; set; } = new NullDiagnosticContext();

    public static ILog NewLogger(this object @this)
    {
        return new Log(@this.GetType());
    }
}