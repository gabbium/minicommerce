using MiniCommerce.Catalog.Web.AcceptanceTests.TestHelpers;

namespace MiniCommerce.Catalog.Web.AcceptanceTests.Endpoints.Products.DeleteProduct;

[Collection(nameof(TestCollection))]
public class DeleteProductFeature(TestFixture fixture) : TestBase(fixture)
{
    private readonly DeleteProductSteps _steps = new(fixture);

    [Fact]
    public async Task RegularUserDeletesProduct()
    {
        await _steps.GivenAnAuthenticatedRegularUser();
        var id = await _steps.GivenAnExistingProduct();
        await _steps.WhenTheyAttemptToDeleteProduct(id);
        await _steps.ThenTheResponseShouldBe204NoContent();
    }

    [Fact]
    public async Task RegularUserAttemptsToDeleteProductWithEmptyId()
    {
        await _steps.GivenAnAuthenticatedRegularUser();
        await _steps.WhenTheyAttemptToDeleteProduct(Guid.Empty);
        await _steps.ThenTheResponseShouldBe400BadRequest();
        await _steps.ThenTheResponseShouldBeValidationProblemDetails(new()
        {
            ["Id"] = ["'Id' must not be empty."]
        });
    }

    [Fact]
    public async Task AnonymousUserAttemptsToDeleteProduct()
    {
        await _steps.GivenAnAnonymousUser();
        await _steps.WhenTheyAttemptToDeleteProduct(Guid.Empty);
        await _steps.ThenTheResponseShouldBe401Unauthorized();
    }

    [Fact]
    public async Task RegularUserAttemptsToDeleteNonExistentProduct()
    {
        await _steps.GivenAnAuthenticatedRegularUser();
        await _steps.WhenTheyAttemptToDeleteProduct(Guid.NewGuid());
        await _steps.ThenTheResponseShouldBe404NotFound();
        await _steps.ThenTheResponseShouldBeProblemDetails("Products.NotFound", "The specified product was not found.");
    }
}
