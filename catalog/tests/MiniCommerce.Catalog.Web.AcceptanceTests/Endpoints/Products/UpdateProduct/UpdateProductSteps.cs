using MiniCommerce.Catalog.Application.Models;
using MiniCommerce.Catalog.Web.AcceptanceTests.Steps;
using MiniCommerce.Catalog.Web.AcceptanceTests.TestHelpers;
using MiniCommerce.Catalog.Web.Endpoints.V1.Products;

namespace MiniCommerce.Catalog.Web.AcceptanceTests.Endpoints.Products.UpdateProduct;

public class UpdateProductSteps(TestFixture fixture) : CommonStepsBase(fixture)
{
    private UpdateProductEndpoint.Request _request = null!;

    public async Task<Guid> GivenAnExistingProduct()
    {
        var request = new CreateProductEndpoint.Request("SKU-001", "Bluetooth Headphones", 129.50m);
        var response = await Fixture.Client.PostAsJsonAsync(CreateProductEndpoint.Route, request);
        var created = await response.Content.ReadFromJsonAsync<ProductResponse>();
        return created!.Id;
    }

    public async Task WhenTheyAttemptToUpdateProduct(Guid id, string name, decimal price)
    {
        _request = new(name, price);
        HttpResponse = await Fixture.Client.PutAsJsonAsync(UpdateProductEndpoint.BuildRoute(id), _request);
    }

    public async Task ThenTheResponseShouldContainProduct()
    {
        var response = await HttpResponse!.Content.ReadFromJsonAsync<ProductResponse>();
        Assert.NotNull(response);
        Assert.NotEqual(Guid.Empty, response.Id);
        Assert.Equal(_request.Name, response.Name);
        Assert.Equal(_request.Price, response.Price);
    }
}
