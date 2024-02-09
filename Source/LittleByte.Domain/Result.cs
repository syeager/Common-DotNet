using System.Diagnostics.CodeAnalysis;

namespace LittleByte.Domain
{
    public class Result(bool succeeded, string? errorMessage = null)
    {
        [MemberNotNullWhen(false, nameof(ErrorMessage))]
        public bool Succeeded { get; } = succeeded;

        public string? ErrorMessage { get; } = errorMessage;

        public static Result Success() => new(true);
    }

    public class Result<T>(bool succeeded, string? errorMessage = null, T? value = null)
        : Result(succeeded, errorMessage)
        where T : class
    {
        [MemberNotNullWhen(true, nameof(Value))]
        public new bool Succeeded => base.Succeeded;
        public T? Value { get; } = value;

        public static Result<T> Success(T value) => new(true, null, value);
    }
}
