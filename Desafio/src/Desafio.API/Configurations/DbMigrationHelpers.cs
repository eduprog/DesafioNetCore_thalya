using Desafio.Domain;
using Desafio.Identity;
using Desafio.Infrastructure;
using Microsoft.AspNetCore.Identity;

namespace Desafio.API;

internal static class DbMigrationHelperExtension
{
    internal static IApplicationBuilder UseDbMigrationHelper(this IApplicationBuilder app)
    {
        DbMigrationHelpers.EnsureSeedData(app).Wait();
        return app;
    }
}
internal static class DbMigrationHelpers
{
    internal static async Task EnsureSeedData(IApplicationBuilder serviceScope)
    {
        var services = serviceScope.ApplicationServices.CreateScope().ServiceProvider;
        await EnsureSeedData(services);
    }

    internal static async Task EnsureSeedData(IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope();
        var env = scope.ServiceProvider.GetRequiredService<IWebHostEnvironment>();

        var identityContext = scope.ServiceProvider.GetRequiredService<IdentityContext>();
        var appDbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();

        //Utilizar com Postgres e SQLite
        //await identityContext.Database.MigrateAsync();
        //await appDbContext.Database.MigrateAsync();

        //Utilizar com InMemory
        identityContext.Database.EnsureCreated();
        appDbContext.Database.EnsureCreated();

        //Usar caso for necessário criar dados iniciais
        await EnsureSeedUserLevel(identityContext, appDbContext);
    }

    internal static async Task EnsureSeedUserLevel(IdentityContext identityContext, AppDbContext appDbContext)
    {
        EUserLevel[] roles = (EUserLevel[])Enum.GetValues(typeof(EUserLevel));
        foreach(var role in roles)
        {
            if(!identityContext.Roles.Any(x => x.Name == role.ToString().ToUpper()))
            {
                IdentityRole identityRole = new IdentityRole
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = role.ToString().ToUpper(),
                    NormalizedName = role.ToString().ToUpper()
                };
                await identityContext.Roles.AddAsync(identityRole);
            }

        }

        await identityContext.SaveChangesAsync();
    }
}
