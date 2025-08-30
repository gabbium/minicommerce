using MiniCommerce.Identity.Web.AcceptanceTests.TestHelpers;

namespace MiniCommerce.Identity.Web.AcceptanceTests.Steps;

public class HealthSteps(TestFixture fixture) : TestStepsBase(fixture)
{
    public async Task WhenTheyAttemptToCheckHealth()
    {
        HttpResponse = await Fixture.Client.GetAsync("/api/health");
    }
}
