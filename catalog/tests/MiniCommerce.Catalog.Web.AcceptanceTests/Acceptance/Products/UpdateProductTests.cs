using MiniCommerce.Catalog.Application.Contracts;
using MiniCommerce.Catalog.Web.AcceptanceTests.Steps;
using MiniCommerce.Catalog.Web.AcceptanceTests.TestHelpers.Fixtures;
using MiniCommerce.Catalog.Web.Endpoints;

namespace MiniCommerce.Catalog.Web.AcceptanceTests.Acceptance.Products;

[Collection(nameof(TestCollection))]
public class UpdateProductTests(TestFixture fixture) : TestBase(fixture)
{
    private readonly ProductSteps _steps = new(fixture);

    [Fact]
    public async Task UserUpdatesProduct()
    {
        await _steps.GivenAnAuthenticatedUser(Policies.CanCreateProduct, Policies.CanUpdateProduct);
        var id = await _steps.GivenAnExistingProduct(new("SKU-001", "Bluetooth Headphones", 129.50m));
        await _steps.WhenTheyAttemptToUpdateProduct(id, new("Wireless Mouse", 79.90m));
        await _steps.ThenResponseIs200Ok();
        await _steps.ThenResponseMatches<ProductResponse>(product =>
        {
            Assert.Equal(id, product.Id);
            Assert.Equal("SKU-001", product.Sku);
            Assert.Equal("Wireless Mouse", product.Name);
            Assert.Equal(79.90m, product.Price);
        });
    }

    [Fact]
    public async Task UserAttemptsToUpdateProductWithEmptyId()
    {
        await _steps.GivenAnAuthenticatedUser(Policies.CanUpdateProduct);
        await _steps.WhenTheyAttemptToUpdateProduct(Guid.Empty, new("Wireless Mouse", 79.90m));
        await _steps.ThenResponseIs400BadRequest();
        await _steps.ThenResponseIsValidationProblemDetails(new()
        {
            ["Id"] = ["'Id' must not be empty."]
        });
    }

    [Fact]
    public async Task UserAttemptsToUpdateProductWithEmptyName()
    {
        await _steps.GivenAnAuthenticatedUser(Policies.CanCreateProduct, Policies.CanUpdateProduct);
        var id = await _steps.GivenAnExistingProduct(new("SKU-001", "Bluetooth Headphones", 129.50m));
        await _steps.WhenTheyAttemptToUpdateProduct(id, new(string.Empty, 79.90m));
        await _steps.ThenResponseIs400BadRequest();
        await _steps.ThenResponseIsValidationProblemDetails(new()
        {
            ["Name"] = ["'Name' must not be empty."]
        });
    }

    [Fact]
    public async Task UserAttemptsToUpdateProductWithEmptyPrice()
    {
        await _steps.GivenAnAuthenticatedUser(Policies.CanCreateProduct, Policies.CanUpdateProduct);
        var id = await _steps.GivenAnExistingProduct(new("SKU-001", "Bluetooth Headphones", 129.50m));
        await _steps.WhenTheyAttemptToUpdateProduct(id, new("Wireless Mouse", 0m));
        await _steps.ThenResponseIs400BadRequest();
        await _steps.ThenResponseIsValidationProblemDetails(new()
        {
            ["Price"] = ["'Price' must not be empty."]
        });
    }

    [Fact]
    public async Task AnonymousUserAttemptsToUpdateProduct()
    {
        await _steps.GivenAnAnonymousUser();
        await _steps.WhenTheyAttemptToUpdateProduct(Guid.NewGuid(), new("Wireless Mouse", 79.90m));
        await _steps.ThenResponseIs401Unauthorized();
    }

    [Fact]
    public async Task ForbiddenUserAttemptsToUpdateProduct()
    {
        await _steps.GivenAnAuthenticatedUser();
        await _steps.WhenTheyAttemptToUpdateProduct(Guid.NewGuid(), new("Wireless Mouse", 79.90m));
        await _steps.ThenResponseIs403Forbidden();
    }

    [Fact]
    public async Task UserAttemptsToUpdateNonExistentProduct()
    {
        await _steps.GivenAnAuthenticatedUser(Policies.CanUpdateProduct);
        await _steps.WhenTheyAttemptToUpdateProduct(Guid.NewGuid(), new("Wireless Mouse", 79.90m));
        await _steps.ThenResponseIs404NotFound();
        await _steps.ThenResponseIsProblemDetails("Products.NotFound", "The specified product was not found.");
    }
}
