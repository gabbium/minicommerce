using MiniCommerce.Identity.Infrastructure.IntegrationTests.TestHelpers.Data;

namespace MiniCommerce.Identity.Infrastructure.IntegrationTests.TestHelpers;

public class TestFixture : IAsyncLifetime
{
    private ITestDatabase _database = null!;
    private ServiceCollectionFactory _factory = null!;

    public async Task InitializeAsync()
    {
        _database = await TestDatabaseFactory.CreateAsync();
        _factory = new ServiceCollectionFactory(_database.GetConnection());
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
