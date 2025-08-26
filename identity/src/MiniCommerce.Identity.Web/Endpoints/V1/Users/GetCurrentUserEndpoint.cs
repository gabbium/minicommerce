using MiniCommerce.Identity.Application.Contracts.Users;
using MiniCommerce.Identity.Application.Features.Users.GetCurrentUser;
using MiniCommerce.Identity.Web.Endpoints.Common;

namespace MiniCommerce.Identity.Web.Endpoints.V1.Users;

public class GetCurrentUserEndpoint : IEndpointV1
{
    public const string Route = "api/v1/users/me";

    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("users/me", async (
            IQueryHandler<GetCurrentUserQuery, UserResponse> handler,
            CancellationToken cancellationToken) =>
        {
            var query = new GetCurrentUserQuery();
            var result = await handler.HandleAsync(query, cancellationToken);
            return result.Match(Results.Ok, CustomResults.Problem);
        })
        .RequireAuthorization()
        .WithTags(Tags.Users);
    }
}
