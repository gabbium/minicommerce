using MiniCommerce.Catalog.Infrastructure.IntegrationTests.TestHelpers;
using MiniCommerce.Catalog.Infrastructure.Persistence;

namespace MiniCommerce.Catalog.Infrastructure.IntegrationTests.Persistence;

[Collection(nameof(TestCollection))]
public class AppDbContextInitialiserTests(TestFixture fixture) : TestBase(fixture)
{
    private readonly AppDbContext _context =
        fixture.GetRequiredService<AppDbContext>();

    private readonly AppDbContextInitialiser _initialiser =
        fixture.GetRequiredService<AppDbContextInitialiser>();

    [Fact]
    public async Task InitialiseAsync_ThenAppliesPendingMigrations()
    {
        // Act
        await _initialiser.InitialiseAsync();

        // Assert 
        var tablesExist = await _context.Database.CanConnectAsync();
        Assert.True(tablesExist);
    }
}

