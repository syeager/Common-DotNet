namespace LittleByte.Data;

public sealed class MissingEntityException : Exception
{
    public Guid Id { get; }
    public Type EntityType { get; }

    public MissingEntityException(Guid id, Type entityType)
    : base("Expected entity was not found")
    {
        Id = id;
        EntityType = entityType;
    }
}