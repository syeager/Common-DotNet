using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace LittleByte.Test.EntityFramework;

public static class DbContextExtension
{
    public static void AddAndSave<T>(this DbContext @this, T entity)
        where T : class
    {
        @this.Add(entity);
        @this.SaveChanges();
    }

    public static void AddRangeAndSave<T>(this DbContext @this, IEnumerable<T> entities)
        where T : class
    {
        @this.AddRange(entities);
        @this.SaveChanges();
    }
}