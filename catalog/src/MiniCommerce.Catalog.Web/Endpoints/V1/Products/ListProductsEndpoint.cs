using MiniCommerce.Catalog.Application.UseCases.Products;
using MiniCommerce.Catalog.Application.UseCases.Products.ListProducts;

namespace MiniCommerce.Catalog.Web.Endpoints.V1.Products;

public class ListProductsEndpoint : IEndpointV1
{
    public record Request(int Page, int PageSize);

    public static string BuildRoute(Request request) => $"api/v1/products?page={request.Page}&pageSize={request.PageSize}";

    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("products", async (
            [AsParameters] Request request,
            IQueryHandler<ListProductsQuery, PaginatedList<ProductResponse>> handler,
            CancellationToken cancellationToken) =>
        {
            var query = new ListProductsQuery(request.Page, request.PageSize);
            var result = await handler.HandleAsync(query, cancellationToken);
            return result.Match(Results.Ok, CustomResults.Problem);
        })
        .RequireAuthorization(Permissions.CanListProducts)
        .WithTags(Tags.Products);
    }
}
