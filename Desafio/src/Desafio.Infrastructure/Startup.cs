using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Desafio.Identity;

namespace Desafio.Infrastructure;

public static class Startup
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration config)
    {
        services
            .AddDatabaseInformation(config)
            .AddServices(config)
            .AddIdentityConfiguration(config);
        return services;
    }
}
