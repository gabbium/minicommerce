using MiniCommerce.Catalog.Application.Contracts;
using MiniCommerce.Catalog.Application.Features.Products.ListProducts;
using MiniCommerce.Catalog.Domain.Aggregates.Products;
using MiniCommerce.Catalog.Infrastructure.Jwt;
using MiniCommerce.Catalog.Infrastructure.Persistence.EFCore;
using MiniCommerce.Catalog.Infrastructure.Persistence.Repositories;
using MiniCommerce.Catalog.Infrastructure.Persistence.Services;

namespace MiniCommerce.Catalog.Infrastructure;

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
            foreach (var permissionName in CatalogPermissionNames.All)
            {
                o.AddPolicy(permissionName, p =>
                    p.RequireClaim(CatalogPermissionNames.ClaimType, permissionName));
            }
        });

        services.AddTransient<IProductRepository, ProductRepository>();

        services.AddTransient<IListProductsService, ListProductsService>();

        return services;
    }
}
