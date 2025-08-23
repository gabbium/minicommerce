using MiniCommerce.Identity.Application.Models;
using MiniCommerce.Identity.Web.AcceptanceTests.Steps;
using MiniCommerce.Identity.Web.AcceptanceTests.TestHelpers;
using MiniCommerce.Identity.Web.Endpoints.V1;

namespace MiniCommerce.Identity.Web.AcceptanceTests.Endpoints.Login;

public class LoginSteps(TestFixture fixture) : CommonStepsBase(fixture)
{
    public async Task GivenAnExistingUser(string email)
    {
        await WhenTheyAttemptToLogin(email);
    }

    public async Task WhenTheyAttemptToLogin(string email)
    {
        var request = new LoginEndpoint.Request(email);
        HttpResponse = await Fixture.Client.PostAsJsonAsync("/api/v1/login", request);
    }

    public async Task ThenTheResponseShouldContainUserAndToken(string email)
    {
        var response = await HttpResponse!.Content.ReadFromJsonAsync<AuthResponse>();
        Assert.NotNull(response);
        Assert.Equal(email, response.User.Email);
        Assert.False(string.IsNullOrEmpty(response.Token.AccessToken));
    }
}
