using MiniCommerce.Identity.Web.AcceptanceTests.TestHelpers;

namespace MiniCommerce.Identity.Web.AcceptanceTests.Endpoints.V1.Users.GetCurrentUser;

[Collection(nameof(TestCollection))]
public class GetCurrentUserFeature(TestFixture fixture) : TestBase(fixture)
{
    private readonly GetCurrentUserSteps _steps = new(fixture);

    [Fact]
    public async Task UserGetsCurrentUser()
    {
        await _steps.GivenAnAuthenticatedUser("user@minicommerce");
        await _steps.WhenTheyAttemptToGetCurrentUser();
        await _steps.ThenResponseIs200Ok();
        await _steps.ThenResponseContainsUserInfo("user@minicommerce");
    }

    [Fact]
    public async Task AnonymousUserAttemptsToGetCurrentUser()
    {
        await _steps.GivenAnAnonymousUser();
        await _steps.WhenTheyAttemptToGetCurrentUser();
        await _steps.ThenResponseIs401Unauthorized();
    }
}
