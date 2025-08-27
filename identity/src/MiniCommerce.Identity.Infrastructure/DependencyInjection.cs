using MiniCommerce.Identity.Application.Abstractions;
using MiniCommerce.Identity.Application.Contracts;
using MiniCommerce.Identity.Application.Features.Users.ListUsers;
using MiniCommerce.Identity.Domain.Aggregates.Permissions;
using MiniCommerce.Identity.Domain.Aggregates.Users;
using MiniCommerce.Identity.Infrastructure.Jwt;
using MiniCommerce.Identity.Infrastructure.Persistence.EFCore;
using MiniCommerce.Identity.Infrastructure.Persistence.Repositories;
using MiniCommerce.Identity.Infrastructure.Persistence.Services;

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

        services
            .AddOptions<JwtOptions>()
            .Bind(configuration.GetSection("Jwt"))
            .ValidateOnStart();

        services.AddSingleton<IValidateOptions<JwtOptions>, JwtOptionsValidator>();

        services
            .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(o =>
            {
                var jwt = configuration.GetSection("Jwt").Get<JwtOptions>()!;

                o.RequireHttpsMetadata = false;
                o.TokenValidationParameters = new TokenValidationParameters
                {
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt.Secret)),
                    ValidIssuer = jwt.Issuer,
                    ValidAudience = jwt.Audience,
                    ClockSkew = TimeSpan.Zero
                };
            });

        services.AddAuthorization(o =>
        {
            foreach (var permissionName in IdentityPermissionNames.All)
            {
                o.AddPolicy(permissionName, p =>
                    p.RequireClaim(IdentityPermissionNames.ClaimType, permissionName));
            }
        });

        services.AddSingleton<IJwtTokenService, JwtTokenService>();

        services.AddTransient<IUserRepository, UserRepository>();
        services.AddTransient<IPermissionRepository, PermissionRepository>();

        services.AddTransient<IListUsersService, ListUsersService>();

        return services;
    }
}
