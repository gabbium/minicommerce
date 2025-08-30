namespace MiniCommerce.Catalog.Application.UseCases.Products.ListProducts;

public class ListProductsQueryValidator : AbstractValidator<ListProductsQuery>
{
    public ListProductsQueryValidator()
    {
        RuleFor(q => q.PageNumber)
            .NotEmpty();

        RuleFor(q => q.PageSize)
            .NotEmpty();
    }
}

