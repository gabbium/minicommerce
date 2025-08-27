namespace MiniCommerce.Identity.Application.Features.Users.ListUsers;

public class ListUsersQueryValidator : AbstractValidator<ListUsersQuery>
{
    public ListUsersQueryValidator()
    {
        RuleFor(q => q.Page)
            .NotEmpty();

        RuleFor(q => q.PageSize)
            .NotEmpty();
    }
}
