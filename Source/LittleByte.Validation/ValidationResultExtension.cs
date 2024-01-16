using FluentValidation.Results;

namespace LittleByte.Validation;

public static class ValidationResultExtension
{
    public static ValidationResult AddFailure<TValidator>(
        this ValidationResult @this,
        string propertyName,
        string attemptedValue,
        string message)
    {
        var failure = new ValidationFailure(propertyName, message, attemptedValue)
        {
            ErrorCode = typeof(TValidator).Name,
        };
        @this.Errors.Add(failure);

        return @this;
    }

    public static ValidationResult AddIfNotNull(this ValidationResult @this, ValidationFailure? failure)
    {
        if (failure != null)
        {
            @this.Errors.Add(failure);
        }

        return @this;
    }
}