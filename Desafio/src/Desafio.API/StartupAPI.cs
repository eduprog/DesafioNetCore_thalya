namespace Desafio.API;

public static class StartupAPI
{
    public static IServiceCollection AddApiConfigurations(this IServiceCollection services)
    {
        services
            .AddVersioning()
            .AddSwagger();

        return services;    
    }
    public static IApplicationBuilder AddBuilderConfiguration(this IApplicationBuilder app)
    {
        app
            .UseSwaggerUI()
            .UseDbMigrationHelper();
        return app;
    }
}
