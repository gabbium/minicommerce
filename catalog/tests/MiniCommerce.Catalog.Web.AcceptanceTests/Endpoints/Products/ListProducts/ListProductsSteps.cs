using MiniCommerce.Catalog.Application.Models;
using MiniCommerce.Catalog.Web.AcceptanceTests.Steps;
using MiniCommerce.Catalog.Web.AcceptanceTests.TestHelpers;
using MiniCommerce.Catalog.Web.AcceptanceTests.TestHelpers.Models;
using MiniCommerce.Catalog.Web.Endpoints.V1.Products;

namespace MiniCommerce.Catalog.Web.AcceptanceTests.Endpoints.Products.ListProducts;

public class ListProductsSteps(TestFixture fixture) : CommonStepsBase(fixture)
{
    private ListProductsEndpoint.Request _request = null!;

    public async Task GivenSomeProductsExist()
    {
        var products = new List<CreateProductEndpoint.Request>
        {
            new("SKU-001", "Bluetooth Headphones", 129.50m),
            new("SKU-002", "Wireless Mouse", 79.90m),
            new("SKU-003", "Mechanical Keyboard", 299.00m)
        };

        foreach (var product in products)
        {
            await Fixture.Client.PostAsJsonAsync(CreateProductEndpoint.Route, product);
        }
    }

    public async Task WhenTheyAttemptToListProducts(int page, int pageSize)
    {
        _request = new(page, pageSize);
        HttpResponse = await Fixture.Client.GetAsync(ListProductsEndpoint.BuildRoute(_request));
    }

    public async Task ThenTheResponseShouldContainProducts()
    {
        Assert.NotNull(HttpResponse);
        Assert.Equal(HttpStatusCode.OK, HttpResponse.StatusCode);

        var paged = await HttpResponse.Content.ReadFromJsonAsync<FlatPagedList<ProductResponse>>();
        Assert.NotNull(paged);
        Assert.Equal(_request.Page, paged.PageNumber);
        Assert.True(paged.Items.Count >= 3);
        Assert.True(paged.TotalCount >= paged.Items.Count);
        Assert.True(paged.TotalPages >= 1);
    }
}
