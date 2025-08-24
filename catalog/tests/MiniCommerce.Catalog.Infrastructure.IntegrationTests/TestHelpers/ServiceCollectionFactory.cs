using MiniCommerce.Catalog.Infrastructure.Persistence;

namespace MiniCommerce.Catalog.Infrastructure.IntegrationTests.TestHelpers;

public class ServiceCollectionFactory
{
    public IServiceProvider Services { get; }

    public ServiceCollectionFactory(DbConnection connection)
    {
        var builder = new HostApplicationBuilder();

        builder.Configuration.AddInMemoryCollection(new Dictionary<string, string?>
        {
            ["Jwt:Secret"] = Guid.NewGuid().ToString(),
        });

        builder.Services.AddInfrastructure(builder.Configuration);

        builder.Services
            .RemoveAll<DbContextOptions<AppDbContext>>()
            .AddDbContext<AppDbContext>((sp, options) =>
            {
                options.UseNpgsql(connection);
            });

        Services = builder.Services.BuildServiceProvider();
    }
}
