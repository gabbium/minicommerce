using MiniCommerce.Identity.Application.Contracts;
using MiniCommerce.Identity.Web.AcceptanceTests.TestHelpers;

namespace MiniCommerce.Identity.Web.AcceptanceTests.Endpoints.V1.Permissions.ListPermissions;

[Collection(nameof(TestCollection))]
public class ListPermissionsFeature(TestFixture fixture) : TestBase(fixture)
{
    private readonly ListPermissionsSteps _steps = new(fixture);

    [Fact]
    public async Task UserListsPermissions()
    {
        await _steps.GivenAnAuthenticatedUser(IdentityPermissionNames.CanCreatePermission, IdentityPermissionNames.CanListPermissions);
        await _steps.GivenPermissionsExist(
        [
            new("catalog:products.list"),
            new("catalog:products.create"),
            new("catalog:products.delete")
        ]);
        await _steps.WhenTheyAttemptToListPermissions(new(1, 2));
        await _steps.ThenResponseIs200Ok();
        await _steps.ThenResponseContainsPermissions(1, 2, 2, 3, 2);
    }

    [Fact]
    public async Task AnonymousUserAttemptsToListPermissions()
    {
        await _steps.GivenAnAnonymousUser();
        await _steps.WhenTheyAttemptToListPermissions(new(1, 2));
        await _steps.ThenResponseIs401Unauthorized();
    }

    [Fact]
    public async Task ForbiddenUserAttemptsToListPermissions()
    {
        await _steps.GivenAnAuthenticatedUser();
        await _steps.WhenTheyAttemptToListPermissions(new(1, 2));
        await _steps.ThenResponseIs403Forbidden();
    }
}
