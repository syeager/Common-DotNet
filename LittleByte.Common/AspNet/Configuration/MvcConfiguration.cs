using System.Diagnostics.CodeAnalysis;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.DependencyInjection;

namespace LittleByte.Common.AspNet.Configuration;

public static class MvcConfiguration
{
    [SuppressMessage("ReSharper", "UnusedMethodReturnValue.Global")]
    public static IMvcBuilder ThrowValidationExceptions(this IMvcBuilder @this) =>
        @this.ConfigureApiBehaviorOptions(options => options.InvalidModelStateResponseFactory = CreateException);

    private static IActionResult CreateException(ActionContext context) =>
        throw new ValidationException("Validation errors", CollectFailures(context.ModelState));

    private static IEnumerable<ValidationFailure> CollectFailures(ModelStateDictionary modelStates) =>
        modelStates
            .Where(pair => pair.Value?.Errors != null)
            .SelectMany(CreateFailures);

    private static IEnumerable<ValidationFailure> CreateFailures(KeyValuePair<string, ModelStateEntry?> pair)
    {
        var (propertyName, modelState) = pair;
        return modelState!.Errors.Select(error => new ValidationFailure(propertyName, error.ErrorMessage));
    }
}