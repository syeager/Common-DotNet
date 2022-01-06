using AutoMapper;
using LittleByte.Core.Exceptions;
using LittleByte.Core.Objects;
using Microsoft.EntityFrameworkCore;

namespace LittleByte.Infra.Queries;

public interface IFindByIdQuery<TDomain>
{
    public ValueTask<TDomain?> FindAsync(Guid id);
    //public ValueTask<TDomain?> FindAsync(string id);
}

public class FindByIdQuery<TDomain, TEntity, TContext> : IFindByIdQuery<TDomain>
    //where TEntity : class, IStringId
    where TEntity : class, IIdObject
    where TContext : DbContext
{
    private readonly TContext dbContext;
    private readonly IMapper mapper;

    public FindByIdQuery(TContext dbContext, IMapper mapper)
    {
        this.dbContext = dbContext;
        this.mapper = mapper;
    }

    public async ValueTask<TDomain?> FindAsync(Guid id)
    {
        var dao = await dbContext.Set<TEntity>().FindAsync(id);
        if(dao == null)
        {
            throw new NotFoundException(typeof(TDomain), id);
        }

        var user = mapper.Map<TDomain>(dao);
        return user;
    }

    // public async ValueTask<TDomain?> FindAsync(string id)
    // {
    //     var dao = await dbContext.FindAsync<TEntity>(id);
    //     if(dao == null)
    //     {
    //         throw new NotFoundException(typeof(TDomain), Guid.Parse(id));
    //     }
    //
    //     var user = mapper.Map<TDomain>(dao);
    //     return user;
    // }
}
