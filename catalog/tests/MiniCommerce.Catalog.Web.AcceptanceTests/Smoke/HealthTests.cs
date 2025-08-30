using MiniCommerce.Catalog.Web.AcceptanceTests.Steps;
using MiniCommerce.Catalog.Web.AcceptanceTests.TestHelpers;

namespace MiniCommerce.Catalog.Web.AcceptanceTests.Smoke;

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
