using AutoMapper;
using LittleByte.Common.Exceptions;
using LittleByte.Common.Objects;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace LittleByte.Common.Infra.Contexts;

public interface IDomainContext
{
    ValueTask<TDomain?> FindAsync<TDomain, TEntity>(Guid id)
        where TEntity : class, IIdObject;

    ValueTask<TDomain?> FindForEditAsync<TDomain, TEntity>(Guid id)
        where TEntity : class, IIdObject;

    ValueTask<TDomain> FindRequiredAsync<TDomain, TEntity>(Guid id)
        where TEntity : class, IIdObject;

    ValueTask<TDomain> FindRequiredForEditAsync<TDomain, TEntity>(Guid id)
        where TEntity : class, IIdObject;
}

public abstract class DomainContext<TContext, TUser, TRole> : IdentityDbContext<TUser, TRole, Guid>, IDomainContext
    where TContext : DbContext
    where TUser : IdentityUser<Guid>
    where TRole : IdentityRole<Guid>
{
    private record EntityMap(object Domain, object Entity);

    private readonly IMapper mapper;
    private readonly Dictionary<Guid, EntityMap> entityMaps = new();

    protected DomainContext(IMapper mapper, DbContextOptions<TContext> options)
        : base(options)
    {
        this.mapper = mapper;
    }

    public EntityEntry<TEntity> Add<TDomain, TEntity>(TDomain domain)
        where TEntity : class
    {
        var entity = mapper.Map<TEntity>(domain);
        return Add(entity);
    }

    public void AddRange<TDomain, TEntity>(IEnumerable<TDomain> domainList)
        where TEntity : class
    {
        foreach(var domain in domainList)
        {
            Add<TDomain, TEntity>(domain);
        }
    }

    public ValueTask<TDomain?> FindAsync<TDomain, TEntity>(Guid id)
        where TEntity : class, IIdObject
    {
        return FindInternalAsync<TDomain, TEntity>(id, false);
    }

    public ValueTask<TDomain?> FindForEditAsync<TDomain, TEntity>(Guid id)
        where TEntity : class, IIdObject
    {
        return FindInternalAsync<TDomain, TEntity>(id, true);
    }

    public async ValueTask<TDomain> FindRequiredAsync<TDomain, TEntity>(Guid id)
        where TEntity : class, IIdObject
    {
        var domain = await FindAsync<TDomain, TEntity>(id);
        if(domain is null)
        {
            throw new NotFoundException(typeof(TDomain), id);
        }

        return domain;
    }

    public async ValueTask<TDomain> FindRequiredForEditAsync<TDomain, TEntity>(Guid id)
        where TEntity : class, IIdObject
    {
        var domain = await FindForEditAsync<TDomain, TEntity>(id);
        if(domain is null)
        {
            throw new NotFoundException(typeof(TDomain), id);
        }

        return domain;
    }

    private async ValueTask<TDomain?> FindInternalAsync<TDomain, TEntity>(Guid id, bool isEditable)
        where TEntity : class, IIdObject
    {
        if(!entityMaps.TryGetValue(id, out var entityMap))
        {
            var query = isEditable
                ? Set<TEntity>().AsTracking()
                : Set<TEntity>();

            var entity = await query.FirstOrDefaultAsync(e => e.Id == id);

            if(entity is null)
            {
                return default;
            }

            var domain = mapper.Map<TDomain>(entity)!;
            entityMap = new EntityMap(domain, entity);
            entityMaps.Add(id, entityMap);
        }

        return (TDomain)entityMap.Domain;
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new())
    {
        foreach(var (_, (domain, entity)) in entityMaps)
        {
            mapper.Map(domain, entity);
        }

        return base.SaveChangesAsync(cancellationToken);
    }
}
