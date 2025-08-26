using MiniCommerce.Catalog.Application.Contracts;
using MiniCommerce.Catalog.Application.Contracts.Products;
using MiniCommerce.Catalog.Application.Features.Products.CreateProduct;
using MiniCommerce.Catalog.Web.Endpoints.Common;

namespace MiniCommerce.Catalog.Web.Endpoints.V1.Products;

public class CreateProductEndpoint : IEndpointV1
{
    public const string Route = "api/v1/products";

    public record Request(string Sku, string Name, decimal Price);

    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("products", async (
            Request request,
            ICommandHandler<CreateProductCommand, ProductResponse> handler,
            CancellationToken cancellationToken) =>
        {
            var command = new CreateProductCommand(request.Sku, request.Name, request.Price);
            var result = await handler.HandleAsync(command, cancellationToken);
            return result.Match(
                p => Results.Created(GetProductByIdEndpoint.BuildRoute(p.Id), p),
                CustomResults.Problem);
        })
        .RequireAuthorization(CatalogPermissionNames.CanCreateProduct)
        .WithTags(Tags.Products);
    }
}
