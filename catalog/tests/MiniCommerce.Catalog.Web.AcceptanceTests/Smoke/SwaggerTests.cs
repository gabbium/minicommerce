using MiniCommerce.Catalog.Web.AcceptanceTests.Steps;
using MiniCommerce.Catalog.Web.AcceptanceTests.TestHelpers;

namespace MiniCommerce.Catalog.Web.AcceptanceTests.Smoke;

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
