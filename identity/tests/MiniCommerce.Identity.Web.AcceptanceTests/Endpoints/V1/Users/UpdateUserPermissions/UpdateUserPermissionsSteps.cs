using MiniCommerce.Identity.Application.Contracts.Permissions;
using MiniCommerce.Identity.Application.Contracts.Users;
using MiniCommerce.Identity.Web.AcceptanceTests.Steps;
using MiniCommerce.Identity.Web.AcceptanceTests.TestHelpers;
using MiniCommerce.Identity.Web.Endpoints.V1.Permissions;
using MiniCommerce.Identity.Web.Endpoints.V1.Users;

namespace MiniCommerce.Identity.Web.AcceptanceTests.Endpoints.V1.Users.UpdateUserPermissions;

public class UpdateUserPermissionsSteps(TestFixture fixture) : CommonStepsBase(fixture)
{
    public async Task<Guid> GivenAnExistingPermission(CreatePermissionEndpoint.CreatePermissionRequest request)
    {
        var response = await Fixture.Client.PostAsJsonAsync(CreatePermissionEndpoint.Route, request);
        var created = await response.Content.ReadFromJsonAsync<PermissionResponse>();
        return created!.Id;
    }
    public async Task<Guid> GivenAnExistingUser(CreateUserEndpoint.CreateUserRequest request)
    {
        var response = await Fixture.Client.PostAsJsonAsync(CreateUserEndpoint.Route, request);
        var created = await response.Content.ReadFromJsonAsync<UserResponse>();
        return created!.Id;
    }

    public async Task WhenTheyAttemptToUpdateUserPermissions(Guid id, UpdateUserPermissionsEndpoint.UpdateUserPermissionsRequest request)
    {
        HttpResponse = await Fixture.Client.PatchAsJsonAsync(UpdateUserPermissionsEndpoint.BuildRoute(id), request);
    }

    public async Task ThenResponseContainsUser(UserResponse expectedUser)
    {
        var response = await ReadBodyAs<UserResponse>();
        Assert.Equal(expectedUser, response);
    }
}
