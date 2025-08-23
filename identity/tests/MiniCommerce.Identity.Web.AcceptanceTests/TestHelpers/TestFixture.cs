namespace MiniCommerce.Identity.Web.AcceptanceTests.TestHelpers;

public class TestFixture : IAsyncLifetime
{
    private CustomWebApplicationFactory _factory = null!;

    public HttpClient Client { get; private set; } = null!;

    public Task InitializeAsync()
    {
        _factory = new CustomWebApplicationFactory();
        Client = _factory.CreateClient();
        return Task.CompletedTask;
    }

    public static Task ResetState()
    {
        return Task.CompletedTask;
    }

    public async Task DisposeAsync()
    {
        await _factory.DisposeAsync();
    }
}
