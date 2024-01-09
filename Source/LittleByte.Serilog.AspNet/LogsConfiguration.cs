using Microsoft.AspNetCore.Builder;
using Serilog;

namespace LittleByte.Serilog.AspNet;

public static class LogsConfiguration
{
    public static IApplicationBuilder UseSerilog(this IApplicationBuilder app)
    {
        Serilog.LogsConfiguration.InitLogging();
        return app.UseSerilogRequestLogging();
    }
}