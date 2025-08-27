using MiniCommerce.Identity.Application.Contracts;
using MiniCommerce.Identity.Web.AcceptanceTests.TestHelpers;

namespace MiniCommerce.Identity.Web.AcceptanceTests.Endpoints.V1.Users.ListUsers;

[Collection(nameof(TestCollection))]
public class ListUsersFeature(TestFixture fixture) : TestBase(fixture)
{
    private readonly ListUsersSteps _steps = new(fixture);

    [Fact]
    public async Task UserListsUsers()
    {
        await _steps.GivenAnAuthenticatedUser(IdentityPermissionNames.CanCreateUser, IdentityPermissionNames.CanListUsers);
        await _steps.GivenUsersExist(
        [
            new("user1@minicommerce"),
            new("user2@minicommerce"),
            new("user3@minicommerce")
        ]);
        await _steps.WhenTheyAttemptToListUsers(new(1, 2));
        await _steps.ThenResponseIs200Ok();
        await _steps.ThenResponseContainsUsers(1, 2, 2, 4, 2);
    }

    [Fact]
    public async Task AnonymousUserAttemptsToListUsers()
    {
        await _steps.GivenAnAnonymousUser();
        await _steps.WhenTheyAttemptToListUsers(new(1, 2));
        await _steps.ThenResponseIs401Unauthorized();
    }

    [Fact]
    public async Task ForbiddenUserAttemptsToListUsers()
    {
        await _steps.GivenAnAuthenticatedUser();
        await _steps.WhenTheyAttemptToListUsers(new(1, 2));
        await _steps.ThenResponseIs403Forbidden();
    }
}
