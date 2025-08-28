using MiniCommerce.Identity.Application.Contracts.Permissions;
using MiniCommerce.Identity.Web.AcceptanceTests.Steps;
using MiniCommerce.Identity.Web.AcceptanceTests.TestHelpers;
using MiniCommerce.Identity.Web.Endpoints.V1.Permissions;

namespace MiniCommerce.Identity.Web.AcceptanceTests.Endpoints.V1.Permissions.DeprecatePermission;

public class DeprecatePermissionSteps(TestFixture fixture) : CommonStepsBase(fixture)
{
    public async Task<Guid> GivenAnExistingPermission(CreatePermissionEndpoint.CreatePermissionRequest request)
    {
        var response = await Fixture.Client.PostAsJsonAsync(CreatePermissionEndpoint.Route, request);
        var created = await response.Content.ReadFromJsonAsync<PermissionResponse>();
        return created!.Id;
    }

    public async Task WhenTheyAttemptToDeprecatePermission(Guid id)
    {
        HttpResponse = await Fixture.Client.PatchAsync(DeprecatePermissionEndpoint.BuildRoute(id), null);
    }

    public async Task ThenResponseContainsPermission(PermissionResponse expectedPermission)
    {
        var response = await ReadBodyAs<PermissionResponse>();
        Assert.Equal(expectedPermission, response);
    }
}
