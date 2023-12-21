using Microsoft.EntityFrameworkCore;
using Desafio.Domain;
using Desafio.Infrastructure.Mapping;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Desafio.Infrastructure;

public class UserContext : IdentityDbContext
{
    #region DbSet
    public DbSet<User> Users => Set<User>();
    #endregion

    public UserContext(DbContextOptions<UserContext> options) : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.HasDefaultSchema("public");
        modelBuilder.ApplyConfiguration(new UserMapping());
    }
}
