using MiniCommerce.Identity.Application.Contracts.Sessions;
using MiniCommerce.Identity.Web.AcceptanceTests.Steps;
using MiniCommerce.Identity.Web.AcceptanceTests.TestHelpers;
using MiniCommerce.Identity.Web.Endpoints.V1.Sessions;

namespace MiniCommerce.Identity.Web.AcceptanceTests.Endpoints.V1.Sessions.LoginUser;

public class LoginUserSteps(TestFixture fixture) : CommonStepsBase(fixture)
{
    public async Task GivenAnExistingUser(LoginUserEndpoint.Request request)
    {
        await WhenTheyAttemptToLogin(request);
    }

    public async Task WhenTheyAttemptToLogin(LoginUserEndpoint.Request request)
    {
        HttpResponse = await Fixture.Client.PostAsJsonAsync(LoginUserEndpoint.Route, request);
    }

    public async Task ThenResponseContainsToken()
    {
        var response = await ReadBodyAs<TokenResponse>();
        Assert.False(string.IsNullOrEmpty(response.AccessToken));
    }
}
