using LittleByte.Common;
using LittleByte.Common.Logging;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Extensions.Hosting;

namespace LittleByte.Serilog;

public static class LogsConfiguration
{
    public const string DefaultTemplate =
        "{Timestamp:HH:mm:ss}|{Level:u3}|{ClassName}.{MemberName}:{LineNumber}|{Message:lj}|{Properties:j}|{Exception}{NewLine}";

    public static IHostBuilder UseSerilog(this IHostBuilder builder)
    {
        builder.UseSerilog(
            (_, services, configuration) =>
            {
                configuration.ReadFrom.Services(services).ConfigureSerilog();
                services.ConfigureLittleByte();
            });

        return builder;
    }

    private static LoggerConfiguration ConfigureSerilog(this LoggerConfiguration config)
    {
        return config
            .Enrich.FromLogContext()
            .Enrich.With<RemoveSourceContextEnricher>()
            .WriteTo.Console(outputTemplate: DefaultTemplate);
    }

    private static void ConfigureLittleByte(this IServiceProvider serviceProvider)
    {
        var diagnosticContext = serviceProvider.Get<IDiagnosticContext>();
        ConfigureLittleByte(diagnosticContext);
    }

    private static void ConfigureLittleByte(IDiagnosticContext diagnosticContext)
    {
        Logs.LogFactory = Log.Create;
        Log.DiagnosticContext = diagnosticContext;
    }

    public static void InitLogging()
    {
        var logger = new LoggerConfiguration()
            .ConfigureSerilog()
            .CreateLogger();

        var diagnosticContext = new DiagnosticContext(logger);
        ConfigureLittleByte(diagnosticContext);

        global::Serilog.Log.Logger = logger;
    }

    public static void CreateBootstrap()
    {
        global::Serilog.Log.Logger = new LoggerConfiguration()
            .WriteTo.Console()
            .CreateBootstrapLogger();
    }
}
