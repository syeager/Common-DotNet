using System.Threading.Tasks;
using FluentValidation;
using JetBrains.Annotations;
using LittleByte.Core.Exceptions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace LittleByte.Extensions.AspNet.Middleware
{
    public static class ModelValidationExceptionExtension
    {
        public static IApplicationBuilder UseModelValidationExceptions(this IApplicationBuilder builder) =>
            builder.UseMiddleware<ModelValidationExceptionMiddleware>();
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
}
