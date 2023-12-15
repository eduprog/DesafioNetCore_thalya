using Desafio.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Desafio.Infrastructure.Mapping;

public class ProductMapping : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> product)
    {
        product.ToTable("person");

        product.Property(x => x.Id).HasColumnName("id").IsRequired();
        product.HasKey(x => x.Id);

        product.Property( x=> x.Description).HasColumnName("description").HasMaxLength(200).IsRequired();
        product.Property( x=> x.ShortDescription).HasColumnName("short_description").HasMaxLength(100).IsRequired();
        product.Property( x=> x.Acronym).HasColumnName("acronym").HasMaxLength(4).IsRequired();
        product.Property( x=> x.Price).HasColumnName("price").HasPrecision(15,4).IsRequired();
        product.Property( x=> x.StoredQuantity).HasColumnName("stored_quantity").HasPrecision(15, 4).IsRequired();
        product.Property( x=> x.Enable).HasColumnName("enable").IsRequired();
        product.Property( x=> x.Salable).HasColumnName("salable").IsRequired();
        product.Property( x=> x.BarCode).HasColumnName("bar_code").HasMaxLength(15).IsRequired();

        #region Relations
        product.HasOne(x => x.Unit)
            .WithMany()
            .HasForeignKey(x => x.Acronym)
            .IsRequired();
        #endregion

    }
}
