using MiniCommerce.Catalog.Infrastructure.Persistence.EFCore;

namespace MiniCommerce.Catalog.Web.AcceptanceTests.TestHelpers.Containers;

public class PostgresContainer
{
    private readonly PostgreSqlContainer _container;

    public PostgresContainer()
    {
        _container = new PostgreSqlBuilder()
            .WithImage("postgres:16-alpine")
            .Build();
    }

    public async Task InitializeAsync()
    {
        await _container.StartAsync();

        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseNpgsql(_container.GetConnectionString())
            .Options;

        using var context = new AppDbContext(options);

        await context.Database.MigrateAsync();
    }

    public DbConnection GetConnection()
    {
        return new NpgsqlConnection(_container.GetConnectionString());
    }

    public async Task ResetAsync()
    {
        var connectionString = _container.GetConnectionString();

        await using var conn = new NpgsqlConnection(connectionString);

        await conn.OpenAsync();

        await using (var dropCmd = conn.CreateCommand())
        {
            dropCmd.CommandText = @"
                DROP SCHEMA public CASCADE;
                CREATE SCHEMA public;
            ";

            await dropCmd.ExecuteNonQueryAsync();
        }

        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseNpgsql(connectionString)
            .Options;

        await using var context = new AppDbContext(options);
        await context.Database.MigrateAsync();
    }

    public async Task DisposeAsync()
    {
        await _container.StopAsync();
        await _container.DisposeAsync();
    }
}
