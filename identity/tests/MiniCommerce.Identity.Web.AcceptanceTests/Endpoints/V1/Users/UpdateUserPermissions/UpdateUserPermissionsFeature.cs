using MiniCommerce.Identity.Application.Contracts;
using MiniCommerce.Identity.Application.Features.Users;
using MiniCommerce.Identity.Web.AcceptanceTests.TestHelpers;

namespace MiniCommerce.Identity.Web.AcceptanceTests.Endpoints.V1.Users.UpdateUserPermissions;

[Collection(nameof(TestCollection))]
public class UpdateUserPermissionsFeature(TestFixture fixture) : TestBase(fixture)
{
    private readonly UpdateUserPermissionsSteps _steps = new(fixture);

    [Fact]
    public async Task UserUpdatesUserPermissions()
    {
        await _steps.GivenAnAuthenticatedUser(
            IdentityPermissionNames.CanCreatePermission,
            IdentityPermissionNames.CanCreateUser,
            IdentityPermissionNames.CanUpdateUserPermissions);
        var userId = await _steps.GivenAnExistingUser(new("user@minicommerce"));
        var permissionId = await _steps.GivenAnExistingPermission(new("catalog:products.list"));
        await _steps.WhenTheyAttemptToUpdateUserPermissions(userId, new([permissionId]));
        await _steps.ThenResponseIs200Ok();
        await _steps.ThenResponseContainsUser(new(userId, "user@minicommerce"));
    }

    [Fact]
    public async Task UserAttemptsToUpdateUserPermissionsWithEmptyUserId()
    {
        await _steps.GivenAnAuthenticatedUser(IdentityPermissionNames.CanUpdateUserPermissions);
        await _steps.WhenTheyAttemptToUpdateUserPermissions(Guid.Empty, new([]));
        await _steps.ThenResponseIs400BadRequest();
        await _steps.ThenResponseIsValidationProblemDetails(new()
        {
            ["UserId"] = ["'User Id' must not be empty."]
        });
    }

    [Fact]
    public async Task AnonymousUserAttemptsToUpdateUserPermissions()
    {
        await _steps.GivenAnAnonymousUser();
        await _steps.WhenTheyAttemptToUpdateUserPermissions(Guid.NewGuid(), new([]));
        await _steps.ThenResponseIs401Unauthorized();
    }

    [Fact]
    public async Task ForbiddenUserAttemptsToUpdateUserPermissions()
    {
        await _steps.GivenAnAuthenticatedUser();
        await _steps.WhenTheyAttemptToUpdateUserPermissions(Guid.NewGuid(), new([]));
        await _steps.ThenResponseIs403Forbidden();
    }

    [Fact]
    public async Task UserAttemptsToUpdateNonExistentUser()
    {
        await _steps.GivenAnAuthenticatedUser(IdentityPermissionNames.CanUpdateUserPermissions);
        await _steps.WhenTheyAttemptToUpdateUserPermissions(Guid.NewGuid(), new([]));
        await _steps.ThenResponseIs404NotFound();
        await _steps.ThenResponseIsProblemDetails(UserErrors.NotFound.Code, UserErrors.NotFound.Description);
    }
}
