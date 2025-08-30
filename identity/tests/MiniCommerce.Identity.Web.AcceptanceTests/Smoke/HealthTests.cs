using MiniCommerce.Identity.Web.AcceptanceTests.Steps;
using MiniCommerce.Identity.Web.AcceptanceTests.TestHelpers.Fixtures;

namespace MiniCommerce.Identity.Web.AcceptanceTests.Smoke;

[Collection(nameof(TestCollection))]
public class HealthTests(TestFixture fixture) : TestBase(fixture)
{
    private readonly HealthSteps _steps = new(fixture);

    [Fact]
    public async Task AnonymousUserChecksHealth()
    {
        await _steps.GivenAnAnonymousUser();
        await _steps.WhenTheyAttemptToCheckHealth();
        await _steps.ThenResponseIs200Ok();
    }
}
