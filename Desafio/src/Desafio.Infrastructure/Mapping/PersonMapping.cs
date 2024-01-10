using Desafio.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Desafio.Infrastructure;

public class PersonMapping : IEntityTypeConfiguration<Person>
{
    public void Configure(EntityTypeBuilder<Person> person)
    {
        person.ToTable("person");

        person.Property(x => x.Id).HasColumnName("id").IsRequired();
        person.HasKey(x => x.Id);

        person.Property(x => x.Name).HasColumnName("name").IsRequired();
        person.Property(x => x.Document).HasColumnName("document").IsRequired();
        person.Property(x => x.City).HasColumnName("city").IsRequired();
        person.Property(x => x.Enable).HasColumnName("enable").IsRequired();
        person.Property(x => x.CanBuy).HasColumnName("can_buy").IsRequired();
        person.Property(x => x.Notes).HasColumnName("notes").IsRequired();
        person.Property(x => x.AlternativeCode).HasColumnName("alternative_code").IsRequired();
        person.Property(x => x.ShortId).HasColumnName("short_id").IsRequired();
    }
}
