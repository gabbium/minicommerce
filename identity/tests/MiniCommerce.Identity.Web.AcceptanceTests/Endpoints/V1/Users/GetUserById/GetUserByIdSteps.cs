using MiniCommerce.Identity.Application.Contracts.Users;
using MiniCommerce.Identity.Web.AcceptanceTests.Steps;
using MiniCommerce.Identity.Web.AcceptanceTests.TestHelpers;
using MiniCommerce.Identity.Web.Endpoints.V1.Users;

namespace MiniCommerce.Identity.Web.AcceptanceTests.Endpoints.V1.Users.GetUserById;

public class GetUserByIdSteps(TestFixture fixture) : CommonStepsBase(fixture)
{
    public async Task<Guid> GivenAnExistingUser(CreateUserEndpoint.CreateUserRequest request)
    {
        var response = await Fixture.Client.PostAsJsonAsync(CreateUserEndpoint.Route, request);
        var created = await response.Content.ReadFromJsonAsync<UserResponse>();
        return created!.Id;
    }

    public async Task WhenTheyAttemptToGetUserById(Guid id)
    {
        HttpResponse = await Fixture.Client.GetAsync(GetUserByIdEndpoint.BuildRoute(id));
    }

    public async Task ThenResponseContainsUser(Guid expectedId, string expectedEmail)
    {
        var response = await ReadBodyAs<UserResponse>();
        Assert.Equal(expectedId, response.Id);
        Assert.Equal(expectedEmail, response.Email);
    }
}
