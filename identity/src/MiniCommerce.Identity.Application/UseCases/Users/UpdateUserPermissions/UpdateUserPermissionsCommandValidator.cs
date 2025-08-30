namespace MiniCommerce.Identity.Application.UseCases.Users.UpdateUserPermissions;

public class UpdateUserPermissionsCommandValidator : AbstractValidator<UpdateUserPermissionsCommand>
{
    public UpdateUserPermissionsCommandValidator()
    {
        RuleFor(c => c.UserId)
            .NotEmpty();
    }
}
