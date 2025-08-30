using MiniCommerce.Catalog.Web.AcceptanceTests.TestHelpers;

namespace MiniCommerce.Catalog.Web.AcceptanceTests.Steps;

public class HealthSteps(TestFixture fixture) : TestStepsBase(fixture)
{
    public async Task WhenTheyAttemptToCheckHealth()
    {
        HttpResponse = await Fixture.Client.GetAsync("/api/health");
    }
}
