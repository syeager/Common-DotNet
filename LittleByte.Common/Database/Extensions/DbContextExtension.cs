using LittleByte.Common.Objects;
using Microsoft.EntityFrameworkCore;

namespace LittleByte.Common.Database.Extensions
{
    public static class DbContextExtension
    {
        public static async Task AddAndSaveAsync<T>(this DbContext dbContext, T entity) where T : class
        {
            dbContext.Add(entity);
            await dbContext.SaveChangesAsync();
        }

        public static async Task RemoveAndSaveAsync<T>(this DbContext dbContext, T entity) where T : class
        {
            dbContext.Remove(entity);
            await dbContext.SaveChangesAsync();
        }

        public static async Task UpdateAndSaveAsync<T>(this DbContext dbContext, T entity) where T : class
        {
            dbContext.Update(entity);
            await dbContext.SaveChangesAsync();
        }

        public static T? FindTracked<T>(this DbContext dbContext, Guid id) where T : class, IIdObject
        {
            var dao = dbContext.ChangeTracker.Entries<T>().FirstOrDefault(e => e.Entity.Id == id)?.Entity;
            return dao;
        }
    }
}