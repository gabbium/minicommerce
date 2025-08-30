using MiniCommerce.Identity.Application.Contracts;
using MiniCommerce.Identity.Web.AcceptanceTests.Steps;
using MiniCommerce.Identity.Web.AcceptanceTests.TestHelpers.Fixtures;
using MiniCommerce.Identity.Web.Endpoints;

namespace MiniCommerce.Identity.Web.AcceptanceTests.Acceptance.Users;

[Collection(nameof(TestCollection))]
public class LoginUserTests(TestFixture fixture) : TestBase(fixture)
{
    private readonly UserSteps _steps = new(fixture);

    [Fact]
    public async Task UserLogsInWithExistentEmail()
    {
        await _steps.GivenAnAuthenticatedUser(PermissionNames.CanCreateUser);
        await _steps.GivenAnExistingUser(new("user@minicommerce"));
        await _steps.WhenTheyAttemptToLogin(new("user@minicommerce"));
        await _steps.ThenResponseIs200Ok();
        await _steps.ThenResponseMatches<TokenResponse>(token =>
        {
            Assert.False(string.IsNullOrEmpty(token.AccessToken));
        });
    }

    [Fact]
    public async Task UserLogsInWithNonExistentEmail()
    {
        await _steps.WhenTheyAttemptToLogin(new("user@minicommerce"));
        await _steps.ThenResponseIs200Ok();
        await _steps.ThenResponseMatches<TokenResponse>(token =>
        {
            Assert.False(string.IsNullOrEmpty(token.AccessToken));
        });
    }

    [Fact]
    public async Task UserAttemptsToLoginWithEmptyEmail()
    {
        await _steps.WhenTheyAttemptToLogin(new(string.Empty));
        await _steps.ThenResponseIs400BadRequest();
        await _steps.ThenResponseIsValidationProblemDetails(new()
        {
            ["Email"] = ["'Email' is not a valid email address."]
        });
    }

    [Fact]
    public async Task UserAttemptsToLoginWithInvalidEmail()
    {
        await _steps.WhenTheyAttemptToLogin(new("user"));
        await _steps.ThenResponseIs400BadRequest();
        await _steps.ThenResponseIsValidationProblemDetails(new()
        {
            ["Email"] = ["'Email' is not a valid email address."]
        });
    }
}
