namespace LittleByte.Domain
{
    public record OperationResult(bool Succeeded, string ErrorMessage = "");
}
