namespace MiniCommerce.Identity.Application.Features.Permissions.ListPermissions;

public class ListPermissionsQueryValidator : AbstractValidator<ListPermissionsQuery>
{
    public ListPermissionsQueryValidator()
    {
        RuleFor(q => q.Page)
            .NotEmpty();

        RuleFor(q => q.PageSize)
            .NotEmpty();
    }
}
