using MiniCommerce.Identity.Application.Contracts;
using MiniCommerce.Identity.Application.UseCases.Permissions.ListPermissions;

namespace MiniCommerce.Identity.Web.Endpoints.V1.Permissions;

public class ListPermissionsEndpoint : IEndpointV1
{
    public record ListPermissionsRequest(int Page, int PageSize);

    public static string BuildRoute(ListPermissionsRequest request) => $"api/v1/permissions?page={request.Page}&pageSize={request.PageSize}";

    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("permissions", async (
            [AsParameters] ListPermissionsRequest request,
            IQueryHandler<ListPermissionsQuery, PaginatedList<PermissionResponse>> handler,
            CancellationToken cancellationToken) =>
        {
            var query = new ListPermissionsQuery(request.Page, request.PageSize);
            var result = await handler.HandleAsync(query, cancellationToken);
            return result.Match(Results.Ok, CustomResults.Problem);
        })
        .RequireAuthorization(PermissionNames.CanListPermissions)
        .WithTags(Tags.Permissions)
        .Produces<PaginatedList<PermissionResponse>>(StatusCodes.Status200OK)
        .ProducesValidationProblem(StatusCodes.Status400BadRequest)
        .ProducesProblem(StatusCodes.Status401Unauthorized)
        .ProducesProblem(StatusCodes.Status403Forbidden);
    }
}
