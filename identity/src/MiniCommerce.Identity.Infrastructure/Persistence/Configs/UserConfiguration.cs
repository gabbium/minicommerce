using MiniCommerce.Identity.Domain.Entities;
using MiniCommerce.Identity.Domain.ValueObjects;

namespace MiniCommerce.Identity.Infrastructure.Persistence.Configs;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(u => u.Id);

        builder.Property(u => u.Email)
            .HasMaxLength(256)
            .IsRequired();

        builder.HasIndex(u => u.Email)
            .IsUnique();

        builder.Property(u => u.Role)
            .HasConversion(
                r => r.Value,
                v => Role.From(v)
            )
            .HasMaxLength(32)
            .IsRequired();
    }
}
