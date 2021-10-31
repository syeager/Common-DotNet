using JetBrains.Annotations;

namespace LittleByte.Infra.Models;

public interface IEntity
{
    public Guid Id { get; }
    public string Identifier { get; }
}

public abstract class Entity : IEntity
{
    public Guid Id { get; [UsedImplicitly] init; }

    public abstract string Identifier { get; }
}