using MiniCommerce.Identity.Application;
using MiniCommerce.Identity.Infrastructure;
using MiniCommerce.Identity.Infrastructure.Persistence;
using MiniCommerce.Identity.Web;
using MiniCommerce.Identity.Web.Endpoints.V1;
using MiniCommerce.Identity.Web.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilogWithDefaults();

builder.Services.AddSwaggerGenWithAuth();

builder.Services
    .AddApplication()
    .AddPresentation()
    .AddInfrastructure(builder.Configuration);

builder.Services.AddEndpoints(Assembly.GetExecutingAssembly());

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwaggerWithUi();
}

app.MapHealthChecks("api/health", new HealthCheckOptions
{
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});

app.UseRequestContextLogging();

app.UseSerilogRequestLoggingWithDefaults();

app.UseExceptionHandler();

app.MapEndpoints<IEndpointV1>(new(1, 0));

await app.InitialiseDatabaseAsync();

await app.RunAsync();
