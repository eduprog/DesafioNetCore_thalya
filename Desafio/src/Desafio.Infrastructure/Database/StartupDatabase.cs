using Desafio.Identity;
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
            //options.UseNpgsql(config.GetConnectionString("PgsqlConnectionIdentity"));

            //Utilizar InMemory
            options.UseInMemoryDatabase(databaseName: "InMemory_Desafio_Identity");

            //Utilizar SQLite
            //options.UseSqlite("Data Source=Desafio_identity.db");
        });

        services.AddDbContext<AppDbContext>(options =>
        {
            //Utilizar Postgres
            //options.UseNpgsql(config.GetConnectionString("PgsqlConnection"));

            //Utilizar InMemory
            options.UseInMemoryDatabase(databaseName: "InMemory_Desafio");

            //Utilizar SQLite
            //options.UseSqlite("Data Source=Desafio.db");
        });

        return services;
    }
    
}
