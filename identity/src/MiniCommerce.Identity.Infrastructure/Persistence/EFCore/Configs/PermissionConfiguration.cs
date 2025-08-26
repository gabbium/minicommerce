using MiniCommerce.Identity.Domain.Aggregates.Permissions;

namespace MiniCommerce.Identity.Infrastructure.Persistence.EFCore.Configs;

public class PermissionConfiguration : IEntityTypeConfiguration<Permission>
{
    public void Configure(EntityTypeBuilder<Permission> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Code)
            .HasMaxLength(128)
            .IsRequired();

        builder.HasIndex(p => p.Code)
            .IsUnique();

        builder.Property(p => p.Deprecated)
            .IsRequired();
    }
}
