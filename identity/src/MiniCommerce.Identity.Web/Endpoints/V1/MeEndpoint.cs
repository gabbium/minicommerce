using MiniCommerce.Identity.Application.Abstractions;

namespace MiniCommerce.Identity.Web.Endpoints.V1;

public class MeEndpoint : IEndpointV1
{
    public record Response(Guid Id, string Email);

    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("me", (IUserContext userContext) =>
        {
            return Results.Ok(new Response(userContext.UserId, userContext.Email));
        })
        .RequireAuthorization()
        .WithTags(Tags.Identity);
    }
}
