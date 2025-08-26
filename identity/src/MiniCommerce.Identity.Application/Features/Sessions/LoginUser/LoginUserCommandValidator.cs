namespace MiniCommerce.Identity.Application.Features.Sessions.LoginUser;

public class LoginUserCommandValidator : AbstractValidator<LoginUserCommand>
{
    public LoginUserCommandValidator()
    {
        RuleFor(c => c.Email)
            .EmailAddress();
    }
}
