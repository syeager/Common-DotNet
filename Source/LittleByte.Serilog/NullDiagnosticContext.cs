using Serilog;

namespace LittleByte.Serilog;

public sealed class NullDiagnosticContext : IDiagnosticContext
{
    public static NullDiagnosticContext Instance { get; } = new();

    private NullDiagnosticContext() { }

    public void Set(string propertyName, object value, bool destructureObjects = false) { }
    public void SetException(Exception exception) { }
}