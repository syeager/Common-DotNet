namespace LittleByte.Validation.Test;

public static class Validator
{
    public static IModelValidator<T> WillPass<T>() => new PassModelValidator<T>();

    public static IModelValidator<T> WillFail<T>() => new FailModelValidator<T>();
}