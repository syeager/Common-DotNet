using JetBrains.Annotations;

namespace LittleByte.Extensions.Pomelo.EntityFrameworkCore.MySql;

[UsedImplicitly]
internal class MySqlOptions
{
    public string ConnectionString { get; init; } = null!;
    public string Version { get; init; } = null!;
    public bool DetailedLogs { get; init; }

    public void Deconstruct(out string connectionString, out string version, out bool detailedLogs)
    {
        connectionString = ConnectionString;
        version = Version;
        detailedLogs = DetailedLogs;
    }
}