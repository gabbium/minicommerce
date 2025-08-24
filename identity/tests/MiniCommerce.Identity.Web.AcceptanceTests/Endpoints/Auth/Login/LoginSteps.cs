using MiniCommerce.Identity.Application.Models;
using MiniCommerce.Identity.Web.AcceptanceTests.Steps;
using MiniCommerce.Identity.Web.AcceptanceTests.TestHelpers;
using MiniCommerce.Identity.Web.Endpoints.V1.Auth;

namespace MiniCommerce.Identity.Web.AcceptanceTests.Endpoints.Auth.Login;

public class LoginSteps(TestFixture fixture) : CommonStepsBase(fixture)
{
    private LoginEndpoint.Request _request = null!;

    public async Task GivenAnExistingUser(string email)
    {
        await WhenTheyAttemptToLogin(email);
    }

    public async Task WhenTheyAttemptToLogin(string email)
    {
        _request = new LoginEndpoint.Request(email);
        HttpResponse = await Fixture.Client.PostAsJsonAsync(LoginEndpoint.Route, _request);
    }

    public async Task ThenTheResponseShouldContainUserAndToken()
    {
        var response = await HttpResponse!.Content.ReadFromJsonAsync<AuthResponse>();
        Assert.NotNull(response);
        Assert.Equal(_request.Email, response.User.Email);
        Assert.False(string.IsNullOrEmpty(response.Token.AccessToken));
    }
}
