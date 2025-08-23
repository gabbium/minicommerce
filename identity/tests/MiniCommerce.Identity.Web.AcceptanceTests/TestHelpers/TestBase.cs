namespace MiniCommerce.Identity.Web.AcceptanceTests.TestHelpers;

public abstract class TestBase(TestFixture fixture) : IAsyncLifetime
{
    protected readonly TestFixture Fixture = fixture;

    public async Task InitializeAsync()
    {
        await TestFixture.ResetState();
    }

    public Task DisposeAsync() => Task.CompletedTask;
}
