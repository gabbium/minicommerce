namespace MiniCommerce.Catalog.Infrastructure.IntegrationTests.TestHelpers.Data;

public interface ITestDatabase
{
    Task InitialiseAsync();

    DbConnection GetConnection();

    string GetConnectionString();

    Task ResetAsync();

    Task DisposeAsync();
}
