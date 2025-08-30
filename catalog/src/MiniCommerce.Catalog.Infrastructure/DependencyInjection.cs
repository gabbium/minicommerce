using MiniCommerce.Catalog.Application.UseCases.Products;
using MiniCommerce.Catalog.Domain.ProductAggregate.Repositories;
using MiniCommerce.Catalog.Infrastructure.Persistence.EFCore;
using MiniCommerce.Catalog.Infrastructure.Persistence.Queries;
using MiniCommerce.Catalog.Infrastructure.Persistence.Repositories;

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

        services.AddScoped<IUnitOfWork, UnitOfWork>();

        services.AddScoped<IProductRepository, ProductRepository>();

        services.AddScoped<IProductQueries, ProductQueries>();

        return services;
    }
}
