using LittleByte.Common.Logging;
using Microsoft.Extensions.DependencyInjection;

namespace LittleByte.Serilog;

public static class LogsConfiguration
{
    public static IServiceCollection AddLogs(this IServiceCollection services)
    {
        using var serviceProvider = services.BuildServiceProvider();
        Logs.DiagnosticContext = serviceProvider.GetRequiredService<IDiagnosticContext>();
        return services;
    }
}
