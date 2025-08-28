namespace MiniCommerce.Identity.Application.Features.Permissions.GetPermissionById;

public class GetPermissionByIdQueryValidator : AbstractValidator<GetPermissionByIdQuery>
{
    public GetPermissionByIdQueryValidator()
    {
        RuleFor(q => q.Id)
            .NotEmpty();
    }
}
