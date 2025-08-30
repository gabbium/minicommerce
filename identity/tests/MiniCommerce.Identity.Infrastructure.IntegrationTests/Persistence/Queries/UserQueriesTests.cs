using MiniCommerce.Identity.Application.Abstractions;
using MiniCommerce.Identity.Application.UseCases.Users.ListUsers;
using MiniCommerce.Identity.Domain.Aggregates.Users.Entities;
using MiniCommerce.Identity.Domain.Aggregates.Users.Repositories;
using MiniCommerce.Identity.Infrastructure.IntegrationTests.TestHelpers;
using MiniCommerce.Identity.Infrastructure.IntegrationTests.TestHelpers.Fixtures;

namespace MiniCommerce.Identity.Infrastructure.IntegrationTests.Persistence.Queries;

[Collection(nameof(TestCollection))]
public class UserQueriesTests(TestFixture fixture) : TestBase(fixture)
{
    private readonly IUserQueries _queries =
        fixture.GetRequiredService<IUserQueries>();

    private readonly IUserRepository _repository =
        fixture.GetRequiredService<IUserRepository>();

    private readonly IUnitOfWork _unitOfWork =
        fixture.GetRequiredService<IUnitOfWork>();

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
        await _unitOfWork.SaveChangesAsync();

        var query = new ListUsersQuery(1, 2);

        // Act
        var result = await _queries.ListAsync(query);

        // Assert
        Assert.Equal(1, result.PageNumber);
        Assert.Equal(2, result.PageSize);
        Assert.Equal(3, result.TotalItems);
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
