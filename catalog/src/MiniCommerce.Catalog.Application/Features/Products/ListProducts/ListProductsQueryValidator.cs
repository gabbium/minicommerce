namespace MiniCommerce.Catalog.Application.Features.Products.ListProducts;

public class ListProductsQueryValidator : AbstractValidator<ListProductsQuery>
{
    public ListProductsQueryValidator()
    {
        RuleFor(x => x.Page)
            .NotEmpty();

        RuleFor(x => x.PageSize)
            .NotEmpty();
    }
}

