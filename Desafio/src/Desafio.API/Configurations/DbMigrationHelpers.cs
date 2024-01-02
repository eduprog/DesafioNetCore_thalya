using Desafio.Identity.Database;
using Desafio.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Desafio.API
{
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
            //await EnsureSeedProducts(userContext);

        }

        //public static async Task EnsureSeedProducts(UserContext userContext)
        //{
        //    if (userContext.Users.Any()) return;

        //    await userContext.Users.AddAsync(new User()
        //    {
        //        Name = "Defaut User",
        //        Password = "1", //LEMBRAR DE SALVAR EM HASH
        //        UserLevel = EUserLevel.Administrator
        //    });

        //    await userContext.SaveChangesAsync();
        //}
    }
}
