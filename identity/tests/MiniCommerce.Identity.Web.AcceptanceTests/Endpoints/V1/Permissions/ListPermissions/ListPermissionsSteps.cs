using MiniCommerce.Identity.Application.Contracts.Permissions;
using MiniCommerce.Identity.Web.AcceptanceTests.Steps;
using MiniCommerce.Identity.Web.AcceptanceTests.TestHelpers;
using MiniCommerce.Identity.Web.AcceptanceTests.TestHelpers.Models;
using MiniCommerce.Identity.Web.Endpoints.V1.Permissions;

namespace MiniCommerce.Identity.Web.AcceptanceTests.Endpoints.V1.Permissions.ListPermissions;

public class ListPermissionsSteps(TestFixture fixture) : CommonStepsBase(fixture)
{
    public async Task GivenPermissionsExist(List<CreatePermissionEndpoint.CreatePermissionRequest> requests)
    {
        foreach (var request in requests)
        {
            await Fixture.Client.PostAsJsonAsync(CreatePermissionEndpoint.Route, request);
        }
    }

    public async Task WhenTheyAttemptToListPermissions(ListPermissionsEndpoint.ListPermissionsRequest request)
    {
        HttpResponse = await Fixture.Client.GetAsync(ListPermissionsEndpoint.BuildRoute(request));
    }

    public async Task ThenResponseContainsPermissions(
        int expectedPage,
        int expectedPageSize,
        int expectedItemsCount,
        int expectedTotalCount,
        int expectedTotalPages)
    {
        var paged = await ReadBodyAs<FlatPagedList<PermissionResponse>>();
        Assert.Equal(expectedPage, paged.Page);
        Assert.Equal(expectedPageSize, paged.PageSize);
        Assert.Equal(expectedItemsCount, paged.Items.Count);
        Assert.Equal(expectedTotalCount, paged.TotalCount);
        Assert.Equal(expectedTotalPages, paged.TotalPages);
    }
}
