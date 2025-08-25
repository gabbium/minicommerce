using MiniCommerce.Catalog.Application.Models;
using MiniCommerce.Catalog.Web.AcceptanceTests.Steps;
using MiniCommerce.Catalog.Web.AcceptanceTests.TestHelpers;
using MiniCommerce.Catalog.Web.Endpoints.V1.Products;

namespace MiniCommerce.Catalog.Web.AcceptanceTests.Endpoints.Products.GetProductById;

public class GetProductByIdSteps(TestFixture fixture) : CommonStepsBase(fixture)
{
    private CreateProductEndpoint.Request _request = null!;

    public async Task<Guid> GivenAnExistingProduct()
    {
        _request = new("SKU-001", "Bluetooth Headphones", 129.50m);
        var response = await Fixture.Client.PostAsJsonAsync(CreateProductEndpoint.Route, _request);
        var created = await response.Content.ReadFromJsonAsync<ProductResponse>();
        return created!.Id;
    }

    public async Task WhenTheyAttemptToGetProductById(Guid id)
    {
        HttpResponse = await Fixture.Client.GetAsync(GetProductByIdEndpoint.BuildRoute(id));
    }

    public async Task ThenTheResponseShouldContainProduct(Guid id)
    {
        Assert.NotNull(HttpResponse);
        Assert.Equal(HttpStatusCode.OK, HttpResponse.StatusCode);

        var response = await HttpResponse.Content.ReadFromJsonAsync<ProductResponse>();
        Assert.NotNull(response);
        Assert.Equal(id, response.Id);
    }
}
