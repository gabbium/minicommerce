namespace MiniCommerce.Identity.Application.UseCases.Users.LoginUser;

public class LoginUserCommandValidator : AbstractValidator<LoginUserCommand>
{
    public LoginUserCommandValidator()
    {
        RuleFor(c => c.Email)
            .EmailAddress();
    }
}
