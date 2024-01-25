using LittleByte.Common;
using LittleByte.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace LittleByte.EntityFramework;

public sealed class IdValueConverter<T>() : ValueConverter<Id<T>, Guid>(id => id.Value, guid => new Id<T>(guid));

public static class IdValueEntity
{
    public static void IdEntity<T>(this ModelBuilder builder)
    {
        builder.Entity<DomainModel<T>>().HasKey(dm => dm.Id);
        builder.Entity<DomainModel<T>>().Property(dm => dm.Id).HasConversion<IdValueConverter<T>>();
    }
}