using MiniCommerce.Identity.Application.Models;
using MiniCommerce.Identity.Web.AcceptanceTests.Steps;
using MiniCommerce.Identity.Web.AcceptanceTests.TestHelpers;
using MiniCommerce.Identity.Web.Endpoints.V1.Auth;

namespace MiniCommerce.Identity.Web.AcceptanceTests.Endpoints.Auth.Login;

public class LoginSteps(TestFixture fixture) : CommonStepsBase(fixture)
{
    public async Task GivenAnExistingUser(LoginEndpoint.Request request)
    {
        await WhenTheyAttemptToLogin(request);
    }

    public async Task WhenTheyAttemptToLogin(LoginEndpoint.Request request)
    {
        HttpResponse = await Fixture.Client.PostAsJsonAsync(LoginEndpoint.Route, request);
    }

    public async Task ThenResponseContainsAuthInfo(string expectedEmail)
    {
        var response = await ReadBodyAs<AuthResponse>();
        Assert.Equal(expectedEmail, response.User.Email);
        Assert.False(string.IsNullOrEmpty(response.Token.AccessToken));
    }
}
