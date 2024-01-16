using static NUnit.Framework.Assert;

namespace LittleByte.Validation.Test
{
    public static class ValidModelExtension
    {
        public static void AssertFirstError<T>(this Valid<T> @this, string propertyName, string errorCode)
            where T : class
        {
            Multiple(() =>
            {
                That(@this.IsSuccess, Is.False);
                var error = @this.Validation.Errors[0];
                That(propertyName, Is.EqualTo(error.PropertyName));
                That(errorCode, Is.EqualTo(error.ErrorCode));
            });
        }
    }
}
