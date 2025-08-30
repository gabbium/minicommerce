namespace MiniCommerce.Catalog.Application.UseCases.Products.ListProducts;

public record ListProductsQuery(int PageNumber, int PageSize) : IQuery<PaginatedList<ProductResponse>>;

