using MiniCommerce.Catalog.Infrastructure.Persistence.EFCore;
using MiniCommerce.Catalog.Web.AcceptanceTests.TestHelpers.Auth;

namespace MiniCommerce.Catalog.Web.AcceptanceTests.TestHelpers.Fixtures;

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

            services.AddSingleton<JwtTokenFactory>();
        });
    }
}
