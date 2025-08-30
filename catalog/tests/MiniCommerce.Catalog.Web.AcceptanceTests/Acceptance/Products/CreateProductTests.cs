using MiniCommerce.Catalog.Application.Contracts;
using MiniCommerce.Catalog.Web.AcceptanceTests.Steps;
using MiniCommerce.Catalog.Web.AcceptanceTests.TestHelpers;
using MiniCommerce.Catalog.Web.Endpoints;

namespace MiniCommerce.Catalog.Web.AcceptanceTests.Acceptance.Products;

[Collection(nameof(TestCollection))]
public class CreateProductTests(TestFixture fixture) : TestBase(fixture)
{
    private readonly ProductSteps _steps = new(fixture);

    [Fact]
    public async Task UserCreatesProductWithValidData()
    {
        await _steps.GivenAnAuthenticatedUser(Policies.CanCreateProduct);
        await _steps.WhenTheyAttemptToCreateProduct(new("SKU-001", "Bluetooth Headphones", 129.50m));
        await _steps.ThenResponseIs201Created();
        await _steps.ThenResponseMatches<ProductResponse>(product =>
        {
            Assert.NotEqual(Guid.Empty, product.Id);
            Assert.Equal("SKU-001", product.Sku);
            Assert.Equal("Bluetooth Headphones", product.Name);
            Assert.Equal(129.50m, product.Price);
        });
    }

    [Fact]
    public async Task UserAttemptsToCreateProductWithEmptySku()
    {
        await _steps.GivenAnAuthenticatedUser(Policies.CanCreateProduct);
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
        await _steps.GivenAnAuthenticatedUser(Policies.CanCreateProduct);
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
        await _steps.GivenAnAuthenticatedUser(Policies.CanCreateProduct);
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
