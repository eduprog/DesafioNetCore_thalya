using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity;
using Desafio.Domain;

namespace Desafio.Infrastructure;

internal static class StartupDatabase
{
    internal static IServiceCollection AddDatabaseInformation(this IServiceCollection services, IConfiguration config)
    {
        services.AddDbContext<UserContext>(options =>
        {
            //Utilizar Postgres
            options.UseNpgsql(config.GetConnectionString("PgsqlConnectionIdentity"));
        });

        services.AddDbContext<AppDbContext>(options =>
        {
            //Utilizar Postgres
            options.UseNpgsql(config.GetConnectionString("PgsqlConnection"));
        });


        services.AddIdentity<User, IdentityRole>()
            .AddEntityFrameworkStores<UserContext>()
            .AddDefaultTokenProviders();

        services.Configure<IdentityOptions>(options =>
        {
            options.User.RequireUniqueEmail = true;
        });


        services.AddScoped<IPersonRepository, PersonRepository>();
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IUnitRepository, UnitRepository>();

        return services;
    }
    
}
