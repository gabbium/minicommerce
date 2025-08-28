using MiniCommerce.Identity.Application.Contracts;
using MiniCommerce.Identity.Application.Contracts.Users;
using MiniCommerce.Identity.Application.Features.Users;
using MiniCommerce.Identity.Web.AcceptanceTests.Steps;
using MiniCommerce.Identity.Web.AcceptanceTests.TestHelpers;

namespace MiniCommerce.Identity.Web.AcceptanceTests.Endpoints.V1.Users;

[Collection(nameof(TestCollection))]
public class GetUserByIdFeature(TestFixture fixture) : TestBase(fixture)
{
    private readonly UserSteps _steps = new(fixture);

    [Fact]
    public async Task UserGetsUserById()
    {
        await _steps.GivenAnAuthenticatedUser(IdentityPermissionNames.CanCreateUser, IdentityPermissionNames.CanGetUserById);
        var id = await _steps.GivenAnExistingUser(new("user@minicommerce"));
        await _steps.WhenTheyAttemptToGetUserById(id);
        await _steps.ThenResponseIs200Ok();
        await _steps.ThenResponseMatches<UserResponse>(user =>
        {
            Assert.Equal(id, user.Id);
            Assert.Equal("user@minicommerce", user.Email);
        });
    }

    [Fact]
    public async Task UserAttemptsToGetUserWithEmptyId()
    {
        await _steps.GivenAnAuthenticatedUser(IdentityPermissionNames.CanGetUserById);
        await _steps.WhenTheyAttemptToGetUserById(Guid.Empty);
        await _steps.ThenResponseIs400BadRequest();
        await _steps.ThenResponseIsValidationProblemDetails(new()
        {
            ["Id"] = ["'Id' must not be empty."]
        });
    }

    [Fact]
    public async Task AnonymousUserAttemptsToGetUserById()
    {
        await _steps.GivenAnAnonymousUser();
        await _steps.WhenTheyAttemptToGetUserById(Guid.NewGuid());
        await _steps.ThenResponseIs401Unauthorized();
    }

    [Fact]
    public async Task ForbiddenUserAttemptsToGetUserById()
    {
        await _steps.GivenAnAuthenticatedUser();
        await _steps.WhenTheyAttemptToGetUserById(Guid.NewGuid());
        await _steps.ThenResponseIs403Forbidden();
    }

    [Fact]
    public async Task UserAttemptsToGetNonExistentUser()
    {
        await _steps.GivenAnAuthenticatedUser(IdentityPermissionNames.CanGetUserById);
        await _steps.WhenTheyAttemptToGetUserById(Guid.NewGuid());
        await _steps.ThenResponseIs404NotFound();
        await _steps.ThenResponseIsProblemDetails(UserErrors.NotFound.Code, UserErrors.NotFound.Description);
    }
}
