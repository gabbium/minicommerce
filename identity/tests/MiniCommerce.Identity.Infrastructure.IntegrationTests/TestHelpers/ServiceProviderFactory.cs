using MiniCommerce.Identity.Infrastructure.Persistence.EFCore;

namespace MiniCommerce.Identity.Infrastructure.IntegrationTests.TestHelpers;

public class ServiceProviderFactory
{
    public IServiceProvider Services { get; }

    public ServiceProviderFactory(DbConnection connection)
    {
        var builder = new HostApplicationBuilder();

        builder.Configuration.AddInMemoryCollection(new Dictionary<string, string?>
        {
            ["Jwt:Secret"] = Guid.NewGuid().ToString(),
        });

        builder.Services.AddInfrastructure(builder.Configuration);

        builder.Services
            .RemoveAll<DbContextOptions<AppDbContext>>()
            .AddDbContext<AppDbContext>((_, opts) =>
            {
                opts.UseNpgsql(connection);
            });

        Services = builder.Services.BuildServiceProvider();
    }
}
