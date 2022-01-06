using LittleByte.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LittleByte.Extensions.Pomelo.EntityFrameworkCore.MySql;

public static class PersistenceConfiguration
{
    public static IServiceCollection AddMySql<TContext>(this IServiceCollection @this, IConfiguration configuration)
        where TContext : DbContext
    {
        var (connectionString, version, detailedLogs) = configuration.GetSection<MySqlOptions>();
        var serverVersion = ServerVersion.Parse(version);

        return @this.AddDbContext<TContext>(builder =>
        {
            builder.UseMySql(connectionString, serverVersion);
            if(detailedLogs)
            {
                builder
                    .EnableDetailedErrors()
                    .EnableSensitiveDataLogging();
            }
        });
    }
}
