using Microsoft.AspNetCore.Builder;
using Serilog;

namespace LittleByte.Serilog.AspNet;

public static class LogsConfiguration
{

    public static WebApplicationBuilder UseSerilog(this WebApplicationBuilder builder)
    {
        builder.Host.UseSerilog(
            (_, services, configuration) =>
            {
                configuration
                    .ReadFrom.Services(services)
                    .ReadFrom.Configuration(builder.Configuration);
                services.ConfigureLittleByte();
            });

        return builder;
    }
}