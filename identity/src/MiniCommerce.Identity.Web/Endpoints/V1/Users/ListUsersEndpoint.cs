using MiniCommerce.Identity.Application.Common.Models;
using MiniCommerce.Identity.Application.Contracts;
using MiniCommerce.Identity.Application.Contracts.Users;
using MiniCommerce.Identity.Application.Features.Users.ListUsers;
using MiniCommerce.Identity.Web.Endpoints.Common;

namespace MiniCommerce.Identity.Web.Endpoints.V1.Users;

public class ListUsersEndpoint : IEndpointV1
{
    public record ListUsersRequest(int Page, int PageSize);

    public static string BuildRoute(ListUsersRequest request) => $"api/v1/users?page={request.Page}&pageSize={request.PageSize}";

    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("users", async (
            [AsParameters] ListUsersRequest request,
            IQueryHandler<ListUsersQuery, PagedList<UserResponse>> handler,
            CancellationToken cancellationToken) =>
        {
            var query = new ListUsersQuery(request.Page, request.PageSize);
            var result = await handler.HandleAsync(query, cancellationToken);
            return result.Match(Results.Ok, CustomResults.Problem);
        })
        .RequireAuthorization(IdentityPermissionNames.CanListUsers)
        .WithTags(Tags.Users)
        .Produces<PagedList<UserResponse>>(StatusCodes.Status200OK)
        .ProducesValidationProblem(StatusCodes.Status400BadRequest)
        .ProducesProblem(StatusCodes.Status401Unauthorized)
        .ProducesProblem(StatusCodes.Status403Forbidden);
    }
}
