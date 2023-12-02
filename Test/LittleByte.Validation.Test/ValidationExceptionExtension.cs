using FluentValidation;

namespace LittleByte.Validation.Test;

public static class ValidationExceptionExtension
{
    public static void AssertFailure(this ValidationException @this, string propertyName)
    {
        var error = @this.Errors.Single(e => e.PropertyName == propertyName);
        Assert.IsNotNull(error, $"No validation failure for property '{propertyName}'");
    }
}