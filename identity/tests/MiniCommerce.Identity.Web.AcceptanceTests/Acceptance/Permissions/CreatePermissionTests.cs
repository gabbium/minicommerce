using MiniCommerce.Identity.Application.Contracts;
using MiniCommerce.Identity.Web.AcceptanceTests.Steps;
using MiniCommerce.Identity.Web.AcceptanceTests.TestHelpers.Fixtures;
using MiniCommerce.Identity.Web.Endpoints;

namespace MiniCommerce.Identity.Web.AcceptanceTests.Acceptance.Permissions;

[Collection(nameof(TestCollection))]
public class CreatePermissionTests(TestFixture fixture) : TestBase(fixture)
{
    private readonly PermissionSteps _steps = new(fixture);

    [Fact]
    public async Task UserCreatesPermissionWithValidData()
    {
        await _steps.GivenAnAuthenticatedUser(PermissionNames.CanCreatePermission);
        await _steps.WhenTheyAttemptToCreatePermission(new("catalog:products.list"));
        await _steps.ThenResponseIs201Created();
        await _steps.ThenResponseMatches<PermissionResponse>(permission =>
        {
            Assert.NotEqual(Guid.Empty, permission.Id);
            Assert.Equal("catalog:products.list", permission.Code);
            Assert.False(permission.Deprecated);
        });
    }

    [Fact]
    public async Task UserAttemptsToCreatePermissionWithEmptyCode()
    {
        await _steps.GivenAnAuthenticatedUser(PermissionNames.CanCreatePermission);
        await _steps.WhenTheyAttemptToCreatePermission(new(string.Empty));
        await _steps.ThenResponseIs400BadRequest();
        await _steps.ThenResponseIsValidationProblemDetails(new()
        {
            ["Code"] = ["'Code' must not be empty."]
        });
    }

    [Fact]
    public async Task AnonymousUserAttemptsToCreatePermission()
    {
        await _steps.GivenAnAnonymousUser();
        await _steps.WhenTheyAttemptToCreatePermission(new("catalog:products.list"));
        await _steps.ThenResponseIs401Unauthorized();
    }

    [Fact]
    public async Task ForbiddenUserAttemptsToCreatePermission()
    {
        await _steps.GivenAnAuthenticatedUser();
        await _steps.WhenTheyAttemptToCreatePermission(new("catalog:products.list"));
        await _steps.ThenResponseIs403Forbidden();
    }
}
