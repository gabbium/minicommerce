namespace MiniCommerce.Identity.Application.UseCases.Permissions.GetPermissionById;

public class GetPermissionByIdQueryValidator : AbstractValidator<GetPermissionByIdQuery>
{
    public GetPermissionByIdQueryValidator()
    {
        RuleFor(q => q.Id)
            .NotEmpty();
    }
}
