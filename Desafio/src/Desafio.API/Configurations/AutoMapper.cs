using AutoMapper;
using Desafio.Application;
using Desafio.Domain;
using Desafio.Infrastructure;

namespace Desafio.API;

public static class AutoMapper
{
    public static IServiceCollection AddAutoMapperConfiguration(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(Startup));
            
        return services;
    }
}

