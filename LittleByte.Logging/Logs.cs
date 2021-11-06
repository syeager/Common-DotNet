using Serilog;

namespace LittleByte.Logging
{
    public sealed class NullDiagnosticContext : IDiagnosticContext
    {
        public void Set(string propertyName, object value, bool destructureObjects = false) { }
    }

    public static class Logs
    {
        public static ILog Props(this ILogger _) => new Log();
        public static ILog Props() => new Log();
        public static IDiagnosticContext DiagnosticContext { get; set; } = new NullDiagnosticContext();
    }
}