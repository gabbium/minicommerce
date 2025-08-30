namespace MiniCommerce.Identity.Infrastructure.IntegrationTests.TestHelpers.Data;

public interface ITestDatabase
{
    Task InitializeAsync();

    DbConnection GetConnection();

    string GetConnectionString();

    Task ResetAsync();

    Task DisposeAsync();
}
