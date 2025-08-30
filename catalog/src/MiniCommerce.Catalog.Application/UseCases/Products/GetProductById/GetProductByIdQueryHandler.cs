using MiniCommerce.Catalog.Domain.ProductAggregate.Errors;
using MiniCommerce.Catalog.Domain.ProductAggregate.Repositories;

namespace MiniCommerce.Catalog.Application.UseCases.Products.GetProductById;

public class GetProductByIdQueryHandler(IProductRepository productRepository) : IQueryHandler<GetProductByIdQuery, ProductResponse>
{
    public async Task<Result<ProductResponse>> HandleAsync(GetProductByIdQuery query, CancellationToken cancellationToken = default)
    {
        var product = await productRepository.GetByIdAsync(query.Id, cancellationToken);

        if (product is null)
            return Result.Failure<ProductResponse>(ProductErrors.NotFound);

        return new ProductResponse(product.Id, product.Sku, product.Name, product.Price);
    }
}
