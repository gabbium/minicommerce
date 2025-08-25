using MiniCommerce.Catalog.Application.Models;
using MiniCommerce.Catalog.Web.AcceptanceTests.Steps;
using MiniCommerce.Catalog.Web.AcceptanceTests.TestHelpers;
using MiniCommerce.Catalog.Web.Endpoints.V1.Products;

namespace MiniCommerce.Catalog.Web.AcceptanceTests.Endpoints.Products.DeleteProduct;

public class DeleteProductSteps(TestFixture fixture) : CommonStepsBase(fixture)
{
    public async Task<Guid> GivenAnExistingProduct()
    {
        var request = new CreateProductEndpoint.Request("SKU-001", "Bluetooth Headphones", 129.50m);
        var response = await Fixture.Client.PostAsJsonAsync(CreateProductEndpoint.Route, request);
        var created = await response.Content.ReadFromJsonAsync<ProductResponse>();
        return created!.Id;
    }

    public async Task WhenTheyAttemptToDeleteProduct(Guid id)
    {
        HttpResponse = await Fixture.Client.DeleteAsync(DeleteProductEndpoint.BuildRoute(id));
    }
}
