using Desafio.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Desafio.Infrastructure.Mapping;

public class UnitMapping : IEntityTypeConfiguration<Unit>
{
    public void Configure(EntityTypeBuilder<Unit> unit)
    {
        unit.ToTable("person");

        unit.Property(x => x.Id).HasColumnName("id").IsRequired();
        unit.HasKey(x => x.Id);

        unit.Property( x=> x.Acronym).HasColumnName("description").HasMaxLength(200).IsRequired();
        unit.Property( x=> x.Description).HasColumnName("short_description").HasMaxLength(100).IsRequired();
        

        #region Relations
        unit.HasMany(x => x.Products)
            .WithOne(x => x.Unit)
            .HasForeignKey(x => x.Acronym)
            .IsRequired();
        #endregion

    }
}
