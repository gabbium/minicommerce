using MiniCommerce.Catalog.Infrastructure.IntegrationTests.TestHelpers;
using MiniCommerce.Catalog.Infrastructure.Persistence.EFCore;

namespace MiniCommerce.Catalog.Infrastructure.IntegrationTests.Persistence.EFCore;

[Collection(nameof(TestCollection))]
public class AppDbContextInitialiserTests(TestFixture fixture) : TestBase(fixture)
{
    private readonly AppDbContext _context =
        fixture.GetRequiredService<AppDbContext>();

    private readonly AppDbContextInitialiser _initialiser =
        fixture.GetRequiredService<AppDbContextInitialiser>();

    [Fact]
    public async Task DatabaseAppliesPendingMigrations()
    {
        // Act
        await _initialiser.InitialiseAsync();

        // Assert 
        var tablesExist = await _context.Database.CanConnectAsync();
        Assert.True(tablesExist);
    }
}

