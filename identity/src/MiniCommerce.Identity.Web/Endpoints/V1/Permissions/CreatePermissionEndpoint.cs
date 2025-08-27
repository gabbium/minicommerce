using MiniCommerce.Identity.Application.Contracts;
using MiniCommerce.Identity.Application.Contracts.Permissions;
using MiniCommerce.Identity.Application.Features.Permissions.CreatePermission;
using MiniCommerce.Identity.Web.Endpoints.Common;

namespace MiniCommerce.Identity.Web.Endpoints.V1.Permissions;

public class CreatePermissionEndpoint : IEndpointV1
{
    public const string Route = "api/v1/permissions";

    public record CreatePermissionRequest(string Code);

    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("permissions", async (
            CreatePermissionRequest request,
            ICommandHandler<CreatePermissionCommand, PermissionResponse> handler,
            CancellationToken cancellationToken) =>
        {
            var command = new CreatePermissionCommand(request.Code);
            var result = await handler.HandleAsync(command, cancellationToken);
            return result.Match(
                p => Results.Created(string.Empty, p),
                CustomResults.Problem);
        })
        .RequireAuthorization(IdentityPermissionNames.CanCreatePermission)
        .WithTags(Tags.Permissions)
        .Produces<PermissionResponse>(StatusCodes.Status201Created)
        .ProducesValidationProblem(StatusCodes.Status400BadRequest)
        .ProducesProblem(StatusCodes.Status401Unauthorized)
        .ProducesProblem(StatusCodes.Status403Forbidden);
    }
}
