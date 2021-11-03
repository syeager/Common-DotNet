using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace LittleByte.Infra;

public interface IRepo
{
    public Task<int> SaveChangesAsync();
}

public class Repo<TContext> : IRepo where TContext : DbContext
{
    protected readonly ILogger logger;
    protected readonly TContext dbContext;
    protected readonly IMapper mapper;
    protected readonly IEntityIdWriteCache modelIdCache;

    public Repo(TContext dbContext, IMapper mapper, IEntityIdWriteCache modelIdCache)
    {
        logger = Log.ForContext(GetType());
        this.dbContext = dbContext;
        this.mapper = mapper;
        this.modelIdCache = modelIdCache;
    }

    public virtual Task<int> SaveChangesAsync() => dbContext.SaveChangesAsync();
}
