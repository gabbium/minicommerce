using MiniCommerce.Catalog.Domain.Aggregates.Products.Entities;

namespace MiniCommerce.Catalog.Infrastructure.Persistence.EFCore.Configurations;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Sku)
            .IsRequired()
            .HasMaxLength(128);

        builder.HasIndex(p => p.Sku)
            .IsUnique();

        builder.Property(p => p.Name)
            .IsRequired()
            .HasMaxLength(256);

        builder.Property(p => p.Price)
            .IsRequired()
            .HasPrecision(10, 2);
    }
}
