using MiniCommerce.Catalog.Infrastructure.Security;
using MiniCommerce.Catalog.Web.AcceptanceTests.TestHelpers;

namespace MiniCommerce.Catalog.Web.AcceptanceTests.Endpoints.Products.GetProductById;

[Collection(nameof(TestCollection))]
public class GetUserByIdFeature(TestFixture fixture) : TestBase(fixture)
{
    private readonly GetProductByIdSteps _steps = new(fixture);

    [Fact]
    public async Task UserGetsProductById()
    {
        await _steps.GivenAnAuthenticatedUser(Permissions.CanCreateProduct, Permissions.CanGetProductById);
        var id = await _steps.GivenAnExistingProduct();
        await _steps.WhenTheyAttemptToGetProductById(id);
        await _steps.ThenTheResponseShouldBe200OK();
        await _steps.ThenTheResponseShouldContainProduct(id);
    }

    [Fact]
    public async Task UserAttemptsToGetProductWithEmptyId()
    {
        await _steps.GivenAnAuthenticatedUser(Permissions.CanGetProductById);
        await _steps.WhenTheyAttemptToGetProductById(Guid.Empty);
        await _steps.ThenTheResponseShouldBe400BadRequest();
        await _steps.ThenTheResponseShouldBeValidationProblemDetails(new()
        {
            ["Id"] = ["'Id' must not be empty."]
        });
    }

    [Fact]
    public async Task AnonymousUserAttemptsToGetProductById()
    {
        await _steps.GivenAnAnonymousUser();
        await _steps.WhenTheyAttemptToGetProductById(Guid.NewGuid());
        await _steps.ThenTheResponseShouldBe401Unauthorized();
    }

    [Fact]
    public async Task ForbiddenUserAttemptsToGetProductById()
    {
        await _steps.GivenAnAuthenticatedUser();
        await _steps.WhenTheyAttemptToGetProductById(Guid.NewGuid());
        await _steps.ThenTheResponseShouldBe403Forbidden();
    }

    [Fact]
    public async Task UserAttemptsToGetNonExistentProduct()
    {
        await _steps.GivenAnAuthenticatedUser(Permissions.CanGetProductById);
        await _steps.WhenTheyAttemptToGetProductById(Guid.NewGuid());
        await _steps.ThenTheResponseShouldBe404NotFound();
        await _steps.ThenTheResponseShouldBeProblemDetails("Products.NotFound", "The specified product was not found.");
    }
}
