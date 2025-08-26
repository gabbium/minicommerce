namespace MiniCommerce.Identity.Application.Features.Permissions.CreatePermission;

public class CreatePermissionCommandValidator : AbstractValidator<CreatePermissionCommand>
{
    public CreatePermissionCommandValidator()
    {
        RuleFor(c => c.Code)
            .NotEmpty();
    }
}
