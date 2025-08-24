using MiniCommerce.Catalog.Web.AcceptanceTests.TestHelpers;

namespace MiniCommerce.Catalog.Web.AcceptanceTests.Endpoints.Products;

[Collection(nameof(TestCollection))]
public class CreateProductFeature(TestFixture fixture) : TestBase(fixture)
{
    private readonly CreateProductSteps _steps = new(fixture);

    [Fact]
    public async Task RegularUserCreatesProductWithValidData()
    {
        await _steps.GivenAnAuthenticatedRegularUser();
        await _steps.WhenTheyAttemptToCreateProduct("SKU-001", "Bluetooth Headphones", 129.50m);
        await _steps.ThenTheResponseShouldBe201Created();
        await _steps.ThenTheResponseShouldContainProduct();
    }

    [Fact]
    public async Task RegularUserAttemptsToCreateProductWithEmptySku()
    {
        await _steps.GivenAnAuthenticatedRegularUser();
        await _steps.WhenTheyAttemptToCreateProduct(string.Empty, "Bluetooth Headphones", 129.50m);
        await _steps.ThenTheResponseShouldBe400BadRequest();
        await _steps.ThenTheResponseShouldBeValidationProblemDetails(new()
        {
            ["Sku"] = ["'Sku' must not be empty."]
        });
    }

    [Fact]
    public async Task RegularUserAttemptsToCreateProductWithEmptyName()
    {
        await _steps.GivenAnAuthenticatedRegularUser();
        await _steps.WhenTheyAttemptToCreateProduct("SKU-001", string.Empty, 129.50m);
        await _steps.ThenTheResponseShouldBe400BadRequest();
        await _steps.ThenTheResponseShouldBeValidationProblemDetails(new()
        {
            ["Name"] = ["'Name' must not be empty."]
        });
    }

    [Fact]
    public async Task RegularUserAttemptsToCreateProductWithEmptyPrice()
    {
        await _steps.GivenAnAuthenticatedRegularUser();
        await _steps.WhenTheyAttemptToCreateProduct("SKU-001", "Bluetooth Headphones", 0m);
        await _steps.ThenTheResponseShouldBe400BadRequest();
        await _steps.ThenTheResponseShouldBeValidationProblemDetails(new()
        {
            ["Price"] = ["'Price' must not be empty."]
        });
    }

    [Fact]
    public async Task AnonymousUserAttemptsToCreateProduct()
    {
        await _steps.GivenAnAnonymousUser();
        await _steps.WhenTheyAttemptToCreateProduct("SKU-001", "Bluetooth Headphones", 129.50m);
        await _steps.ThenTheResponseShouldBe401Unauthorized();
    }
}
