using MiniCommerce.Catalog.Application.UseCases.Products;
using MiniCommerce.Catalog.Web.AcceptanceTests.TestHelpers;
using MiniCommerce.Catalog.Web.Endpoints.V1.Products;

namespace MiniCommerce.Catalog.Web.AcceptanceTests.Steps;

public class ProductSteps(TestFixture fixture) : TestStepsBase(fixture)
{
    public async Task<Guid> GivenAnExistingProduct(CreateProductEndpoint.Request request)
    {
        var response = await Fixture.Client.PostAsJsonAsync(CreateProductEndpoint.Route, request);
        var created = await response.Content.ReadFromJsonAsync<ProductResponse>();
        return created!.Id;
    }

    public async Task WhenTheyAttemptToListProducts(ListProductsEndpoint.Request request)
    {
        HttpResponse = await Fixture.Client.GetAsync(ListProductsEndpoint.BuildRoute(request));
    }

    public async Task WhenTheyAttemptToGetProductById(Guid id)
    {
        HttpResponse = await Fixture.Client.GetAsync(GetProductByIdEndpoint.BuildRoute(id));
    }

    public async Task WhenTheyAttemptToCreateProduct(CreateProductEndpoint.Request request)
    {
        HttpResponse = await Fixture.Client.PostAsJsonAsync(CreateProductEndpoint.Route, request);
    }

    public async Task WhenTheyAttemptToUpdateProduct(Guid id, UpdateProductEndpoint.Request request)
    {
        HttpResponse = await Fixture.Client.PutAsJsonAsync(UpdateProductEndpoint.BuildRoute(id), request);
    }

    public async Task WhenTheyAttemptToDeleteProduct(Guid id)
    {
        HttpResponse = await Fixture.Client.DeleteAsync(DeleteProductEndpoint.BuildRoute(id));
    }
}
