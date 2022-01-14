using JetBrains.Annotations;
using LittleByte.Core.Objects;

namespace LittleByte.Infra.Models;

public interface IEntity : IIdObject
{
    public string Identifier { get; }
}

public abstract class Entity : IEntity
{
    public Guid Id
    {
        get;
        [UsedImplicitly]
        init;
    }

    public abstract string Identifier { get; }
}
