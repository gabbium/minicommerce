using MiniCommerce.Identity.Application.Contracts;
using MiniCommerce.Identity.Domain.Aggregates.Users.Errors;
using MiniCommerce.Identity.Web.AcceptanceTests.Steps;
using MiniCommerce.Identity.Web.AcceptanceTests.TestHelpers.Fixtures;
using MiniCommerce.Identity.Web.Endpoints;

namespace MiniCommerce.Identity.Web.AcceptanceTests.Acceptance.Users;

[Collection(nameof(TestCollection))]
public class UpdateUserPermissionsTests(TestFixture fixture) : TestBase(fixture)
{
    private readonly UserSteps _userSteps = new(fixture);
    private readonly PermissionSteps _permissionSteps = new(fixture);

    [Fact]
    public async Task UserUpdatesUserPermissions()
    {
        await _userSteps.GivenAnAuthenticatedUser(Policies.CanCreatePermission, Policies.CanCreateUser, Policies.CanUpdateUserPermissions);
        var userId = await _userSteps.GivenAnExistingUser(new("user@minicommerce"));
        var permissionId = await _permissionSteps.GivenAnExistingPermission(new("catalog:products.list"));
        await _userSteps.WhenTheyAttemptToUpdateUserPermissions(userId, new([permissionId]));
        await _userSteps.ThenResponseIs200Ok();
        await _userSteps.ThenResponseMatches<UserResponse>(user =>
        {
            Assert.Equal(userId, user.Id);
            Assert.Equal("user@minicommerce", user.Email);
        });
    }

    [Fact]
    public async Task UserAttemptsToUpdateUserPermissionsWithEmptyUserId()
    {
        await _userSteps.GivenAnAuthenticatedUser(Policies.CanUpdateUserPermissions);
        await _userSteps.WhenTheyAttemptToUpdateUserPermissions(Guid.Empty, new([]));
        await _userSteps.ThenResponseIs400BadRequest();
        await _userSteps.ThenResponseIsValidationProblemDetails(new()
        {
            ["UserId"] = ["'User Id' must not be empty."]
        });
    }

    [Fact]
    public async Task AnonymousUserAttemptsToUpdateUserPermissions()
    {
        await _userSteps.GivenAnAnonymousUser();
        await _userSteps.WhenTheyAttemptToUpdateUserPermissions(Guid.NewGuid(), new([]));
        await _userSteps.ThenResponseIs401Unauthorized();
    }

    [Fact]
    public async Task ForbiddenUserAttemptsToUpdateUserPermissions()
    {
        await _userSteps.GivenAnAuthenticatedUser();
        await _userSteps.WhenTheyAttemptToUpdateUserPermissions(Guid.NewGuid(), new([]));
        await _userSteps.ThenResponseIs403Forbidden();
    }

    [Fact]
    public async Task UserAttemptsToUpdateNonExistentUser()
    {
        await _userSteps.GivenAnAuthenticatedUser(Policies.CanUpdateUserPermissions);
        await _userSteps.WhenTheyAttemptToUpdateUserPermissions(Guid.NewGuid(), new([]));
        await _userSteps.ThenResponseIs404NotFound();
        await _userSteps.ThenResponseIsProblemDetails(UserErrors.NotFound.Code, UserErrors.NotFound.Description);
    }
}
