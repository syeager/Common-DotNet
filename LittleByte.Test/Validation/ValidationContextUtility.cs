using FluentValidation;
using LittleByte.Common;

namespace LittleByte.Test.Validation
{
    public static class ValidationContextUtility
    {
        public static ValidationContext<T> Empty<T>()
            where T : class
        {
            return new ValidationContext<T>(null!);
        }

        public static ValidationContext<X> Empty()
        {
            return new ValidationContext<X>(null!);
        }
    }
}
