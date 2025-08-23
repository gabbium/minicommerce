using MiniCommerce.Identity.Web.AcceptanceTests.TestHelpers;

namespace MiniCommerce.Identity.Web.AcceptanceTests.Endpoints.Swagger;

[Collection(nameof(TestCollection))]
public class GetSwaggerFeature(TestFixture fixture) : TestBase(fixture)
{
    private readonly GetSwaggerSteps _steps = new(fixture);

    [Fact]
    public async Task AnonymousUserChecksSwaggerJson()
    {
        await _steps.GivenAnAnonymousUser();
        await _steps.WhenTheyAttemptToChecksSwaggerJson();
        await _steps.ThenTheResponseShouldBe200OK();
    }

    [Fact]
    public async Task AnonymousUserChecksSwaggerUi()
    {
        await _steps.GivenAnAnonymousUser();
        await _steps.WhenTheyAttemptToChecksSwaggerUi();
        await _steps.ThenTheResponseShouldBe200OK();
    }

    [Fact]
    public async Task AnonymousUserChecksSwaggerRedocs()
    {
        await _steps.GivenAnAnonymousUser();
        await _steps.WhenTheyAttemptToChecksSwaggerRedocs();
        await _steps.ThenTheResponseShouldBe200OK();
    }
}
