using MiniCommerce.Identity.Domain.Entities;

namespace MiniCommerce.Identity.Infrastructure.Persistence;

public class AppDbContextInitialiser(ILogger<AppDbContextInitialiser> logger, AppDbContext context)
{
    public async Task InitialiseAsync()
    {
        try
        {
            await context.Database.MigrateAsync();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred while initialising the database");
            throw;
        }
    }

    public async Task SeedAsync()
    {
        try
        {
            await TrySeedAsync();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred while seeding the database");
            throw;
        }
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
