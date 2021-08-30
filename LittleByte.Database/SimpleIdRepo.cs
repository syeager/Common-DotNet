using System;
using System.Threading.Tasks;
using LittleByte.Domain;
using Microsoft.EntityFrameworkCore;

namespace LittleByte.Database
{
    public abstract class SimpleIdRepo<TDomain, TDao> : SimpleRepo<TDomain, TDao>, IIdRepo<TDomain>
        where TDao : class, IEntity<TDomain>, new()
        where TDomain : class
    {
        protected SimpleIdRepo(DbContext dbContext)
            : base(dbContext)
        {
        }

        public virtual async ValueTask<TDomain?> FindAsync(Guid id)
        {
            var dao = await dbContext.FindAsync<TDao>(id);
            var domain = dao?.ToDomain();
            return domain;
        }

        public virtual ValueTask DeleteAsync(Guid id)
        {
            var dao = new TDao {Id = id};
            dbContext.Attach(dao);
            dbContext.Remove(dao);
            return ValueTask.CompletedTask;
        }
    }
}