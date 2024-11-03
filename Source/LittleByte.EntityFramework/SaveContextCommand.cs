using Microsoft.EntityFrameworkCore;

namespace LittleByte.EntityFramework;

public interface ISaveContextCommand
{
    Task CommitChangesAsync();
}

public sealed class SaveContextCommand<T>(T dbContext) : ISaveContextCommand
    where T : DbContext
{
    public Task CommitChangesAsync()
    {
        return dbContext.SaveChangesAsync();
    }
}