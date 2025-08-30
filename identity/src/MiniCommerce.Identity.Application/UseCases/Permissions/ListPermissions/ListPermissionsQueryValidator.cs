namespace MiniCommerce.Identity.Application.UseCases.Permissions.ListPermissions;

public class ListPermissionsQueryValidator : AbstractValidator<ListPermissionsQuery>
{
    public ListPermissionsQueryValidator()
    {
        RuleFor(q => q.PageNumber)
            .NotEmpty();

        RuleFor(q => q.PageSize)
            .NotEmpty();
    }
}
