using Desafio.Application;
using Desafio.Domain;
using Desafio.Identity;
using Desafio.Identity.Database;
using Microsoft.AspNetCore.Identity;
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
        //services.AddScoped<IUserService, UserService>();
        services.AddScoped<IIdentityService, IdentityService>();
        

        return services;
    }
}
