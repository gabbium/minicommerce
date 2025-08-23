using CleanArch;
using MiniCommerce.Identity.Application.Commands.Login;
using MiniCommerce.Identity.Application.Models;
using MiniCommerce.Identity.Infrastructure.Persistence;
using MiniCommerce.Identity.Web.AcceptanceTests.TestHelpers.Persistence;

namespace MiniCommerce.Identity.Web.AcceptanceTests.TestHelpers;

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

    public async Task AuthenticateAsync(string email)
    {
        using var scope = _scopeFactory.CreateScope();

        var handler = scope.ServiceProvider.GetRequiredService<ICommandHandler<LoginCommand, AuthResponse>>();

        var command = new LoginCommand(email);

        var result = await handler.HandleAsync(command);

        if (result.IsSuccess)
        {
            Client.DefaultRequestHeaders.Authorization = new("Bearer", result.Value.Token.AccessToken);
            return;
        }

        var errors = string.Join(Environment.NewLine, result.Error);

        throw new Exception($"Unable to authenticate {email}.{Environment.NewLine}{errors}");
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
