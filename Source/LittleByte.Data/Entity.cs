using LittleByte.Common;

namespace LittleByte.Data;

public abstract class Entity : IIdObject
{
    public required Guid Id { get; init; }
}