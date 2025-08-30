using MiniCommerce.Identity.Application.Contracts;
using MiniCommerce.Identity.Domain.Aggregates.Permissions.Errors;
using MiniCommerce.Identity.Web.AcceptanceTests.Steps;
using MiniCommerce.Identity.Web.AcceptanceTests.TestHelpers;
using MiniCommerce.Identity.Web.Endpoints;

namespace MiniCommerce.Identity.Web.AcceptanceTests.Acceptance.Permissions;

[Collection(nameof(TestCollection))]
public class GetPermissionByIdTests(TestFixture fixture) : TestBase(fixture)
{
    private readonly PermissionSteps _steps = new(fixture);

    [Fact]
    public async Task UserGetsPermissionById()
    {
        await _steps.GivenAnAuthenticatedUser(Policies.CanCreatePermission, Policies.CanGetPermissionById);
        var id = await _steps.GivenAnExistingPermission(new("catalog:products.list"));
        await _steps.WhenTheyAttemptToGetPermissionById(id);
        await _steps.ThenResponseIs200Ok();
        await _steps.ThenResponseMatches<PermissionResponse>(permission =>
        {
            Assert.NotEqual(Guid.Empty, permission.Id);
            Assert.Equal("catalog:products.list", permission.Code);
            Assert.False(permission.Deprecated);
        });
    }

    [Fact]
    public async Task UserAttemptsToGetPermissionWithEmptyId()
    {
        await _steps.GivenAnAuthenticatedUser(Policies.CanGetPermissionById);
        await _steps.WhenTheyAttemptToGetPermissionById(Guid.Empty);
        await _steps.ThenResponseIs400BadRequest();
        await _steps.ThenResponseIsValidationProblemDetails(new()
        {
            ["Id"] = ["'Id' must not be empty."]
        });
    }

    [Fact]
    public async Task AnonymousUserAttemptsToGetPermissionById()
    {
        await _steps.GivenAnAnonymousUser();
        await _steps.WhenTheyAttemptToGetPermissionById(Guid.NewGuid());
        await _steps.ThenResponseIs401Unauthorized();
    }

    [Fact]
    public async Task ForbiddenUserAttemptsToGetPermissionById()
    {
        await _steps.GivenAnAuthenticatedUser();
        await _steps.WhenTheyAttemptToGetPermissionById(Guid.NewGuid());
        await _steps.ThenResponseIs403Forbidden();
    }

    [Fact]
    public async Task UserAttemptsToGetNonExistentPermission()
    {
        await _steps.GivenAnAuthenticatedUser(Policies.CanGetPermissionById);
        await _steps.WhenTheyAttemptToGetPermissionById(Guid.NewGuid());
        await _steps.ThenResponseIs404NotFound();
        await _steps.ThenResponseIsProblemDetails(PermissionErrors.NotFound.Code, PermissionErrors.NotFound.Description);
    }
}
