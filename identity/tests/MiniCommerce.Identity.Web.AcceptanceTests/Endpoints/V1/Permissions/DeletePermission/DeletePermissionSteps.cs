using MiniCommerce.Identity.Application.Contracts.Permissions;
using MiniCommerce.Identity.Web.AcceptanceTests.Steps;
using MiniCommerce.Identity.Web.AcceptanceTests.TestHelpers;
using MiniCommerce.Identity.Web.Endpoints.V1.Permissions;

namespace MiniCommerce.Identity.Web.AcceptanceTests.Endpoints.V1.Permissions.DeletePermission;

public class DeletePermissionSteps(TestFixture fixture) : CommonStepsBase(fixture)
{
    public async Task<Guid> GivenAnExistingPermission(CreatePermissionEndpoint.CreatePermissionRequest request)
    {
        var response = await Fixture.Client.PostAsJsonAsync(CreatePermissionEndpoint.Route, request);
        var created = await response.Content.ReadFromJsonAsync<PermissionResponse>();
        return created!.Id;
    }

    public async Task WhenTheyAttemptToDeletePermission(Guid id)
    {
        HttpResponse = await Fixture.Client.DeleteAsync(DeletePermissionEndpoint.BuildRoute(id));
    }
}
