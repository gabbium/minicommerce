using MiniCommerce.Identity.Application.Contracts.Users;
using MiniCommerce.Identity.Web.AcceptanceTests.Steps;
using MiniCommerce.Identity.Web.AcceptanceTests.TestHelpers;
using MiniCommerce.Identity.Web.Endpoints.V1.Users;

namespace MiniCommerce.Identity.Web.AcceptanceTests.Endpoints.V1.Users.GetCurrentUser;

public class GetCurrentUserSteps(TestFixture fixture) : CommonStepsBase(fixture)
{
    public async Task WhenTheyAttemptToGetCurrentUser()
    {
        HttpResponse = await Fixture.Client.GetAsync(GetCurrentUserEndpoint.Route);
    }

    public async Task ThenResponseContainsUserInfo(string expectedEmail)
    {
        var response = await ReadBodyAs<UserResponse>();
        Assert.NotEqual(Guid.Empty, response.Id);
        Assert.Equal(expectedEmail, response.Email);
    }
}
