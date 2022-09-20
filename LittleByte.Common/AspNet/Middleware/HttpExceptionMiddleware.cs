using JetBrains.Annotations;
using LittleByte.Common.AspNet.Extensions;
using LittleByte.Common.AspNet.Responses;
using LittleByte.Common.Exceptions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace LittleByte.Common.AspNet.Middleware
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
