using MiniCommerce.Catalog.Application;
using MiniCommerce.Catalog.Infrastructure;
using MiniCommerce.Catalog.Infrastructure.Persistence.EFCore;
using MiniCommerce.Catalog.Web;
using MiniCommerce.Catalog.Web.Endpoints.V1;
using MiniCommerce.Catalog.Web.Extensions.Auth;
using MiniCommerce.Catalog.Web.Extensions.Docs;
using MiniCommerce.Catalog.Web.Extensions.Logging;
using MiniCommerce.Catalog.Web.Extensions.Middlewares;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilogWithDefaults();

builder.Services
    .AddApplication()
    .AddPresentation()
    .AddInfrastructure(builder.Configuration);

builder.Services
    .AddAuthWithJwt(builder.Configuration)
    .AddSwaggerGenWithAuth()
    .AddEndpoints(Assembly.GetExecutingAssembly());

var app = builder.Build();

app.UseExceptionHandler();

if (app.Environment.IsDevelopment())
{
    app.UseSwaggerWithUi();
}

app.UseRequestContextLogging();

app.UseSerilogRequestLoggingWithDefaults();

app.MapHealthChecks("api/health", new HealthCheckOptions
{
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});

app.MapEndpoints<IEndpointV1>(new(1, 0));

await app.InitialiseDatabaseAsync();

await app.RunAsync();
