using MiniCommerce.Identity.Application.Contracts;
using MiniCommerce.Identity.Web.AcceptanceTests.Steps;
using MiniCommerce.Identity.Web.AcceptanceTests.TestHelpers;
using MiniCommerce.Identity.Web.Endpoints;

namespace MiniCommerce.Identity.Web.AcceptanceTests.Acceptance.Users;

[Collection(nameof(TestCollection))]
public class ListUsersTests(TestFixture fixture) : TestBase(fixture)
{
    private readonly UserSteps _steps = new(fixture);

    [Fact]
    public async Task UserListsUsers()
    {
        await _steps.GivenAnAuthenticatedUser(Policies.CanCreateUser, Policies.CanListUsers);
        await _steps.GivenAnExistingUser(new("user1@minicommerce"));
        await _steps.GivenAnExistingUser(new("user2@minicommerce"));
        await _steps.GivenAnExistingUser(new("user3@minicommerce"));
        await _steps.WhenTheyAttemptToListUsers(new(1, 2));
        await _steps.ThenResponseIs200Ok();
        await _steps.ThenResponseMatches<PaginatedList<UserResponse>>(paged =>
        {
            Assert.Equal(2, paged.Items.Count);
            Assert.Equal(1, paged.PageNumber);
            Assert.Equal(2, paged.PageSize);
            Assert.Equal(4, paged.TotalItems);
            Assert.Equal(2, paged.TotalPages);
        });
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
