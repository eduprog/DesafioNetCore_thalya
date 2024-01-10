using Desafio.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Desafio.Infrastructure.Mapping;

public class UnitMapping : IEntityTypeConfiguration<Unit>
{
    public void Configure(EntityTypeBuilder<Unit> unit)
    {
        unit.ToTable("unit");

        unit.Property(x => x.Id).HasColumnName("id").IsRequired();

        unit.Property(x => x.Acronym).HasColumnName("acronym").IsRequired();
        unit.HasKey(x => x.Acronym);
        unit.Property(x => x.Description).HasColumnName("description").IsRequired();
        unit.Property(x => x.ShortId).HasColumnName("short_id").IsRequired();


        #region Relations
        unit.HasMany(x => x.Products)
            .WithOne(x => x.Unit)
            .HasForeignKey(x => x.Acronym)
            .IsRequired();
        #endregion

    }
}
