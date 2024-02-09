namespace LittleByte.Domain;

public sealed class UnexpectedResultException(Result? result) : Exception(result?.ErrorMessage ?? "Result is null");