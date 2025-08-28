using MiniCommerce.Identity.Application.Contracts.Permissions;
using MiniCommerce.Identity.Web.AcceptanceTests.Steps;
using MiniCommerce.Identity.Web.AcceptanceTests.TestHelpers;
using MiniCommerce.Identity.Web.Endpoints.V1.Permissions;

namespace MiniCommerce.Identity.Web.AcceptanceTests.Endpoints.V1.Permissions.GetPermissionById;

public class GetPermissionByIdSteps(TestFixture fixture) : CommonStepsBase(fixture)
{
    public async Task<Guid> GivenAnExistingPermission(CreatePermissionEndpoint.CreatePermissionRequest request)
    {
        var response = await Fixture.Client.PostAsJsonAsync(CreatePermissionEndpoint.Route, request);
        var created = await response.Content.ReadFromJsonAsync<PermissionResponse>();
        return created!.Id;
    }

    public async Task WhenTheyAttemptToGetPermissionById(Guid id)
    {
        HttpResponse = await Fixture.Client.GetAsync(GetPermissionByIdEndpoint.BuildRoute(id));
    }

    public async Task ThenResponseContainsPermission(Guid expectedId, string expectedCode)
    {
        var response = await ReadBodyAs<PermissionResponse>();
        Assert.Equal(expectedId, response.Id);
        Assert.Equal(expectedCode, response.Code);
        Assert.False(response.Deprecated);
    }
}
