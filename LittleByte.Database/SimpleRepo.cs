using System.Threading.Tasks;
using LittleByte.Domain;
using LittleByte.Validation;
using Microsoft.EntityFrameworkCore;

namespace LittleByte.Database
{
    public abstract class SimpleRepo<TDomain, TDao> : IRepo<TDomain>
        where TDomain : class
        where TDao : IEntity<TDomain>, new()
    {
        protected readonly DbContext dbContext;

        protected SimpleRepo(DbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public abstract TDao ToDao(TDomain entity);

        public TDao ToDao(ValidModel<TDomain> entity) => ToDao(entity.GetModelOrThrow());

        public virtual ValueTask AddAsync(ValidModel<TDomain> entity)
        {
            var dao = ToDao(entity);
            dbContext.Add(dao);
            return ValueTask.CompletedTask;
        }

        public virtual ValueTask DeleteAsync(TDomain entity)
        {
            var dao = ToDao(entity);
            dbContext.Remove(dao);
            return ValueTask.CompletedTask;
        }

        public virtual ValueTask UpdateAsync(ValidModel<TDomain> entity)
        {
            var dao = ToDao(entity);
            dbContext.Update(dao);
            return ValueTask.CompletedTask;
        }

        public virtual ValueTask SaveAsync()
        {
            return new(dbContext.SaveChangesAsync());
        }
    }
}