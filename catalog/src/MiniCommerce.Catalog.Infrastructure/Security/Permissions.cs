namespace MiniCommerce.Catalog.Infrastructure.Security;

public static class Permissions
{
    public const string CanListProducts = "catalog:products.list";
    public const string CanGetProductById = "catalog:products.getbyid";
    public const string CanCreateProduct = "catalog:products.create";
    public const string CanUpdateProduct = "catalog:products.update";
    public const string CanDeleteProduct = "catalog:products.delete";

    public static IEnumerable<string> All
    {
        get
        {
            yield return CanListProducts;
            yield return CanGetProductById;
            yield return CanCreateProduct;
            yield return CanUpdateProduct;
            yield return CanDeleteProduct;
        }
    }
}
