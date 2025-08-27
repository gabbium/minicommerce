using MiniCommerce.Identity.Application.Contracts.Sessions;
using MiniCommerce.Identity.Application.Features.Sessions.LoginUser;
using MiniCommerce.Identity.Web.Endpoints.Common;

namespace MiniCommerce.Identity.Web.Endpoints.V1.Sessions;

public class LoginUserEndpoint : IEndpointV1
{
    public const string Route = "api/v1/sessions/login";

    public record LoginUserRequest(string Email);

    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("sessions/login", async (
            LoginUserRequest request,
            ICommandHandler<LoginUserCommand, TokenResponse> handler,
            CancellationToken cancellationToken) =>
        {
            var command = new LoginUserCommand(request.Email);
            var result = await handler.HandleAsync(command, cancellationToken);
            return result.Match(Results.Ok, CustomResults.Problem);
        })
        .WithTags(Tags.Sessions)
        .Produces<TokenResponse>(StatusCodes.Status200OK)
        .ProducesValidationProblem(StatusCodes.Status400BadRequest);
    }
}
