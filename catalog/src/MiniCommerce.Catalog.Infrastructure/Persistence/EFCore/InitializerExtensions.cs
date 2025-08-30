namespace MiniCommerce.Catalog.Infrastructure.Persistence.EFCore;

public static class InitializerExtensions
{
    public static void AddAsyncSeeding(this DbContextOptionsBuilder builder, IServiceProvider serviceProvider)
    {
        builder.UseAsyncSeeding(async (_, _, _) =>
        {
            var initializer = serviceProvider.GetRequiredService<AppDbContextInitializer>();
            await initializer.SeedAsync();
        });
    }

    public static async Task InitialiseDatabaseAsync(this IHost host)
    {
        using var scope = host.Services.CreateScope();
        var initializer = scope.ServiceProvider.GetRequiredService<AppDbContextInitializer>();
        await initializer.InitializeAsync();
    }
}
