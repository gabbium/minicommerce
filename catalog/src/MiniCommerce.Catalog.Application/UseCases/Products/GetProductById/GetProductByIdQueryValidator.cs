namespace MiniCommerce.Catalog.Application.UseCases.Products.GetProductById;

public class GetProductByIdQueryValidator : AbstractValidator<GetProductByIdQuery>
{
    public GetProductByIdQueryValidator()
    {
        RuleFor(q => q.Id)
            .NotEmpty();
    }
}
