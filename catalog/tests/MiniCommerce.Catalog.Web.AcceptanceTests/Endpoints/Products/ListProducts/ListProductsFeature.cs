using MiniCommerce.Catalog.Web.AcceptanceTests.TestHelpers;

namespace MiniCommerce.Catalog.Web.AcceptanceTests.Endpoints.Products.ListProducts;

[Collection(nameof(TestCollection))]
public class ListProductsFeature(TestFixture fixture) : TestBase(fixture)
{
    private readonly ListProductsSteps _steps = new(fixture);

    [Fact]
    public async Task RegularUserListsProducts()
    {
        await _steps.GivenAnAuthenticatedRegularUser();
        await _steps.GivenSomeProductsExist();
        await _steps.WhenTheyAttemptToListProducts(1, 10);
        await _steps.ThenTheResponseShouldBe200OK();
        await _steps.ThenTheResponseShouldContainProducts();
    }

    [Fact]
    public async Task AnonymousUserAttemptsToListProducts()
    {
        await _steps.GivenAnAnonymousUser();
        await _steps.WhenTheyAttemptToListProducts(1, 10);
        await _steps.ThenTheResponseShouldBe401Unauthorized();
    }
}
