using FluentValidation;
using JetBrains.Annotations;

namespace LittleByte.AspNet;

public static class ModelValidationExceptionExtension
{
    public static IApplicationBuilder UseModelValidationExceptions(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<ModelValidationExceptionMiddleware>();
    }
}

public class ModelValidationExceptionMiddleware
{
    private readonly RequestDelegate next;

    public ModelValidationExceptionMiddleware(RequestDelegate next)
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
        catch(ValidationException exception)
        {
            exception = new ValidationException(exception.Errors);
            throw new BadRequestException(exception.Message);
        }
    }
}