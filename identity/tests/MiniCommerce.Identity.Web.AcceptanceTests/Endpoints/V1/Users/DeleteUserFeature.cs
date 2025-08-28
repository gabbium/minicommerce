using MiniCommerce.Identity.Application.Contracts;
using MiniCommerce.Identity.Application.Features.Users;
using MiniCommerce.Identity.Web.AcceptanceTests.Steps;
using MiniCommerce.Identity.Web.AcceptanceTests.TestHelpers;

namespace MiniCommerce.Identity.Web.AcceptanceTests.Endpoints.V1.Users;

[Collection(nameof(TestCollection))]
public class DeleteUserFeature(TestFixture fixture) : TestBase(fixture)
{
    private readonly UserSteps _steps = new(fixture);

    [Fact]
    public async Task UserDeletesUser()
    {
        await _steps.GivenAnAuthenticatedUser(IdentityPermissionNames.CanCreateUser, IdentityPermissionNames.CanDeleteUser);
        var id = await _steps.GivenAnExistingUser(new("user@minicommerce"));
        await _steps.WhenTheyAttemptToDeleteUser(id);
        await _steps.ThenResponseIs204NoContent();
    }

    [Fact]
    public async Task UserAttemptsToDeleteUserWithEmptyId()
    {
        await _steps.GivenAnAuthenticatedUser(IdentityPermissionNames.CanDeleteUser);
        await _steps.WhenTheyAttemptToDeleteUser(Guid.Empty);
        await _steps.ThenResponseIs400BadRequest();
        await _steps.ThenResponseIsValidationProblemDetails(new()
        {
            ["Id"] = ["'Id' must not be empty."]
        });
    }

    [Fact]
    public async Task AnonymousUserAttemptsToDeleteUser()
    {
        await _steps.GivenAnAnonymousUser();
        await _steps.WhenTheyAttemptToDeleteUser(Guid.Empty);
        await _steps.ThenResponseIs401Unauthorized();
    }

    [Fact]
    public async Task ForbiddenUserAttemptsToDeleteUser()
    {
        await _steps.GivenAnAuthenticatedUser();
        await _steps.WhenTheyAttemptToDeleteUser(Guid.Empty);
        await _steps.ThenResponseIs403Forbidden();
    }

    [Fact]
    public async Task UserAttemptsToDeleteNonExistentUser()
    {
        await _steps.GivenAnAuthenticatedUser(IdentityPermissionNames.CanDeleteUser);
        await _steps.WhenTheyAttemptToDeleteUser(Guid.NewGuid());
        await _steps.ThenResponseIs404NotFound();
        await _steps.ThenResponseIsProblemDetails(UserErrors.NotFound.Code, UserErrors.NotFound.Description);
    }
}
