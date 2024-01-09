using Microsoft.AspNetCore.HttpOverrides;

namespace LittleByte.AspNet;

public static class ForwardHeadersConfiguration
{
    public static IApplicationBuilder SetForwardedHeaders(this IApplicationBuilder app)
    {
        var forwardOptions = new ForwardedHeadersOptions
        {
            ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto,
            RequireHeaderSymmetry = false
        };

        forwardOptions.KnownNetworks.Clear();
        forwardOptions.KnownProxies.Clear();

        app.UseForwardedHeaders(forwardOptions);
        return app;
    }
}