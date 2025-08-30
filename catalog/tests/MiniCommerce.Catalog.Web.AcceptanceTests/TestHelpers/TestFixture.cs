using MiniCommerce.Catalog.Infrastructure.Persistence.EFCore;
using MiniCommerce.Catalog.Web.AcceptanceTests.TestHelpers.Data;
using MiniCommerce.Catalog.Web.Endpoints;
using MiniCommerce.Catalog.Web.Extensions.Auth;

namespace MiniCommerce.Catalog.Web.AcceptanceTests.TestHelpers;

public class TestFixture : IAsyncLifetime
{
    private ITestDatabase _database = null!;
    private CustomWebApplicationFactory _webAppFactory = null!;
    private IServiceScopeFactory _scopeFactory = null!;

    public HttpClient Client { get; private set; } = null!;

    public async Task InitializeAsync()
    {
        _database = await TestDatabaseFactory.CreateAsync();
        _webAppFactory = new CustomWebApplicationFactory(_database.GetConnection());
        _scopeFactory = _webAppFactory.Services.GetRequiredService<IServiceScopeFactory>();

        Client = _webAppFactory.CreateClient();
    }

    public string CreateAccessToken(params string[] permissions)
    {
        using var scope = _scopeFactory.CreateScope();

        var jwtOptions = scope.ServiceProvider.GetRequiredService<IOptions<JwtOptions>>();

        var secret = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.Value.Secret));
        var creds = new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);

        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, Guid.NewGuid().ToString()),
            new(ClaimTypes.Email, "user@minicommerce")
        };

        claims.AddRange(permissions.Select(p => new Claim(Permissions.ClaimType, p)));

        var token = new JwtSecurityToken(
            issuer: jwtOptions.Value.Issuer,
            audience: jwtOptions.Value.Audience,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(1),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public async Task ResetStateAsync()
    {
        await _database.ResetAsync();

        using var scope = _scopeFactory.CreateScope();
        var initialiser = scope.ServiceProvider.GetRequiredService<AppDbContextInitialiser>();
        await initialiser.SeedAsync();
    }

    public async Task DisposeAsync()
    {
        await _database.DisposeAsync();
        await _webAppFactory.DisposeAsync();
    }
}
