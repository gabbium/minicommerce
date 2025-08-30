using MiniCommerce.Catalog.Infrastructure.Persistence.EFCore;

namespace MiniCommerce.Catalog.Infrastructure.IntegrationTests.TestHelpers;

public class ServiceProviderFactory
{
    public IServiceProvider Services { get; }

    public ServiceProviderFactory(DbConnection connection)
    {
        var builder = new HostApplicationBuilder();

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
