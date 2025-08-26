using MiniCommerce.Identity.Infrastructure.Persistence.EFCore;

namespace MiniCommerce.Identity.Web.AcceptanceTests.TestHelpers;

public class CustomWebApplicationFactory(DbConnection connection) : WebApplicationFactory<IWebMarker>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseSetting("Jwt:Secret", Guid.NewGuid().ToString());

        builder.ConfigureTestServices(services =>
        {
            services
                .RemoveAll<DbContextOptions<AppDbContext>>()
                .AddDbContext<AppDbContext>((_, opts) =>
                {
                    opts.UseNpgsql(connection);
                });
        });
    }
}
