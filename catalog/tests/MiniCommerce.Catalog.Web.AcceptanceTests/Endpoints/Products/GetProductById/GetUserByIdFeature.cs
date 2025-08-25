using MiniCommerce.Catalog.Web.AcceptanceTests.TestHelpers;

namespace MiniCommerce.Catalog.Web.AcceptanceTests.Endpoints.Products.GetProductById;

[Collection(nameof(TestCollection))]
public class GetUserByIdFeature(TestFixture fixture) : TestBase(fixture)
{
    private readonly GetProductByIdSteps _steps = new(fixture);

    [Fact]
    public async Task RegularUserGetsProductById()
    {
        await _steps.GivenAnAuthenticatedRegularUser();
        var id = await _steps.GivenAnExistingProduct();
        await _steps.WhenTheyAttemptToGetProductById(id);
        await _steps.ThenTheResponseShouldBe200OK();
        await _steps.ThenTheResponseShouldContainProduct(id);
    }

    [Fact]
    public async Task RegularUserAttemptsToGetProductWithEmptyId()
    {
        await _steps.GivenAnAuthenticatedRegularUser();
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
    public async Task RegularUserAttemptsToGetNonExistentProduct()
    {
        await _steps.GivenAnAuthenticatedRegularUser();
        await _steps.WhenTheyAttemptToGetProductById(Guid.NewGuid());
        await _steps.ThenTheResponseShouldBe404NotFound();
        await _steps.ThenTheResponseShouldBeProblemDetails("Products.NotFound", "The specified product was not found.");
    }
}
