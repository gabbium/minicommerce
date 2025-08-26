namespace MiniCommerce.Catalog.Infrastructure.IntegrationTests.TestHelpers.Data;

public static class TestDatabaseFactory
{
    public static async Task<ITestDatabase> CreateAsync()
    {
        var database = new PostgresTestDatabase();
        await database.InitialiseAsync();
        return database;
    }
}
