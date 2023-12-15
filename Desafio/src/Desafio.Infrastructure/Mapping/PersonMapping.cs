using Desafio.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Desafio.Infrastructure.Mapping;

public class PersonMapping : IEntityTypeConfiguration<Person>
{
    public void Configure(EntityTypeBuilder<Person> person)
    {
        person.ToTable("person");

        person.Property(x => x.Id).HasColumnName("id").IsRequired();
        person.HasKey(x => x.Id);

        person.Property( x=> x.Name).HasColumnName("name").HasMaxLength(200).IsRequired();
        person.Property( x=> x.Document).HasColumnName("document").HasMaxLength(14).IsRequired();
        person.Property( x=> x.City).HasColumnName("city").HasMaxLength(100).IsRequired();
        person.Property( x=> x.Enable).HasColumnName("enable").IsRequired();
        person.Property( x=> x.CanSell).HasColumnName("can_sell").IsRequired();
        person.Property( x=> x.Notes).HasColumnName("notes").HasMaxLength(500).IsRequired();
        person.Property( x=> x.AlternativeCode).HasColumnName("alternative_code").HasMaxLength(15).IsRequired();

    }
}
