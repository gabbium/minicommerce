using MiniCommerce.Identity.Application.Contracts;
using MiniCommerce.Identity.Application.Contracts.Users;
using MiniCommerce.Identity.Web.AcceptanceTests.Steps;
using MiniCommerce.Identity.Web.AcceptanceTests.TestHelpers;

namespace MiniCommerce.Identity.Web.AcceptanceTests.Endpoints.V1.Users;

[Collection(nameof(TestCollection))]
public class CreateUserFeature(TestFixture fixture) : TestBase(fixture)
{
    private readonly UserSteps _steps = new(fixture);

    [Fact]
    public async Task UserCreatesUserWithValidData()
    {
        await _steps.GivenAnAuthenticatedUser(IdentityPermissionNames.CanCreateUser);
        await _steps.WhenTheyAttemptToCreateUser(new("user@minicommerce"));
        await _steps.ThenResponseIs201Created();
        await _steps.ThenResponseMatches<UserResponse>(user =>
        {
            Assert.NotEqual(Guid.Empty, user.Id);
            Assert.Equal("user@minicommerce", user.Email);
        });
    }

    [Fact]
    public async Task UserAttemptsToCreateUserWithEmptyEmail()
    {
        await _steps.GivenAnAuthenticatedUser(IdentityPermissionNames.CanCreateUser);
        await _steps.WhenTheyAttemptToCreateUser(new(string.Empty));
        await _steps.ThenResponseIs400BadRequest();
        await _steps.ThenResponseIsValidationProblemDetails(new()
        {
            ["Email"] = ["'Email' is not a valid email address."]
        });
    }

    [Fact]
    public async Task UserAttemptsToCreateUserWithInvalidEmail()
    {
        await _steps.GivenAnAuthenticatedUser(IdentityPermissionNames.CanCreateUser);
        await _steps.WhenTheyAttemptToCreateUser(new("user"));
        await _steps.ThenResponseIs400BadRequest();
        await _steps.ThenResponseIsValidationProblemDetails(new()
        {
            ["Email"] = ["'Email' is not a valid email address."]
        });
    }

    [Fact]
    public async Task AnonymousUserAttemptsToCreateUser()
    {
        await _steps.GivenAnAnonymousUser();
        await _steps.WhenTheyAttemptToCreateUser(new("user@minicommerce"));
        await _steps.ThenResponseIs401Unauthorized();
    }

    [Fact]
    public async Task ForbiddenUserAttemptsToCreateUser()
    {
        await _steps.GivenAnAuthenticatedUser();
        await _steps.WhenTheyAttemptToCreateUser(new("user@minicommerce"));
        await _steps.ThenResponseIs403Forbidden();
    }
}
