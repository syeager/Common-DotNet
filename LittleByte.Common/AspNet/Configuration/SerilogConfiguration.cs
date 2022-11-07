using LittleByte.Common.Logging;
using Microsoft.AspNetCore.Builder;
using Serilog;

namespace LittleByte.Common.AspNet.Configuration;

public static class SerilogConfiguration
{
    public static WebApplicationBuilder UseSerilog(this WebApplicationBuilder builder)
    {
        builder.Host.UseSerilog(
            (context, services, configuration) => configuration
                .ReadFrom.Configuration(context.Configuration)
                .ReadFrom.Services(services).Enrich.With<RemoveSourceContextEnricher>());

        return builder;
    }
}
