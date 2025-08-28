using MiniCommerce.Identity.Application.Contracts.Sessions;
using MiniCommerce.Identity.Web.AcceptanceTests.Steps;
using MiniCommerce.Identity.Web.AcceptanceTests.TestHelpers;

namespace MiniCommerce.Identity.Web.AcceptanceTests.Endpoints.V1.Sessions;

[Collection(nameof(TestCollection))]
public class LoginUserFeature(TestFixture fixture) : TestBase(fixture)
{
    private readonly UserSteps _userSteps = new(fixture);
    private readonly SessionSteps _sessionSteps = new(fixture);

    [Fact]
    public async Task UserLogsInWithExistentEmail()
    {
        await _userSteps.GivenAnExistingUser(new("user@minicommerce"));
        await _sessionSteps.WhenTheyAttemptToLogin(new("user@minicommerce"));
        await _sessionSteps.ThenResponseIs200Ok();
        await _sessionSteps.ThenResponseMatches<TokenResponse>(token =>
        {
            Assert.False(string.IsNullOrEmpty(token.AccessToken));
        });
    }

    [Fact]
    public async Task UserLogsInWithNonExistentEmail()
    {
        await _sessionSteps.WhenTheyAttemptToLogin(new("user@minicommerce"));
        await _sessionSteps.ThenResponseIs200Ok();
        await _sessionSteps.ThenResponseMatches<TokenResponse>(token =>
        {
            Assert.False(string.IsNullOrEmpty(token.AccessToken));
        });
    }

    [Fact]
    public async Task UserAttemptsToLoginWithEmptyEmail()
    {
        await _sessionSteps.WhenTheyAttemptToLogin(new(string.Empty));
        await _sessionSteps.ThenResponseIs400BadRequest();
        await _sessionSteps.ThenResponseIsValidationProblemDetails(new()
        {
            ["Email"] = ["'Email' is not a valid email address."]
        });
    }

    [Fact]
    public async Task UserAttemptsToLoginWithInvalidEmail()
    {
        await _sessionSteps.WhenTheyAttemptToLogin(new("user"));
        await _sessionSteps.ThenResponseIs400BadRequest();
        await _sessionSteps.ThenResponseIsValidationProblemDetails(new()
        {
            ["Email"] = ["'Email' is not a valid email address."]
        });
    }
}
