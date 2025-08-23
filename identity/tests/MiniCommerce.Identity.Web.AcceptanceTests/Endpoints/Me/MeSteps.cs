using MiniCommerce.Identity.Web.AcceptanceTests.Steps;
using MiniCommerce.Identity.Web.AcceptanceTests.TestHelpers;
using MiniCommerce.Identity.Web.Endpoints.V1;

namespace MiniCommerce.Identity.Web.AcceptanceTests.Endpoints.Me;

public class MeSteps(TestFixture fixture) : CommonStepsBase(fixture)
{
    public async Task WhenTheyAttemptToFetchMe()
    {
        HttpResponse = await Fixture.Client.GetAsync("/api/v1/me");
    }

    public async Task ThenTheResponseShouldContainUser(string email)
    {
        var response = await HttpResponse!.Content.ReadFromJsonAsync<MeEndpoint.Response>();
        Assert.NotNull(response);
        Assert.NotEqual(Guid.Empty, response.Id);
        Assert.Equal(email, response.Email);
    }
}
