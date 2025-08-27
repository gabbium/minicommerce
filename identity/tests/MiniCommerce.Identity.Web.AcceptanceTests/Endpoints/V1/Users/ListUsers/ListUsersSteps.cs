using MiniCommerce.Identity.Application.Contracts.Users;
using MiniCommerce.Identity.Web.AcceptanceTests.Steps;
using MiniCommerce.Identity.Web.AcceptanceTests.TestHelpers;
using MiniCommerce.Identity.Web.AcceptanceTests.TestHelpers.Models;
using MiniCommerce.Identity.Web.Endpoints.V1.Users;

namespace MiniCommerce.Identity.Web.AcceptanceTests.Endpoints.V1.Users.ListUsers;

public class ListUsersSteps(TestFixture fixture) : CommonStepsBase(fixture)
{
    public async Task GivenUsersExist(List<CreateUserEndpoint.CreateUserRequest> requests)
    {
        foreach (var request in requests)
        {
            await Fixture.Client.PostAsJsonAsync(CreateUserEndpoint.Route, request);
        }
    }

    public async Task WhenTheyAttemptToListUsers(ListUsersEndpoint.ListUsersRequest request)
    {
        HttpResponse = await Fixture.Client.GetAsync(ListUsersEndpoint.BuildRoute(request));
    }

    public async Task ThenResponseContainsUsers(
        int expectedPage,
        int expectedPageSize,
        int expectedItemsCount,
        int expectedTotalCount,
        int expectedTotalPages)
    {
        var paged = await ReadBodyAs<FlatPagedList<UserResponse>>();
        Assert.Equal(expectedPage, paged.Page);
        Assert.Equal(expectedPageSize, paged.PageSize);
        Assert.Equal(expectedItemsCount, paged.Items.Count);
        Assert.Equal(expectedTotalCount, paged.TotalCount);
        Assert.Equal(expectedTotalPages, paged.TotalPages);
    }
}
