using Microsoft.EntityFrameworkCore;

namespace LittleByte.EntityFramework;

public interface ISaveContextCommand
{
    Task CommitChangesAsync();
}

public class SaveContextCommand<T> : ISaveContextCommand
    where T : DbContext
{
    private readonly T dbContext;

    public SaveContextCommand(T dbContext)
    {
        this.dbContext = dbContext;
    }

    public virtual Task CommitChangesAsync()
    {
        return dbContext.SaveChangesAsync();
    }
}