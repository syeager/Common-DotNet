namespace LittleByte.Domain;

public sealed class UnexpectedResultException(OperationResult? result) : Exception(result?.ErrorMessage ?? "Result is null");