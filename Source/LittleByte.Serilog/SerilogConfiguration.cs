using LittleByte.Common;
using LittleByte.Common.Logging;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Extensions.Hosting;

namespace LittleByte.Serilog;

public static class SerilogConfiguration
{
    public const string DefaultTemplate =
        "{Timestamp:HH:mm:ss}|{Level:u3}|{ClassName}.{MemberName}:{LineNumber}|{Message:lj}|{Properties:j}|{Exception}{NewLine}";

    public static void CreateBootstrap()
    {
        global::Serilog.Log.Logger = new LoggerConfiguration()
            .WriteTo.Console(outputTemplate: DefaultTemplate)
            .CreateBootstrapLogger();
    }

    public static IHostBuilder UseSerilog(this IHostBuilder builder, IConfiguration configuration)
    {
        builder.UseSerilog(
            (_, services, config) =>
            {
                config
                    .ReadFrom.Services(services)
                    .ReadFrom.Configuration(configuration);
                services.ConfigureLittleByte();
            });

        return builder;
    }

    public static void UseSerilog(Action<LoggerConfiguration> configure)
    {
        var config = new LoggerConfiguration();
        configure(config);
        var logger = config.CreateLogger();

        var diagnosticContext = new DiagnosticContext(logger);
        ConfigureLittleByte(diagnosticContext);

        global::Serilog.Log.Logger = logger;
    }

    public static void ConfigureLittleByte(this IServiceProvider serviceProvider)
    {
        var diagnosticContext = serviceProvider.Get<IDiagnosticContext>();
        ConfigureLittleByte(diagnosticContext);
    }

    private static void ConfigureLittleByte(IDiagnosticContext diagnosticContext)
    {
        Logs.LogFactory = Log.Create;
        Log.DiagnosticContext = diagnosticContext;
    }
}