using MiniCommerce.Identity.Application.Abstractions;
using MiniCommerce.Identity.Domain.Aggregates.Permissions.Repositories;
using MiniCommerce.Identity.Domain.Aggregates.Users.Repositories;
using MiniCommerce.Identity.Infrastructure.Jwt;
using MiniCommerce.Identity.Infrastructure.Persistence.EFCore;
using MiniCommerce.Identity.Infrastructure.Persistence.Queries;
using MiniCommerce.Identity.Infrastructure.Persistence.Repositories;

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

        services.AddScoped<IUnitOfWork, UnitOfWork>();

        services.AddTransient<IPermissionRepository, PermissionRepository>();
        services.AddTransient<IPermissionQueries, PermissionQueries>();

        services.AddTransient<IUserRepository, UserRepository>();
        services.AddTransient<IUserQueries, UserQueries>();

        services.AddSingleton<IJwtTokenService, JwtTokenService>();

        return services;
    }
}
