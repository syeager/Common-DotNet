using AutoMapper;
using AutoMapper.Internal;
using LittleByte.Core.Exceptions;
using LittleByte.Core.Objects;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace LittleByte.Infra.Contexts;

public interface IDomainContext
{
    ValueTask<TDomain?> FindAsync<TDomain, TEntity>(Guid id)
        where TDomain : IIdObject
        where TEntity : class, IIdObject;

    ValueTask<TDomain?> FindForEditAsync<TDomain, TEntity>(Guid id)
        where TDomain : IIdObject
        where TEntity : class, IIdObject;

    ValueTask<TDomain> FindRequiredAsync<TDomain, TEntity>(Guid id)
        where TDomain : IIdObject
        where TEntity : class, IIdObject;

    ValueTask<TDomain> FindRequiredForEditAsync<TDomain, TEntity>(Guid id)
        where TDomain : IIdObject
        where TEntity : class, IIdObject;
}

public abstract class DomainContext<TContext, TUser, TRole> : IdentityDbContext<TUser, TRole, Guid>, IDomainContext
    where TContext : DbContext
    where TUser : IdentityUser<Guid>
    where TRole : IdentityRole<Guid>
{
    private readonly Dictionary<Guid, IIdObject> modifiedDomainModels = new();

    public IMapper Mapper { get; }

    protected DomainContext(IMapper mapper, DbContextOptions<TContext> options)
        : base(options)
    {
        Mapper = mapper;
    }

    public EntityEntry<TEntity> Add<TDomain, TEntity>(TDomain domain)
        where TEntity : class
    {
        var entity = Mapper.Map<TEntity>(domain);
        return Add(entity);
    }

    public ValueTask<TDomain?> FindAsync<TDomain, TEntity>(Guid id)
        where TDomain : IIdObject
        where TEntity : class, IIdObject
    {
        return FindInternalAsync<TDomain, TEntity>(id, false);
    }

    public ValueTask<TDomain?> FindForEditAsync<TDomain, TEntity>(Guid id)
        where TDomain : IIdObject
        where TEntity : class, IIdObject
    {
        return FindInternalAsync<TDomain, TEntity>(id, true);
    }

    public async ValueTask<TDomain> FindRequiredAsync<TDomain, TEntity>(Guid id)
        where TDomain : IIdObject
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
        where TDomain : IIdObject
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
        where TDomain : IIdObject
        where TEntity : class, IIdObject
    {
        var entity = await Set<TEntity>()
            .AsNoTrackingWithIdentityResolution()
            .FirstOrDefaultAsync(e => e.Id == id);
        if(entity is null)
        {
            return default;
        }

        var domain = Mapper.Map<TDomain>(entity)!;

        if(isEditable)
        {
            Update<TEntity>(domain);
        }

        return domain;
    }

    public void Update<TEntity>(IIdObject domain)
    {
        modifiedDomainModels[domain.Id] = domain;
    }

    public void Add<TEntity>(object domain)
    {
        var entity = Mapper.Map<TEntity>(domain)!;
        Add(entity);
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new())
    {
        foreach(var (_, value) in modifiedDomainModels)
        {
            var destinationType = GetEntityType(value);
            var entity = Mapper.Map(value, destinationType);
            Update(entity);
        }

        return base.SaveChangesAsync(cancellationToken);
    }

    private Type GetEntityType(IIdObject value)
    {
        var destinationType = ((IGlobalConfiguration)Mapper.ConfigurationProvider).GetAllTypeMaps()
            .FirstOrDefault(tm => tm.SourceType == value.GetType())?.DestinationType;
        if(destinationType is null)
        {
            throw new Exception(); // TODO
        }

        return destinationType;
    }
}
