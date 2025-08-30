namespace MiniCommerce.Catalog.Application.UseCases.Products.CreateProduct;

public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
{
    public CreateProductCommandValidator()
    {
        RuleFor(c => c.Sku)
            .NotEmpty();

        RuleFor(c => c.Name)
            .NotEmpty();

        RuleFor(c => c.Price)
            .NotEmpty();
    }
}
