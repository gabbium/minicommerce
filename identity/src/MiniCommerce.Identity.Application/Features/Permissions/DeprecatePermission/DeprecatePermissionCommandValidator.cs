namespace MiniCommerce.Identity.Application.Features.Permissions.DeprecatePermission;

public class DeprecatePermissionCommandValidator : AbstractValidator<DeprecatePermissionCommand>
{
    public DeprecatePermissionCommandValidator()
    {
        RuleFor(c => c.Id)
            .NotEmpty();
    }
}
