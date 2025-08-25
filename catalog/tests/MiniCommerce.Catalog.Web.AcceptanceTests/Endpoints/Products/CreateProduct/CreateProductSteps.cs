using MiniCommerce.Catalog.Application.Models;
using MiniCommerce.Catalog.Web.AcceptanceTests.Steps;
using MiniCommerce.Catalog.Web.AcceptanceTests.TestHelpers;
using MiniCommerce.Catalog.Web.Endpoints.V1.Products;

namespace MiniCommerce.Catalog.Web.AcceptanceTests.Endpoints.Products.CreateProduct;

public class CreateProductSteps(TestFixture fixture) : CommonStepsBase(fixture)
{
    private CreateProductEndpoint.Request _request = null!;

    public async Task WhenTheyAttemptToCreateProduct(string sku, string name, decimal price)
    {
        _request = new(sku, name, price);
        HttpResponse = await Fixture.Client.PostAsJsonAsync(CreateProductEndpoint.Route, _request);
    }

    public async Task ThenTheResponseShouldContainProduct()
    {
        var response = await HttpResponse!.Content.ReadFromJsonAsync<ProductResponse>();
        Assert.NotNull(response);
        Assert.NotEqual(Guid.Empty, response.Id);
        Assert.Equal(_request.Sku, response.Sku);
        Assert.Equal(_request.Name, response.Name);
        Assert.Equal(_request.Price, response.Price);
    }
}
