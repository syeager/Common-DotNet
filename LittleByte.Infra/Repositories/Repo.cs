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

    public Repo(TContext dbContext, IMapper mapper)
    {
        logger = Log.ForContext(GetType());
        this.dbContext = dbContext;
        this.mapper = mapper;
    }

    public virtual Task<int> SaveChangesAsync() => dbContext.SaveChangesAsync();
}
