using MiniCommerce.Identity.Application.Abstractions;
using MiniCommerce.Identity.Application.UseCases.Permissions.ListPermissions;
using MiniCommerce.Identity.Domain.Aggregates.Permissions.Entities;
using MiniCommerce.Identity.Domain.Aggregates.Permissions.Repositories;
using MiniCommerce.Identity.Infrastructure.IntegrationTests.TestHelpers;
using MiniCommerce.Identity.Infrastructure.IntegrationTests.TestHelpers.Fixtures;

namespace MiniCommerce.Identity.Infrastructure.IntegrationTests.Persistence.Queries;

[Collection(nameof(TestCollection))]
public class PermissionsQueriesTests(TestFixture fixture) : TestBase(fixture)
{
    private readonly IPermissionQueries _queries =
        fixture.GetRequiredService<IPermissionQueries>();

    private readonly IPermissionRepository _repository =
        fixture.GetRequiredService<IPermissionRepository>();

    private readonly IUnitOfWork _unitOfWork =
        fixture.GetRequiredService<IUnitOfWork>();

    [Fact]
    public async Task PermissionsArePagedAndOrderedCorrectly()
    {
        // Arrange
        var permission1 = new Permission("catalog:products.create");
        var permission2 = new Permission("catalog:products.delete");
        var permission3 = new Permission("catalog:products.list");

        await _repository.AddAsync(permission1);
        await _repository.AddAsync(permission2);
        await _repository.AddAsync(permission3);
        await _unitOfWork.SaveChangesAsync();

        var query = new ListPermissionsQuery(1, 2);

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
                Assert.Equal(permission1.Id, first.Id);
                Assert.Equal(permission1.Code, first.Code);
                Assert.Equal(permission1.Deprecated, first.Deprecated);
            },
            second =>
            {
                Assert.Equal(permission2.Id, second.Id);
                Assert.Equal(permission2.Code, second.Code);
                Assert.Equal(permission2.Deprecated, second.Deprecated);
            }
        );
    }
}
