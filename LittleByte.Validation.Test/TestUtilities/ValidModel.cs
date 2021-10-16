using FluentValidation.Results;

namespace LittleByte.Validation.Test.TestUtilities
{
    public static class ValidModel
    {
        public static ValidModel<T> Succeeded<T>()
            where T : class
        {
            return new ValidModel<T>(null, new ValidationResult());
        }

        public static ValidModel<T> Failed<T>()
            where T : class
        {
            return new ValidModel<T>(null, new ValidationResult(new[] { new ValidationFailure("", "") }));
        }
    }
}
