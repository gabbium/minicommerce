using MiniCommerce.Identity.Application.Contracts;
using MiniCommerce.Identity.Application.Features.Permissions;
using MiniCommerce.Identity.Web.AcceptanceTests.TestHelpers;

namespace MiniCommerce.Identity.Web.AcceptanceTests.Endpoints.V1.Permissions.DeletePermission;

[Collection(nameof(TestCollection))]
public class DeletePermissionFeature(TestFixture fixture) : TestBase(fixture)
{
    private readonly DeletePermissionSteps _steps = new(fixture);

    [Fact]
    public async Task UserDeletesPermission()
    {
        await _steps.GivenAnAuthenticatedUser(IdentityPermissionNames.CanCreatePermission, IdentityPermissionNames.CanDeletePermission);
        var id = await _steps.GivenAnExistingPermission(new("catalog:products.list"));
        await _steps.WhenTheyAttemptToDeletePermission(id);
        await _steps.ThenResponseIs204NoContent();
    }

    [Fact]
    public async Task UserAttemptsToDeletePermissionWithEmptyId()
    {
        await _steps.GivenAnAuthenticatedUser(IdentityPermissionNames.CanDeletePermission);
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
        await _steps.GivenAnAuthenticatedUser(IdentityPermissionNames.CanDeletePermission);
        await _steps.WhenTheyAttemptToDeletePermission(Guid.NewGuid());
        await _steps.ThenResponseIs404NotFound();
        await _steps.ThenResponseIsProblemDetails(PermissionErrors.NotFound.Code, PermissionErrors.NotFound.Description);
    }
}
