using MiniCommerce.Identity.Application.Contracts;
using MiniCommerce.Identity.Web.AcceptanceTests.TestHelpers;
using MiniCommerce.Identity.Web.Endpoints.V1.Users;

namespace MiniCommerce.Identity.Web.AcceptanceTests.Steps;

public class UserSteps(TestFixture fixture) : TestStepsBase(fixture)
{
    public async Task<Guid> GivenAnExistingUser(CreateUserEndpoint.CreateUserRequest request)
    {
        var response = await Fixture.Client.PostAsJsonAsync(CreateUserEndpoint.Route, request);
        var created = await response.Content.ReadFromJsonAsync<UserResponse>();
        return created!.Id;
    }

    public async Task WhenTheyAttemptToListUsers(ListUsersEndpoint.ListUsersRequest request)
    {
        HttpResponse = await Fixture.Client.GetAsync(ListUsersEndpoint.BuildRoute(request));
    }

    public async Task WhenTheyAttemptToGetUserById(Guid id)
    {
        HttpResponse = await Fixture.Client.GetAsync(GetUserByIdEndpoint.BuildRoute(id));
    }

    public async Task WhenTheyAttemptToGetCurrentUser()
    {
        HttpResponse = await Fixture.Client.GetAsync(GetCurrentUserEndpoint.Route);
    }

    public async Task WhenTheyAttemptToCreateUser(CreateUserEndpoint.CreateUserRequest request)
    {
        HttpResponse = await Fixture.Client.PostAsJsonAsync(CreateUserEndpoint.Route, request);
    }

    public async Task WhenTheyAttemptToLogin(LoginUserEndpoint.LoginUserRequest request)
    {
        HttpResponse = await Fixture.Client.PostAsJsonAsync(LoginUserEndpoint.Route, request);
    }

    public async Task WhenTheyAttemptToUpdateUserPermissions(Guid id, UpdateUserPermissionsEndpoint.UpdateUserPermissionsRequest request)
    {
        HttpResponse = await Fixture.Client.PatchAsJsonAsync(UpdateUserPermissionsEndpoint.BuildRoute(id), request);
    }

    public async Task WhenTheyAttemptToDeleteUser(Guid id)
    {
        HttpResponse = await Fixture.Client.DeleteAsync(DeleteUserEndpoint.BuildRoute(id));
    }
}
