using MiniCommerce.Identity.Web.AcceptanceTests.TestHelpers;

namespace MiniCommerce.Identity.Web.AcceptanceTests.Endpoints.Login;

[Collection(nameof(TestCollection))]
public class LoginFeature(TestFixture fixture) : TestBase(fixture)
{
    private readonly LoginSteps _steps = new(fixture);

    [Fact]
    public async Task UserLogsInWithExistentEmail()
    {
        await _steps.GivenAnExistingUser("user@minicommerce.com");
        await _steps.WhenTheyAttemptToLogin("user@minicommerce.com");
        await _steps.ThenTheResponseShouldBe200OK();
        await _steps.ThenTheResponseShouldContainUserAndToken("user@minicommerce.com");
    }

    [Fact]
    public async Task UserLogsInWithNonExistentEmail()
    {
        await _steps.WhenTheyAttemptToLogin("user@minicommerce.com");
        await _steps.ThenTheResponseShouldBe200OK();
        await _steps.ThenTheResponseShouldContainUserAndToken("user@minicommerce.com");
    }

    [Fact]
    public async Task UserAttemptsToLoginWithEmptyEmail()
    {
        await _steps.WhenTheyAttemptToLogin(string.Empty);
        await _steps.ThenTheResponseShouldBe400BadRequest();
        await _steps.ThenTheResponseShouldBeValidationProblemDetails(new()
        {
            ["Email"] = ["'Email' is not a valid email address."]
        });
    }

    [Fact]
    public async Task UserAttemptsToLoginWithInvalidEmail()
    {
        await _steps.WhenTheyAttemptToLogin("user");
        await _steps.ThenTheResponseShouldBe400BadRequest();
        await _steps.ThenTheResponseShouldBeValidationProblemDetails(new()
        {
            ["Email"] = ["'Email' is not a valid email address."]
        });
    }
}
