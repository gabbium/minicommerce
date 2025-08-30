using MiniCommerce.Identity.Application.Contracts;
using MiniCommerce.Identity.Domain.Aggregates.Permissions.Errors;
using MiniCommerce.Identity.Web.AcceptanceTests.Steps;
using MiniCommerce.Identity.Web.AcceptanceTests.TestHelpers;
using MiniCommerce.Identity.Web.Endpoints;

namespace MiniCommerce.Identity.Web.AcceptanceTests.Acceptance.Permissions;

[Collection(nameof(TestCollection))]
public class DeprecatePermissionTests(TestFixture fixture) : TestBase(fixture)
{
    private readonly PermissionSteps _steps = new(fixture);

    [Fact]
    public async Task UserDeprecatesPermission()
    {
        await _steps.GivenAnAuthenticatedUser(Policies.CanCreatePermission, Policies.CanDeprecatePermission);
        var id = await _steps.GivenAnExistingPermission(new("catalog:products.list"));
        await _steps.WhenTheyAttemptToDeprecatePermission(id);
        await _steps.ThenResponseIs200Ok();
        await _steps.ThenResponseMatches<PermissionResponse>(permission =>
        {
            Assert.Equal(id, permission.Id);
            Assert.Equal("catalog:products.list", permission.Code);
            Assert.True(permission.Deprecated);
        });
    }

    [Fact]
    public async Task UserAttemptsToDeprecatePermissionWithEmptyId()
    {
        await _steps.GivenAnAuthenticatedUser(Policies.CanDeprecatePermission);
        await _steps.WhenTheyAttemptToDeprecatePermission(Guid.Empty);
        await _steps.ThenResponseIs400BadRequest();
        await _steps.ThenResponseIsValidationProblemDetails(new()
        {
            ["Id"] = ["'Id' must not be empty."]
        });
    }

    [Fact]
    public async Task AnonymousUserAttemptsToDeprecatePermission()
    {
        await _steps.GivenAnAnonymousUser();
        await _steps.WhenTheyAttemptToDeprecatePermission(Guid.NewGuid());
        await _steps.ThenResponseIs401Unauthorized();
    }

    [Fact]
    public async Task ForbiddenUserAttemptsToDeprecatePermission()
    {
        await _steps.GivenAnAuthenticatedUser();
        await _steps.WhenTheyAttemptToDeprecatePermission(Guid.NewGuid());
        await _steps.ThenResponseIs403Forbidden();
    }

    [Fact]
    public async Task UserAttemptsToDeprecateNonExistentPermission()
    {
        await _steps.GivenAnAuthenticatedUser(Policies.CanDeprecatePermission);
        await _steps.WhenTheyAttemptToDeprecatePermission(Guid.NewGuid());
        await _steps.ThenResponseIs404NotFound();
        await _steps.ThenResponseIsProblemDetails(PermissionErrors.NotFound.Code, PermissionErrors.NotFound.Description);
    }
}
