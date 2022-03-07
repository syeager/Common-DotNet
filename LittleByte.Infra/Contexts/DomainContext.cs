using AutoMapper;
using AutoMapper.Internal;
using LittleByte.Core.Exceptions;
using LittleByte.Core.Objects;
using LittleByte.Validation;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace LittleByte.Infra.Contexts;

public interface IDomainContext
{
    ValueTask<Valid<TDomain>?> FindAsync<TDomain, TEntity>(Guid id)
        where TDomain : IIdObject
        where TEntity : class, IIdObject;

    ValueTask<Valid<TDomain>?> FindForEditAsync<TDomain, TEntity>(Guid id)
        where TDomain : IIdObject
        where TEntity : class, IIdObject;

    ValueTask<Valid<TDomain>> FindRequiredAsync<TDomain, TEntity>(Guid id)
        where TDomain : IIdObject
        where TEntity : class, IIdObject;

    ValueTask<Valid<TDomain>> FindRequiredForEditAsync<TDomain, TEntity>(Guid id)
        where TDomain : IIdObject
        where TEntity : class, IIdObject;
}

public abstract class DomainContext<TContext, TUser, TRole> : IdentityDbContext<TUser, TRole, Guid>, IDomainContext
    where TContext : DbContext
    where TUser : IdentityUser<Guid>
    where TRole : IdentityRole<Guid>
{
    private readonly Dictionary<Guid, object> trackedDomainModels = new();

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

    public ValueTask<Valid<TDomain>?> FindAsync<TDomain, TEntity>(Guid id)
        where TDomain : IIdObject
        where TEntity : class, IIdObject
    {
        return FindInternalAsync<TDomain, TEntity>(id, false);
    }

    public ValueTask<Valid<TDomain>?> FindForEditAsync<TDomain, TEntity>(Guid id)
        where TDomain : IIdObject
        where TEntity : class, IIdObject
    {
        return FindInternalAsync<TDomain, TEntity>(id, true);
    }

    public async ValueTask<Valid<TDomain>> FindRequiredAsync<TDomain, TEntity>(Guid id)
        where TDomain : IIdObject
        where TEntity : class, IIdObject
    {
        var domain = await FindAsync<TDomain, TEntity>(id);
        if(domain is null)
        {
            throw new NotFoundException(typeof(TDomain), id);
        }

        return domain.Value;
    }

    public async ValueTask<Valid<TDomain>> FindRequiredForEditAsync<TDomain, TEntity>(Guid id)
        where TDomain : IIdObject
        where TEntity : class, IIdObject
    {
        var domain = await FindForEditAsync<TDomain, TEntity>(id);
        if(domain is null)
        {
            throw new NotFoundException(typeof(TDomain), id);
        }

        return domain.Value;
    }

    private async ValueTask<Valid<TDomain>?> FindInternalAsync<TDomain, TEntity>(Guid id, bool isEditable)
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

        var domain = Mapper.Map<Valid<TDomain>>(entity);

        if(isEditable)
        {
            if(domain.IsSuccess)
            {
                Track(domain.GetModelOrThrow().Id, domain);
            }
            else
            {
                // TODO: Log.
            }
        }

        return domain;
    }

    public void Track(Guid id, object domain) => trackedDomainModels[id] = domain;

    public void Track(IIdObject domain) => Track(domain.Id, domain);

    public void Add<TEntity>(object domain)
    {
        var entity = Mapper.Map<TEntity>(domain)!;
        Add(entity);
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new())
    {
        foreach(var (_, value) in trackedDomainModels)
        {
            var destinationType = GetEntityType(value);
            var entity = Mapper.Map(value, value.GetType(), destinationType);
            Update(entity);
        }

        return base.SaveChangesAsync(cancellationToken);
    }

    private Type GetEntityType(object value)
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
