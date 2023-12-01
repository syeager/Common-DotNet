using LittleByte.Common.Logging;

namespace LittleByte.EntityFramework;

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
    private readonly Dictionary<string, Guid> cache = new();
    private readonly ILog log;

    public EntityIdCache()
    {
        log = this.NewLogger();
    }

    public void Add(IEntity entity)
    {
        Add(entity.Identifier, entity.Id);
    }

    public void Add(string identifier, Guid id)
    {
        log
            .Push("Entity.Id", id)
            .Push("Entity.Identifier", identifier)
            .Debug("Cache entry Id");
        cache[identifier] = id;
    }

    public Guid Get(string identifier)
    {
        return cache[identifier];
    }
}