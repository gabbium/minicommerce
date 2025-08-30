using MiniCommerce.Catalog.Domain.ProductAggregate.Errors;
using MiniCommerce.Catalog.Domain.ProductAggregate.Repositories;

namespace MiniCommerce.Catalog.Application.UseCases.Products.DeleteProduct;

public class DeleteProductCommandHandler(IProductRepository productRepository, IUnitOfWork unitOfWork) : ICommandHandler<DeleteProductCommand>
{
    public async Task<Result> HandleAsync(DeleteProductCommand command, CancellationToken cancellationToken = default)
    {
        var product = await productRepository.GetByIdAsync(command.Id, cancellationToken);

        if (product is null)
            return Result.Failure(ProductErrors.NotFound);

        await productRepository.DeleteAsync(product, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
