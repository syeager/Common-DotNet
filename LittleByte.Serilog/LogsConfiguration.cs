using LittleByte.Common;
using LittleByte.Common.Logging;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace LittleByte.Serilog;

public static class LogsConfiguration
{
    public const string DefaultTemplate =
        "{Timestamp:HH:mm:ss}|{Level:u3}|{ClassName}.{MemberName}:{LineNumber}|{Message:lj}|{Properties:j}|{Exception}{NewLine}";

    public static IHostBuilder UseSerilog(this IHostBuilder builder)
    {
        builder.UseSerilog(
            (_, services, configuration) => configuration
                .ReadFrom.Services(services).Enrich.With<RemoveSourceContextEnricher>());

        Logs.LogFactory = Log.Create;

        builder.ConfigureServices(serviceProvider =>
        {
            using var services = serviceProvider.BuildServiceProvider();
            Log.DiagnosticContext = services.Get<IDiagnosticContext>();
        });

        return builder;
    }
}
