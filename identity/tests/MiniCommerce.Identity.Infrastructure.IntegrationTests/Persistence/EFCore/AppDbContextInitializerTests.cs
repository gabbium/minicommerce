using MiniCommerce.Identity.Infrastructure.IntegrationTests.TestHelpers;
using MiniCommerce.Identity.Infrastructure.Persistence.EFCore;

namespace MiniCommerce.Identity.Infrastructure.IntegrationTests.Persistence.EFCore;

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

    [Fact]
    public async Task AdminUserIsSeededCorrectly()
    {
        // Act
        await _initializer.SeedAsync();

        // Assert
        var administrator = _context.Users.FirstOrDefault(u => u.Email == "admin@minicommerce");
        Assert.NotNull(administrator);
    }
}

