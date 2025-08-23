using MiniCommerce.Identity.Application.Interfaces;
using MiniCommerce.Identity.Domain.Interfaces;
using MiniCommerce.Identity.Infrastructure.Persistence;
using MiniCommerce.Identity.Infrastructure.Persistence.Repositories;
using MiniCommerce.Identity.Infrastructure.Services;

namespace MiniCommerce.Identity.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>((sp, opts) =>
        {
            opts.UseNpgsql(configuration.GetConnectionString("DefaultConnection")).AddAsyncSeeding(sp);
        });

        services.AddScoped<AppDbContextInitialiser>();

        services
            .AddHealthChecks()
            .AddDbContextCheck<AppDbContext>("Database");

        services.AddSingleton<ITokenService, TokenService>();

        services.AddTransient<IUserRepository, UserRepository>();

        return services;
    }
}
