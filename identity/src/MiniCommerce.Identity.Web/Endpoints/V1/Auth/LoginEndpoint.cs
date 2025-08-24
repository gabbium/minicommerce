using MiniCommerce.Identity.Application.Features.Auth.Login;
using MiniCommerce.Identity.Application.Models;

namespace MiniCommerce.Identity.Web.Endpoints.V1.Auth;

public class LoginEndpoint : IEndpointV1
{
    public const string Route = "api/v1/auth/login";

    public record Request(string Email);

    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("auth/login", async (
            Request request,
            ICommandHandler<LoginCommand, AuthResponse> handler,
            CancellationToken cancellationToken) =>
        {
            var command = new LoginCommand(request.Email);
            var result = await handler.HandleAsync(command, cancellationToken);
            return result.Match(Results.Ok, CustomResults.Problem);
        })
        .WithTags(Tags.Auth);
    }
}
