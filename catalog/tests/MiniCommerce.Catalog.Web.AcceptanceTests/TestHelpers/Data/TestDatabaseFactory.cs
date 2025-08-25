namespace MiniCommerce.Catalog.Web.AcceptanceTests.TestHelpers.Data;

public static class TestDatabaseFactory
{
    public static async Task<ITestDatabase> CreateAsync()
    {
        var database = new PostgreTestDatabase();

        await database.InitialiseAsync();

        return database;
    }
}
