namespace MiniCommerce.Catalog.Domain.ProductAggregate.Errors;

public static class ProductErrors
{
    public static Error NotFound => Error.NotFound("Products.NotFound", "The specified product was not found.");

    public static Error SkuAlreadyExists => Error.Conflict("Products.SkuAlreadyExists", "The specified SKU is already in use.");
}
