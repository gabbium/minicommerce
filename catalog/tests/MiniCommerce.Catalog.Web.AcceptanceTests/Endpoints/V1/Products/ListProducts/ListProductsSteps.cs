using MiniCommerce.Catalog.Application.Contracts.Products;
using MiniCommerce.Catalog.Web.AcceptanceTests.Steps;
using MiniCommerce.Catalog.Web.AcceptanceTests.TestHelpers;
using MiniCommerce.Catalog.Web.AcceptanceTests.TestHelpers.Models;
using MiniCommerce.Catalog.Web.Endpoints.V1.Products;

namespace MiniCommerce.Catalog.Web.AcceptanceTests.Endpoints.V1.Products.ListProducts;

public class ListProductsSteps(TestFixture fixture) : CommonStepsBase(fixture)
{
    public async Task GivenProductsExist(List<CreateProductEndpoint.Request> requests)
    {
        foreach (var request in requests)
        {
            await Fixture.Client.PostAsJsonAsync(CreateProductEndpoint.Route, request);
        }
    }

    public async Task WhenTheyAttemptToListProducts(ListProductsEndpoint.Request request)
    {
        HttpResponse = await Fixture.Client.GetAsync(ListProductsEndpoint.BuildRoute(request));
    }

    public async Task ThenResponseContainsProducts(
        int expectedPage,
        int expectedPageSize,
        int expectedItemsCount,
        int expectedTotalCount,
        int expectedTotalPages)
    {
        var paged = await ReadBodyAs<FlatPagedList<ProductResponse>>();
        Assert.Equal(expectedPage, paged.Page);
        Assert.Equal(expectedPageSize, paged.PageSize);
        Assert.Equal(expectedItemsCount, paged.Items.Count);
        Assert.Equal(expectedTotalCount, paged.TotalCount);
        Assert.Equal(expectedTotalPages, paged.TotalPages);
    }
}
