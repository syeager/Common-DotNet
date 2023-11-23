using LittleByte.Common.Infra.Models;
using Serilog;

namespace LittleByte.Common.Infra.Services;

public interface IEntityIdReadCache
{
    public Guid Get(string identifier);
}

public interface IEntityIdWriteCache : IEntityIdReadCache
{
    public void Add(IEntity entity);
    public void Add(string identifier, Guid id);
}

public class EntityIdCache : IEntityIdWriteCache
{
    private readonly ILogger logger = Log.ForContext<EntityIdCache>();
    private readonly Dictionary<string, Guid> cache = new();

    public void Add(IEntity entity) => Add(entity.Identifier, entity.Id);

    public void Add(string identifier, Guid id)
    {
        logger
            .ForContext("Entity.Id", id)
            .ForContext("Entity.Identifier", identifier)
            .Debug("Cache entry Id");
        cache[identifier] = id;
    }

    public Guid Get(string identifier) => cache[identifier];
}
