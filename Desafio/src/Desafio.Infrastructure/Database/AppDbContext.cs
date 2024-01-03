using Microsoft.EntityFrameworkCore;
using Desafio.Infrastructure.Mapping;
using Desafio.Domain;

namespace Desafio.Infrastructure;

public class AppDbContext : DbContext
{
    #region DbSet
    public DbSet<Unit> Units => Set<Unit>();
    public DbSet<Person> People => Set<Person>();
    public DbSet<Product> Products => Set<Product>();
    #endregion

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("public");
        modelBuilder.ApplyConfiguration(new PersonMapping());
        modelBuilder.ApplyConfiguration(new ProductMapping());
        modelBuilder.ApplyConfiguration(new UnitMapping());
    }
}
