using MiniCommerce.Catalog.Domain.Abstractions;
using MiniCommerce.Catalog.Infrastructure.Jwt;
using MiniCommerce.Catalog.Infrastructure.Persistence;
using MiniCommerce.Catalog.Infrastructure.Repositories;

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
            .AddJwtBearer(options =>
            {
                var jwt = configuration.GetSection("Jwt").Get<JwtOptions>()!;

                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt.Secret)),
                    ValidIssuer = jwt.Issuer,
                    ValidAudience = jwt.Audience,
                    ClockSkew = TimeSpan.Zero
                };
            });

        services.AddAuthorization();

        services.AddTransient<IProductRepository, ProductRepository>();

        return services;
    }
}
