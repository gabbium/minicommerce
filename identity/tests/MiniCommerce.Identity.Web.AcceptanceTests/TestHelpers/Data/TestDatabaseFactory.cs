namespace MiniCommerce.Identity.Web.AcceptanceTests.TestHelpers.Data;

public static class TestDatabaseFactory
{
    public static async Task<ITestDatabase> CreateAsync()
    {
        var database = new PostgresTestDatabase();
        await database.InitialiseAsync();
        return database;
    }
}
