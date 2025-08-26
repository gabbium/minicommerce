using MiniCommerce.Identity.Application.Contracts;
using MiniCommerce.Identity.Application.Contracts.Users;
using MiniCommerce.Identity.Application.Features.Users.CreateUser;
using MiniCommerce.Identity.Web.Endpoints.Common;

namespace MiniCommerce.Identity.Web.Endpoints.V1.Users;

public class CreateUserEndpoint : IEndpointV1
{
    public const string Route = "api/v1/users";

    public record Request(string Email);

    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("users", async (
            Request request,
            ICommandHandler<CreateUserCommand, UserResponse> handler,
            CancellationToken cancellationToken) =>
        {
            var command = new CreateUserCommand(request.Email);
            var result = await handler.HandleAsync(command, cancellationToken);
            return result.Match(
                u => Results.Created(string.Empty, u),
                CustomResults.Problem);
        })
        .RequireAuthorization(IdentityPermissionNames.CanCreateUser)
        .WithTags(Tags.Users);
    }
}
