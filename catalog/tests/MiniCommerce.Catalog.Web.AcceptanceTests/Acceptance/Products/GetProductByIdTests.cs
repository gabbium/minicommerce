using MiniCommerce.Catalog.Application.Contracts;
using MiniCommerce.Catalog.Web.AcceptanceTests.Steps;
using MiniCommerce.Catalog.Web.AcceptanceTests.TestHelpers.Fixtures;
using MiniCommerce.Catalog.Web.Endpoints;

namespace MiniCommerce.Catalog.Web.AcceptanceTests.Acceptance.Products;

[Collection(nameof(TestCollection))]
public class GetProductByIdTests(TestFixture fixture) : TestBase(fixture)
{
    private readonly ProductSteps _steps = new(fixture);

    [Fact]
    public async Task UserGetsProductById()
    {
        await _steps.GivenAnAuthenticatedUser(Policies.CanCreateProduct, Policies.CanGetProductById);
        var id = await _steps.GivenAnExistingProduct(new("SKU-001", "Bluetooth Headphones", 129.50m));
        await _steps.WhenTheyAttemptToGetProductById(id);
        await _steps.ThenResponseIs200Ok();
        await _steps.ThenResponseMatches<ProductResponse>(product =>
        {
            Assert.Equal(id, product.Id);
            Assert.Equal("SKU-001", product.Sku);
            Assert.Equal("Bluetooth Headphones", product.Name);
            Assert.Equal(129.50m, product.Price);
        });
    }

    [Fact]
    public async Task UserAttemptsToGetProductWithEmptyId()
    {
        await _steps.GivenAnAuthenticatedUser(Policies.CanGetProductById);
        await _steps.WhenTheyAttemptToGetProductById(Guid.Empty);
        await _steps.ThenResponseIs400BadRequest();
        await _steps.ThenResponseIsValidationProblemDetails(new()
        {
            ["Id"] = ["'Id' must not be empty."]
        });
    }

    [Fact]
    public async Task AnonymousUserAttemptsToGetProductById()
    {
        await _steps.GivenAnAnonymousUser();
        await _steps.WhenTheyAttemptToGetProductById(Guid.NewGuid());
        await _steps.ThenResponseIs401Unauthorized();
    }

    [Fact]
    public async Task ForbiddenUserAttemptsToGetProductById()
    {
        await _steps.GivenAnAuthenticatedUser();
        await _steps.WhenTheyAttemptToGetProductById(Guid.NewGuid());
        await _steps.ThenResponseIs403Forbidden();
    }

    [Fact]
    public async Task UserAttemptsToGetNonExistentProduct()
    {
        await _steps.GivenAnAuthenticatedUser(Policies.CanGetProductById);
        await _steps.WhenTheyAttemptToGetProductById(Guid.NewGuid());
        await _steps.ThenResponseIs404NotFound();
        await _steps.ThenResponseIsProblemDetails("Products.NotFound", "The specified product was not found.");
    }
}
