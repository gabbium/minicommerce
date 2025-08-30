namespace MiniCommerce.Identity.Infrastructure.IntegrationTests.TestHelpers.Data;

public static class TestDatabaseFactory
{
    public static async Task<ITestDatabase> CreateAsync()
    {
        var database = new PostgresTestDatabase();
        await database.InitializeAsync();
        return database;
    }
}
