using Desafio.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Desafio.Infrastructure.Mapping;

public class ProductMapping : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> product)
    {
        product.ToTable("product");

        product.Property(x => x.Id).HasColumnName("id").IsRequired();
        product.HasKey(x => x.Id);

        product.Property(x => x.Description).HasColumnName("description").IsRequired();
        product.Property(x => x.ShortDescription).HasColumnName("short_description").IsRequired();
        product.Property(x => x.Acronym).HasColumnName("acronym").IsRequired();
        product.Property(x => x.Price).HasColumnName("price").HasPrecision(15, 4).IsRequired();
        product.Property(x => x.StoredQuantity).HasColumnName("stored_quantity").HasPrecision(15, 4).IsRequired();
        product.Property(x => x.Enable).HasColumnName("enable").IsRequired();
        product.Property(x => x.Sellable).HasColumnName("sellable").IsRequired();
        product.Property(x => x.BarCode).HasColumnName("bar_code").IsRequired();
        product.Property(x => x.ShortId).HasColumnName("short_id").IsRequired();

        #region Relations
        product.HasOne(x => x.Unit)
            .WithMany(x => x.Products)
            .HasForeignKey(x => x.Acronym)
            .IsRequired();
        #endregion

    }
}
