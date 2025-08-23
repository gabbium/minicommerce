namespace MiniCommerce.Identity.Web.AcceptanceTests.TestHelpers.Persistence;

public static class TestDatabaseFactory
{
    public static async Task<ITestDatabase> CreateAsync()
    {
        var database = new PostgreTestDatabase();

        await database.InitialiseAsync();

        return database;
    }
}
