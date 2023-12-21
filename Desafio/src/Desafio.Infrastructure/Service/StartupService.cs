using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Desafio.Infrastructure;

internal static class StartupService
{
    internal static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IPersonService, PersonService>();
        services.AddScoped<IProductService, ProductService>();
        services.AddScoped<IUnitService, UnitService>();
        services.AddScoped<IUserService, UserService>();

        return services;
    }
}
