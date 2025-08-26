using MiniCommerce.Catalog.Application.Contracts.Products;
using MiniCommerce.Catalog.Domain.Aggregates.Products;

namespace MiniCommerce.Catalog.Application.Features.Products.UpdateProduct;

public class UpdateProductCommandHandler(IProductRepository productRepository) : ICommandHandler<UpdateProductCommand, ProductResponse>
{
    public async Task<Result<ProductResponse>> HandleAsync(UpdateProductCommand command, CancellationToken cancellationToken = default)
    {
        var product = await productRepository.GetByIdAsync(command.Id, cancellationToken);

        if (product is null)
            return Result.Failure<ProductResponse>(ProductErrors.NotFound);

        product.ChangeName(command.Name);
        product.ChangePrice(command.Price);

        await productRepository.UpdateAsync(product, cancellationToken);
        await productRepository.SaveChangesAsync(cancellationToken);

        return new ProductResponse(product.Id, product.Sku, product.Name, product.Price);
    }
}
