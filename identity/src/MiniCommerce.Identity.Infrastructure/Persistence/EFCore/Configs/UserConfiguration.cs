using MiniCommerce.Identity.Domain.Aggregates.Users;

namespace MiniCommerce.Identity.Infrastructure.Persistence.EFCore.Configs;

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
    }
}
