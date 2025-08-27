using MiniCommerce.Identity.Application.Contracts.Users;
using MiniCommerce.Identity.Web.AcceptanceTests.Steps;
using MiniCommerce.Identity.Web.AcceptanceTests.TestHelpers;
using MiniCommerce.Identity.Web.Endpoints.V1.Users;

namespace MiniCommerce.Identity.Web.AcceptanceTests.Endpoints.V1.Users.DeleteUser;

public class DeleteUserSteps(TestFixture fixture) : CommonStepsBase(fixture)
{
    public async Task<Guid> GivenAnExistingUser(CreateUserEndpoint.CreateUserRequest request)
    {
        var response = await Fixture.Client.PostAsJsonAsync(CreateUserEndpoint.Route, request);
        var created = await response.Content.ReadFromJsonAsync<UserResponse>();
        return created!.Id;
    }

    public async Task WhenTheyAttemptToDeleteUser(Guid id)
    {
        HttpResponse = await Fixture.Client.DeleteAsync(DeleteUserEndpoint.BuildRoute(id));
    }
}
