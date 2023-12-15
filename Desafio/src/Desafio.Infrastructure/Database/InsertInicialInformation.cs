using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Desafio.Infrastructure;

internal static class InsertInicialInformation
{

    internal static IServiceCollection AddInicialInformation(this IServiceCollection services, IConfiguration config)
    {
        InsertUser();
        InsertPerson();
        InsertUnit();
        InsertProduct();
        return services;
    }
    private static void InsertPerson()
    {

    }
    private static void InsertUser()
    {

    }
    private static void InsertUnit()
    {

    }
    private static void InsertProduct()
    {

    }
}
