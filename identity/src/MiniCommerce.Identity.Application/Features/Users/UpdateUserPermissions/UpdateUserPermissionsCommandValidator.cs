namespace MiniCommerce.Identity.Application.Features.Users.UpdateUserPermissions;

public class UpdateUserPermissionsCommandValidator : AbstractValidator<UpdateUserPermissionsCommand>
{
    public UpdateUserPermissionsCommandValidator()
    {
        RuleFor(c => c.UserId)
            .NotEmpty();
    }
}
