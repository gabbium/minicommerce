using System.Net;
using MiniCommerce.Identity.Web.AcceptanceTests.TestHelpers;

namespace MiniCommerce.Identity.Web.AcceptanceTests.Steps;

public abstract class CommonStepsBase(TestFixture fixture)
{
    protected TestFixture Fixture = fixture;
    protected HttpResponseMessage? HttpResponse;

    public Task ThenTheResponseShouldBe200OK()
    {
        Assert.NotNull(HttpResponse);
        Assert.Equal(HttpStatusCode.OK, HttpResponse.StatusCode);
        return Task.CompletedTask;
    }
}
