namespace LittleByte.Core.Extensions
{
    public static class StringExtension
    {
        // https://github.com/ServiceStack/ServiceStack/blob/master/src/ServiceStack/FluentValidation/Validators/EmailValidator.cs
        public static bool IsEmail(this string @this)
        {
            var index = @this.IndexOf('@');

            return
                index > 0
                && index != @this.Length - 1
                && index == @this.LastIndexOf('@');
        }
    }
}
