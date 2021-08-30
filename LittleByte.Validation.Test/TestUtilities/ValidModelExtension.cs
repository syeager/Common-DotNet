using NUnit.Framework;

namespace LittleByte.Validation.Test.TestUtilities
{
    public static class ValidModelExtension
    {
        public static void AssertFirstError<T>(this ValidModel<T> @this, string propertyName, string errorCode) where T : class
        {
            Assert.IsFalse(@this.IsSuccess);
            var error = @this.Validation.Errors[0];
            Assert.AreEqual(propertyName, error.PropertyName);
            Assert.AreEqual(errorCode, error.ErrorCode);
        }
    }
}
