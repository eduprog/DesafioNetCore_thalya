using Desafio.Domain;
using Desafio.Identity;
using Desafio.Infrastructure;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Desafio.API;

public static class DbMigrationHelperExtension
{
    public static void UseDbMigrationHelper(this WebApplication app)
    {
        DbMigrationHelpers.EnsureSeedData(app).Wait();
    }
}
public static class DbMigrationHelpers
{
    public static async Task EnsureSeedData(WebApplication serviceScope)
    {
        var services = serviceScope.Services.CreateScope().ServiceProvider;
        await EnsureSeedData(services);
    }

    public static async Task EnsureSeedData(IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope();
        var env = scope.ServiceProvider.GetRequiredService<IWebHostEnvironment>();

        var identityContext = scope.ServiceProvider.GetRequiredService<IdentityContext>();
        var appDbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();

        await identityContext.Database.MigrateAsync();
        await appDbContext.Database.MigrateAsync();

        //Usar caso for necessário criar dados iniciais
        await EnsureSeedProducts(identityContext, appDbContext);

    }

    public static async Task EnsureSeedProducts(IdentityContext identityContext, AppDbContext appDbContext)
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
