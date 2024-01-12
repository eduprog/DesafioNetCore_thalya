using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.OpenApi.Models;

namespace Desafio.API;

internal static class SwaggerConfiguration
{
    internal static IServiceCollection AddSwagger(this IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "Desafio.API",
                Version = "v1"
            });

            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Description = @"JWT Authorization header using the Bearer scheme.",
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.Http,
                Scheme = "Bearer"
            });

            options.AddSecurityRequirement(new OpenApiSecurityRequirement()
            {
                {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    },
                    Scheme = "oauth2",
                    Name = "Bearer",
                    In = ParameterLocation.Header,

                    },
                    new List<string>()
                }
            });
        });

        return services;
    }

    internal static IApplicationBuilder UseSwaggerUI(this IApplicationBuilder app)
    {
        app.UseSwagger();
        app.UseSwaggerUI(options =>
        {
            var apiVersionProvider = app.ApplicationServices.GetService<IApiVersionDescriptionProvider>();
            if (apiVersionProvider == null)
                throw new ArgumentException("API Versioning not registered.");

            foreach (var description in apiVersionProvider.ApiVersionDescriptions)
            {
                options.SwaggerEndpoint(
                $"/swagger/{description.GroupName}/swagger.json",
                description.GroupName);
            }
        });
        return app; 
    }
}
