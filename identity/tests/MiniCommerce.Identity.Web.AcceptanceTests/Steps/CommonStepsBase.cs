using MiniCommerce.Identity.Web.AcceptanceTests.TestHelpers;

namespace MiniCommerce.Identity.Web.AcceptanceTests.Steps;

public abstract class CommonStepsBase(TestFixture fixture)
{
    protected TestFixture Fixture = fixture;
    protected HttpResponseMessage? HttpResponse;

    public Task GivenAnAnonymousUser()
    {
        Fixture.Client.DefaultRequestHeaders.Authorization = null;
        return Task.CompletedTask;
    }

    public Task GivenAnAuthenticatedUser(params string[] permissions)
    {
        var accessToken = Fixture.CreateAccessToken(permissions);
        Fixture.Client.DefaultRequestHeaders.Authorization = new("Bearer", accessToken);
        return Task.CompletedTask;
    }

    public Task ThenResponseIs200Ok()
    {
        Assert.NotNull(HttpResponse);
        Assert.Equal(HttpStatusCode.OK, HttpResponse.StatusCode);
        return Task.CompletedTask;
    }

    public Task ThenResponseIs201Created()
    {
        Assert.NotNull(HttpResponse);
        Assert.Equal(HttpStatusCode.Created, HttpResponse.StatusCode);
        return Task.CompletedTask;
    }

    public Task ThenResponseIs204NoContent()
    {
        Assert.NotNull(HttpResponse);
        Assert.Equal(HttpStatusCode.NoContent, HttpResponse.StatusCode);
        return Task.CompletedTask;
    }

    public Task ThenResponseIs400BadRequest()
    {
        Assert.NotNull(HttpResponse);
        Assert.Equal(HttpStatusCode.BadRequest, HttpResponse.StatusCode);
        return Task.CompletedTask;
    }

    public Task ThenResponseIs401Unauthorized()
    {
        Assert.NotNull(HttpResponse);
        Assert.Equal(HttpStatusCode.Unauthorized, HttpResponse.StatusCode);
        return Task.CompletedTask;
    }

    public Task ThenResponseIs403Forbidden()
    {
        Assert.NotNull(HttpResponse);
        Assert.Equal(HttpStatusCode.Forbidden, HttpResponse.StatusCode);
        return Task.CompletedTask;
    }

    public Task ThenResponseIs404NotFound()
    {
        Assert.NotNull(HttpResponse);
        Assert.Equal(HttpStatusCode.NotFound, HttpResponse.StatusCode);
        return Task.CompletedTask;
    }

    public async Task ThenResponseIsProblemDetails(string title, string detail)
    {
        Assert.NotNull(HttpResponse);

        var problem = await ReadBodyAs<ProblemDetails>();
        Assert.Equal(title, problem.Title);
        Assert.Equal(detail, problem.Detail);
        Assert.NotNull(problem.Type);
        Assert.NotNull(problem.Status);
    }

    public async Task ThenResponseIsValidationProblemDetails(Dictionary<string, string[]> errors)
    {
        Assert.NotNull(HttpResponse);

        var problem = await ReadBodyAs<ValidationProblemDetails>();
        Assert.NotEmpty(problem.Errors);
        Assert.Equal(errors, problem.Errors);
        Assert.Null(problem.Detail);
        Assert.Equal(StatusCodes.Status400BadRequest, problem.Status);
        Assert.Equal("One or more validation errors occurred", problem.Title);
        Assert.Equal("https://tools.ietf.org/html/rfc7231#section-6.5.1", problem.Type);
    }

    protected async Task<T> ReadBodyAs<T>()
    {
        var model = await HttpResponse!.Content.ReadFromJsonAsync<T>();
        Assert.NotNull(model);
        return model!;
    }
}
