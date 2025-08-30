using MiniCommerce.Identity.Web.AcceptanceTests.Steps;
using MiniCommerce.Identity.Web.AcceptanceTests.TestHelpers.Fixtures;

namespace MiniCommerce.Identity.Web.AcceptanceTests.Smoke;

[Collection(nameof(TestCollection))]
public class SwaggerTests(TestFixture fixture) : TestBase(fixture)
{
    private readonly SwaggerSteps _steps = new(fixture);

    [Fact]
    public async Task AnonymousUserChecksSwaggerJson()
    {
        await _steps.GivenAnAnonymousUser();
        await _steps.WhenTheyAttemptToCheckSwaggerJson();
        await _steps.ThenResponseIs200Ok();
    }

    [Fact]
    public async Task AnonymousUserChecksSwaggerUi()
    {
        await _steps.GivenAnAnonymousUser();
        await _steps.WhenTheyAttemptToCheckSwaggerUi();
        await _steps.ThenResponseIs200Ok();
    }

    [Fact]
    public async Task AnonymousUserChecksSwaggerRedocs()
    {
        await _steps.GivenAnAnonymousUser();
        await _steps.WhenTheyAttemptToCheckSwaggerRedocs();
        await _steps.ThenResponseIs200Ok();
    }
}
