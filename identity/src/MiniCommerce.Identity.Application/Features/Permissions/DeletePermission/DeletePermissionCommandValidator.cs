namespace MiniCommerce.Identity.Application.Features.Permissions.DeletePermission;

public class DeletePermissionCommandValidator : AbstractValidator<DeletePermissionCommand>
{
    public DeletePermissionCommandValidator()
    {
        RuleFor(c => c.Id)
            .NotEmpty();
    }
}
