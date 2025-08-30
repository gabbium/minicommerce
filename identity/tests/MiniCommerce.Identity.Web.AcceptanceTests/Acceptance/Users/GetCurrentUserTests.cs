using MiniCommerce.Identity.Application.Contracts;
using MiniCommerce.Identity.Web.AcceptanceTests.Steps;
using MiniCommerce.Identity.Web.AcceptanceTests.TestHelpers.Fixtures;

namespace MiniCommerce.Identity.Web.AcceptanceTests.Acceptance.Users;

[Collection(nameof(TestCollection))]
public class GetCurrentUserTests(TestFixture fixture) : TestBase(fixture)
{
    private readonly UserSteps _steps = new(fixture);

    [Fact]
    public async Task UserGetsCurrentUser()
    {
        await _steps.GivenAnAuthenticatedUser("user@minicommerce");
        await _steps.WhenTheyAttemptToGetCurrentUser();
        await _steps.ThenResponseIs200Ok();
        await _steps.ThenResponseMatches<UserResponse>(user =>
        {
            Assert.NotEqual(Guid.Empty, user.Id);
            Assert.Equal("user@minicommerce", user.Email);
        });
    }

    [Fact]
    public async Task AnonymousUserAttemptsToGetCurrentUser()
    {
        await _steps.GivenAnAnonymousUser();
        await _steps.WhenTheyAttemptToGetCurrentUser();
        await _steps.ThenResponseIs401Unauthorized();
    }
}
