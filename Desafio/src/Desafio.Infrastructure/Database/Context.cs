using Microsoft.EntityFrameworkCore;
using Desafio.Domain;

namespace Desafio.Infrastructure;

public class Context : DbContext
{
    public DbSet<Person> People => Set<Person>();
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        string stringConnection = "Host=localhost;Port=5432;Database=microplan;Username=postgres;Password=12345";

        optionsBuilder.UseNpgsql(stringConnection);
    }
}
