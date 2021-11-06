using NUnit.Framework;

namespace LittleByte.Validation.Test.TestUtilities
{
    public static class ValidModelExtension
    {
        public static void AssertFirstError<T>(this Valid<T> @this, string propertyName, string errorCode) where T : class
        {
            Assert.IsFalse(@this.IsSuccess, "Expected failed validation");
            var error = @this.Validation.Errors[0];
            Assert.AreEqual(propertyName, error.PropertyName, "Incorrect property name");
            Assert.AreEqual(errorCode, error.ErrorCode, "Incorrect error code");
        }
    }
}
