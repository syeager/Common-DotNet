using System.Diagnostics.CodeAnalysis;

namespace LittleByte.Domain
{
    public class OperationResult(bool succeeded, string? errorMessage = null)
    {
        [MemberNotNullWhen(false, nameof(ErrorMessage))]
        public bool Succeeded { get; } = succeeded;

        public string? ErrorMessage { get; } = errorMessage;

        public string Type => GetType().Name;

        public void Deconstruct(out bool succeeded, out string? errorMessage)
        {
            succeeded = Succeeded;
            errorMessage = ErrorMessage;
        }
    }
}
