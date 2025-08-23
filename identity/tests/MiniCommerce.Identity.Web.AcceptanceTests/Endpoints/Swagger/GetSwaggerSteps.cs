using MiniCommerce.Identity.Web.AcceptanceTests.Steps;
using MiniCommerce.Identity.Web.AcceptanceTests.TestHelpers;

namespace MiniCommerce.Identity.Web.AcceptanceTests.Endpoints.Swagger;

public class GetSwaggerSteps(TestFixture fixture) : CommonStepsBase(fixture)
{
    public async Task WhenTheyAttemptToChecksSwaggerJson()
    {
        HttpResponse = await Fixture.Client.GetAsync("/api/specification.json");
    }

    public async Task WhenTheyAttemptToChecksSwaggerUi()
    {
        HttpResponse = await Fixture.Client.GetAsync("/api");
    }

    public async Task WhenTheyAttemptToChecksSwaggerRedocs()
    {
        HttpResponse = await Fixture.Client.GetAsync("/docs");
    }
}
