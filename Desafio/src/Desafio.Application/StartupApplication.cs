using Desafio.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Desafio.Application;

public static class StartupApplication
{
    public static IServiceCollection AddApplicationConfigurations(this IServiceCollection services, IConfiguration config)
    {
        services
            .AddRepositories(config)
            .AddServices()
            .AddAutoMapper(Assembly.GetExecutingAssembly());
        return services;
    }
}
