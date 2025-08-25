using MiniCommerce.Catalog.Web.AcceptanceTests.TestHelpers;

namespace MiniCommerce.Catalog.Web.AcceptanceTests.Endpoints.Products.UpdateProduct;

[Collection(nameof(TestCollection))]
public class UpdateProductFeature(TestFixture fixture) : TestBase(fixture)
{
    private readonly UpdateProductSteps _steps = new(fixture);

    [Fact]
    public async Task RegularUserUpdatesProduct()
    {
        await _steps.GivenAnAuthenticatedRegularUser();
        var id = await _steps.GivenAnExistingProduct();
        await _steps.WhenTheyAttemptToUpdateProduct(id, "Wireless Mouse", 79.90m);
        await _steps.ThenTheResponseShouldBe200OK();
        await _steps.ThenTheResponseShouldContainProduct();
    }

    [Fact]
    public async Task RegularUserAttemptsToUpdateProductWithEmptyId()
    {
        await _steps.GivenAnAuthenticatedRegularUser();
        await _steps.WhenTheyAttemptToUpdateProduct(Guid.Empty, "Wireless Mouse", 79.90m);
        await _steps.ThenTheResponseShouldBe400BadRequest();
        await _steps.ThenTheResponseShouldBeValidationProblemDetails(new()
        {
            ["Id"] = ["'Id' must not be empty."]
        });
    }

    [Fact]
    public async Task RegularUserAttemptsToUpdateProductWithEmptyName()
    {
        await _steps.GivenAnAuthenticatedRegularUser();
        var id = await _steps.GivenAnExistingProduct();
        await _steps.WhenTheyAttemptToUpdateProduct(id, string.Empty, 79.90m);
        await _steps.ThenTheResponseShouldBe400BadRequest();
        await _steps.ThenTheResponseShouldBeValidationProblemDetails(new()
        {
            ["Name"] = ["'Name' must not be empty."]
        });
    }

    [Fact]
    public async Task RegularUserAttemptsToUpdateProductWithEmptyPrice()
    {
        await _steps.GivenAnAuthenticatedRegularUser();
        var id = await _steps.GivenAnExistingProduct();
        await _steps.WhenTheyAttemptToUpdateProduct(id, "Wireless Mouse", 0m);
        await _steps.ThenTheResponseShouldBe400BadRequest();
        await _steps.ThenTheResponseShouldBeValidationProblemDetails(new()
        {
            ["Price"] = ["'Price' must not be empty."]
        });
    }

    [Fact]
    public async Task AnonymousUserAttemptsToUpdateProduct()
    {
        await _steps.GivenAnAnonymousUser();
        await _steps.WhenTheyAttemptToUpdateProduct(Guid.NewGuid(), "Wireless Mouse", 79.90m);
        await _steps.ThenTheResponseShouldBe401Unauthorized();
    }

    [Fact]
    public async Task RegularUserAttemptsToUpdateNonExistentProduct()
    {
        await _steps.GivenAnAuthenticatedRegularUser();
        await _steps.WhenTheyAttemptToUpdateProduct(Guid.NewGuid(), "Wireless Mouse", 79.90m);
        await _steps.ThenTheResponseShouldBe404NotFound();
        await _steps.ThenTheResponseShouldBeProblemDetails("Products.NotFound", "The specified product was not found.");
    }
}
