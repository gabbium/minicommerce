using MiniCommerce.Identity.Domain.Aggregates.Permissions.Errors;
using MiniCommerce.Identity.Web.AcceptanceTests.Steps;
using MiniCommerce.Identity.Web.AcceptanceTests.TestHelpers.Fixtures;
using MiniCommerce.Identity.Web.Endpoints;

namespace MiniCommerce.Identity.Web.AcceptanceTests.Acceptance.Permissions;

[Collection(nameof(TestCollection))]
public class DeletePermissionTests(TestFixture fixture) : TestBase(fixture)
{
    private readonly PermissionSteps _steps = new(fixture);

    [Fact]
    public async Task UserDeletesPermission()
    {
        await _steps.GivenAnAuthenticatedUser(PermissionNames.CanCreatePermission, PermissionNames.CanDeletePermission);
        var id = await _steps.GivenAnExistingPermission(new("catalog:products.list"));
        await _steps.WhenTheyAttemptToDeletePermission(id);
        await _steps.ThenResponseIs204NoContent();
    }

    [Fact]
    public async Task UserAttemptsToDeletePermissionWithEmptyId()
    {
        await _steps.GivenAnAuthenticatedUser(PermissionNames.CanDeletePermission);
        await _steps.WhenTheyAttemptToDeletePermission(Guid.Empty);
        await _steps.ThenResponseIs400BadRequest();
        await _steps.ThenResponseIsValidationProblemDetails(new()
        {
            ["Id"] = ["'Id' must not be empty."]
        });
    }

    [Fact]
    public async Task AnonymousUserAttemptsToDeletePermission()
    {
        await _steps.GivenAnAnonymousUser();
        await _steps.WhenTheyAttemptToDeletePermission(Guid.Empty);
        await _steps.ThenResponseIs401Unauthorized();
    }

    [Fact]
    public async Task ForbiddenUserAttemptsToDeletePermission()
    {
        await _steps.GivenAnAuthenticatedUser();
        await _steps.WhenTheyAttemptToDeletePermission(Guid.Empty);
        await _steps.ThenResponseIs403Forbidden();
    }

    [Fact]
    public async Task UserAttemptsToDeleteNonExistentPermission()
    {
        await _steps.GivenAnAuthenticatedUser(PermissionNames.CanDeletePermission);
        await _steps.WhenTheyAttemptToDeletePermission(Guid.NewGuid());
        await _steps.ThenResponseIs404NotFound();
        await _steps.ThenResponseIsProblemDetails(PermissionErrors.NotFound.Code, PermissionErrors.NotFound.Description);
    }
}
