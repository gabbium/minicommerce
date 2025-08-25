namespace MiniCommerce.Catalog.Application.Features.Products;

public static class ProductErrors
{
    public static Error NotFound => Error.NotFound("Products.NotFound", "The specified product was not found.");
}
