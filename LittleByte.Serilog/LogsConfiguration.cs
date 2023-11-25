using LittleByte.Common.Logging;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace LittleByte.Serilog;

public static class LogsConfiguration
{
    public const string DefaultTemplate =
        "{Timestamp:HH:mm:ss}|{Level:u3}|{ClassName}.{MemberName}:{LineNumber}|{Message:lj}|{Properties:j}|{Exception}{NewLine}";

    public static IServiceCollection AddLogs(this IServiceCollection services)
    {
        Logs.LogFactory = Log.Create;

        using var serviceProvider = services.BuildServiceProvider();
        Log.DiagnosticContext = serviceProvider.GetRequiredService<IDiagnosticContext>();
        return services;
    }
}
