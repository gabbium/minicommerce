using MiniCommerce.Identity.Application.Contracts;
using MiniCommerce.Identity.Application.UseCases.Permissions.GetPermissionById;

namespace MiniCommerce.Identity.Web.Endpoints.V1.Permissions;

public class GetPermissionByIdEndpoint : IEndpointV1
{
    public static string BuildRoute(Guid id) => $"api/v1/permissions/{id}";

    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("permissions/{id}", async (
            Guid id,
            IQueryHandler<GetPermissionByIdQuery, PermissionResponse> handler,
            CancellationToken cancellationToken) =>
        {
            var query = new GetPermissionByIdQuery(id);
            var result = await handler.HandleAsync(query, cancellationToken);
            return result.Match(Results.Ok, CustomResults.Problem);
        })
        .RequireAuthorization(PermissionNames.CanGetPermissionById)
        .WithTags(Tags.Permissions)
        .Produces<PermissionResponse>(StatusCodes.Status200OK)
        .ProducesValidationProblem(StatusCodes.Status400BadRequest)
        .ProducesProblem(StatusCodes.Status401Unauthorized)
        .ProducesProblem(StatusCodes.Status403Forbidden)
        .ProducesProblem(StatusCodes.Status404NotFound);
    }
}
