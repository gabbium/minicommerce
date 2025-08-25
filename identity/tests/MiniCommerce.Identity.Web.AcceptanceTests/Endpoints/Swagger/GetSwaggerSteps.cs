using MiniCommerce.Identity.Web.AcceptanceTests.Steps;
using MiniCommerce.Identity.Web.AcceptanceTests.TestHelpers;

namespace MiniCommerce.Identity.Web.AcceptanceTests.Endpoints.Swagger;

public class GetSwaggerSteps(TestFixture fixture) : CommonStepsBase(fixture)
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
