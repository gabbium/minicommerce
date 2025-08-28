using MiniCommerce.Identity.Web.AcceptanceTests.TestHelpers;
using MiniCommerce.Identity.Web.Endpoints.V1.Sessions;

namespace MiniCommerce.Identity.Web.AcceptanceTests.Steps;

public class SessionSteps(TestFixture fixture) : CommonStepsBase(fixture)
{
    public async Task WhenTheyAttemptToLogin(LoginUserEndpoint.LoginUserRequest request)
    {
        HttpResponse = await Fixture.Client.PostAsJsonAsync(LoginUserEndpoint.Route, request);
    }
}
