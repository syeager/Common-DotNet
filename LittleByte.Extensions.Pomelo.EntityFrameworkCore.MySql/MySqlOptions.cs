using JetBrains.Annotations;

namespace LittleByte.Extensions.Pomelo.EntityFrameworkCore.MySql;

[UsedImplicitly]
internal class MySqlOptions
{
    public string ConnectionString { get; init; } = null!;
    public string Version { get; init; } = null!;

    public void Deconstruct(out string connectionString, out string version)
    {
        connectionString = ConnectionString;
        version = Version;
    }
}
