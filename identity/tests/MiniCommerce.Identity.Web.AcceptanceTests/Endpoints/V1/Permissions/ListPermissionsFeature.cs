using MiniCommerce.Identity.Application.Contracts;
using MiniCommerce.Identity.Application.Contracts.Permissions;
using MiniCommerce.Identity.Web.AcceptanceTests.Steps;
using MiniCommerce.Identity.Web.AcceptanceTests.TestHelpers;
using MiniCommerce.Identity.Web.AcceptanceTests.TestHelpers.Models;

namespace MiniCommerce.Identity.Web.AcceptanceTests.Endpoints.V1.Permissions;

[Collection(nameof(TestCollection))]
public class ListPermissionsFeature(TestFixture fixture) : TestBase(fixture)
{
    private readonly PermissionSteps _steps = new(fixture);

    [Fact]
    public async Task UserListsPermissions()
    {
        await _steps.GivenAnAuthenticatedUser(IdentityPermissionNames.CanCreatePermission, IdentityPermissionNames.CanListPermissions);
        await _steps.GivenAnExistingPermission(new("catalog:products.list"));
        await _steps.GivenAnExistingPermission(new("catalog:products.create"));
        await _steps.GivenAnExistingPermission(new("catalog:products.delete"));
        await _steps.WhenTheyAttemptToListPermissions(new(1, 2));
        await _steps.ThenResponseIs200Ok();
        await _steps.ThenResponseMatches<FlatPagedList<PermissionResponse>>(paged =>
        {
            Assert.Equal(1, paged.Page);
            Assert.Equal(2, paged.PageSize);
            Assert.Equal(2, paged.Items.Count);
            Assert.Equal(3, paged.TotalCount);
            Assert.Equal(2, paged.TotalPages);
        });
    }

    [Fact]
    public async Task AnonymousUserAttemptsToListPermissions()
    {
        await _steps.GivenAnAnonymousUser();
        await _steps.WhenTheyAttemptToListPermissions(new(1, 2));
        await _steps.ThenResponseIs401Unauthorized();
    }

    [Fact]
    public async Task ForbiddenUserAttemptsToListPermissions()
    {
        await _steps.GivenAnAuthenticatedUser();
        await _steps.WhenTheyAttemptToListPermissions(new(1, 2));
        await _steps.ThenResponseIs403Forbidden();
    }
}
