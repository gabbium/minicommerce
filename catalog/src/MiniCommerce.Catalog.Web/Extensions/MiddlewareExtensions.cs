using MiniCommerce.Catalog.Web.Middlewares;

namespace MiniCommerce.Catalog.Web.Extensions;

public static class MiddlewareExtensions
{
    public static IApplicationBuilder UseRequestContextLogging(this IApplicationBuilder app)
    {
        app.UseMiddleware<RequestContextLoggingMiddleware>();

        return app;
    }
}
