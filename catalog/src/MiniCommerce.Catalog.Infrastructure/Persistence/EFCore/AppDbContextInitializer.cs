namespace MiniCommerce.Catalog.Infrastructure.Persistence.EFCore;

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

    public Task TrySeedAsync()
    {
        return Task.CompletedTask;
    }
}
