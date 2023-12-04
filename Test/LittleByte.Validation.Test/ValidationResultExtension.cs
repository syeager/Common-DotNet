using FluentValidation.Results;
using static NUnit.Framework.Assert;

namespace LittleByte.Validation.Test;

public static class ValidationResultExtension
{
    public static void AssertFailure(this ValidationResult @this, string propertyName)
    {
        That(@this.IsValid, Is.False, "Expected failed validation");
        var error = @this.Errors.Single(e => e.PropertyName == propertyName);
        That(error, Is.Not.Null, $"No validation failure for property '{propertyName}'");
    }
}