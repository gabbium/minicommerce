using MiniCommerce.Catalog.Infrastructure.IntegrationTests.TestHelpers.Containers;

namespace MiniCommerce.Catalog.Infrastructure.IntegrationTests.TestHelpers.Fixtures;

public class TestFixture : IAsyncLifetime
{
    private PostgresContainer _database = null!;
    private TestServiceProviderFactory _factory = null!;

    public async Task InitializeAsync()
    {
        _database = new PostgresContainer();
        await _database.InitializeAsync();

        _factory = new TestServiceProviderFactory(_database.GetConnection());
    }

    public T GetRequiredService<T>() where T : class
    {
        return _factory.Services.GetRequiredService<T>();
    }

    public async Task ResetStateAsync()
    {
        await _database.ResetAsync();
    }

    public async Task DisposeAsync()
    {
        await _database.DisposeAsync();
    }
}
