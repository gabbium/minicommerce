using MiniCommerce.Identity.Application.Contracts;
using MiniCommerce.Identity.Application.Features.Permissions;
using MiniCommerce.Identity.Web.AcceptanceTests.TestHelpers;

namespace MiniCommerce.Identity.Web.AcceptanceTests.Endpoints.V1.Permissions.GetPermissionById;

[Collection(nameof(TestCollection))]
public class GetPermissionByIdFeature(TestFixture fixture) : TestBase(fixture)
{
    private readonly GetPermissionByIdSteps _steps = new(fixture);

    [Fact]
    public async Task UserGetsPermissionById()
    {
        await _steps.GivenAnAuthenticatedUser(IdentityPermissionNames.CanCreatePermission, IdentityPermissionNames.CanGetPermissionById);
        var id = await _steps.GivenAnExistingPermission(new("catalog:products.list"));
        await _steps.WhenTheyAttemptToGetPermissionById(id);
        await _steps.ThenResponseIs200Ok();
        await _steps.ThenResponseContainsPermission(id, "catalog:products.list");
    }

    [Fact]
    public async Task UserAttemptsToGetPermissionWithEmptyId()
    {
        await _steps.GivenAnAuthenticatedUser(IdentityPermissionNames.CanGetPermissionById);
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
        await _steps.GivenAnAuthenticatedUser(IdentityPermissionNames.CanGetPermissionById);
        await _steps.WhenTheyAttemptToGetPermissionById(Guid.NewGuid());
        await _steps.ThenResponseIs404NotFound();
        await _steps.ThenResponseIsProblemDetails(PermissionErrors.NotFound.Code, PermissionErrors.NotFound.Description);
    }
}
