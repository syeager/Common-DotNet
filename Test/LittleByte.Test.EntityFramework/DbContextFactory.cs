using System;
using Microsoft.EntityFrameworkCore;

namespace LittleByte.Test.EntityFramework;

public static class DbContextFactory
{
    public static void InMemory<T>(ref T? context, Func<DbContextOptions<T>, T>? create = null)
        where T : DbContext
    {
        if(context == null)
        {
            var options = new DbContextOptionsBuilder<T>()
                .UseInMemoryDatabase(typeof(T).Name)
                .Options;

            context = create is not null
                ? create(options)
                : (T)Activator.CreateInstance(typeof(T), options)!;
        }

        context.Database.EnsureDeleted();
        context.Database.EnsureCreated();
    }
}
