using MiniCommerce.Identity.Application.UseCases.Permissions.DeletePermission;

namespace MiniCommerce.Identity.Web.Endpoints.V1.Permissions;

public class DeletePermissionEndpoint : IEndpointV1
{
    public static string BuildRoute(Guid id) => $"api/v1/permissions/{id}";

    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapDelete("permissions/{id}", async (
            Guid id,
            ICommandHandler<DeletePermissionCommand> handler,
            CancellationToken cancellationToken) =>
        {
            var command = new DeletePermissionCommand(id);
            var result = await handler.HandleAsync(command, cancellationToken);
            return result.Match(Results.NoContent, CustomResults.Problem);
        })
        .RequireAuthorization(Policies.CanDeletePermission)
        .WithTags(Tags.Permissions)
        .Produces(StatusCodes.Status204NoContent)
        .ProducesValidationProblem(StatusCodes.Status400BadRequest)
        .ProducesProblem(StatusCodes.Status401Unauthorized)
        .ProducesProblem(StatusCodes.Status403Forbidden)
        .ProducesProblem(StatusCodes.Status404NotFound);
    }
}
