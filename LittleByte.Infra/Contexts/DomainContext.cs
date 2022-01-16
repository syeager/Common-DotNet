using AutoMapper;
using LittleByte.Core.Exceptions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace LittleByte.Infra.Contexts;

public abstract class DomainContext<TContext, TUser, TRole> : IdentityDbContext<TUser, TRole, Guid>
    where TContext : DbContext
    where TUser : IdentityUser<Guid>
    where TRole : IdentityRole<Guid>
{
    private readonly IMapper mapper;
    private readonly Dictionary<object, object> domainToDao = new();

    protected DomainContext(IMapper mapper, DbContextOptions<TContext> options)
        : base(options)
    {
        this.mapper = mapper;
    }

    public EntityEntry<TEntity> Add<TDomain, TEntity>(TDomain domain) where TEntity : class
    {
        var entity = mapper.Map<TEntity>(domain);
        return Add(entity);
    }

    public async ValueTask<TDomain> FindForEditAsync<TDomain, TEntity>(Guid id) where TEntity : class
    {
        var dao = await FindAsync<TEntity>(id);
        if(dao is null)
        {
            throw new NotFoundException(typeof(TDomain), id);
        }

        var domain = mapper.Map<TDomain>(dao)!;
        domainToDao[domain] = dao;
        return domain;
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new())
    {
        foreach(var (domain, dao) in domainToDao)
        {
            mapper.Map(domain, dao);
        }

        return base.SaveChangesAsync(cancellationToken);
    }
}
