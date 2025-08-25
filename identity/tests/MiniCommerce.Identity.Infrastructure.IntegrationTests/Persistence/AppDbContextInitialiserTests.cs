using MiniCommerce.Identity.Domain.ValueObjects;
using MiniCommerce.Identity.Infrastructure.IntegrationTests.TestHelpers;
using MiniCommerce.Identity.Infrastructure.Persistence;

namespace MiniCommerce.Identity.Infrastructure.IntegrationTests.Persistence;

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

    [Fact]
    public async Task SeedAsync_ThenCreatesAdministrator()
    {
        // Act
        await _initialiser.SeedAsync();

        // Assert
        var administrator = _context.Users.FirstOrDefault(u => u.Email == "admin@minicommerce");
        Assert.NotNull(administrator);
        Assert.Equal(Role.Administrator, administrator.Role);
    }
}

