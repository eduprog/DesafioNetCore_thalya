using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Desafio.Application;

internal static class AddExceptionMiddlewareStartup
{
    internal static IServiceCollection AddExceptionMiddleware(this IServiceCollection services)
    {
        services.AddScoped<ExceptionMiddleware>();
        return services;
    }
    internal static IApplicationBuilder ExceptionMiddleware(this IApplicationBuilder app)
    {
        return app.UseMiddleware<ExceptionMiddleware>();
    }
}
