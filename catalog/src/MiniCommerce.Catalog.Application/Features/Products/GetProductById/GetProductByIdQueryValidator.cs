namespace MiniCommerce.Catalog.Application.Features.Products.GetProductById;

public class GetProductByIdQueryValidator : AbstractValidator<GetProductByIdQuery>
{
    public GetProductByIdQueryValidator()
    {
        RuleFor(q => q.Id)
            .NotEmpty();
    }
}
