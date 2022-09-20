using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace LittleByte.Common.Logging.Configuration;

public static class LogsConfiguration
{
    public static IServiceCollection AddLogs(this IServiceCollection services)
    {
        using var serviceProvider = services.BuildServiceProvider();
        Logs.DiagnosticContext = serviceProvider.GetRequiredService<IDiagnosticContext>();
        return services;
    }
}
