using Microsoft.EntityFrameworkCore;

namespace LittleByte.Infra.Commands;

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

    public virtual Task CommitChangesAsync() => dbContext.SaveChangesAsync();
}