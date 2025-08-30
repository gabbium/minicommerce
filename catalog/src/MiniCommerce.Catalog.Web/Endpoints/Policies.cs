namespace MiniCommerce.Catalog.Web.Endpoints;

public static class Policies
{
    public const string ClaimType = "https://gabbium.dev/claims/permission";

    public const string CanListProducts = "catalog:products.list";
    public const string CanGetProductById = "catalog:products.get.byId";
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
