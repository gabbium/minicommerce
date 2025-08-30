using MiniCommerce.Catalog.Application.Contracts;
using MiniCommerce.Catalog.Domain.Aggregates.Products.Errors;
using MiniCommerce.Catalog.Domain.Aggregates.Products.Repositories;

namespace MiniCommerce.Catalog.Application.UseCases.Products.UpdateProduct;

public class UpdateProductCommandHandler(IProductRepository productRepository, IUnitOfWork unitOfWork) : ICommandHandler<UpdateProductCommand, ProductResponse>
{
    public async Task<Result<ProductResponse>> HandleAsync(UpdateProductCommand command, CancellationToken cancellationToken = default)
    {
        var product = await productRepository.GetByIdAsync(command.Id, cancellationToken);

        if (product is null)
            return Result.Failure<ProductResponse>(ProductErrors.NotFound);

        product.ChangeName(command.Name);
        product.ChangePrice(command.Price);

        await productRepository.UpdateAsync(product, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return new ProductResponse(product.Id, product.Sku, product.Name, product.Price);
    }
}
