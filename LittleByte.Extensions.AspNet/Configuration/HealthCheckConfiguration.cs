using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using LittleByte.Extensions.AspNet.Json;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace LittleByte.Extensions.AspNet.Configuration
{
    public static class HealthCheckConfiguration
    {
        public static IApplicationBuilder UseHealthChecks(this IApplicationBuilder @this)
        {
            return @this.UseHealthChecks("/health", new HealthCheckOptions {ResponseWriter = WriteHealthResponse});
        }

        private static Task WriteHealthResponse(HttpContext httpContext, HealthReport report)
        {
            httpContext.Response.ContentType = "application/json";
            var responseJson = JsonSerializer.Serialize(report, new JsonSerializerOptions
            {
                WriteIndented = true,
                Converters =
                {
                    new JsonStringEnumConverter(),
                    new TimespanConverter(@"ss\:fff"),
                }
            });
            return httpContext.Response.WriteAsync(responseJson);
        }
    }
}
