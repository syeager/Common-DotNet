namespace LittleByte.Validation.Test;

public static class IModelValidatorExtension
{
    public static void Fail<T>(this IModelValidator<T> @this) where T : class
    {
        @this.Sign(Arg.Any<T>()).ReturnsForAnyArgs(ValidModel.Failed<T>());
    }

    public static void Pass<T>(this IModelValidator<T> @this) where T : class
    {
        @this.Sign(Arg.Any<T>()).ReturnsForAnyArgs(ValidModel.Succeeded<T>());
    }
}
