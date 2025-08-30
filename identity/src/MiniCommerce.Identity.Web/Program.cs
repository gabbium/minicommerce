using MiniCommerce.Identity.Application;
using MiniCommerce.Identity.Infrastructure;
using MiniCommerce.Identity.Infrastructure.Persistence.EFCore;
using MiniCommerce.Identity.Web;
using MiniCommerce.Identity.Web.Endpoints.V1;
using MiniCommerce.Identity.Web.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilogWithDefaults();

builder.Services
    .AddApplication()
    .AddPresentation()
    .AddInfrastructure(builder.Configuration);

builder.Services
    .AddJwtBearer(builder.Configuration)
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
