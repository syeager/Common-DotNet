using AutoMapper;
using LittleByte.Common;
using LittleByte.Data;
using LittleByte.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace LittleByte.AutoMapper.EntityFramework;

public abstract class DomainContext<TContext, TUser, TRole>(IMapper mapper, DbContextOptions<TContext> options)
    : IdentityDbContext<TUser, TRole, Guid>(options), IDomainContext
    where TContext : DbContext
    where TUser : IdentityUser<Guid>
    where TRole : IdentityRole<Guid>
{
    private readonly Dictionary<Guid, EntityMap> entityMaps = new();

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
        return domain is null ? throw new MissingEntityException(id, typeof(TDomain)) : domain;
    }

    public async ValueTask<TDomain> FindRequiredForEditAsync<TDomain, TEntity>(Guid id)
        where TEntity : class, IIdObject
    {
        var domain = await FindForEditAsync<TDomain, TEntity>(id);
        return domain is null ? throw new MissingEntityException(id, typeof(TDomain)) : domain;
    }

    public void Add<TDomain, TEntity>(TDomain domain)
        where TEntity : class
    {
        var entity = mapper.Map<TEntity>(domain);
        Add(entity);
    }

    public void AddRange<TDomain, TEntity>(IEnumerable<TDomain> domainList)
        where TEntity : class
    {
        foreach(var domain in domainList)
        {
            Add<TDomain, TEntity>(domain);
        }
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

    private record EntityMap(object Domain, object Entity);
}