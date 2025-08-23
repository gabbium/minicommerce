using MiniCommerce.Identity.Web.AcceptanceTests.TestHelpers;

namespace MiniCommerce.Identity.Web.AcceptanceTests.Endpoints.Users.Me;

[Collection(nameof(TestCollection))]
public class MeFeature(TestFixture fixture) : TestBase(fixture)
{
    private readonly MeSteps _steps = new(fixture);

    [Fact]
    public async Task UserFetchesMe()
    {
        await _steps.GivenAnAuthenticatedUser("user@minicommerce.com");
        await _steps.WhenTheyAttemptToFetchMe();
        await _steps.ThenTheResponseShouldBe200OK();
        await _steps.ThenTheResponseShouldContainUser("user@minicommerce.com");
    }

    [Fact]
    public async Task AnonymousUserAttemptsToFetchMe()
    {
        await _steps.GivenAnAnonymousUser();
        await _steps.WhenTheyAttemptToFetchMe();
        await _steps.ThenTheResponseShouldBe401Unauthorized();
    }
}
