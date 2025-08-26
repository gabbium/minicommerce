using MiniCommerce.Catalog.Infrastructure.IntegrationTests.TestHelpers.Data;

namespace MiniCommerce.Catalog.Infrastructure.IntegrationTests.TestHelpers;

public class TestFixture : IAsyncLifetime
{
    private ITestDatabase _database = null!;
    private ServiceProviderFactory _factory = null!;

    public async Task InitializeAsync()
    {
        _database = await TestDatabaseFactory.CreateAsync();
        _factory = new ServiceProviderFactory(_database.GetConnection());
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
