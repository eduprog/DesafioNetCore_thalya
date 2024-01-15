using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Desafio.Identity
{
    public static class StartupIdentity
    {
        public static IServiceCollection AddIdentityConfigurations(this IServiceCollection services, IConfiguration config)
        {
            services
                .AddIdentitySetup(config);
            return services;
        }
    }
}
