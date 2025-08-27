using MiniCommerce.Identity.Application.Contracts.Users;
using MiniCommerce.Identity.Web.AcceptanceTests.Steps;
using MiniCommerce.Identity.Web.AcceptanceTests.TestHelpers;
using MiniCommerce.Identity.Web.Endpoints.V1.Users;

namespace MiniCommerce.Identity.Web.AcceptanceTests.Endpoints.V1.Users.CreateUser;

public class CreateUserSteps(TestFixture fixture) : CommonStepsBase(fixture)
{
    public async Task WhenTheyAttemptToCreateUser(CreateUserEndpoint.CreateUserRequest request)
    {
        HttpResponse = await Fixture.Client.PostAsJsonAsync(CreateUserEndpoint.Route, request);
    }

    public async Task ThenResponseContainsUser(string expectedEmail)
    {
        var response = await ReadBodyAs<UserResponse>();
        Assert.NotEqual(Guid.Empty, response.Id);
        Assert.Equal(expectedEmail, response.Email);
    }
}
