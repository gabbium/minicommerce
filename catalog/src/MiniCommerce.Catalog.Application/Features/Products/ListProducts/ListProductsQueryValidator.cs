namespace MiniCommerce.Catalog.Application.Features.Products.ListProducts;

public class ListProductsQueryValidator : AbstractValidator<ListProductsQuery>
{
    public ListProductsQueryValidator()
    {
        RuleFor(q => q.Page)
            .NotEmpty();

        RuleFor(q => q.PageSize)
            .NotEmpty();
    }
}

