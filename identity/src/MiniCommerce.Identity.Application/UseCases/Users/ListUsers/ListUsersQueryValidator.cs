namespace MiniCommerce.Identity.Application.UseCases.Users.ListUsers;

public class ListUsersQueryValidator : AbstractValidator<ListUsersQuery>
{
    public ListUsersQueryValidator()
    {
        RuleFor(q => q.PageNumber)
            .NotEmpty();

        RuleFor(q => q.PageSize)
            .NotEmpty();
    }
}
