using MiniCommerce.Identity.Application.Abstractions;
using MiniCommerce.Identity.Application.Contracts.Users;

namespace MiniCommerce.Identity.Application.Features.Users.GetCurrentUser;

public class GetCurrentUserQueryHandler(IUserContext userContext) : IQueryHandler<GetCurrentUserQuery, UserResponse>
{
    public Task<Result<UserResponse>> HandleAsync(GetCurrentUserQuery query, CancellationToken cancellationToken = default)
    {
        var response = new UserResponse(userContext.UserId, userContext.Email);
        return Task.FromResult(Result.Success(response));
    }
}
