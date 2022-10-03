using FluentValidation.Results;
using LittleByte.Common.Validation;

namespace LittleByte.Test.Validation
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
            return new Valid<T>(null, new ValidationResult(new[] {new ValidationFailure("", "")}));
        }
    }
}
