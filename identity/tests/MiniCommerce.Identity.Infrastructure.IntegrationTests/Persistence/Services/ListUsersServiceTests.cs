using MiniCommerce.Identity.Application.Features.Users.ListUsers;
using MiniCommerce.Identity.Domain.Aggregates.Users;
using MiniCommerce.Identity.Infrastructure.IntegrationTests.TestHelpers;

namespace MiniCommerce.Identity.Infrastructure.IntegrationTests.Persistence.Services;

[Collection(nameof(TestCollection))]
public class ListUsersServiceTests(TestFixture fixture) : TestBase(fixture)
{
    private readonly IListUsersService _service =
        fixture.GetRequiredService<IListUsersService>();

    private readonly IUserRepository _repository =
        fixture.GetRequiredService<IUserRepository>();

    [Fact]
    public async Task UsersArePagedAndOrderedCorrectly()
    {
        // Arrange
        var user1 = new User("user1@minicommerce");
        var user2 = new User("user2@minicommerce");
        var user3 = new User("user3@minicommerce");

        await _repository.AddAsync(user1);
        await _repository.AddAsync(user2);
        await _repository.AddAsync(user3);
        await _repository.SaveChangesAsync();

        var query = new ListUsersQuery(1, 2);

        // Act
        var result = await _service.ListAsync(query);

        // Assert
        Assert.Equal(1, result.Page);
        Assert.Equal(2, result.PageSize);
        Assert.Equal(3, result.TotalCount);
        Assert.Equal(2, result.TotalPages);
        Assert.Equal(2, result.Items.Count);

        Assert.Collection(
            result.Items,
            first =>
            {
                Assert.Equal(user1.Id, first.Id);
                Assert.Equal(user1.Email, first.Email);
            },
            second =>
            {
                Assert.Equal(user2.Id, second.Id);
                Assert.Equal(user2.Email, second.Email);
            }
        );
    }
}
