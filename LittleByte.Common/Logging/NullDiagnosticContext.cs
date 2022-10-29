using Serilog;

namespace LittleByte.Common.Logging;

public sealed class NullDiagnosticContext : IDiagnosticContext
{
    public void Set(string propertyName, object value, bool destructureObjects = false) { }
    public void SetException(Exception exception) { }
}