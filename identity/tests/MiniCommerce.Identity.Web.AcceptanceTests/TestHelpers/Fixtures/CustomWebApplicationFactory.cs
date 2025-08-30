using MiniCommerce.Identity.Infrastructure.Persistence.EFCore;
using MiniCommerce.Identity.Web.AcceptanceTests.TestHelpers.Auth;

namespace MiniCommerce.Identity.Web.AcceptanceTests.TestHelpers.Fixtures;

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

            services.AddSingleton<JwtTokenFactory>();
        });
    }
}
