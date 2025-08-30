using MiniCommerce.Catalog.Application.Contracts;
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
        .RequireAuthorization(PermissionNames.CanListProducts)
        .WithTags(Tags.Products)
        .Produces<PaginatedList<ProductResponse>>(StatusCodes.Status200OK)
        .ProducesValidationProblem(StatusCodes.Status400BadRequest)
        .ProducesProblem(StatusCodes.Status401Unauthorized)
        .ProducesProblem(StatusCodes.Status403Forbidden);
    }
}
