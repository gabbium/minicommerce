using MiniCommerce.Identity.Web.AcceptanceTests.TestHelpers;

namespace MiniCommerce.Identity.Web.AcceptanceTests.Endpoints.V1.Sessions.LoginUser;

[Collection(nameof(TestCollection))]
public class LoginUserFeature(TestFixture fixture) : TestBase(fixture)
{
    private readonly LoginUserSteps _steps = new(fixture);

    [Fact]
    public async Task UserLogsInWithExistentEmail()
    {
        await _steps.GivenAnExistingUser(new("user@minicommerce"));
        await _steps.WhenTheyAttemptToLogin(new("user@minicommerce"));
        await _steps.ThenResponseIs200Ok();
        await _steps.ThenResponseContainsToken();
    }

    [Fact]
    public async Task UserLogsInWithNonExistentEmail()
    {
        await _steps.WhenTheyAttemptToLogin(new("user@minicommerce"));
        await _steps.ThenResponseIs200Ok();
        await _steps.ThenResponseContainsToken();
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
