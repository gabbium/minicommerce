using MiniCommerce.Identity.Web.AcceptanceTests.TestHelpers;

namespace MiniCommerce.Identity.Web.AcceptanceTests.Endpoints.Users.Me;

[Collection(nameof(TestCollection))]
public class MeFeature(TestFixture fixture) : TestBase(fixture)
{
    private readonly MeSteps _steps = new(fixture);

    [Fact]
    public async Task UserGetsMe()
    {
        await _steps.GivenAnAuthenticatedUser("user@minicommerce");
        await _steps.WhenTheyAttemptToGetMe();
        await _steps.ThenResponseIs200Ok();
        await _steps.ThenResponseContainsUserInfo("user@minicommerce");
    }

    [Fact]
    public async Task AnonymousUserAttemptsToGetMe()
    {
        await _steps.GivenAnAnonymousUser();
        await _steps.WhenTheyAttemptToGetMe();
        await _steps.ThenResponseIs401Unauthorized();
    }
}
