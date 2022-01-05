using System.IO;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace LittleByte.Extensions.AspNet.Extensions
{
    public static class HttpResponseExtension
    {
        public static async Task WriteJsonAsync(this HttpResponse response, object body, int statusCode)
        {
            response.StatusCode = statusCode;

            await using var utf8Stream = new MemoryStream();
            await JsonSerializer.SerializeAsync(utf8Stream, body, body.GetType(),
                new JsonSerializerOptions {PropertyNamingPolicy = JsonNamingPolicy.CamelCase});
            var json = Encoding.UTF8.GetString(utf8Stream.ToArray());
            response.ContentType = "application/json";
            await response.WriteAsync(json);
        }
    }
}
