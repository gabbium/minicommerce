using MiniCommerce.Identity.Domain.Entities;
using MiniCommerce.Identity.Domain.ValueObjects;

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
        var administrator = new User("admin@minicommerce", Role.Administrator);

        if (await context.Users.AllAsync(x => x.Email != administrator.Email))
        {
            await context.Users.AddAsync(administrator);
            await context.SaveChangesAsync();
        }
    }
}
