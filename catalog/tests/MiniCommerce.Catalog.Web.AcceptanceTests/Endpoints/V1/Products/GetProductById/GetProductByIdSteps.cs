using MiniCommerce.Catalog.Application.Contracts.Products;
using MiniCommerce.Catalog.Web.AcceptanceTests.Steps;
using MiniCommerce.Catalog.Web.AcceptanceTests.TestHelpers;
using MiniCommerce.Catalog.Web.Endpoints.V1.Products;

namespace MiniCommerce.Catalog.Web.AcceptanceTests.Endpoints.V1.Products.GetProductById;

public class GetProductByIdSteps(TestFixture fixture) : CommonStepsBase(fixture)
{
    public async Task<Guid> GivenAnExistingProduct(CreateProductEndpoint.Request request)
    {
        var response = await Fixture.Client.PostAsJsonAsync(CreateProductEndpoint.Route, request);
        var created = await response.Content.ReadFromJsonAsync<ProductResponse>();
        return created!.Id;
    }

    public async Task WhenTheyAttemptToGetProductById(Guid id)
    {
        HttpResponse = await Fixture.Client.GetAsync(GetProductByIdEndpoint.BuildRoute(id));
    }

    public async Task ThenResponseContainsProduct(Guid expectedId, string expectedSku, string expectedName, decimal expectedPrice)
    {
        var response = await ReadBodyAs<ProductResponse>();
        Assert.Equal(expectedId, response.Id);
        Assert.Equal(expectedSku, response.Sku);
        Assert.Equal(expectedName, response.Name);
        Assert.Equal(expectedPrice, response.Price);
    }
}
