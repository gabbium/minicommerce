using MiniCommerce.Catalog.Application.Models;
using MiniCommerce.Catalog.Domain.Abstractions;
using MiniCommerce.Catalog.Domain.Entities;

namespace MiniCommerce.Catalog.Application.Features.Products.CreateProduct;

public class CreateProductCommandHandler(IProductRepository productRepository) : ICommandHandler<CreateProductCommand, ProductResponse>
{
    public async Task<Result<ProductResponse>> HandleAsync(CreateProductCommand command, CancellationToken cancellationToken = default)
    {
        var product = new Product(command.Sku, command.Name, command.Price);

        await productRepository.AddAsync(product, cancellationToken);
        await productRepository.SaveChangesAsync(cancellationToken);

        return new ProductResponse(product.Id, product.Sku, product.Name, product.Price);
    }
}
