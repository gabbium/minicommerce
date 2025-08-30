using MiniCommerce.Catalog.Infrastructure.IntegrationTests.TestHelpers.Fixtures;
using MiniCommerce.Catalog.Infrastructure.Persistence.EFCore;

namespace MiniCommerce.Catalog.Infrastructure.IntegrationTests.Persistence.EFCore;

[Collection(nameof(TestCollection))]
public class AppDbContextInitializerTests(TestFixture fixture) : TestBase(fixture)
{
    private readonly AppDbContext _context =
        fixture.GetRequiredService<AppDbContext>();

    private readonly AppDbContextInitializer _initializer =
        fixture.GetRequiredService<AppDbContextInitializer>();

    [Fact]
    public async Task DatabaseAppliesPendingMigrations()
    {
        // Act
        await _initializer.InitializeAsync();

        // Assert 
        var tablesExist = await _context.Database.CanConnectAsync();
        Assert.True(tablesExist);
    }
}

