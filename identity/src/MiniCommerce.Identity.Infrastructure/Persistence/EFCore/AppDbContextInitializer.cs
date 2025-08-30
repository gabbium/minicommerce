using MiniCommerce.Identity.Domain.Aggregates.Users.Entities;

namespace MiniCommerce.Identity.Infrastructure.Persistence.EFCore;

public class AppDbContextInitializer(AppDbContext context)
{
    public async Task InitializeAsync()
    {
        await context.Database.MigrateAsync();
    }

    public async Task SeedAsync()
    {
        await TrySeedAsync();
    }

    public async Task TrySeedAsync()
    {
        var administrator = new User("admin@minicommerce");

        if (await context.Users.AllAsync(u => u.Email != administrator.Email))
        {
            await context.Users.AddAsync(administrator);
            await context.SaveChangesAsync();
        }
    }
}
