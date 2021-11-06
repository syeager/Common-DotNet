using FluentValidation.Results;

namespace LittleByte.Validation.Test.TestUtilities
{
    public static class ValidModel
    {
        public static Valid<T> Succeeded<T>()
            where T : class
        {
            return new Valid<T>(null, new ValidationResult());
        }

        public static Valid<T> Failed<T>()
            where T : class
        {
            return new Valid<T>(null, new ValidationResult(new[] { new ValidationFailure("", "") }));
        }
    }
}
