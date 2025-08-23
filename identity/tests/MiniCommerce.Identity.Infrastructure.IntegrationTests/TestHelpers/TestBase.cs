namespace MiniCommerce.Identity.Infrastructure.IntegrationTests.TestHelpers;

public abstract class TestBase(TestFixture fixture) : IAsyncLifetime
{
    protected readonly TestFixture Fixture = fixture;

    public async Task InitializeAsync()
    {
        await Fixture.ResetStateAsync();
    }

    public Task DisposeAsync() => Task.CompletedTask;
}
