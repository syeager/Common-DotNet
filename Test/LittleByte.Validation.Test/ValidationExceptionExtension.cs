using FluentValidation;
using static NUnit.Framework.Assert;

namespace LittleByte.Validation.Test;

public static class ValidationExceptionExtension
{
    public static void AssertFailure(this ValidationException @this, string propertyName)
    {
        var error = @this.Errors.Single(e => e.PropertyName == propertyName);
        That(error, Is.Not.Null, $"No validation failure for property '{propertyName}'");
    }
}