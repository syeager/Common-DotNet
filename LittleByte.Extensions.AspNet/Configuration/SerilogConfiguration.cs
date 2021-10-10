using Microsoft.AspNetCore.Builder;
using Serilog;

namespace LittleByte.Extensions.AspNet;

public static class SerilogConfiguration
{
    public static WebApplicationBuilder UseSerilog(this WebApplicationBuilder builder)
    {
        builder.Host.UseSerilog(
            (context, services, configuration) => configuration
                .ReadFrom.Configuration(context.Configuration)
                .ReadFrom.Services(services));

        return builder;
    }
}
