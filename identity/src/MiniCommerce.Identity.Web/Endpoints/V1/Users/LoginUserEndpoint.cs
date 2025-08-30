using MiniCommerce.Identity.Application.Contracts;
using MiniCommerce.Identity.Application.UseCases.Users.LoginUser;

namespace MiniCommerce.Identity.Web.Endpoints.V1.Users;

public class LoginUserEndpoint : IEndpointV1
{
    public const string Route = "api/v1/users/login";

    public record LoginUserRequest(string Email);

    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("users/login", async (
            LoginUserRequest request,
            ICommandHandler<LoginUserCommand, TokenResponse> handler,
            CancellationToken cancellationToken) =>
        {
            var command = new LoginUserCommand(request.Email);
            var result = await handler.HandleAsync(command, cancellationToken);
            return result.Match(Results.Ok, CustomResults.Problem);
        })
        .WithTags(Tags.Users)
        .Produces<TokenResponse>(StatusCodes.Status200OK)
        .ProducesValidationProblem(StatusCodes.Status400BadRequest);
    }
}
