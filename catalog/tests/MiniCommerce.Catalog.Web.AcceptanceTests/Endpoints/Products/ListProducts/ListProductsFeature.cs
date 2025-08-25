using MiniCommerce.Catalog.Infrastructure.Security;
using MiniCommerce.Catalog.Web.AcceptanceTests.TestHelpers;

namespace MiniCommerce.Catalog.Web.AcceptanceTests.Endpoints.Products.ListProducts;

[Collection(nameof(TestCollection))]
public class ListProductsFeature(TestFixture fixture) : TestBase(fixture)
{
    private readonly ListProductsSteps _steps = new(fixture);

    [Fact]
    public async Task UserListsProducts()
    {
        await _steps.GivenAnAuthenticatedUser(Permissions.CanCreateProduct, Permissions.CanListProducts);
        await _steps.GivenProductsExist(
        [
            new("SKU-001", "Bluetooth Headphones", 129.50m),
            new("SKU-002", "Wireless Mouse", 79.90m),
            new("SKU-003", "Mechanical Keyboard", 299.00m)
        ]);
        await _steps.WhenTheyAttemptToListProducts(new(1, 2));
        await _steps.ThenResponseIs200Ok();
        await _steps.ThenResponseContainsProducts(1, 2, 2, 3, 2);
    }

    [Fact]
    public async Task AnonymousUserAttemptsToListProducts()
    {
        await _steps.GivenAnAnonymousUser();
        await _steps.WhenTheyAttemptToListProducts(new(1, 2));
        await _steps.ThenResponseIs401Unauthorized();
    }

    [Fact]
    public async Task ForbiddenUserAttemptsToListProducts()
    {
        await _steps.GivenAnAuthenticatedUser();
        await _steps.WhenTheyAttemptToListProducts(new(1, 2));
        await _steps.ThenResponseIs403Forbidden();
    }
}
