using MiniCommerce.Catalog.Infrastructure.Persistence;

namespace MiniCommerce.Catalog.Web.AcceptanceTests.TestHelpers;

public class CustomWebApplicationFactory(DbConnection connection) : WebApplicationFactory<IWebMarker>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseSetting("Jwt:Secret", Guid.NewGuid().ToString());

        builder.ConfigureTestServices(services =>
        {
            services
                .RemoveAll<DbContextOptions<AppDbContext>>()
                .AddDbContext<AppDbContext>((sp, options) =>
                {
                    options.UseNpgsql(connection);
                });
        });
    }
}
