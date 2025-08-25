using MiniCommerce.Catalog.Application.Features.Products.DeleteProduct;

namespace MiniCommerce.Catalog.Web.Endpoints.V1.Products;

public class DeleteProductEndpoint : IEndpointV1
{
    public static string BuildRoute(Guid id) => $"api/v1/products/{id}";

    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapDelete("products/{id}", async (
            Guid id,
            ICommandHandler<DeleteProductCommand> handler,
            CancellationToken cancellationToken) =>
        {
            var command = new DeleteProductCommand(id);

            var result = await handler.HandleAsync(command, cancellationToken);

            return result.Match(Results.NoContent, CustomResults.Problem);
        })
        .RequireAuthorization()
        .WithTags(Tags.Products);
    }
}
