namespace MiniCommerce.Identity.Application.UseCases.Permissions.DeprecatePermission;

public class DeprecatePermissionCommandValidator : AbstractValidator<DeprecatePermissionCommand>
{
    public DeprecatePermissionCommandValidator()
    {
        RuleFor(c => c.Id)
            .NotEmpty();
    }
}
