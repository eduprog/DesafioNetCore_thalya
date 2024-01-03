using Desafio.Identity.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Desafio.Infrastructure;

internal static class StartupDatabase
{
    internal static IServiceCollection AddDatabaseInformation(this IServiceCollection services, IConfiguration config)
    {
        services.AddDbContext<IdentityContext>(options =>
        {
            //Utilizar Postgres
            options.UseNpgsql(config.GetConnectionString("PgsqlConnectionIdentity"));
        });

        services.AddDbContext<AppDbContext>(options =>
        {
            //Utilizar Postgres
            options.UseNpgsql(config.GetConnectionString("PgsqlConnection"));
        });

        return services;
    }
    
}
