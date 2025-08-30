using MiniCommerce.Identity.Infrastructure.Persistence.EFCore;
using MiniCommerce.Identity.Web.AcceptanceTests.TestHelpers.Auth;
using MiniCommerce.Identity.Web.AcceptanceTests.TestHelpers.Containers;

namespace MiniCommerce.Identity.Web.AcceptanceTests.TestHelpers.Fixtures;

public class TestFixture : IAsyncLifetime
{
    private PostgresContainer _database = null!;
    private CustomWebApplicationFactory _app = null!;

    public HttpClient Client { get; private set; } = null!;

    public async Task InitializeAsync()
    {
        _database = new PostgresContainer();
        await _database.InitializeAsync();

        _app = new CustomWebApplicationFactory(_database.GetConnection());

        Client = _app.CreateClient();
    }

    public async Task ResetStateAsync()
    {
        await _database.ResetAsync();

        using var scope = _app.Services.CreateScope();
        var initializer = scope.ServiceProvider.GetRequiredService<AppDbContextInitializer>();
        await initializer.SeedAsync();
    }

    public string CreateAccessToken(params string[] permissions)
    {
        using var scope = _app.Services.CreateScope();
        var jwtTokenFactory = scope.ServiceProvider.GetRequiredService<JwtTokenFactory>();
        return jwtTokenFactory.CreateAccessToken("user@minicommerce", permissions);
    }

    public async Task DisposeAsync()
    {
        await _database.DisposeAsync();
        await _app.DisposeAsync();
    }
}
