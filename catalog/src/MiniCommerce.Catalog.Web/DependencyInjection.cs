using MiniCommerce.Catalog.Application.Abstractions;
using MiniCommerce.Catalog.Web.Extensions.Middlewares;
using MiniCommerce.Catalog.Web.Services;

namespace MiniCommerce.Catalog.Web;

public static class DependencyInjection
{
    public static IServiceCollection AddPresentation(this IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();

        services
            .AddApiVersioning(o =>
            {
                o.AssumeDefaultVersionWhenUnspecified = true;
                o.DefaultApiVersion = new(1, 0);
                o.ReportApiVersions = true;
            })
            .AddApiExplorer(o =>
            {
                o.GroupNameFormat = "'v'VVV";
                o.SubstituteApiVersionInUrl = true;
            });


        services.ConfigureHttpJsonOptions(o =>
        {
            o.SerializerOptions.Converters.Add(new JsonStringEnumConverter());
        });

        services.AddExceptionHandler<GlobalExceptionHandler>();

        services.AddProblemDetails();

        services.AddHttpContextAccessor();

        services.AddScoped<IUserContext, UserContext>();

        return services;
    }
}
