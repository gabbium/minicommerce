using MiniCommerce.Catalog.Application.Contracts;
using MiniCommerce.Catalog.Web.AcceptanceTests.TestHelpers;

namespace MiniCommerce.Catalog.Web.AcceptanceTests.Endpoints.V1.Products.GetProductById;

[Collection(nameof(TestCollection))]
public class GetProductByIdFeature(TestFixture fixture) : TestBase(fixture)
{
    private readonly GetProductByIdSteps _steps = new(fixture);

    [Fact]
    public async Task UserGetsProductById()
    {
        await _steps.GivenAnAuthenticatedUser(CatalogPermissionNames.CanCreateProduct, CatalogPermissionNames.CanGetProductById);
        var id = await _steps.GivenAnExistingProduct(new("SKU-001", "Bluetooth Headphones", 129.50m));
        await _steps.WhenTheyAttemptToGetProductById(id);
        await _steps.ThenResponseIs200Ok();
        await _steps.ThenResponseContainsProduct(id, "SKU-001", "Bluetooth Headphones", 129.50m);
    }

    [Fact]
    public async Task UserAttemptsToGetProductWithEmptyId()
    {
        await _steps.GivenAnAuthenticatedUser(CatalogPermissionNames.CanGetProductById);
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
        await _steps.GivenAnAuthenticatedUser(CatalogPermissionNames.CanGetProductById);
        await _steps.WhenTheyAttemptToGetProductById(Guid.NewGuid());
        await _steps.ThenResponseIs404NotFound();
        await _steps.ThenResponseIsProblemDetails("Products.NotFound", "The specified product was not found.");
    }
}
