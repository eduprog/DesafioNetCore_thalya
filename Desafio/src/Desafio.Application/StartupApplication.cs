using Desafio.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Desafio.Application;

public static class StartupApplication
{
    public static IServiceCollection AddApplicationConfigurations(this IServiceCollection services, IConfiguration config)
    {
        services
            .AddExceptionMiddleware()
            .AddRepositories(config)
            .AddServices()
            .AddAutoMapper(Assembly.GetExecutingAssembly());
        return services;
    }
    public static IApplicationBuilder UseExceptionMiddleware(this IApplicationBuilder app)
    {
        return app.UseMiddleware<ExceptionMiddleware>();
    }
}
