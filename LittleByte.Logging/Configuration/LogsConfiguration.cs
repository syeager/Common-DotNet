using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace LittleByte.Logging.Configuration;

public static class LogsConfiguration
{
    public static void AddLogs(this IServiceCollection services)
    {
        using var serviceProvider = services.BuildServiceProvider();
        Logs.DiagnosticContext = serviceProvider.GetRequiredService<IDiagnosticContext>();
    }
}