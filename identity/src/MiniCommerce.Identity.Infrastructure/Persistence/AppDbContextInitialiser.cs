using MiniCommerce.Identity.Domain.Entities;

namespace MiniCommerce.Identity.Infrastructure.Persistence;

public class AppDbContextInitialiser(AppDbContext context)
{
    public async Task InitialiseAsync()
    {
        await context.Database.MigrateAsync();
    }

    public async Task SeedAsync()
    {
        await TrySeedAsync();
    }

    public async Task TrySeedAsync()
    {
        var administrator = new User("admin@minicommerce.com");

        if (await context.Users.AllAsync(x => x.Email != administrator.Email))
        {
            await context.Users.AddAsync(administrator);
            await context.SaveChangesAsync();
        }
    }
}
