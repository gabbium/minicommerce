using MiniCommerce.Identity.Application.Commands.Login;
using MiniCommerce.Identity.Application.Models;

namespace MiniCommerce.Identity.Web.Endpoints.V1;

public class LoginEndpoint : IEndpointV1
{
    public record Request(string Email);

    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("login", async (
            Request request,
            ICommandHandler<LoginCommand, AuthResponse> handler,
            CancellationToken cancellationToken) =>
        {
            var command = new LoginCommand(request.Email);
            var result = await handler.HandleAsync(command, cancellationToken);
            return result.Match(Results.Ok, CustomResults.Problem);
        })
        .WithTags(Tags.Identity);
    }
}
