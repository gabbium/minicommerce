namespace MiniCommerce.Catalog.Infrastructure.Persistence;

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

    public Task TrySeedAsync()
    {
        return Task.CompletedTask;
    }
}
