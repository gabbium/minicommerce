using MiniCommerce.Catalog.Infrastructure.Security;
using MiniCommerce.Catalog.Web.AcceptanceTests.TestHelpers;

namespace MiniCommerce.Catalog.Web.AcceptanceTests.Endpoints.Products.CreateProduct;

[Collection(nameof(TestCollection))]
public class CreateProductFeature(TestFixture fixture) : TestBase(fixture)
{
    private readonly CreateProductSteps _steps = new(fixture);

    [Fact]
    public async Task UserCreatesProductWithValidData()
    {
        await _steps.GivenAnAuthenticatedUser(Permissions.CanCreateProduct);
        await _steps.WhenTheyAttemptToCreateProduct(new("SKU-001", "Bluetooth Headphones", 129.50m));
        await _steps.ThenResponseIs201Created();
        await _steps.ThenResponseContainsProduct("SKU-001", "Bluetooth Headphones", 129.50m);
    }

    [Fact]
    public async Task UserAttemptsToCreateProductWithEmptySku()
    {
        await _steps.GivenAnAuthenticatedUser(Permissions.CanCreateProduct);
        await _steps.WhenTheyAttemptToCreateProduct(new(string.Empty, "Bluetooth Headphones", 129.50m));
        await _steps.ThenResponseIs400BadRequest();
        await _steps.ThenResponseIsValidationProblemDetails(new()
        {
            ["Sku"] = ["'Sku' must not be empty."]
        });
    }

    [Fact]
    public async Task UserAttemptsToCreateProductWithEmptyName()
    {
        await _steps.GivenAnAuthenticatedUser(Permissions.CanCreateProduct);
        await _steps.WhenTheyAttemptToCreateProduct(new("SKU-001", string.Empty, 129.50m));
        await _steps.ThenResponseIs400BadRequest();
        await _steps.ThenResponseIsValidationProblemDetails(new()
        {
            ["Name"] = ["'Name' must not be empty."]
        });
    }

    [Fact]
    public async Task UserAttemptsToCreateProductWithEmptyPrice()
    {
        await _steps.GivenAnAuthenticatedUser(Permissions.CanCreateProduct);
        await _steps.WhenTheyAttemptToCreateProduct(new("SKU-001", "Bluetooth Headphones", 0m));
        await _steps.ThenResponseIs400BadRequest();
        await _steps.ThenResponseIsValidationProblemDetails(new()
        {
            ["Price"] = ["'Price' must not be empty."]
        });
    }

    [Fact]
    public async Task AnonymousUserAttemptsToCreateProduct()
    {
        await _steps.GivenAnAnonymousUser();
        await _steps.WhenTheyAttemptToCreateProduct(new("SKU-001", "Bluetooth Headphones", 129.50m));
        await _steps.ThenResponseIs401Unauthorized();
    }

    [Fact]
    public async Task ForbiddenUserAttemptsToCreateProduct()
    {
        await _steps.GivenAnAuthenticatedUser();
        await _steps.WhenTheyAttemptToCreateProduct(new("SKU-001", "Bluetooth Headphones", 129.50m));
        await _steps.ThenResponseIs403Forbidden();
    }
}
