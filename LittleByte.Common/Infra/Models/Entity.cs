using JetBrains.Annotations;
using LittleByte.Common.Objects;

namespace LittleByte.Common.Infra.Models;

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

    public virtual string Identifier => Id.ToString();
}
