using MiniCommerce.Catalog.Application.Contracts;
using MiniCommerce.Catalog.Application.Contracts.Products;
using MiniCommerce.Catalog.Application.Features.Products.GetProductById;
using MiniCommerce.Catalog.Web.Endpoints.Common;

namespace MiniCommerce.Catalog.Web.Endpoints.V1.Products;

public class GetProductByIdEndpoint : IEndpointV1
{
    public static string BuildRoute(Guid id) => $"api/v1/products/{id}";

    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("products/{id}", async (
            Guid id,
            IQueryHandler<GetProductByIdQuery, ProductResponse> handler,
            CancellationToken cancellationToken) =>
        {
            var query = new GetProductByIdQuery(id);
            var result = await handler.HandleAsync(query, cancellationToken);
            return result.Match(Results.Ok, CustomResults.Problem);
        })
        .RequireAuthorization(CatalogPermissionNames.CanGetProductById)
        .WithTags(Tags.Products);
    }
}
