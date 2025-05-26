using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistence.DatabaseContext;

namespace Persistence.Builders;

public static class DatabaseInjection
{
    public static void AddDatabaseInjection(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("Postgres");
        services.AddHealthChecks()
            .AddNpgSql(connectionString!);
        services.AddDbContext<AppDbContext>(options =>
            options.UseNpgsql(connectionString)
        );
    }
}