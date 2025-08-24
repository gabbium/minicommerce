using MiniCommerce.Catalog.Infrastructure.Jwt;
using MiniCommerce.Catalog.Infrastructure.Persistence;
using MiniCommerce.Catalog.Web.AcceptanceTests.TestHelpers.Data;

namespace MiniCommerce.Catalog.Web.AcceptanceTests.TestHelpers;

public class TestFixture : IAsyncLifetime
{
    private ITestDatabase _database = null!;
    private CustomWebApplicationFactory _factory = null!;
    private IServiceScopeFactory _scopeFactory = null!;

    public HttpClient Client { get; private set; } = null!;

    public async Task InitializeAsync()
    {
        _database = await TestDatabaseFactory.CreateAsync();
        _factory = new CustomWebApplicationFactory(_database.GetConnection());
        _scopeFactory = _factory.Services.GetRequiredService<IServiceScopeFactory>();

        Client = _factory.CreateClient();
    }

    public void AuthenticateAsDefault()
    {
        Authenticate("user@minicommerce");
    }

    public void Authenticate(string email)
    {
        using var scope = _scopeFactory.CreateScope();

        var jwtOptions = scope.ServiceProvider.GetRequiredService<IOptions<JwtOptions>>();

        var secret = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.Value.Secret));
        var creds = new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);

        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, Guid.NewGuid().ToString()),
            new(ClaimTypes.Email, email)
        };

        var token = new JwtSecurityToken(
            issuer: jwtOptions.Value.Issuer,
            audience: jwtOptions.Value.Audience,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(1),
            signingCredentials: creds
        );

        var accessToken = new JwtSecurityTokenHandler().WriteToken(token);

        Client.DefaultRequestHeaders.Authorization = new("Bearer", accessToken);
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
        await _factory.DisposeAsync();
    }
}
