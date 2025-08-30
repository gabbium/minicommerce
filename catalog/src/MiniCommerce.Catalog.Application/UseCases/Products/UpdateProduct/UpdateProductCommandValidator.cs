namespace MiniCommerce.Catalog.Application.UseCases.Products.UpdateProduct;

public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
{
    public UpdateProductCommandValidator()
    {
        RuleFor(c => c.Id)
            .NotEmpty();

        RuleFor(c => c.Name)
            .NotEmpty();

        RuleFor(c => c.Price)
            .NotEmpty();
    }
}
