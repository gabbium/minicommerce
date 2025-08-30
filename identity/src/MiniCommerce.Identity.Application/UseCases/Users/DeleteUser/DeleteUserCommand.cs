namespace MiniCommerce.Identity.Application.UseCases.Users.DeleteUser;

public record DeleteUserCommand(Guid Id) : ICommand;
