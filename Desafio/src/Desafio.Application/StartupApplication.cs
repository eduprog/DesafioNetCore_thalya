using Desafio.Identity;
using Desafio.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desafio.Application;

public static class StartupApplication
{
    public static IServiceCollection AddApplicationConfigurations(this IServiceCollection services, IConfiguration config)
    {
        services
            .AddRepositories(config)
            .AddServices();
        return services;
    }
}
