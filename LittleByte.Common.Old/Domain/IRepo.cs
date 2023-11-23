using LittleByte.Common.Validation;

namespace LittleByte.Common.Domain
{
    public interface IRepo<TEntity>
    {
        ValueTask AddAsync(Valid<TEntity> entity);
        ValueTask DeleteAsync(TEntity entity);

        ValueTask UpdateAsync(Valid<TEntity> entity);
        ValueTask SaveAsync();
    }

    public interface IIdRepo<TEntity> : IRepo<TEntity>
    {
        ValueTask DeleteAsync(Guid id);
        ValueTask<TEntity?> FindAsync(Guid id);
    }
}
