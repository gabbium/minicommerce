using MiniCommerce.Catalog.Web.AcceptanceTests.Steps;
using MiniCommerce.Catalog.Web.AcceptanceTests.TestHelpers;
using MiniCommerce.Catalog.Web.Endpoints;

namespace MiniCommerce.Catalog.Web.AcceptanceTests.Acceptance.Products;

[Collection(nameof(TestCollection))]
public class DeleteProductTests(TestFixture fixture) : TestBase(fixture)
{
    private readonly ProductSteps _steps = new(fixture);

    [Fact]
    public async Task UserDeletesProduct()
    {
        await _steps.GivenAnAuthenticatedUser(Policies.CanCreateProduct, Policies.CanDeleteProduct);
        var id = await _steps.GivenAnExistingProduct(new("SKU-001", "Bluetooth Headphones", 129.50m));
        await _steps.WhenTheyAttemptToDeleteProduct(id);
        await _steps.ThenResponseIs204NoContent();
    }

    [Fact]
    public async Task UserAttemptsToDeleteProductWithEmptyId()
    {
        await _steps.GivenAnAuthenticatedUser(Policies.CanDeleteProduct);
        await _steps.WhenTheyAttemptToDeleteProduct(Guid.Empty);
        await _steps.ThenResponseIs400BadRequest();
        await _steps.ThenResponseIsValidationProblemDetails(new()
        {
            ["Id"] = ["'Id' must not be empty."]
        });
    }

    [Fact]
    public async Task AnonymousUserAttemptsToDeleteProduct()
    {
        await _steps.GivenAnAnonymousUser();
        await _steps.WhenTheyAttemptToDeleteProduct(Guid.Empty);
        await _steps.ThenResponseIs401Unauthorized();
    }

    [Fact]
    public async Task ForbiddenUserAttemptsToDeleteProduct()
    {
        await _steps.GivenAnAuthenticatedUser();
        await _steps.WhenTheyAttemptToDeleteProduct(Guid.Empty);
        await _steps.ThenResponseIs403Forbidden();
    }

    [Fact]
    public async Task UserAttemptsToDeleteNonExistentProduct()
    {
        await _steps.GivenAnAuthenticatedUser(Policies.CanDeleteProduct);
        await _steps.WhenTheyAttemptToDeleteProduct(Guid.NewGuid());
        await _steps.ThenResponseIs404NotFound();
        await _steps.ThenResponseIsProblemDetails("Products.NotFound", "The specified product was not found.");
    }
}
