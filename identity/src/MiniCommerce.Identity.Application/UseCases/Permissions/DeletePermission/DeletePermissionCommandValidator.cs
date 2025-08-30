namespace MiniCommerce.Identity.Application.UseCases.Permissions.DeletePermission;

public class DeletePermissionCommandValidator : AbstractValidator<DeletePermissionCommand>
{
    public DeletePermissionCommandValidator()
    {
        RuleFor(c => c.Id)
            .NotEmpty();
    }
}
