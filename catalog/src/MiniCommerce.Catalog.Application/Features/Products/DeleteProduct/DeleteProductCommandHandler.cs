using MiniCommerce.Catalog.Application.Models;
using MiniCommerce.Catalog.Domain.Abstractions;

namespace MiniCommerce.Catalog.Application.Features.Products.DeleteProduct;

public class DeleteProductCommandHandler(IProductRepository productRepository) : ICommandHandler<DeleteProductCommand>
{
    public async Task<Result> HandleAsync(DeleteProductCommand command, CancellationToken cancellationToken = default)
    {
        var product = await productRepository.GetByIdAsync(command.Id, cancellationToken);

        if (product is null)
            return Result.Failure<ProductResponse>(ProductErrors.NotFound);

        await productRepository.DeleteAsync(product, cancellationToken);
        await productRepository.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
