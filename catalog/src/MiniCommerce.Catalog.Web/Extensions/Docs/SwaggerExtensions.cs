namespace MiniCommerce.Catalog.Web.Extensions.Docs;

public static class SwaggerExtensions
{
    public static IServiceCollection AddSwaggerGenWithAuth(this IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();

        services.AddOpenApiDocument(d =>
        {
            d.Title = "MiniCommerce.Catalog.V1";
            d.DocumentName = "v1";
            d.Version = "v1";
            d.ApiGroupNames = ["v1"];

            d.AddSecurity("Bearer", [], new OpenApiSecurityScheme
            {
                Description = "JWT Authorization header using the Bearer scheme (Example: 'Bearer 12345abcdef')",
                Name = "Authorization",
                In = OpenApiSecurityApiKeyLocation.Header,
                Type = OpenApiSecuritySchemeType.Http,
                Scheme = "Bearer"
            });

            d.OperationProcessors.Add(new AspNetCoreOperationSecurityScopeProcessor("Bearer"));
        });

        return services;
    }

    public static IApplicationBuilder UseSwaggerWithUi(this IApplicationBuilder app)
    {
        app.UseOpenApi(s => s.Path = "/api/specification.json");

        app.UseSwaggerUi(s =>
        {
            s.Path = "/api";
            s.DocumentPath = "/api/specification.json";
        });

        app.UseReDoc(s =>
        {
            s.Path = "/docs";
            s.DocumentPath = "/api/specification.json";
        });

        return app;
    }
}
