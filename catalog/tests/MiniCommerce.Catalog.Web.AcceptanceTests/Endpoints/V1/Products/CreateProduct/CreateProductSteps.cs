using MiniCommerce.Catalog.Application.Contracts.Products;
using MiniCommerce.Catalog.Web.AcceptanceTests.Steps;
using MiniCommerce.Catalog.Web.AcceptanceTests.TestHelpers;
using MiniCommerce.Catalog.Web.Endpoints.V1.Products;

namespace MiniCommerce.Catalog.Web.AcceptanceTests.Endpoints.V1.Products.CreateProduct;

public class CreateProductSteps(TestFixture fixture) : CommonStepsBase(fixture)
{
    public async Task WhenTheyAttemptToCreateProduct(CreateProductEndpoint.Request request)
    {
        HttpResponse = await Fixture.Client.PostAsJsonAsync(CreateProductEndpoint.Route, request);
    }

    public async Task ThenResponseContainsProduct(string expectedSku, string expectedName, decimal expectedPrice)
    {
        var response = await ReadBodyAs<ProductResponse>();
        Assert.NotEqual(Guid.Empty, response.Id);
        Assert.Equal(expectedSku, response.Sku);
        Assert.Equal(expectedName, response.Name);
        Assert.Equal(expectedPrice, response.Price);
    }
}
