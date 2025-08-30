using MiniCommerce.Catalog.Web.AcceptanceTests.TestHelpers;

namespace MiniCommerce.Catalog.Web.AcceptanceTests.Steps;

public class SwaggerSteps(TestFixture fixture) : TestStepsBase(fixture)
{
    public async Task WhenTheyAttemptToCheckSwaggerJson()
    {
        HttpResponse = await Fixture.Client.GetAsync("/api/specification.json");
    }

    public async Task WhenTheyAttemptToCheckSwaggerUi()
    {
        HttpResponse = await Fixture.Client.GetAsync("/api");
    }

    public async Task WhenTheyAttemptToCheckSwaggerRedocs()
    {
        HttpResponse = await Fixture.Client.GetAsync("/docs");
    }
}
