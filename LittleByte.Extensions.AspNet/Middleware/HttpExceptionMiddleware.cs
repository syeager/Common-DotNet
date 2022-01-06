using System.Threading.Tasks;
using JetBrains.Annotations;
using LittleByte.Core.Exceptions;
using LittleByte.Extensions.AspNet.Extensions;
using LittleByte.Extensions.AspNet.Responses;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace LittleByte.Extensions.AspNet.Middleware
{
    public static class HttpExceptionMiddlewareExtension
    {
        public static IApplicationBuilder UseHttpExceptions(this IApplicationBuilder builder) =>
            builder.UseMiddleware<HttpExceptionMiddleware>();
    }

    public class HttpExceptionMiddleware
    {
        private readonly RequestDelegate next;

        public HttpExceptionMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        [UsedImplicitly]
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch(HttpException exception)
            {
                var result = new ApiResponse(exception.StatusCode, exception.Message);

                await context.Response.WriteJsonAsync(result, (int)exception.StatusCode);
            }
        }
    }
}
