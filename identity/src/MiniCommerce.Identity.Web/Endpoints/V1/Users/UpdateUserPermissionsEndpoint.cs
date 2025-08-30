using MiniCommerce.Identity.Application.Contracts;
using MiniCommerce.Identity.Application.UseCases.Users.UpdateUserPermissions;

namespace MiniCommerce.Identity.Web.Endpoints.V1.Users;

public class UpdateUserPermissionsEndpoint : IEndpointV1
{
    public static string BuildRoute(Guid id) => $"api/v1/users/{id}/permissions";

    public record UpdateUserPermissionsRequest(IReadOnlyList<Guid> PermissionIds);

    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPatch("users/{id}/permissions", async (
            Guid id,
            UpdateUserPermissionsRequest request,
            ICommandHandler<UpdateUserPermissionsCommand, UserResponse> handler,
            CancellationToken cancellationToken) =>
        {
            var command = new UpdateUserPermissionsCommand(id, request.PermissionIds);
            var result = await handler.HandleAsync(command, cancellationToken);
            return result.Match(Results.Ok, CustomResults.Problem);
        })
        .RequireAuthorization(PermissionNames.CanUpdateUserPermissions)
        .WithTags(Tags.Users)
        .Produces<UserResponse>(StatusCodes.Status200OK)
        .ProducesValidationProblem(StatusCodes.Status400BadRequest)
        .ProducesProblem(StatusCodes.Status401Unauthorized)
        .ProducesProblem(StatusCodes.Status403Forbidden)
        .ProducesProblem(StatusCodes.Status404NotFound);
    }
}
