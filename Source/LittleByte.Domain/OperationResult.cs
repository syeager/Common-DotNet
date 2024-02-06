namespace LittleByte.Domain
{
    public record OperationResult(bool Succeeded, string ErrorMessage = "")
    {
        public static OperationResult Success() => new(true);
    }
}
