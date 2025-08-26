using MiniCommerce.Identity.Application.Contracts.Permissions;
using MiniCommerce.Identity.Web.AcceptanceTests.Steps;
using MiniCommerce.Identity.Web.AcceptanceTests.TestHelpers;
using MiniCommerce.Identity.Web.Endpoints.V1.Permissions;

namespace MiniCommerce.Identity.Web.AcceptanceTests.Endpoints.V1.Permissions.CreatePermission;

public class CreatePermissionSteps(TestFixture fixture) : CommonStepsBase(fixture)
{
    public async Task WhenTheyAttemptToCreatePermission(CreatePermissionEndpoint.Request request)
    {
        HttpResponse = await Fixture.Client.PostAsJsonAsync(CreatePermissionEndpoint.Route, request);
    }

    public async Task ThenResponseContainsPermission(string expectedCode)
    {
        var response = await ReadBodyAs<PermissionResponse>();
        Assert.NotEqual(Guid.Empty, response.Id);
        Assert.Equal(expectedCode, response.Code);
        Assert.False(response.Deprecated);
    }
}
