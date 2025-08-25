using MiniCommerce.Identity.Domain.ValueObjects;

namespace MiniCommerce.Identity.Infrastructure.Security;

public static class Permissions
{
    public static readonly PermissionDefinition CatalogCanListProducts =
        new("catalog:products.list", Role.User);

    public static readonly PermissionDefinition CatalogCanGetProductById =
        new("catalog:products.getbyid", Role.User);

    public static readonly PermissionDefinition CatalogCanCreateProduct =
        new("catalog:products.create", Role.Administrator);

    public static readonly PermissionDefinition CatalogCanUpdateProduct =
        new("catalog:products.update", Role.Administrator);

    public static readonly PermissionDefinition CatalogCanDeleteProduct =
        new("catalog:products.delete", Role.Administrator);

    public static IEnumerable<PermissionDefinition> All
    {
        get
        {
            yield return CatalogCanListProducts;
            yield return CatalogCanGetProductById;
            yield return CatalogCanCreateProduct;
            yield return CatalogCanUpdateProduct;
            yield return CatalogCanDeleteProduct;
        }
    }
}
