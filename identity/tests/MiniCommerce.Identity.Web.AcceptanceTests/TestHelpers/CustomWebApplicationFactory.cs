using MiniCommerce.Identity.Infrastructure.Persistence;

namespace MiniCommerce.Identity.Web.AcceptanceTests.TestHelpers;

public class CustomWebApplicationFactory(DbConnection connection) : WebApplicationFactory<IWebMarker>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
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
