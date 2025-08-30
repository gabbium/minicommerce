namespace MiniCommerce.Identity.Application.UseCases.Permissions.CreatePermission;

public class CreatePermissionCommandValidator : AbstractValidator<CreatePermissionCommand>
{
    public CreatePermissionCommandValidator()
    {
        RuleFor(c => c.Code)
            .NotEmpty();
    }
}
