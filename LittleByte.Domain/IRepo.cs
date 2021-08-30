using System;
using System.Threading.Tasks;
using LittleByte.Validation;

namespace LittleByte.Domain
{
    public interface IRepo<TEntity>
    {
        ValueTask AddAsync(ValidModel<TEntity> entity);
        ValueTask DeleteAsync(TEntity entity);
        
        ValueTask UpdateAsync(ValidModel<TEntity> entity);
        ValueTask SaveAsync();
    }

    public interface IIdRepo<TEntity> : IRepo<TEntity>
    {
        ValueTask DeleteAsync(Guid id);
        ValueTask<TEntity?> FindAsync(Guid id);
    }
}