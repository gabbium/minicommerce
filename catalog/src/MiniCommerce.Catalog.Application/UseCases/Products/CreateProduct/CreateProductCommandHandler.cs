using MiniCommerce.Catalog.Application.Contracts;
using MiniCommerce.Catalog.Domain.Aggregates.Products.Entities;
using MiniCommerce.Catalog.Domain.Aggregates.Products.Repositories;

namespace MiniCommerce.Catalog.Application.UseCases.Products.CreateProduct;

public class CreateProductCommandHandler(IProductRepository productRepository, IUnitOfWork unitOfWork) : ICommandHandler<CreateProductCommand, ProductResponse>
{
    public async Task<Result<ProductResponse>> HandleAsync(CreateProductCommand command, CancellationToken cancellationToken = default)
    {
        var product = new Product(command.Sku, command.Name, command.Price);

        await productRepository.AddAsync(product, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return new ProductResponse(product.Id, product.Sku, product.Name, product.Price);
    }
}
