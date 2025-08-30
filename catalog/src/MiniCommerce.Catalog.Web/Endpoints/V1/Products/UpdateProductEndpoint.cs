using MiniCommerce.Catalog.Application.Contracts;
using MiniCommerce.Catalog.Application.UseCases.Products.UpdateProduct;

namespace MiniCommerce.Catalog.Web.Endpoints.V1.Products;

public class UpdateProductEndpoint : IEndpointV1
{
    public static string BuildRoute(Guid id) => $"api/v1/products/{id}";

    public record Request(string Name, decimal Price);

    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPut("products/{id}", async (
            Guid id,
            Request request,
            ICommandHandler<UpdateProductCommand, ProductResponse> handler,
            CancellationToken cancellationToken) =>
        {
            var command = new UpdateProductCommand(id, request.Name, request.Price);
            var result = await handler.HandleAsync(command, cancellationToken);
            return result.Match(Results.Ok, CustomResults.Problem);
        })
        .RequireAuthorization(Policies.CanUpdateProduct)
        .WithTags(Tags.Products);
    }
}
