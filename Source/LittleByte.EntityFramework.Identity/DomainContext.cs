using LittleByte.Common;
using LittleByte.Data;
using LittleByte.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace LittleByte.EntityFramework.Identity;

public abstract class DomainContext<TContext,TAccount>(DbContextOptions<TContext> options)
    : IdentityDbContext<TAccount, IdentityRole<Guid>, Guid>(options), IDomainContext
    where TContext : DbContext
    where TAccount : IdentityUser<Guid>
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