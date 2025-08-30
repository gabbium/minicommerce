using MiniCommerce.Catalog.Application.Contracts;
using MiniCommerce.Catalog.Application.UseCases.Products.GetProductById;

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
        .RequireAuthorization(PermissionNames.CanGetProductById)
        .WithTags(Tags.Products)
        .Produces<ProductResponse>(StatusCodes.Status200OK)
        .ProducesValidationProblem(StatusCodes.Status400BadRequest)
        .ProducesProblem(StatusCodes.Status401Unauthorized)
        .ProducesProblem(StatusCodes.Status403Forbidden)
        .ProducesProblem(StatusCodes.Status404NotFound);
    }
}
