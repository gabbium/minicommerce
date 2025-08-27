using MiniCommerce.Identity.Application.Contracts;
using MiniCommerce.Identity.Application.Features.Users.DeleteUser;
using MiniCommerce.Identity.Web.Endpoints.Common;

namespace MiniCommerce.Identity.Web.Endpoints.V1.Users;

public class DeleteUserEndpoint : IEndpointV1
{
    public static string BuildRoute(Guid id) => $"api/v1/users/{id}";

    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapDelete("users/{id}", async (
            Guid id,
            ICommandHandler<DeleteUserCommand> handler,
            CancellationToken cancellationToken) =>
        {
            var command = new DeleteUserCommand(id);
            var result = await handler.HandleAsync(command, cancellationToken);
            return result.Match(Results.NoContent, CustomResults.Problem);
        })
        .RequireAuthorization(IdentityPermissionNames.CanDeleteUser)
        .WithTags(Tags.Users)
        .Produces(StatusCodes.Status204NoContent)
        .ProducesValidationProblem(StatusCodes.Status400BadRequest)
        .ProducesProblem(StatusCodes.Status401Unauthorized)
        .ProducesProblem(StatusCodes.Status403Forbidden)
        .ProducesProblem(StatusCodes.Status404NotFound);
    }
}
