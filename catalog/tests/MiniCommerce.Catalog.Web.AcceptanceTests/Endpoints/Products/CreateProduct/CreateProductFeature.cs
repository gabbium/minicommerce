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
        await _steps.WhenTheyAttemptToCreateProduct("SKU-001", "Bluetooth Headphones", 129.50m);
        await _steps.ThenTheResponseShouldBe201Created();
        await _steps.ThenTheResponseShouldContainProduct();
    }

    [Fact]
    public async Task UserAttemptsToCreateProductWithEmptySku()
    {
        await _steps.GivenAnAuthenticatedUser(Permissions.CanCreateProduct);
        await _steps.WhenTheyAttemptToCreateProduct(string.Empty, "Bluetooth Headphones", 129.50m);
        await _steps.ThenTheResponseShouldBe400BadRequest();
        await _steps.ThenTheResponseShouldBeValidationProblemDetails(new()
        {
            ["Sku"] = ["'Sku' must not be empty."]
        });
    }

    [Fact]
    public async Task UserAttemptsToCreateProductWithEmptyName()
    {
        await _steps.GivenAnAuthenticatedUser(Permissions.CanCreateProduct);
        await _steps.WhenTheyAttemptToCreateProduct("SKU-001", string.Empty, 129.50m);
        await _steps.ThenTheResponseShouldBe400BadRequest();
        await _steps.ThenTheResponseShouldBeValidationProblemDetails(new()
        {
            ["Name"] = ["'Name' must not be empty."]
        });
    }

    [Fact]
    public async Task UserAttemptsToCreateProductWithEmptyPrice()
    {
        await _steps.GivenAnAuthenticatedUser(Permissions.CanCreateProduct);
        await _steps.WhenTheyAttemptToCreateProduct("SKU-001", "Bluetooth Headphones", 0m);
        await _steps.ThenTheResponseShouldBe400BadRequest();
        await _steps.ThenTheResponseShouldBeValidationProblemDetails(new()
        {
            ["Price"] = ["'Price' must not be empty."]
        });
    }

    [Fact]
    public async Task AnonymousUserAttemptsToCreateProduct()
    {
        await _steps.GivenAnAnonymousUser();
        await _steps.WhenTheyAttemptToCreateProduct("SKU-001", "Bluetooth Headphones", 129.50m);
        await _steps.ThenTheResponseShouldBe401Unauthorized();
    }

    [Fact]
    public async Task ForbiddenUserAttemptsToCreateProduct()
    {
        await _steps.GivenAnAuthenticatedUser();
        await _steps.WhenTheyAttemptToCreateProduct("SKU-001", "Bluetooth Headphones", 129.50m);
        await _steps.ThenTheResponseShouldBe403Forbidden();
    }
}
