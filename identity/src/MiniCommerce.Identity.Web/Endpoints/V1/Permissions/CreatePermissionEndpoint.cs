using MiniCommerce.Identity.Application.Contracts;
using MiniCommerce.Identity.Application.Contracts.Permissions;
using MiniCommerce.Identity.Application.Features.Permissions.CreatePermission;
using MiniCommerce.Identity.Web.Endpoints.Common;

namespace MiniCommerce.Identity.Web.Endpoints.V1.Permissions;

public class CreatePermissionEndpoint : IEndpointV1
{
    public const string Route = "api/v1/permissions";

    public record Request(string Code);

    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("permissions", async (
            Request request,
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
        .WithTags(Tags.Permissions);
    }
}
