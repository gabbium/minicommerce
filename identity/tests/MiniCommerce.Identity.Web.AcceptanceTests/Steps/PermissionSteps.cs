using MiniCommerce.Identity.Application.Contracts;
using MiniCommerce.Identity.Web.AcceptanceTests.TestHelpers.Fixtures;
using MiniCommerce.Identity.Web.Endpoints.V1.Permissions;

namespace MiniCommerce.Identity.Web.AcceptanceTests.Steps;

public class PermissionSteps(TestFixture fixture) : StepsBase(fixture)
{
    public async Task<Guid> GivenAnExistingPermission(CreatePermissionEndpoint.CreatePermissionRequest request)
    {
        var response = await Fixture.Client.PostAsJsonAsync(CreatePermissionEndpoint.Route, request);
        var created = await response.Content.ReadFromJsonAsync<PermissionResponse>();
        return created!.Id;
    }

    public async Task WhenTheyAttemptToListPermissions(ListPermissionsEndpoint.ListPermissionsRequest request)
    {
        HttpResponse = await Fixture.Client.GetAsync(ListPermissionsEndpoint.BuildRoute(request));
    }

    public async Task WhenTheyAttemptToGetPermissionById(Guid id)
    {
        HttpResponse = await Fixture.Client.GetAsync(GetPermissionByIdEndpoint.BuildRoute(id));
    }

    public async Task WhenTheyAttemptToCreatePermission(CreatePermissionEndpoint.CreatePermissionRequest request)
    {
        HttpResponse = await Fixture.Client.PostAsJsonAsync(CreatePermissionEndpoint.Route, request);
    }

    public async Task WhenTheyAttemptToDeprecatePermission(Guid id)
    {
        HttpResponse = await Fixture.Client.PatchAsync(DeprecatePermissionEndpoint.BuildRoute(id), null);
    }

    public async Task WhenTheyAttemptToDeletePermission(Guid id)
    {
        HttpResponse = await Fixture.Client.DeleteAsync(DeletePermissionEndpoint.BuildRoute(id));
    }
}
