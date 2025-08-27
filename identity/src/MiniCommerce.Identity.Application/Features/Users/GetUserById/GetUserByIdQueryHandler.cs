using MiniCommerce.Identity.Application.Contracts.Users;
using MiniCommerce.Identity.Domain.Aggregates.Users;

namespace MiniCommerce.Identity.Application.Features.Users.GetUserById;

public class GetUserByIdQueryHandler(IUserRepository userRepository) : IQueryHandler<GetUserByIdQuery, UserResponse>
{
    public async Task<Result<UserResponse>> HandleAsync(GetUserByIdQuery query, CancellationToken cancellationToken = default)
    {
        var user = await userRepository.GetByIdAsync(query.Id, cancellationToken);

        if (user is null)
            return Result.Failure<UserResponse>(UserErrors.NotFound);

        return new UserResponse(user.Id, user.Email);
    }
}
