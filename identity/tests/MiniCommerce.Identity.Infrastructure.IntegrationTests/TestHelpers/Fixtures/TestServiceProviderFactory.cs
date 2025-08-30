using MiniCommerce.Identity.Infrastructure.Persistence.EFCore;

namespace MiniCommerce.Identity.Infrastructure.IntegrationTests.TestHelpers;

public class TestServiceProviderFactory
{
    public IServiceProvider Services { get; }

    public TestServiceProviderFactory(DbConnection connection)
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
