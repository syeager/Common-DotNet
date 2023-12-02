using JetBrains.Annotations;

namespace LittleByte.AspNet;

public static class HttpExceptionMiddlewareExtension
{
    public static IApplicationBuilder UseHttpExceptions(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<HttpExceptionMiddleware>();
    }
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

            await context.Response.WriteJsonAsync(result, (int) exception.StatusCode);
        }
    }
}