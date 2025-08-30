namespace MiniCommerce.Identity.Application.UseCases.Users.CreateUser;

public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
{
    public CreateUserCommandValidator()
    {
        RuleFor(c => c.Email)
            .EmailAddress();
    }
}
