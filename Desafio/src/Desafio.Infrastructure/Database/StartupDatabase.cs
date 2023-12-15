using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Desafio.Infrastructure;

internal static class StartupDatabase
{
    internal static IServiceCollection AddDatabaseInformation(this IServiceCollection services, IConfiguration config)
    {
        services.AddDbContext<UserContext>(options =>
        {
            //Utilizar Banco InMemory
            //options.UseInMemoryDatabase(databaseName: "Desafio_Identidade");
            //services.AddInicialInformation(config);

            //Utilizar SqLite

            //Utilizar Postgres

            var strConnectionUser = config.GetConnectionString("PgsqlConnectionIdentity");
            options.UseNpgsql(strConnectionUser);
        });

        services.AddDbContext<Context>(options =>
        {
            //Utilizar Banco InMemory
            //options.UseInMemoryDatabase(databaseName: "Desafio_Identidade");
            //services.AddInicialInformation(config);

            //Utilizar SqLite

            //Utilizar Postgres

            var strConnectionUser = config.GetConnectionString("PgsqlConnection");
            options.UseNpgsql(strConnectionUser);
        });

        //services.AddScoped<IPessoaRepository, PessoaRepository>();
        //services.AddScoped<IEmpresaRepository, EmpresaRepository>();
        return services;
    }
    
}
