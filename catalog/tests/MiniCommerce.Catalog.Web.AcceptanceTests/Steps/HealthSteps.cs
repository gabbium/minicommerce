using MiniCommerce.Catalog.Web.AcceptanceTests.TestHelpers.Fixtures;

namespace MiniCommerce.Catalog.Web.AcceptanceTests.Steps;

public class HealthSteps(TestFixture fixture) : StepsBase(fixture)
{
    public async Task WhenTheyAttemptToCheckHealth()
    {
        HttpResponse = await Fixture.Client.GetAsync("/api/health");
    }
}
