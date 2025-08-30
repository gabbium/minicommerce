using MiniCommerce.Catalog.Application.Contracts;
using MiniCommerce.Catalog.Web.AcceptanceTests.Steps;
using MiniCommerce.Catalog.Web.AcceptanceTests.TestHelpers;
using MiniCommerce.Catalog.Web.Endpoints;

namespace MiniCommerce.Catalog.Web.AcceptanceTests.Acceptance.Products;

[Collection(nameof(TestCollection))]
public class ListProductsTests(TestFixture fixture) : TestBase(fixture)
{
    private readonly ProductSteps _steps = new(fixture);

    [Fact]
    public async Task UserListsProducts()
    {
        await _steps.GivenAnAuthenticatedUser(Policies.CanCreateProduct, Policies.CanListProducts);
        await _steps.GivenAnExistingProduct(new("SKU-001", "Bluetooth Headphones", 129.50m));
        await _steps.GivenAnExistingProduct(new("SKU-002", "Wireless Mouse", 79.90m));
        await _steps.GivenAnExistingProduct(new("SKU-003", "Mechanical Keyboard", 299.00m));
        await _steps.WhenTheyAttemptToListProducts(new(1, 2));
        await _steps.ThenResponseIs200Ok();
        await _steps.ThenResponseMatches<PaginatedList<ProductResponse>>(paged =>
        {
            Assert.Equal(2, paged.Items.Count);
            Assert.Equal(1, paged.PageNumber);
            Assert.Equal(2, paged.PageSize);
            Assert.Equal(3, paged.TotalItems);
            Assert.Equal(2, paged.TotalPages);
        });
    }

    [Fact]
    public async Task AnonymousUserAttemptsToListProducts()
    {
        await _steps.GivenAnAnonymousUser();
        await _steps.WhenTheyAttemptToListProducts(new(1, 2));
        await _steps.ThenResponseIs401Unauthorized();
    }

    [Fact]
    public async Task ForbiddenUserAttemptsToListProducts()
    {
        await _steps.GivenAnAuthenticatedUser();
        await _steps.WhenTheyAttemptToListProducts(new(1, 2));
        await _steps.ThenResponseIs403Forbidden();
    }
}
