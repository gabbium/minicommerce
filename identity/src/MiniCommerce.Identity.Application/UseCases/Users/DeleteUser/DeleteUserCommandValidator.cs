namespace MiniCommerce.Identity.Application.UseCases.Users.DeleteUser;

public class DeleteUserCommandValidator : AbstractValidator<DeleteUserCommand>
{
    public DeleteUserCommandValidator()
    {
        RuleFor(c => c.Id)
            .NotEmpty();
    }
}
