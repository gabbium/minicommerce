namespace MiniCommerce.Catalog.Web.AcceptanceTests.TestHelpers.Data;

public interface ITestDatabase
{
    Task InitialiseAsync();

    DbConnection GetConnection();

    string GetConnectionString();

    Task ResetAsync();

    Task DisposeAsync();
}
