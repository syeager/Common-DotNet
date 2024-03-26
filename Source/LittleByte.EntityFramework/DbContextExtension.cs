using LittleByte.Common;
using LittleByte.Data;
using LittleByte.Domain;
using Microsoft.EntityFrameworkCore;

namespace LittleByte.EntityFramework;

public static class DbContextExtension
{
    public static async Task AddAndSaveAsync<T>(this DbContext dbContext, T entity)
        where T : class
    {
        dbContext.Add(entity);
        await dbContext.SaveChangesAsync();
    }

    public static async Task RemoveAndSaveAsync<T>(this DbContext dbContext, T entity)
        where T : class
    {
        dbContext.Remove(entity);
        await dbContext.SaveChangesAsync();
    }

    public static async Task UpdateAndSaveAsync<T>(this DbContext dbContext, T entity)
        where T : class
    {
        dbContext.Update(entity);
        await dbContext.SaveChangesAsync();
    }

    public static T? FindTracked<T>(this DbContext dbContext, Guid id)
        where T : class, IIdObject
    {
        var dao = dbContext.ChangeTracker.Entries<T>().FirstOrDefault(e => e.Entity.Id == id)?.Entity;
        return dao;
    }
}

public abstract class Database<T>(DbContextOptions<T> options) : DbContext(options)
    where T : DbContext
{
    public ValueTask<TDomain?> FindAsync<TDomain>(Id<TDomain> id)
        where TDomain : DomainModel<TDomain>
    {
        return FindInternalAsync(id, false);
    }

    public ValueTask<TDomain?> FindForEditAsync<TDomain>(Id<TDomain> id)
        where TDomain : DomainModel<TDomain>
    {
        return FindInternalAsync(id, true);
    }

    public async ValueTask<TDomain> FindRequiredAsync<TDomain>(Id<TDomain> id)
        where TDomain : DomainModel<TDomain>
    {
        var domain = await FindAsync(id);
        return domain ?? throw new MissingEntityException(id, typeof(TDomain));
    }

    public async ValueTask<TDomain> FindRequiredForEditAsync<TDomain>(Id<TDomain> id)
        where TDomain : DomainModel<TDomain>
    {
        var domain = await FindForEditAsync(id);
        return domain ?? throw new MissingEntityException(id, typeof(TDomain));
    }

    private async ValueTask<TDomain?> FindInternalAsync<TDomain>(Id<TDomain> id, bool isEditable)
        where TDomain : DomainModel<TDomain>
    {
        var query = isEditable
            ? Set<TDomain>().AsTracking()
            : Set<TDomain>();

        var entity = await query.FirstOrDefaultAsync(e => e.Id == id);
        return entity ?? default;
    }
}