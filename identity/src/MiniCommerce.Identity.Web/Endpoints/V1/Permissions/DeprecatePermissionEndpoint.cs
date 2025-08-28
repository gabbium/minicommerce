using MiniCommerce.Identity.Application.Contracts;
using MiniCommerce.Identity.Application.Contracts.Permissions;
using MiniCommerce.Identity.Application.Features.Permissions.DeprecatePermission;
using MiniCommerce.Identity.Web.Endpoints.Common;

namespace MiniCommerce.Identity.Web.Endpoints.V1.Permissions;

public class DeprecatePermissionEndpoint : IEndpointV1
{
    public static string BuildRoute(Guid id) => $"api/v1/permissions/{id}";

    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPatch("permissions/{id}", async (
            Guid id,
            ICommandHandler<DeprecatePermissionCommand, PermissionResponse> handler,
            CancellationToken cancellationToken) =>
        {
            var command = new DeprecatePermissionCommand(id);
            var result = await handler.HandleAsync(command, cancellationToken);
            return result.Match(Results.Ok, CustomResults.Problem);
        })
        .RequireAuthorization(IdentityPermissionNames.CanDeprecatePermission)
        .WithTags(Tags.Permissions)
        .Produces<PermissionResponse>(StatusCodes.Status200OK)
        .ProducesValidationProblem(StatusCodes.Status400BadRequest)
        .ProducesProblem(StatusCodes.Status401Unauthorized)
        .ProducesProblem(StatusCodes.Status403Forbidden)
        .ProducesProblem(StatusCodes.Status404NotFound);
    }
}
